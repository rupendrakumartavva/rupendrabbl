(function () {

    'use strict';
    var controllerId = 'NoPoBoxController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', 'appConstants', '$routeParams', 'UtilityFactory', '$timeout', 'SessionFactory', '$filter', 'MAR_validation_service', 'errorFactory', 'BBLSubmissionFactory', 'popupFactory', 'authService','$window', NoPoBoxController]);

    function NoPoBoxController($scope, $rootScope, $location, requestService, appConstants, $routeParams, UtilityFactory, $timeout, SessionFactory, $filter, MAR_validation_service, errorFactory, BBLSubmissionFactory, popupFactory, authService, $window) {
        var vm = this;
        //var autoSuggestCount = ''
        vm.dataavail = false;
        //var autoSuggestKey = '';
        //var previousname = '';
        vm.nopobox = {};
        vm.prevObj = {};
        vm.navigatepath = '';
        vm.Address = [];
        vm.submissionStatusData = {};
        vm.stateValidation = false;
        //vm.count = "1";
        vm.showStateAsDropdown = false;
        SessionFactory.setSessionAsDirty();
        vm.fieldsDisable = false;
        vm.not_valid_Address = false;
        vm.currentpage_errors = {};
        vm.validations_wrt_contry = errorFactory.isCountryUS(true);

        init();

        /*
         * Function: init
         * init (initialize) method: first method to be executed on controller load.
         */

        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                if (UtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    SubmissionStatus().then(function (response) {
                        vm.submissionStatusData = response.data;
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        } else {
                            vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                            StreetsDropDown().then(function (streetresponse) {
                                vm.StreetTypes = streetresponse.data.StreetList;
                                vm.Countries = streetresponse.data.CountryList;
                                requestService.getStateList({ CountryCode: "US" }).then(function (response) {
                                    vm.StatesList = response.data.Status;
                                    getPreviousData().then(function (response) {
                                        vm.nopobox.Country = "US";
                                        if (vm.submissionStatusData.IsBusinessMustbeinDc) {
                                            vm.nopobox = MAR_validation_service.invalid_DC_address();
                                            vm.fieldsDisable = MAR_validation_service.initialDisablingFields()
                                        }
                                        if (angular.isDefined(response.data.WebserviceList[0])) {
                                            if (response.data.WebserviceList[0].Street != null) {
                                                vm.nopobox = response.data.WebserviceList[0];
                                                vm.nopobox.IsDataChange = false;
                                                vm.prevObj = angular.copy(vm.nopobox);
                                                if (vm.submissionStatusData.IsBusinessMustbeinDc == true) {
                                                    vm.getSuggestions(vm.nopobox.Street.substring(0, 4));
                                                }
                                            }
                                        }
                                        vm.prevObj = angular.copy(vm.nopobox);
                                        vm.countryChanged(true);
                                        $('#dvLoadingSection').css('display', 'none');
                                        $("#dvMainsection").css("display", "block");
                                    }, ErrorWhileProcessing);
                                });
                            }, ErrorWhileProcessing);
                        }
                    }, ErrorWhileProcessing);
                } else {
                    SessionFactory.setSessionAsClear();
                    BBLSubmissionFactory.noGuidAvailable();
                }
            }, function () {
                $location.path("/login");
            });
        }

        function ErrorWhileProcessing(response) {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error :" + JSON.stringify(response.status));
        };

        function getPreviousData() {
            vm.MasterId = UtilityFactory.getMasterId($routeParams.guid);
            return requestService.GetSubmissionCofoHop(UtilityFactory.getMasterId($routeParams.guid));
        }

        function SubmissionStatus() {
            return requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid));
        }

        function StreetsDropDown() {
            return requestService.streetsDropDown();
        }

        vm.setErrorMsg = function (id) {
            $('#error_msg').html('');
        }

        vm.countryChanged = function (cameFrominit) {
            var country = angular.copy(vm.nopobox.Country);
            if (!cameFrominit) {
                errorFactory.setAllFormControlsEmpty(vm.nopoboxForm);
                vm.nopobox = {};
                vm.nopobox.State = null;
                vm.nopobox.Country = country;
            }
            vm.nopoboxForm.zip.$setValidity('customlength', true);
            vm.nopoboxForm.zip.$setValidity('customminlength', true);
            vm.nopoboxForm.telephone.$setValidity('customlength', true);
            vm.nopoboxForm.telephone.$setValidity('customminlength', true);

            if (vm.nopobox.Country != "US") {
                vm.showStateAsDropdown = true;
            } else {
                vm.showStateAsDropdown = false;
            }
            vm.validations_wrt_contry = errorFactory.isCountryUS(!angular.copy(vm.showStateAsDropdown));
        }

        vm.checkZipMaxLength = function () {
            if (vm.nopobox.Zip != undefined) {
                if (vm.nopobox.Zip.length > vm.validations_wrt_contry.zip.maxlength) {
                    vm.nopoboxForm.zip.$setValidity('customlength', false);
                } else {
                    vm.nopoboxForm.zip.$setValidity('customlength', true);
                }
                if (vm.nopobox.Zip.length < vm.validations_wrt_contry.zip.minlength) {
                    vm.nopoboxForm.zip.$setValidity('customminlength', false);
                } else {
                    vm.nopoboxForm.zip.$setValidity('customminlength', true);
                }
            } else {
                vm.nopoboxForm.zip.$setValidity('customlength', true);
                vm.nopoboxForm.zip.$setValidity('customminlength', true);
            }
        }

        vm.checkTelephoneMaxLength = function () {
            if (vm.nopobox.Telephone != "") {
                if (vm.nopobox.Telephone.length > vm.validations_wrt_contry.telephone.maxlength) {
                    vm.nopoboxForm.telephone.$setValidity('customlength', false);
                } else {
                    vm.nopoboxForm.telephone.$setValidity('customlength', true);
                }
                if (vm.nopobox.Telephone.length < vm.validations_wrt_contry.telephone.minlength) {
                    vm.nopoboxForm.telephone.$setValidity('customminlength', false);
                } else {
                    vm.nopoboxForm.telephone.$setValidity('customminlength', true);
                }
            } else {
                vm.nopoboxForm.telephone.$setValidity('customlength', true);
                vm.nopoboxForm.telephone.$setValidity('customminlength', true);
            }
        }

        vm.stateFieldvalidation = function () {
            if ((vm.nopobox.State != undefined) && ((vm.nopobox.State.toUpperCase() == 'DC') || vm.nopobox.State.toUpperCase() == 'DISTRICT OF COLUMBIA')) {
                vm.stateValidation = true;
                $timeout(function () {
                    vm.nopobox.State = '';
                    $('#error_msg').html('You may have to start a new application if you have a DC Business Premise Address because it was previously mentioned in the Pre-Application Questions as the Business is not going to be located in DC.').focus();
                }, 1);
            } else {
                vm.stateValidation = false;
                $('#error_msg').html('');
            }
        }

        vm.getrelatedAddress = function (address) {
            vm.nopobox = angular.copy(MAR_validation_service.getTotalAddress(address));
            if (vm.prevObj.Street != vm.nopobox.FullAddress) {
                vm.nopobox.IsDataChange = true;
            } else {
                vm.nopobox.IsDataChange = false;
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
                            vm.nopobox = MAR_validation_service.invalid_DC_address(street);
                            vm.not_valid_Address = true;
                        }
                    }, ErrorWhileProcessing);
                    break;
                case "smallerstring":
                    vm.Address = [];
                    vm.nopobox = MAR_validation_service.invalid_DC_address(street);
                    vm.not_valid_Address = false;
                    break;
                case "morethenrequired":
                    if ($filter('filter')(vm.Address, { FullAddress: angular.copy(vm.nopobox.Street) }, vm.startsWith).length == 0) {
                        vm.not_valid_Address = true;
                        vm.nopobox = MAR_validation_service.invalid_DC_address(street);
                    }
                    break;
            }
        }

        vm.startsWith = function (fulladdress, viewValue) {
            return fulladdress.substr(0, viewValue.length).toLowerCase() == viewValue.toLowerCase();
        }

        function deleteNopoBoxfromDb(data) {
            return requestService.DeleteEhopAddress(data);
        }

        function deleteSucessful(response) {
            SessionFactory.setSessionAsClear();
            SessionFactory.setFormAsSubmitted();
            $location.path('/' + vm.navigatepath);
        }

        function deletionNoPoBox() {
            vm.nopoboxEmpty = {};
            vm.nopoboxEmpty.UserType = "Nopobox";
            vm.nopoboxEmpty.FileNumber = 'NA';
            vm.nopoboxEmpty.DateofIssue = '01/01/1990';
            vm.nopoboxEmpty.MasterId = UtilityFactory.getMasterId($routeParams.guid);
            vm.nopoboxEmpty.Type = "NOPO";
            vm.nopoboxEmpty.IsDataChange = true;
            deleteNopoBoxfromDb(vm.nopoboxEmpty).then(deleteSucessful, ErrorWhileProcessing);
        }

        vm.CheckAndExit = function (path) {
            vm.submitted = true;
            vm.navigatepath = path;
            if (path.indexOf('app') != -1) {
                vm.navigatepath = path + '/' + $routeParams.guid;
            } else {
                vm.navigatepath = path;
            }

            if (vm.nopoboxForm.$invalid) {
                if (SessionFactory.isFormEmpty(vm.nopoboxForm)) {
                    $('#dvLoadingSection').css('display', 'block');
                    $("#dvMainsection").css("display", "none");
                    deletionNoPoBox();
                } else {
                    SessionFactory.setSessionAsDirty();
                    popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigatepath);
                }
            } else {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                validdata();
                InsertEhopToDB(vm.nopobox).then(function (response) {
                    SessionFactory.setSessionAsClear();
                    SessionFactory.setFormAsSubmitted();
                    $location.path('/' + vm.navigatepath);
                }, ErrorWhileProcessing);
            }

            //if (!vm.nopoboxForm.$invalid) {
            //    SessionFactory.setSessionAsClear();
            //} else {
            //    SessionFactory.compareObjectsInCurrentSession(vm.nopobox, vm.prevObj);
            //}
            //if (vm.not_valid_Address) {
            //    SessionFactory.setSessionAsClear();
            //    SessionFactory.setFormAsSubmitted();
            //    $location.path('/' + vm.navigatepath);
            //}
            //else if (SessionFactory.isSessionDirty()) {

            //    //$('#NoPoBox .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.incompleteData + "</h3>");
            //    //$("#NoPoBox").modal('show');
            //} else {
            //    if (vm.submissionStatusData.IsBusinessMustbeinDc) {
            //        vm.count = "3";
            //    }
            //    if (Object.keys(vm.nopobox).length == vm.count) {
            //        deletionNoPoBox();
            //    } else {

            //    }
            //}
        }
        vm.navToBack = function () {
            $window.history.back();
        }


        vm.navToCorpRegistrationFromPO = function () {
            vm.submitted = true;
            if (vm.nopoboxForm.$invalid) {
                $('#error_msg').html(vm.currentpage_errors.allfieldsNotFilled).focus();
            }
            else {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                validdata();
                InsertEhopToDB(vm.nopobox).then(function (response) {
                    vm.navToRegisteredAgent();
                }, ErrorWhileProcessing);
            }
        }

        function InsertEhopToDB(data) {
            return requestService.FetchedDataToDb(data);
        }

        function validdata() {
            vm.nopobox.StreetType = vm.nopobox.StreetType;
            vm.nopobox.Type = "NOPO";
            vm.nopobox.Number = 'NOPO';
            vm.nopobox.FileNumber = 'NOPO';
            vm.nopobox.DateofIssue = '01/01/1900';
            vm.nopobox.MasterId = UtilityFactory.getMasterId($routeParams.guid);
            if (!vm.submissionStatusData.IsBusinessMustbeinDc) {
                if (vm.prevObj.AddressNumber != vm.nopobox.AddressNumber || vm.prevObj.StreetName != vm.nopobox.StreetName || vm.prevObj.StreetType != vm.nopobox.StreetType || vm.prevObj.Quadrant != vm.nopobox.Quadrant || vm.prevObj.AddressNumberSufix != vm.nopobox.AddressNumberSufix) {
                    vm.nopobox.IsDataChange = true;
                } else {
                    vm.nopobox.IsDataChange = false;
                }
            }
        }

        vm.navToRegisteredAgent = function () {
            SessionFactory.setSessionAsClear();
            SessionFactory.setFormAsSubmitted();
            if (vm.submissionStatusData.IsCorporationDivision == false) {
                if (vm.submissionStatusData.TradeName != '' && vm.submissionStatusData.TradeName != 'NA') {
                    $location.path('/corpreqregwithtradesecond/' + $routeParams.guid);
                }
                else {
                    if ((vm.submissionStatusData.BusinessStructure.toUpperCase() == 'SOLE PROPRIETORSHIP' || vm.submissionStatusData.BusinessStructure.toUpperCase() == 'GENERAL PARTNERSHIP' || vm.submissionStatusData.BusinessStructure.toUpperCase() == 'JOINT VENTURE') && (vm.submissionStatusData.TradeName == '' || vm.submissionStatusData.TradeName == 'NA')) {
                        $location.path('/corpnotregisteredaddress/' + $routeParams.guid);
                    }
                    else if ((vm.submissionStatusData.BusinessStructure.toUpperCase() != 'SOLE PROPRIETORSHIP' || vm.submissionStatusData.BusinessStructure.toUpperCase() != 'GENERAL PARTNERSHIP' || vm.submissionStatusData.BusinessStructure.toUpperCase() != 'JOINT VENTURE') && (vm.submissionStatusData.TradeName == '' || vm.submissionStatusData.TradeName == 'NA')) {
                        $location.path('/corpreqregisterfirst/' + $routeParams.guid);
                    }
                }
            }
            else {
                $location.path('/physicallocation/corpreg/' + $routeParams.guid);
            }
        }

        //vm.dontNavigate = function () {
        //    $('#NoPoBox').modal('hide');
        //}

        //vm.navigateAnyway = function () {
        //    $('#NoPoBox').modal('hide');
        //    SessionFactory.setSessionAsClear();
        //    SessionFactory.setFormAsSubmitted();
        //    $location.path('/' + vm.navigatepath);
        //}

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                SessionFactory.compareObjectsInCurrentSession(vm.nopobox, vm.prevObj);
                if (SessionFactory.isSessionDirty()) {
                    vm.navigatepath = next.split('#')[1].slice(1);
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.navigateaway, vm.navigatepath);
                    //$('#NoPoBox .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.navigateaway + "</h3>");
                    //$("#NoPoBox").modal('show');
                    return;
                }
            }
        });
    }
})();