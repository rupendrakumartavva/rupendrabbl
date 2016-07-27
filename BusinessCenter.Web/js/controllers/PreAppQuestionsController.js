(function () {
    'use strict';
    var controllerId = 'PreAppQuestionsController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', '$window', '$timeout', 'requestService', 'UtilityFactory', 'appConstants', 'errorFactory', 'popupFactory', 'authService', PreAppQuestionsController]);

    function PreAppQuestionsController($scope, $rootScope, $location, $window, $timeout, requestService, UtilityFactory, appConstants, errorFactory, popupFactory, authService) {
        $('.panel-body >h1').each(function (ndx, ele) {
            if (ele.offsetWidth < ele.scrollWidth) {
                $(ele).css('font-size', Math.ceil($(ele).width() / 10) + 5);
            }
        });

        var vm = this;
        var oldcontrolid = null;
        vm.currentpage_errors = {};
        var check = '';
        var ValidateBussStructure = '';
        vm.activities = {
            "activitiesList": [

                {
                    "activity": "Automobiles, Parking and Towing",
                    "categories": [
                        "Ambulance",
                        "Auto Rental",
                        "Auto Wash",
                        "Automobile Repossessor",
                        "Automobile Repossessor - Business",
                        "Consumer Goods (Auto Repair)",
                        "Driving School",
                        "Motor Vehicle Dealer",
                        "Motor Vehicle Salesman",
                        "Parking Facility",
                        "Parking Facility Attendant",
                        "Tow Truck",
                        "Tow Truck Business",
                        "Tow Truck Storage Lot",
                        "Used Car Buyer Seller",
                        "Used Car Lot",
                        "Used Car Seller Only",
                        "Valet Parking"
                    ]
                },
                {
                    "activity": "Beauty Care/ Barber/ Health Spa",
                    "categories": [
                        "Barber Chair",
                        "Barber Shop",
                        "Beauty Booth",
                        "Beauty Shop",
                        "Beauty Shop Braiding",
                        "Beauty Shop Electrology",
                        "Beauty Shop Esthetics",
                        "Beauty Shop Nails",
                        "Health Spa",
                        "Health Spa Sales",
                        "Massage Establishment"

                    ]
                },
                {
                    "activity": "Charity",
                    "categories": [
                        "Charitable Exempt",
                        "Charitable Solicitation"

                    ]
                },
                  {
                      "activity": "Electronic Repair",
                      "categories": [
                          "Consumer Goods (Electronic Repair)"

                      ]
                  },
                {
                    "activity": "Employment Services",
                    "categories": [
                        "Employer Paid Personnel Service",
                        "Employment Agency",
                        "Employment Counseling"

                    ]
                },
                {
                    "activity": "Entertainment and Recreation",
                    "categories": [
                        "Billiard Parlor",
                        "Mechanical Amusement Machine",
                        "Public Hall",
                        "Swimming Pool",
                        "Theater (Live)"

                    ]
                },
                 {
                     "activity": "Fuel, Environmental & Hazardous Materials",
                     "categories": [
                         "Asbestos Business",
                         "Gasoline Dealer",
                         "Pesticide Applicator",
                         "Pesticide Operator",
                         "Solid Waste Collection",
                         "Solid Waste Vehicle"

                     ]
                 },
                   {
                       "activity": "General Business, Cigarettes, Mattress, Solicitor, Patent Medicine",
                       "categories": [
                           "Cigarette Retail",
                           "Cigarette Wholesale",
                           "General Business Licenses",
                           "Mattress Manufacturer",
                           "Mattress Sale",
                           "Mattress Storage",
                           "Patent Medicine",
                           "Solicitor"

                       ]
                   },
                    {
                        "activity": "Home Improvement and Security",
                        "categories": [
                            "Gen Contr/Construction Mngr",
                            "Home Improvement Contractor",
                            "Home Improvement Salesman",
                            "Security Alarm Agent",
                            "Security Alarm Dealer"

                        ]
                    },
                    {
                        "activity": "Laundry/Cleaning, Funeral, Moving, Storage",
                        "categories": [
                            "Dry Cleaners",
                            "Funeral Establishment",
                            "Moving and Storage",
                            "Power Laundry"

                        ]
                    },
                      {
                          "activity": "Pet Care & Retail",
                          "categories": [
                              "Pet Shop"

                          ]
                      },
                       {
                           "activity": "Real Estate & Rentals",
                           "categories": [
                               "Apartment",
                               "Bed and Breakfast",
                               "Boarding House",
                               "Cooperative Association",
                               "Hotel",
                               "Inn and Motel",
                               "One Family Rental",
                               "Rooming House"

                           ]
                       },
                {
                    "activity": "Restaurant, Grocery and Food Sales",
                    "categories": [
                        "Bakery",
                        "Caterers",
                        "Delicatessen",
                        "Food Products",
                        "Food Vending Machine",
                        "Grocery Store",
                        "Marine Food Product Wholesale",
                        "Marine Food Retail",
                        "Mobile Delicatessen",
                        "Restaurant"

                    ]
                },
               {
                   "activity": "Used Goods Dealing",
                   "categories": [
                       "Auction Sale",
                       "Auctioneer",
                       "Pawnbrokers",
                       "Secondhand Dealers (A)",
                       "Secondhand Dealers (B)",
                       "Secondhand Dealers (C)"
                   ]
               }
            ]
        };
        var apicalls = {
            'step1': 'BusinessActivities',
            'step2': 'PrimaryCategoryList',
            'step3': 'SecondaryCategoryList',
            'step4': 'getsub',
            'step5': 'ScreeningQuestions',
            'step6': 'TotalFees'
        };

        var categoryids = {
            'BusinessActivities': 'ActivityID',
            'PrimaryCategoryList': 'PrimaryID',
            'SecondaryCategoryList': 'Secondary',
            'getsub': 'SubSubCategory',
            'ScreeningQuestions': ''
        };

        var validateStep4 = {};
        vm.step4validation = false;
        vm.validateRange = false;
        vm.NothingSelected = false;
        vm.homebased = false;
        vm.hop = false;
        vm.contentSection = [
            { cId: 'c01', num: 10 },
            { cId: 'c02', num: 25 },
            { cId: 'c02', num: 50 }
        ];

        vm.filterByStatusKeyword = '';
        vm.itemPage = 10;
        vm.currentPage = 1;

        vm.currentPageid = getNextPage();
        var stepid = vm.currentPageid.slice(-1);
        vm.nextNavigation = '';

        init();

        /*
     * Function: init
     * init (initialize) method: first method to be executed on controller load.
     */

        function init() {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            authService.refreshToken().then(function () {
                if (checkDataAvailability()) {
                    vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                    $rootScope.preAppQuestions = getCurrentObject();
                    if (angular.isDefined($rootScope.nextStepData)) {
                        setQuestionsInView($rootScope.nextStepData);
                        delete $rootScope.nextStepData;
                    } else {
                        displayCatogeryList().then(setQuestionsInView, errorWhileProcessing);
                    }
                    vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                } else {
                    $location.path('/newbblwelcome');
                }
            }, function () {
                $location.path("/login");
            });
        }

        function displayCatogeryList(boolval) {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            var keys = Object.keys($rootScope.preAppQuestions);
            var obj = {};
            var pageid = '';
            if (vm.currentPageid == 'step6') {
                obj = $rootScope.preAppQuestions['step5'][0];
                obj['UserID'] = localStorage.userId;
                $.each(keys, function (index, value) {
                    if (value != 'step5')
                        obj[categoryids[apicalls[value]]] = $rootScope.preAppQuestions[value];
                });
            } else {
                $.each(keys, function (index, value) {
                    if (parseInt(value.slice(-1)) <= parseInt(vm.currentPageid.slice(-1))) {
                        obj[categoryids[apicalls[value]]] = $rootScope.preAppQuestions[value];
                    }
                });
            }
            if (boolval) {
                pageid = vm.currentPageid.substring(0, vm.currentPageid.length - 1) + "" + (++stepid);
            } else {
                pageid = vm.currentPageid;
            }
            return requestService.getCurrentStepQeustions(apicalls[pageid], obj);
        }

        function setQuestionsInView(response) {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            vm.categories = response.data;
            if (parseInt(vm.currentPageid.slice(-1)) < 5) {
                $timeout(function () {
                    setCheckedItems();
                }, 100);
            }
            if (vm.currentPageid == 'step5')
                setScreeningQuestions();
            $("#dvMainsection").css("display", "block");
            $("#dvLoadingSection").css("display", "none");
        }

        function errorWhileProcessing(data) {
            $("#dvMainsection").css("display", "block");
            $("#dvLoadingSection").css("display", "none");
            console.log("Error Occured ");
        }

        function checkDataAvailability() {
            if (vm.currentPageid == 'step1') {
                if (localStorage.getItem('preAppQuestionsData') == null) {
                    $rootScope.preAppQuestions = {};
                    $rootScope.preAppQuestions['step0'] = new Array();
                    localStorage.setItem('preAppQuestionsData', JSON.stringify($rootScope.preAppQuestions));
                }
            }

            if (localStorage.getItem('preAppQuestionsData') != null) {
                return true;
            }
            return false;
        }

        function getCurrentObject() {
            return JSON.parse(localStorage.getItem('preAppQuestionsData'));
        }

        function setCheckedItems() {
            var object = $rootScope.preAppQuestions[vm.currentPageid];
            if (angular.isDefined(object)) {
                var objarr = object.split(',');
                for (var i = 0; i < objarr.length; i++) {
                    $('#' + objarr[i]).addClass('checked');
                    oldcontrolid = "#c" + objarr[i];
                }
            }
            $("#dvMainsection").css("display", "block");
            $("#dvLoadingSection").css("display", "none");
        }

        function setScreeningQuestions() {
            if (angular.isDefined($rootScope.preAppQuestions[vm.currentPageid])) {
                vm.categories = $rootScope.preAppQuestions[vm.currentPageid];
                vm.disable_items_from = vm.categories[0].SubQuestion.length;
            }
            $("#dvMainsection").css("display", "block");
            $("#dvLoadingSection").css("display", "none");
        }

        function getNextPage() {
            return $location.path().split('/')[2];
        }

        vm.continueToApply = function () {
            vm.createChecklist = false;
            $('#preappque').modal('hide');
        }

        vm.continueToNavigate = function () {
            $('#preappque').modal('hide');
            localStorage.removeItem('preAppQuestionsData');
            if (vm.createChecklist) {
                requestService.getApplicationData(vm.categories).then(function (response) {
                    if (response.data.Result) {
                        localStorage.removeItem('preAppQuestionsData');
                        delete $rootScope.preAppQuestions;
                        UtilityFactory.addMasterId(UtilityFactory.getGuid(), response.data.Result);
                        UtilityFactory.updateMasterIdsList();
                        $location.path('/appchecklist/' + UtilityFactory.getGuidByMasterId(response.data.Result));
                    }
                }, errorWhileProcessing);
            } else {
                $location.path(vm.nextNavigation.split('#')[1]);
            }
        }

        vm.isNumber = function (val) {
            return /^[-]?\d+$/.test(val);
        };

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is used for filtering data depending on type we are selecting.

        //------------------------------------------------------------------

        vm.filterByStatus = function (type) {
            vm.filterByStatusKeyword = type;
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is used for expand or collapse the table

        //------------------------------------------------------------------

        vm.expandCollapse = function (event) {
            if ($(event.target).hasClass('see-details')) {
                $(event.target).addClass('hide-details').removeClass('see-details');
                $(event.target).parent().parent().css('border-bottom', '0px solid');
                $(event.target).parent().parent().parent().find('.details').show();
            }
            else {
                $(event.target).addClass('see-details').removeClass('hide-details');
                $(event.target).parent().parent().parent().find('.details').hide();
            }
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "mybbl" page.
        // Last Modified    :

        //------------------------------------------------------------------

        vm.exitWithOutSaving = function () {
            vm.nextNavigation = "#/mybbl";
            $('#preappque .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.createChecklistnavigation + "</h3>");
            $('.confirm').text('Yes');
            $('.cancel_button').text('No');
            $('#preappque').modal('show');
        }

        vm.navToMyBBL = function () {
            $('#preappque').modal('hide');
            localStorage.removeItem('preAppQuestionsData');
            getBblServiceList().then(function (response) {
                vm.businessList = response.data.Result;
                $location.path('/mybbl');
            }, errorWhileProcessing);
        }

        function getBblServiceList() {
            return requestService.BblServiceList({ UserID: localStorage.userId });
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "newbblwelcome" page.
        // Last Modified    :

        //------------------------------------------------------------------

        vm.navToApply = function () {
            $location.path("/newbblwelcome");
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "preappquestions/step1" page.
        // Last Modified    :

        //------------------------------------------------------------------

        vm.navToPreAppQues_Step1 = function () {
            $location.path("/preappquestions/step1");
        }

        function getCatogeryList() {
            return displayCatogeryList(true);
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "preappquestions/step2" page.
        // Last Modified    :

        //------------------------------------------------------------------

        vm.navToPreAppQues_Step2 = function () {
            checkNextNavigation();
        }

        function checkNextNavigation() {
            if (setPrerequestQuestions()) {
                getCatogeryList('checknavigation').then(setNextNavigation, errorWhileProcessing);
            } else {
                vm.NothingSelected = true;
                $timeout(function () {
                    $('.error_msg').focus();
                }, 1);

                vm.gberror = true;
            }
        }

        function setNextNavigation(response) {
            if (response.data.length > 0) {
                $rootScope.nextStepData = response;
                $location.path("/preappquestions/" + vm.currentPageid.substring(0, vm.currentPageid.length - 1) + "" + (stepid));
            } else {
                getCatogeryList('checknavigation').then(setNextNavigation, errorWhileProcessing);
            }
        }

        vm.navToPrevious_Step = function () {
            if (document.querySelectorAll('.checked > input').length == 0) {
                delete $rootScope.preAppQuestions[vm.currentPageid];
                localStorage.setItem('preAppQuestionsData', JSON.stringify($rootScope.preAppQuestions));
            }
            $window.history.back();
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "preappquestions/step3" page.
        // Last Modified    :

        //------------------------------------------------------------------

        vm.navToPreAppQues_Step3 = function () {
            checkNextNavigation()
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "preappquestions/step4" page.
        // Last Modified    :

        //------------------------------------------------------------------

        vm.navToPreAppQues_Step4 = function () {
            checkNextNavigation()
        }

        vm.navToPreAppQues_Step4WithNone = function () {
            if (document.querySelectorAll('.checked > input').length > 0) {
                vm.NothingSelected = false;
                vm.NoneSelected = true;
                $timeout(function () {
                    $(".error_msg").focus();
                }, 1);
            } else {
                if ($rootScope.preAppQuestions[vm.currentPageid] != undefined) {
                    for (var i = parseInt(vm.currentPageid.slice(-1)) ; i < 6; i++) {
                        if (i == 4) {
                            if (parseInt(localStorage.getItem("subcategoryFor")) == 2)
                                i = i + 1;
                        }
                        delete $rootScope.preAppQuestions['step' + i];
                    }
                }
                localStorage.setItem('preAppQuestionsData', JSON.stringify($rootScope.preAppQuestions));
                getCatogeryList('checknavigation').then(setNextNavigation, errorWhileProcessing);
            }
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "preappquestions/step5" page.
        // Last Modified    :

        //------------------------------------------------------------------

        vm.navToPreAppQues_Step5 = function () {
            if (setPrerequestQuestions()) {
                $location.path("/preappquestions/step5");
            } else {
                vm.NothingSelected = true;
                $timeout(function () {
                    $('.error_msg').focus();
                }, 1);
            }
        }

        vm.navToPreAppQues_Step6 = function () {
            vm.step4validation = false;
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            ValidateBussStructure = true;
            for (var i = 0; i < vm.categories[0].SubQuestion.length; i++) {
                if (vm.categories[0].SubQuestion[i].Type == 'RadioButton') {
                    if ((vm.categories[0].SubQuestion[i].Question.indexOf('Home based') != -1 || vm.categories[0].SubQuestion[i].Question.indexOf('Home Occupancy Permit') != -1)) {
                        if (vm.categories[0].SubQuestion[i].Answer == "" && vm.categories[0].SubQuestion[i - 1].Question.indexOf('(2) or four (4)') == -1) {
                            vm.categories[0].SubQuestion[i].Answer = "NO";
                        }
                    }
                }

                if (vm.categories[0].SubQuestion[i].Question.indexOf('Trade Name') == -1) {
                    if (vm.categories[0].SubQuestion[i].Answer == "") {
                        vm.step4validation = true;
                        $timeout(function () {
                            $('#errorText').focus();
                        }, 0);
                    }
                }
            }
            if (!vm.step4validation)
                storeFinalData();

            if (vm.step4validation == false || vm.validateRange == false) {
                $("#dvMainsection").css("display", "block");
                $("#dvLoadingSection").css("display", "none");
            }

            if (vm.validateRange) {
                $location.path("/preappquestions/step6");
            }
        }

        vm.revisePreAppQue = function () {
            $location.path("/preappquestions/step1");
        }

        vm.navToStep5 = function () {
            $location.path("/preappquestions/step5");
        }
        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "appchecklist" page.
        // Last Modified    :

        //------------------------------------------------------------------

        vm.navigateToAppChecklist = function () {
            $('#preappque .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.createCheckList + "</h3>");
            $('.confirm').text('Confirm');
            $('.cancel_button').text('Cancel');
            $('#preappque').modal('show');
            vm.createChecklist = true;
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is user for selecting the checkbox.
        // Last Modified    :

        //------------------------------------------------------------------

        vm.toggleCheckbox = function (ctlID) {
            vm.NothingSelected = false;
            vm.NoneSelected = false;
            var controlID = "#c" + ctlID;
            var controlElement = angular.element(document.querySelector(controlID)).parent();
            if (controlElement.hasClass('checked')) {
                controlElement.removeClass('checked');
            }
            else {
                if (!(vm.currentPageid === 'step3')) {
                    if (oldcontrolid != null) {
                        angular.element(document.querySelector(oldcontrolid)).parent().removeClass('checked');
                    }
                    oldcontrolid = controlID;
                }
                controlElement.addClass('checked');
            }
        }

        vm.toggleRadio = function (ctlID) {
            vm.NothingSelected = false;
            vm.NoneSelected = false;
            var controlID = "#c" + ctlID;
            var controlElement = angular.element(document.querySelector(controlID)).parent();
            if (oldcontrolid != null) {
                angular.element(document.querySelector(oldcontrolid)).parent().removeClass('checked');
            }
            oldcontrolid = controlID;
            controlElement.addClass('checked');
        }

        vm.setNextAnswer = function (ndx) {
            if (vm.categories[0].SubQuestion[ndx + 1] != undefined && vm.categories[0].SubQuestion[ndx + 1].Type == 'RadioButton' && vm.categories[0].SubQuestion[ndx + 1].Question.indexOf('Corporations Division') == -1
                && vm.categories[0].SubQuestion[ndx].Question.indexOf('(2)') == -1
                && vm.categories[0].SubQuestion[ndx + 1].Question.indexOf('FEIN') == -1
                )
                vm.categories[0].SubQuestion[ndx + 1].Answer = '';
            if (vm.categories[0].SubQuestion[ndx].Question.indexOf('Corporations Division') != -1) {
                vm.categories[0].SubQuestion[ndx + 1].Answer = 'Select One';
                vm.categories[0].SubQuestion[ndx + 2].Answer = '';
                vm.disable_items_from = vm.categories[0].SubQuestion.length;
                $("#error" + parseInt(ndx + 1)).html('');
                $("#er" + parseInt(ndx + 2)).html('');
            }
        }

        vm.checkSelection_Disable = function (selectedvalue, ndx) {
            vm.categories[0].SubQuestion[ndx].Answer = selectedvalue;
            if (selectedvalue != 'Select One')
                if (vm.categories[0].SubQuestion[ndx - 1].Answer == 'NO' && !(selectedvalue == 'Sole Proprietorship' || selectedvalue == 'General Partnership' || selectedvalue == 'Joint Venture')) {
                    vm.stopnavigation = true;
                    vm.disable_items_from = ndx;
                    $("#error" + ndx).html(
                        "Because your business is a " + selectedvalue.toUpperCase() + ", you must register with DCRA's Corporations Division " +
                        "prior to submitting your Business License Application. Please exit <i>BBL Online</i>  and visit the DCRA Corporations " +
                       "Division at 1100 4th St., SW or call at 202-442-4400.");
                    $("#er" + parseInt(ndx + 1)).html('');
                    vm.categories[0].SubQuestion[ndx + 1].Answer = '';
                } else {
                    vm.stopnavigation = false;
                    vm.disable_items_from = vm.categories[0].SubQuestion.length;
                    $("#error" + ndx).html('');
                }
        }

        vm.checkRange = function (ndx) {
            var start = vm.categories[0].SubQuestion[ndx].StartRange;
            var end = vm.categories[0].SubQuestion[ndx].EndRange;
            var ans = vm.categories[0].SubQuestion[ndx].Answer;
            if (start > 0 && end > 0) {
                if (vm.isNumber(ans)) {
                    if (parseInt(ans) >= parseInt(start) && parseInt(ans) <= end) {
                        $("#er" + ndx).html('');
                    }
                    else {
                        $("#er" + ndx).html('Quantity must be greater than or equal to ' + start + ' and less than or equal to ' + end);
                    }
                } else {
                    $("#er" + ndx).html('Quantity must be greater than or equal to ' + start + ' and less than or equal to ' + end);
                }
            } else {
                if (vm.categories[0].SubQuestion[ndx].Question.indexOf('Trade') != -1 && ans.length > 0 && vm.categories[0].SubQuestion[ndx - 2].Answer == 'NO') {
                    vm.stopnavigation = true;
                    vm.disable_items_from = ndx;
                    $("#er" + ndx).html("Because you indicated you have a Trade Name for your business, " +
                        "you must register with DCRA's Corporations Division prior to submitting your Business License application. " +
                    "Please exit <i>BBL Online</i>  and visit the DCRA Corporations Division at 1100 4th St SW or call 202-442-4400.");
                } else {
                    vm.stopnavigation = false;
                    $("#er" + ndx).html('');
                    vm.disable_items_from = vm.categories[0].SubQuestion.length;
                }
            }
        }

        function setPrerequestQuestions() {
            $("#dvMainsection").css("display", "block");
            $("#dvLoadingSection").css("display", "none");
            var list = [];
            var checkedElements = document.querySelectorAll('.checked > input');
            if (checkedElements.length == 0) {
                return false;
            }
            for (var i = 0; i < checkedElements.length; i++) {
                list.push(checkedElements[i].id.substring(1));

                if (JSON.parse($("#" + checkedElements[i].id).val()).IsSubCategory) {
                    if (localStorage.getItem("subcategoryFor") == null) {
                        localStorage.setItem('subcategoryFor', vm.currentPageid.slice(-1));
                    }
                    else {
                        localStorage.removeItem('subcategoryFor');
                    }
                } else {
                    if (parseInt(vm.currentPageid.slice(-1)) == 2)
                        localStorage.removeItem('subcategoryFor');
                }
            }
            if (!angular.equals(JSON.stringify($rootScope.preAppQuestions[vm.currentPageid]), JSON.stringify(list.toString()))) {
                if (vm.currentPageid == "step1") {
                    localStorage.removeItem("subcategoryFor");
                }
                for (i = parseInt(vm.currentPageid.slice(-1)) ; i < 6; i++) {
                    if (i == 4 && parseInt(vm.currentPageid.slice(-1)) != 2) {
                        if (parseInt(localStorage.getItem("subcategoryFor")) == 2)
                            i = i + 1;
                    }
                    delete $rootScope.preAppQuestions['step' + i];
                }
            }
            $rootScope.preAppQuestions[vm.currentPageid] = list.toString();
            localStorage.setItem('preAppQuestionsData', JSON.stringify($rootScope.preAppQuestions));
            //$("#dvMainsection").css("display", "block");
            //$("#dvLoadingSection").css("display", "none");
            return true;
        }
        //Validations For Screening Questions.
        function storeFinalData() {
            var itemlist = [];
            for (var i = 0; i < vm.categories[0].SubQuestion.length; i++) {
                if (vm.categories[0].SubQuestion[i].Type == "Textbox") {
                    if (vm.categories[0].SubQuestion[i].Question.indexOf('Trade Name') == -1) {
                        if (vm.categories[0].SubQuestion[i].StartRange > 0 && vm.categories[0].SubQuestion[i].EndRange > 0) {
                            if (vm.isNumber(vm.categories[0].SubQuestion[i].Answer)) {
                                if (vm.categories[0].SubQuestion[i].Answer >= vm.categories[0].SubQuestion[i].StartRange && vm.categories[0].SubQuestion[i].Answer <= vm.categories[0].SubQuestion[i].EndRange) {
                                    itemlist.push(vm.categories[0].SubQuestion[i].Answer);
                                } else {
                                    vm.validateRange = false;
                                    break;
                                }
                            }
                        } else if (vm.categories[0].SubQuestion[i].Question.indexOf('Business Owner')) {
                            itemlist.push(vm.categories[0].SubQuestion[i].Answer);
                        }
                        else {
                            vm.validateRange = false;
                            break;
                        }
                    }
                }
                if (ValidateBussStructure) {
                    if (vm.categories[0].SubQuestion[i].Question.indexOf('Business Structure') != -1) {
                        if (vm.categories[0].SubQuestion[i].Answer == 'Select One') {
                            vm.validateRange = false;
                            vm.step4validation = true;
                            $timeout(function () {
                                $('#errorText').focus();
                            }, 100);
                            break;
                        }
                    }
                }
                //if (vm.categories[0].SubQuestion[i].Question.indexOf('Trade') != -1) {
                //    if (vm.categories[0].SubQuestion[i].Answer == "") {
                //       // vm.categories[0].SubQuestion[i].Answer = "NA";
                //    }
                //}
            }
            if (i == vm.categories[0].SubQuestion.length) {
                vm.validateRange = true;
                vm.categories[0].ItemQty = itemlist.toString();
                $rootScope.preAppQuestions[vm.currentPageid] = vm.categories;
                localStorage.setItem('preAppQuestionsData', JSON.stringify($rootScope.preAppQuestions));
            }
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (parseInt(next.indexOf("preappquestions")) == -1 && parseInt(next.indexOf("appchecklist")) == -1) {
                vm.nextNavigation = next;
                if (localStorage.getItem('preAppQuestionsData') != null) {
                    if (Object.keys(JSON.parse(localStorage.getItem('preAppQuestionsData'))).length > 1) {
                        event.preventDefault();
                        $('#preappque .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.createChecklistnavigation + "</h3>");
                        $('.confirm').text('Yes');
                        $('.cancel_button').text('No');
                        $('#preappque').modal('show');
                        return;
                    }
                }
            }
        });
    }
}());