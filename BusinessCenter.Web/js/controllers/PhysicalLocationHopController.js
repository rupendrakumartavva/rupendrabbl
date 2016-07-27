(function () {

    var controllerid = 'PhysicalLocationHopController';
    angular.module('DCRA').controller(controllerid, ['$rootScope', '$scope', '$location', 'requestService', 'appConstants', '$timeout', '$routeParams', 'UtilityFactory', '$filter', 'MAR_validation_service', 'errorFactory', 'SessionFactory', 'popupFactory', 'BBLSubmissionFactory', 'authService','$window', PhysicalLocationHopController]);

    function PhysicalLocationHopController($rootScope, $scope, $location, requestService, appConstants, $timeout, $routeParams, UtilityFactory, $filter, MAR_validation_service, errorFactory, SessionFactory, popupFactory, BBLSubmissionFactory, authService, $window) {

        var vm = this;
        vm.searchmatch = false;
        vm.dataavail = false;
        vm.searchZoningClicked = true;
        vm.hop = {};
        vm.hop.date = '01/01/1900';
        vm.searchdefined = '';
        vm.hopinfovalidation = {};
        vm.previousSearchDetails = {};
        vm.submissionStatusData = {};
        //vm.navigate = false;
        vm.fieldsDisable = false;
        vm.not_valid_Address = false;
        vm.frominit = false;
        vm.incorrectCount = 0;
        vm.notMapped_Database = false;
        vm.currentpage_errors = {};
        vm.Address = [];
        SessionFactory.setSessionAsDirty();
        var MAR_service = MAR_validation_service;

        vm.containsfilterkey = false;
        function StreetsDropDown() {
            return requestService.streetsDropDown();
        }
        init();

        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                if (UtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid)).then(function (response) {
                        vm.submissionStatusData = response.data;
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            $location.path('/mybbl');
                        } else {
                            vm.fieldsDisable = MAR_validation_service.initialDisablingFields();
                            StreetsDropDown().then(function (streetresponse) {
                                vm.StreetTypes = streetresponse.data.StreetList;
                                vm.Countries = streetresponse.data.CountryList;
                                getPreviousData().then(function (response) {
                                    vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                                    if (angular.isDefined(response.data.WebserviceList[0])) {
                                        vm.hop.number = response.data.WebserviceList[0].Number;
                                        vm.hop.date = response.data.WebserviceList[0].DateofIssue;
                                        vm.hop.date = '01/01/1900';
                                        searchZoningFetch(vm.frominit = true).then(function () {
                                            vm.searchZoningClicked = true;
                                            vm.hopinfo = {};
                                            if (!(angular.isDefined(vm.correctSearch) && vm.correctSearch == 'as')) {
                                                if (response.data.WebserviceList[0].IsValid) {
                                                    vm.IsValid = response.data.WebserviceList[0].IsValid;
                                                    vm.correctSearch = false;
                                                    vm.uploadType = true;
                                                    vm.hopinfo.StreetTypeId = '';
                                                    vm.hopinfo = response.data.WebserviceList[0];
                                                    vm.hopinfovalidation = angular.copy(vm.hopinfo);
                                                    vm.hopinfo.IsDataChange = false;
                                                } else {
                                                    vm.uploadType = false;
                                                    vm.correctSearch = true;
                                                }
                                            } else {
                                                vm.uploadType = true;
                                                vm.hopinfo.StreetTypeId = '';
                                                vm.hopinfo = response.data.WebserviceList[0];
                                                vm.hopinfovalidation = angular.copy(vm.hopinfo);
                                                vm.getSuggestions(vm.hopinfo.Street.substring(0, 4));
                                            }
                                            vm.searchdefined = angular.copy(vm.correctSearch);
                                        });
                                    }
                                    vm.searchdefined = angular.copy(vm.correctSearch);
                                    $('#dvLoadingSection').css('display', 'none');
                                    $("#dvMainsection").css("display", "block");
                                }, ErrorWhileProcessing);
                            }, ErrorWhileProcessing);
                        }
                    });
                } else {
                    SessionFactory.setSessionAsClear()
                    BBLSubmissionFactory.noGuidAvailable();
                }
            }, function () {
                $location.path("/login");
            });
        }



        function getPreviousData() {
            return requestService.GetSubmissionCofoHop(UtilityFactory.getMasterId($routeParams.guid));
        }

        function ErrorWhileProcessing(response) {
            console.log("Error :" + JSON.stringify(response.status));
        };


        $scope.beforeRender = function ($view, $dates, $leftDate, $upDate, $rightDate) {
            var tempDate = new Date();
            var localOffset = tempDate.getTimezoneOffset() * 60000;
            var utcDateValue = tempDate.getTime();
            for (var i = 0; i < $dates.length; i++) {
                if ($dates[i].utcDateValue >= utcDateValue) {
                    $dates[i].selectable = false;
                }
            }
        }


        vm.checkSearchZoning = function (id) {
            vm.notMapped_Database = false;
            vm.disableCorrect = false;
            $('#' + id).html('');
            $('#error_msg').html('');
            vm.searchZoningClicked = false;
            delete vm.correctSearch;
            delete vm.hop.searchDetails;
            delete vm.hopinfo;
            delete vm.uploadType;
            delete vm.disableCorrect;
            vm.searchmatch = false;
            if (vm.hop.number == undefined && vm.searchdefined == vm.correctSearch) {
                delete vm.hop.number;
            }
        }

        $scope.$watch('vm.hop.date', function () {
            vm.checkSearchZoning();
        });

        vm.hopErrorMsg = function (id) {
            $('#' + id).html('');
            $('#error_msg').html('');
        }


        vm.searchZoning = function () {
            vm.checkSearchZoning();
            $('#error_msg').html('');
            vm.searchZoningClicked = true;
            vm.contact_us.$invalid ? $('#error_msg').html(vm.currentpage_errors.hopallfieldsNotFilled).focus() : searchZoningFetch();
        }

        function searchZoningFetch() {
            $('#dvSubMainsection').css('display', 'none');
            $("#dvSubLoadingSection").css("display", "block");
            return searchZoneandFetch().then(DisplayFetchedData, ErrorWhileProcessing);
        }

        function searchZoneandFetch() {
            var type = $location.path().split('/')[2];
            return requestService.searchZone({ Number: vm.hop.number, DateofIssue: vm.hop.date, Type: type });
        }

        function DisplayFetchedData(response) {
            vm.submitted = false;
            vm.hop.searchDetails = response.data[0];

            if (vm.frominit) {
                vm.frominit = false;
            } else {
                if (angular.equals(vm.previousSearchDetails, vm.hop)) {
                    vm.hop.searchDetails.IsDataChange = false;
                } else {
                    vm.hop.searchDetails.IsDataChange = true;
                }
            }

            vm.incorrectCount = 0;
            if (vm.hop.searchDetails.Status == 'NODATA') {
                vm.searchmatch = false;
                vm.correctSearch = 'as';
            } else {
                vm.previousSearchDetails = angular.copy(vm.hop);
                vm.searchmatch = true;
                if (!vm.hop.searchDetails.IsCofoHop) {
                    vm.notMapped_Properties = "";
                    if (!vm.hop.searchDetails.IsStreetNumber) {
                        vm.notMapped_Properties += " Street # ";
                    } else if (!vm.hop.searchDetails.IsStreetName) {
                        vm.notMapped_Properties += " Street # + StreetName "
                    } else if (!vm.hop.searchDetails.IsStreetType) {
                        vm.notMapped_Properties += "Street # + StreetName + StreetType "
                    } else {
                        vm.notMapped_Properties = "Street # + StreetName + StreetType + Quadrant";
                    }
                    vm.notMapped_Database = true;
                    vm.correctSearch = false;
                } else {
                    vm.notMapped_Database = false;
                }
            }

            $('#dvSubMainsection').css('display', 'block');
            $("#dvSubLoadingSection").css("display", "none");
        };

        vm.incorrectSearch = function () {
            if (vm.incorrectCount == 0) {
                vm.correctSearch = false;
                vm.disableCorrect = true;

                $timeout(function () {
                    if (!vm.frominit)
                        $('.highlight').focus();
                }, 1);
                vm.incorrectCount++;
            }
        }

        vm.toggleRadio = function (result) {
            delete vm.uploadType;
            $("#errormsg").html('');
            if (result == 'true') {
                vm.correctSearch = true;
                vm.uploadType = false;
            } else {
                vm.correctSearch = false;
            }
        }

        vm.toggleCheckbox = function (ctlID) {
            var controlElement = angular.element(document.querySelector('#' + ctlID)).parent();
            if (controlElement.hasClass('checked')) {
                controlElement.removeClass('checked');
                delete vm.hopinfo;
                vm[ctlID] = false;
            }
            else {
                controlElement.addClass('checked');
                vm[ctlID] = true;
                vm.submitted = false;
                vm.hopinfo = MAR_service.invalid_DC_address();
                $('#streetaddress').hide();
            }
        }

        function storeSearchZoneToDb(data) {
            $("#errormsg").html('');
            return requestService.FetchedDataToDb(data);
        }

        vm.norelatedMatch = function () {
            console.log("no match");
        }

        $scope.noResults = false;

        vm.getrelatedAddress = function (address) {
            vm.hopinfo = angular.copy(MAR_validation_service.getTotalAddress(address));
            if (vm.hopinfovalidation.Street != vm.hopinfo.FullAddress) {
                vm.hopinfo.IsDataChange = true;
            } else {
                vm.hopinfo.IsDataChange = false;
            }
        }

        vm.getSuggestions = function (street, spinnerid) {
            vm.not_valid_Address = false;
            angular.isDefined(street) ? street : street = '';
            switch (MAR_validation_service.manageStreet(street.length)) {
                case "fillmarAddress":
                    vm.dataavail = false;
                    $('#' + spinnerid).show().next().prop('readonly', true);
                    MAR_validation_service.getTypeAheadData(street).then(function (response) {
                        $('#' + spinnerid).hide().next().prop('readonly', false);
                        if (response.length > 0) {
                            vm.Address = response;
                            vm.dataavail = true;
                        } else {
                            vm.hopinfo = MAR_validation_service.invalid_DC_address(street);
                            vm.not_valid_Address = true;
                        }
                    }, ErrorWhileProcessing);
                    break;
                case "smallerstring":
                    vm.Address = [];
                    vm.hopinfo = MAR_validation_service.invalid_DC_address(street);
                    vm.not_valid_Address = false;
                    break;
                case "morethenrequired":
                    if ($filter('filter')(vm.Address, { FullAddress: angular.copy(vm.hopinfo.Street) }, vm.startsWith).length == 0) {
                        vm.not_valid_Address = true;
                        vm.hopinfo = MAR_validation_service.invalid_DC_address(street);
                    }
                    break;
            }
        }

        vm.startsWith = function (fulladdress, viewValue) {
            return fulladdress.substr(0, viewValue.length).toLowerCase() == viewValue.toLowerCase();
        }

        function checkExisting(obj) {
            if (angular.equals(JSON.stringify(obj), JSON.stringify(vm.hopinfovalidation)))
                return true;
            return false;
        }

        function SubmitHopDetails() {
            SessionFactory.setSessionAsClear();
            vm.navigate = true;
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            var data = {};

            if (angular.isDefined(vm.hopinfo)) {
                if (vm.uploadType == false) {
                    data = vm.hop.searchDetails;
                    data.IsValid = false;
                } else {
                    data = vm.hopinfo;
                    //  data.StreetTypeId = vm.hop.searchDetails.StreetTypeId;
                    data.StreetType = data.StreetType;
                    data.IsUploadSupportDoc = true;
                    data.IsValid = true;
                    data.OccupancyAddssValidate = "InCorrect";
                }

            } else {
                data = vm.hop.searchDetails;
                data.IsValid = false;
                data.OccupancyAddssValidate = "Correct";
            }

            if (vm.searchdefined != vm.correctSearch) {
                data.IsDataChange = true;
            }

            data.MasterId = UtilityFactory.getMasterId($routeParams.guid);
            data.Number = vm.hop.number;
            data.DateofIssue = vm.hop.date;

            data.Type = $location.path().split('/')[2];
            storeSearchZoneToDb(data).then(function (response) {
                if (response.data) {
                    if (vm.navigationPath != '') {
                        $location.path('/' + vm.navigationPath);
                    }
                    else {
                        vm.navToRegisteredAgent();
                        $('#dvLoadingSection').css('display', 'none');
                        $("#dvMainsection").css("display", "block");
                    }
                }
            }, ErrorWhileProcessing);

        }

        vm.navToNextStepfromhop = function (path) {
            vm.navigationPath = path;

            if (vm.contact_us.$invalid || !vm.searchZoningClicked || angular.isUndefined(vm.correctSearch) || (vm.correctSearch != true && vm.corpnorregadd_form.$invalid)) {

                $('#error_msg').html(vm.currentpage_errors.NextButtonIncompleteData).focus();


            } else {
                SubmitHopDetails();
            }
        }

        vm.checkAndExit = function (path) {
            vm.submitted = true;
            $('#error_msg').html('');
            if (path.indexOf('app') != -1) {
                vm.navigationPath = path + '/' + $routeParams.guid;
            } else {
                vm.navigationPath = path
            }
            if (modelDefined(vm.hop)) {
                if (vm.not_valid_Address) {
                    $location.path('/' + vm.navigationPath);
                }
                else if (!vm.searchZoningClicked || angular.isUndefined(vm.correctSearch) || (vm.correctSearch != true && vm.corpnorregadd_form.$invalid)) {
                    if (!vm.searchZoningClicked && Object.keys(vm.hop).length >= 2) {
                        popupFactory.showpopup(vm.currentpage_errors.searchNotClicked, vm.navigationPath);
                        //$('#hopPopUp .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.searchNotClicked + "</h3>");
                    } else {
                        popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigationPath);
                        //$('#hopPopUp .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.incompleteData + "</h3>");
                    }
                    //$('#hopPopUp').modal('show');
                } else {
                    SubmitHopDetails();
                }
            } else {
                SessionFactory.setSessionAsClear();
                //vm.navigate = true;
                vm.hop.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                vm.hop.number = '';
                vm.hop.DateofIssue = '';
                vm.hop.IsDataChange = true;
                vm.hop.Type = $location.path().split('/')[2];
                storeSearchZoneToDb(vm.hop).then(function (response) {
                    if (response.data) {
                        $location.path('/' + vm.navigationPath);
                    }
                }, ErrorWhileProcessing);
            }
        }

        function modelDefined(obj) {
            var result = DefineObject(obj, 'partial');
            return result;
        }

        function modelTotallyDefined(obj) {
            var result = DefineObject(obj, 'total');
            return result;
        }

        function DefineObject(obj, validationtype) {
            if (angular.isDefined(obj)) {
                var flag = 1;
                var keyarr = Object.keys(obj);

                for (var i = 0; i < keyarr.length; i++) {
                    if (angular.isUndefined(obj[keyarr[i]]) || obj[keyarr[i]] == '')
                        flag++;
                }
                if (validationtype == 'partial') {
                    if (flag == keyarr.length)
                        return false;
                } else {
                    if (flag > 0)
                        return false;
                }

                return true;
            }
            return false;
        }

        vm.navToDetermineeHOPEligibility = function () {
            if (vm.hop.number == undefined && vm.hop.DateofIssue == undefined) {
                SessionFactory.setSessionAsClear();
                vm.navigate = true;
            }
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            $location.path('/ehopeligibility/' + $routeParams.guid);
        }

        vm.navToBack = function () {
            $window.history.back();
        }

        vm.navToRegisteredAgent = function () {

            if (vm.submissionStatusData.IsCorporationDivision == true) {
                $location.path('/physicallocation/corpreg/' + $routeParams.guid);
            }
            else {
                if (vm.submissionStatusData.TradeName != '' && vm.submissionStatusData.TradeName != 'NA') {
                    if (vm.submissionStatusData.BusinessStructure.toUpperCase() != 'SOLE PROPRIETORSHIP') {
                        $location.path('/corpreqregwithtradesecond/' + $routeParams.guid);
                    }
                } else {
                    if ((vm.submissionStatusData.BusinessStructure.toUpperCase() == 'SOLE PROPRIETORSHIP' || vm.submissionStatusData.BusinessStructure.toUpperCase() == 'GENERAL PARTNERSHIP' || vm.submissionStatusData.BusinessStructure.toUpperCase() == 'JOINT VENTURE') && (vm.submissionStatusData.TradeName == '' || vm.submissionStatusData.TradeName == 'NA')) {
                        $location.path('/corpnotregisteredaddress/' + $routeParams.guid);
                    }
                    else if ((vm.submissionStatusData.BusinessStructure.toUpperCase() != 'SOLE PROPRIETORSHIP' || vm.submissionStatusData.BusinessStructure.toUpperCase() != 'GENERAL PARTNERSHIP' || vm.submissionStatusData.BusinessStructure.toUpperCase() != 'JOINT VENTURE') && (vm.submissionStatusData.TradeName == '' || vm.submissionStatusData.TradeName == 'NA')) {
                        $location.path('/corpreqregisterfirst/' + $routeParams.guid);
                    }
                }
            }
        }

        function isSessionIncomplete() {
            if (SessionFactory.isSessionDirty()) {
                if (Object.keys(vm.hop).length >= 2 && (!vm.searchZoningClicked || angular.isUndefined(vm.correctSearch) || !angular.equals(vm.correctSearch, vm.searchdefined) || (vm.correctSearch != true && (vm.corpnorregadd_form.$invalid || !checkExisting(vm.hopinfo))))) {
                    SessionFactory.setSessionAsDirty();
                } else {
                    SessionFactory.setSessionAsClear();
                }
            }
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                $('#dvLoadingSection').css('display', 'none');
                $("#dvMainsection").css("display", "block");
                vm.navigationPath = next.split('#')[1].slice(1);
                isSessionIncomplete();
                if (SessionFactory.isSessionDirty()) {
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.navigateaway, vm.navigationPath);
                }
                //if (!vm.navigate) {
                //    if ((!vm.searchZoningClicked || angular.isUndefined(vm.correctSearch) || !angular.equals(vm.correctSearch, vm.searchdefined) || (vm.correctSearch != true && (vm.corpnorregadd_form.$invalid || !checkExisting(vm.hopinfo))))) {
                //        event.preventDefault();
                //        $('#hopPopUp .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.navigateaway + "</h3>");
                //        $('#hopPopUp').modal('show');
                //        return;
                //    }
                //}
            }
        });
    }
}());