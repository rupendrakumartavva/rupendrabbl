(function () {
    var controllerid = 'PhysicalLocationCofoController';
    angular.module('DCRA').controller(controllerid, ['$rootScope', '$scope', '$location', 'requestService', 'appConstants', '$timeout', '$routeParams', 'UtilityFactory', '$filter', 'MAR_validation_service', 'errorFactory', 'SessionFactory', 'popupFactory', 'BBLSubmissionFactory', 'authService', '$window', PhysicalLocationCofoController]);

    function PhysicalLocationCofoController($rootScope, $scope, $location, requestService, appConstants, $timeout, $routeParams, UtilityFactory, $filter, MAR_validation_service, errorFactory, SessionFactory, popupFactory, BBLSubmissionFactory, authService, $window) {
        var vm = this;
        vm.searchmatch = '';
        vm.dataavail = false;
        vm.navigationPath = '';
        vm.nocofoSelected = false;
        vm.cofo = {};
        vm.cofo.date = '01/01/1900';
        var data = {};
        vm.searchdefined = '';
        vm.cofoInfovalidation = {};
        vm.previousSearchDetails = {};
        vm.submissionStatusData = {};
        vm.searchZoningClicked = true;
        vm.frominit = false;
        vm.fieldsDisable = false;
        vm.incorrectCount = 0;
        vm.notMapped_Database = false;
        vm.Address = [];
        SessionFactory.setSessionAsDirty();

        init();

        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
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
                                vm.cofoinfo = {};
                                if (angular.isDefined(response.data.WebserviceList[0])) {
                                    if (!response.data.WebserviceList[0].DonothaveCof) {
                                        if (response.data.WebserviceList[0].Number != "") {
                                            vm.cofo.number = response.data.WebserviceList[0].Number;
                                            vm.cofo.date = '01/01/1900';
                                            searchZoningFetch(vm.frominit = true).then(function () {
                                                vm.searchZoningClicked = true;
                                                if (!(angular.isDefined(vm.correctSearch) && vm.correctSearch == 'as')) {
                                                    if (response.data.WebserviceList[0].IsValid) {
                                                        vm.correctSearch = false;
                                                        vm.uploadType = true;
                                                        vm.cofoinfo.StreetTypeId = '';
                                                        vm.cofoinfo = response.data.WebserviceList[0];
                                                        vm.getSuggestions(vm.cofoinfo.Street.substring(0, 4));
                                                        vm.cofoinfo.IsDataChange = false;
                                                        vm.cofoInfovalidation = angular.copy(vm.cofoinfo);
                                                    } else {
                                                        vm.correctSearch = true;
                                                        vm.uploadType = false;
                                                    }
                                                } else {
                                                    vm.uploadType = true;
                                                    vm.cofoinfo.StreetTypeId = '';
                                                    vm.cofoinfo = response.data.WebserviceList[0];
                                                    vm.cofoInfovalidation = angular.copy(vm.cofoinfo);
                                                    vm.getSuggestions(vm.cofoinfo.Street.substring(0, 4));
                                                }
                                                vm.searchdefined = angular.copy(vm.correctSearch);
                                            });
                                        }
                                    }
                                    else {
                                        vm.noCofoMsg = vm.noCofo = vm.nocofoSelected = response.data.WebserviceList[0].DonothaveCof;
                                    }
                                }
                                vm.searchdefined = angular.copy(vm.correctSearch);
                            }, ErrorWhileProcessing);
                        }, ErrorWhileProcessing);
                        $('#dvLoadingSection').css('display', 'none');
                        $("#dvMainsection").css("display", "block");
                    }
                });
            }, function () {
                $location.path("/login");
            });
        }

        function ErrorWhileProcessing(response) {
            console.log("Error :" + JSON.stringify(response.status));
        };

        function StreetsDropDown() {
            return requestService.streetsDropDown();
        }

        function getPreviousData() {
            return requestService.GetSubmissionCofoHop(UtilityFactory.getMasterId($routeParams.guid));
        }

        function DisplayFetchedData(response) {
            vm.searchZoningClicked = true;
            vm.submitted = false;
            vm.cofo.searchDetails = response.data[0];
            if (vm.frominit) {
                vm.frominit = false;
            } else {
                if (angular.equals(vm.previousSearchDetails, vm.cofo)) {
                    vm.cofo.searchDetails.IsDataChange = false;
                } else {
                    vm.cofo.searchDetails.IsDataChange = true;
                }
            }

            vm.incorrectCount = 0;
            vm.previousSearchDetails = angular.copy(vm.cofo);
            if (vm.cofo.searchDetails.Status == 'NODATA') {
                $timeout(function () {
                    $('.panel-body-text').focus();
                });
                vm.searchmatch = false;
                vm.correctSearch = 'as';
            } else {
                vm.searchmatch = true;
                vm.notMapped_Properties = '';
                if (!vm.cofo.searchDetails.IsCofoHop) {
                    vm.notMapped_Properties = "";
                    if (!vm.cofo.searchDetails.IsStreetNumber) {
                        vm.notMapped_Properties += " Street # ";
                    } else if (!vm.cofo.searchDetails.IsStreetName) {
                        vm.notMapped_Properties += " Street # + StreetName ";
                    } else if (!vm.cofo.searchDetails.IsStreetType) {
                        vm.notMapped_Properties += "Street # + StreetName + StreetType ";
                    } else {
                        vm.notMapped_Properties = "Street # + StreetName + StreetType + Quadrant";
                    }
                    vm.notMapped_Database = true;
                    vm.correctSearch = false;
                    vm.disableCorrect = true;
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

        function searchZoneandFetch() {
            $("#dvSubLoadingSection").css("display", "block");
            $('#dvSubMainsection').css('display', 'none');
            var type = $location.path().split('/')[2];
            vm.cofo.date = '01/01/1900';
            return requestService.searchZone({ Number: vm.cofo.number, DateofIssue: vm.cofo.date, Type: type, MasterId: UtilityFactory.getMasterId($routeParams.guid) });
        }

        function searchZoningFetch(boolval) {
            return searchZoneandFetch().then(DisplayFetchedData, ErrorWhileProcessing);
        }

        vm.checkSearchZoning = function (id) {
            vm.notMapped_Database = false;
            vm.disableCorrect = false;
            $('#' + id).html('');
            $('#error_msg').html('');
            vm.frominit = false;
            vm.searchZoningClicked = false;
            delete vm.correctSearch;
            delete vm.cofo.searchDetails;
            delete vm.cofoinfo;
            delete vm.uploadType;
            delete vm.noCofoMsg;
            delete vm.disableCorrect;
            vm.searchmatch = '';
            if (vm.cofo.number == undefined && vm.searchdefined == vm.correctSearch) {
                delete vm.cofo.number;
            }
        }

        vm.cofoErrorMsg = function (id) {
            $('#' + id).html('');
            $('#error_msg').html('');
        }

        vm.searchZoning = function () {
            vm.checkSearchZoning();
            //  $('#' + id).html('');
            $('#error_msg').html('');
            vm.searchZoningClicked = true;
            vm.contact_us.$invalid ? $('#error_msg').html(vm.currentpage_errors.allfieldsNotFilled).focus() : searchZoningFetch();
        }

        vm.toggleRadio = function (result) {
            delete vm.uploadType;
            if (result == 'true') {
                vm.correctSearch = true;
                vm.uploadType = false;
            } else {
                vm.correctSearch = false;
            }
            vm.cofoErrorMsg('error_msg');
        }

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

        vm.toggleCheckbox = function (ctlId) {
            $("#error_msg").html('');
            var controlElement = angular.element(document.querySelector('#' + ctlId)).parent();
            if (controlElement.hasClass('checked')) {
                controlElement.removeClass('checked');
                vm[ctlId] = false;
                vm.nocofoSelected = false;
                vm.noCofoMsg = false;
                delete vm.cofoinfo;
                if (ctlId != "uploadType")
                    SaveWhenNoCofo();
            } else {
                controlElement.addClass('checked');
                vm[ctlId] = true;
                vm.submitted = false;
                if (ctlId == "noCofo") {
                    vm.cofoErrorMsg('error_msg');
                    noCofoChecked();
                } else {
                    vm.cofoinfo = MAR_validation_service.invalid_DC_address();
                }
            }
        }

        function noCofoChecked() {
            $("#errormsg").html('');
            vm.nocofoSelected = true;
            if ((Object.keys(vm.cofo).length > 1) && modelDefined(vm.cofo)) {
                popupFactory.showpopup(vm.currentpage_errors.donothaveCofo, "");
                var modalInstance = popupFactory.getPopupInstance();
                modalInstance.result.then(function () {
                    manage_nocofo();
                }, function () {
                    if (vm.noCofo) {
                        vm.noCofo = vm.nocofoSelected = false;
                    }
                });
            } else {
                manage_nocofo();
            }
        };

        function storeSearchZoneToDb(data) {
            $("#errormsg").html('');
            return requestService.FetchedDataToDb(data);
        }

        vm.getrelatedAddress = function (address) {
            vm.cofoinfo = angular.copy(MAR_validation_service.getTotalAddress(address));
            if (vm.cofoInfovalidation.Street != vm.cofoinfo.FullAddress) {
                vm.cofoinfo.IsDataChange = true;
            } else {
                vm.cofoinfo.IsDataChange = false;
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
                            vm.cofoinfo = MAR_validation_service.invalid_DC_address(street);
                            vm.not_valid_Address = true;
                        }
                    }, ErrorWhileProcessing);
                    break;
                case "smallerstring":
                    vm.Address = [];
                    vm.cofoinfo = MAR_validation_service.invalid_DC_address(street);
                    vm.not_valid_Address = false;
                    break;
                case "morethenrequired":
                    if ($filter('filter')(vm.Address, { FullAddress: angular.copy(vm.cofoinfo.Street) }, vm.startsWith).length == 0) {
                        vm.not_valid_Address = true;
                        vm.cofoinfo = MAR_validation_service.invalid_DC_address(street);
                    }
                    break;
            }
        }

        vm.startsWith = function (fulladdress, viewValue) {
            return fulladdress.substr(0, viewValue.length).toLowerCase() == viewValue.toLowerCase();
        }

        vm.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#cofo_msg').html('');
            $("#error_msg").html('');
        }

        vm.checkAndExit = function (path) {
            vm.submitted = true;
            $('#error_msg').html('');

            if (path.indexOf('app') != -1) {
                vm.navigationPath = path + '/' + $routeParams.guid;
            } else {
                vm.navigationPath = path
            }
            if (modelDefined(vm.cofo) && !vm.nocofoSelected) {
                if (vm.not_valid_Address) {
                    $location.path('/' + vm.navigationPath);
                }
                else if (!vm.searchZoningClicked || angular.isUndefined(vm.correctSearch) || (vm.correctSearch != true && vm.corpnorregadd_form.$invalid)) {
                    if (!vm.searchZoningClicked && Object.keys(vm.cofo).length >= 2) {
                        popupFactory.showpopup(vm.currentpage_errors.searchNotClicked, vm.navigationPath);
                    } else {
                        popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigationPath);
                    }
                }
                else {
                    submitCofo();
                }
            } else {
                SessionFactory.setSessionAsClear();
                if (!vm.nocofoSelected) {
                    vm.cofo.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                    vm.cofo.number = '';
                    vm.cofo.DateofIssue = '';
                    vm.cofo.Type = $location.path().split('/')[2];
                    vm.cofo.IsDataChange = true;
                    storeSearchZoneToDb(vm.cofo).then(function (response) {
                        if (response.data == "true") {
                            $location.path('/' + vm.navigationPath);
                        }
                    }, ErrorWhileProcessing)
                } else {
                    $location.path('/' + vm.navigationPath);
                }
            }
        }

        function SaveWhenNoCofo() {
            requestService.SaveWhenNoCofo({ MasterId: UtilityFactory.getMasterId($routeParams.guid), Type: 'COFO', DonothaveCof: vm.nocofoSelected, Number: '' }).then(function (response) {
                if (response.data) {
                    if (vm.nocofoSelected) {
                        delete vm.cofo.number;
                        delete vm.cofo.date;
                        delete vm.cofo.searchDetails;
                        delete vm.correctSearch;
                        delete vm.cofoinfo;
                        delete vm.uploadType;
                        vm.searchmatch = false;
                        vm.noCofoMsg = true;
                        $timeout(function () {
                            $('.highlight').focus();
                        })
                        return;
                    }
                }
            }, function (response) {
                console.log("Error");
            });
        }

        function manage_nocofo() {
            $('#cofoPopup').modal('hide');
            if (vm.noCofo) {
                SaveWhenNoCofo();
            } else {
                SessionFactory.setSessionAsClear();
                if (vm.navigationPath != '')
                    $location.path('/' + vm.navigationPath);
            }
        }

        function submitCofo() {
            SessionFactory.setSessionAsClear();
            vm.cofo.date = '01/01/1900';
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            if (angular.isDefined(vm.cofoinfo)) {
                if (vm.uploadType == false) {
                    data = vm.cofo.searchDetails;
                    data.IsValid = false;
                } else {
                    data = vm.cofoinfo;
                    data.StreetType = data.StreetType;
                    data.IsUploadSupportDoc = true;
                    data.OccupancyAddssValidate = "InCorrect";
                    data.IsValid = true;
                }
            } else {
                data = vm.cofo.searchDetails;
                data.IsValid = false;
                data.OccupancyAddssValidate = "Correct";
            }

            if (vm.searchdefined != vm.correctSearch) {
                data.IsDataChange = true;
            }

            data.MasterId = UtilityFactory.getMasterId($routeParams.guid);
            data.Number = vm.cofo.number;
            data.DateofIssue = vm.cofo.date;
            data.Type = $location.path().split('/')[2];
            storeSearchZoneToDb(data).then(function (response) {
                if (response.data) {
                    if (vm.navigationPath != '') {
                        $location.path('/' + vm.navigationPath);
                    }
                    else {
                        vm.navToRegisteredAgent();
                    }
                }
            }, ErrorWhileProcessing);
        }

        vm.navToNextStepfromcofo = function (path) {
            vm.navigationPath = path;
            if (vm.contact_us.$invalid || !vm.searchZoningClicked || angular.isUndefined(vm.correctSearch) || (vm.correctSearch != true && vm.corpnorregadd_form.$invalid)) {
                $('#error_msg').html(vm.currentpage_errors.NextButtonIncompleteData).focus();
            } else {
                submitCofo();
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

        function checkExisting(obj) {
            if (angular.equals(JSON.stringify(obj), JSON.stringify(vm.cofoInfovalidation)))
                return true;
            return false;
        }

        function isSessionIncomplete() {
            if (SessionFactory.isSessionDirty()) {
                if (Object.keys(vm.cofo).length >= 2 && (!vm.searchZoningClicked || angular.isUndefined(vm.correctSearch) || !angular.equals(vm.correctSearch, vm.searchdefined) || (vm.correctSearch != undefined && vm.correctSearch != true && (vm.corpnorregadd_form.$invalid || !checkExisting(vm.cofoinfo))))) {
                    SessionFactory.setSessionAsDirty();
                } else {
                    SessionFactory.setSessionAsClear();
                }
            }
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                vm.navigationPath = next.split('#')[1].slice(1);
                isSessionIncomplete();
                if (SessionFactory.isSessionDirty()) {
                    event.preventDefault();
                    //if (vm.uploadType != true) {
                    //    popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigationPath);
                    //} else {
                    popupFactory.showpopup(vm.currentpage_errors.navigateaway, vm.navigationPath);
                    // }
                }
                //if (!vm.navigate) {
                //    if ((!vm.searchZoningClicked || angular.isUndefined(vm.correctSearch) || !angular.equals(vm.correctSearch, vm.searchdefined) || (vm.correctSearch != true && (vm.corpnorregadd_form.$invalid || !checkExisting(vm.cofoinfo))))) {
                //        event.preventDefault();
                //        if (vm.uploadType != true) {
                //            $('#cofoPopup .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.incompleteData + "</h3>");
                //        } else {
                //            $('#cofoPopup .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.navigateaway + "</h3>");
                //        }
                //        $('#cofoPopup').modal('show');
                //        return;
                //    }
                //}
            }
        });

        vm.navToBack = function () {
            $window.history.back();
        }

        vm.navToRegisteredAgent = function () {
            if (vm.submissionStatusData.IsCorporationDivision == true) {
                $location.path('/physicallocation/corpreg/' + $routeParams.guid);
            }
            else {
                if (vm.submissionStatusData.TradeName != '' && vm.submissionStatusData.TradeName != 'NA') {
                    $location.path('/corpreqregwithtradesecond/' + $routeParams.guid);
                } else {
                    if ((vm.submissionStatusData.BusinessStructure.toUpperCase() == 'SOLE PROPRIETORSHIP' || vm.submissionStatusData.BusinessStructure.toUpperCase() == 'GENERAL PARTNERSHIP' || vm.submissionStatusData.BusinessStructure.toUpperCase() == 'JOINT VENTURE') && (vm.submissionStatusData.TradeName == '' || vm.submissionStatusData.TradeName == 'NA')) {
                        $location.path('/corpnotregisteredaddress/' + $routeParams.guid);
                    }
                    else if ((vm.submissionStatusData.BusinessStructure.toUpperCase() != 'SOLE PROPRIETORSHIP' || vm.submissionStatusData.BusinessStructure.toUpperCase() != 'GENERAL PARTNERSHIP' || vm.submissionStatusData.BusinessStructure.toUpperCase() != 'JOINT VENTURE') && (vm.submissionStatusData.TradeName == '' || vm.submissionStatusData.TradeName == 'NA')) {
                        $location.path('/corpreqregisterfirst/' + $routeParams.guid);
                    }
                    //if ((vm.submissionStatusData.BusinessStructure == 'SOLE PROPRIETORSHIP' || vm.submissionStatusData.BusinessStructure == 'GENERAL PARTNERSHIP' || vm.submissionStatusData.BusinessStructure == 'JOINT VENTURE') && (vm.submissionStatusData.TradeName == '' || vm.submissionStatusData.TradeName == 'NA')) {
                    //    $location.path('/corpnotregisteredaddress/' + $routeParams.guid);
                    //}
                    //else if ((vm.submissionStatusData.BusinessStructure != 'SOLE PROPRIETORSHIP' || vm.submissionStatusData.BusinessStructure != 'GENERAL PARTNERSHIP' || vm.submissionStatusData.BusinessStructure != 'JOINT VENTURE') && (vm.submissionStatusData.TradeName == '' || vm.submissionStatusData.TradeName == 'NA')) {
                    //    $location.path('/corpreqregisterfirst/' + $routeParams.guid);
                    //}
                }
            }
        }
    }
}());