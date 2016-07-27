(function () {

    'use strict';
    var controllerId = 'eHOPHomeAddressController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', 'appConstants', '$routeParams', 'UtilityFactory', '$filter', 'MAR_validation_service', 'BBLSubmissionFactory', 'errorFactory', 'SessionFactory', 'popupFactory', 'authService','$window', eHOPHomeAddressController]);

    function eHOPHomeAddressController($scope, $rootScope, $location, requestService, appConstants, $routeParams, UtilityFactory, $filter, MAR_validation_service, BBLSubmissionFactory, errorFactory, SessionFactory, popupFactory, authService, $window) {
        var vm = this;
        var autoSuggestCount = ''
        vm.dataavail = false;
        var autoSuggestKey = '';
        var previousname = '';
        vm.previousObj = {};
        vm.address = {};
        vm.navigationPath = '';
        vm.ehopaddressempty = {};
        vm.submissionStatusData = {};
        vm.fieldsDisable = false;
        vm.Address = [];
        vm.currentpage_errors = {};
        SessionFactory.setSessionAsDirty();
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
                    requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid)).then(function (response) {
                        vm.submissionStatusData = response.data;
                        vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        } else {

                            vm.fieldsDisable = MAR_validation_service.initialDisablingFields();
                            vm.ehopaddress = {};
                            vm.address = {};
                            vm.address.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                            vm.address.FileNumber = 'NA';
                            vm.address.UserType = 'ehop';
                            vm.address.UserSelectTpe = "Primses Address";
                            StreetsDropDown().then(function (streetresponse) {
                                vm.StreetTypes = streetresponse.data.StreetList;
                                vm.Countries = streetresponse.data.CountryList;
                                requestService.GetPriAdd(vm.address).then(function (response) {
                                    if (response.data.BusinessAddressLine3 != null) {
                                        vm.ehopaddress = response.data;
                                        vm.address.type = "2";
                                        vm.ehopaddress.Street = response.data.BusinessAddressLine3;
                                        vm.ehopaddress.City = response.data.BusinessCity;
                                        vm.ehopaddress.State = response.data.BusinessState;
                                        vm.ehopaddress.Zip = response.data.ZipCode;
                                        vm.ehopaddress.Telephone = response.data.Telphone;
                                        vm.ehopaddress.Quadrant = response.data.Quardrant;
                                        vm.ehopaddress.StreetName = response.data.BusinessAddressLine1;
                                        vm.ehopaddress.AddressNumber = response.data.AddressNumber;
                                        vm.ehopaddress.Country = response.data.BusinessCountry;
                                        vm.ehopaddress.AddressID = response.data.AddressID;
                                        vm.ehopaddress.AddressNumberSufix = response.data.AddressNumberSufix;
                                        vm.ehopaddress.Xcoord = response.data.Xcoord;
                                        vm.ehopaddress.Ycoord = response.data.Ycoord;
                                        vm.ehopaddress.Anc = response.data.Anc;
                                        vm.ehopaddress.Ward = response.data.Ward;
                                        vm.ehopaddress.Cluster = response.data.Cluster;
                                        vm.ehopaddress.Latitude = response.data.Latitude;
                                        vm.ehopaddress.Longitude = response.data.Longitude;
                                        vm.ehopaddress.Vote_Prcnct = response.data.Vote_Prcnct;
                                        vm.ehopaddress.StreetType = response.data.BusinessAddressLine2;
                                        vm.ehopaddress.IsDataChange = false;
                                        vm.getSuggestions(vm.ehopaddress.Street.substring(0, 4));
                                    } else {
                                        vm.ehopaddress = MAR_validation_service.invalid_DC_address();
                                    }
                                    vm.previousObj = angular.copy(vm.ehopaddress);
                                    $('#dvLoadingSection').css('display', 'none');
                                    $("#dvMainsection").css("display", "block");
                                }, ErrorWhileProcessing);
                            }, ErrorWhileProcessing);
                        }
                    });
                } else {
                    SessionFactory.setSessionAsClear();
                    BBLSubmissionFactory.noGuidAvailable();
                }
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

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "physicallocation/corpreg" page

        //------------------------------------------------------------------

        vm.navToCorpRegistrationFromEhop = function () {
            if (vm.ehophomeadd_form.$invalid) {
                $('#error_msg').html(vm.currentpage_errors.allfieldsNotFilled).focus();
            }
            else {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                vm.navigationPath = '';
                submitEhopHomeAdd();
            }
        }

        function submitEhopHomeAdd() {
            vm.ehopaddress.UserType = "ehop";
            vm.ehopaddress.FileNumber = 'NA';
            vm.ehopaddress.DateofIssue = '01/01/1990';
            vm.ehopaddress.MasterId = UtilityFactory.getMasterId($routeParams.guid);
            vm.ehopaddress.Type = "ehop";
            InsertEhopToDB(vm.ehopaddress).then(InsertionResult, ErrorWhileProcessing);
        }

        function DeleteEhopAddressToDB(data) {
            return requestService.DeleteEhopAddress(data);
        }

        function deletionEhopAddress() {
            vm.ehopaddressempty.UserType = "ehop";
            vm.ehopaddressempty.FileNumber = 'NA';
            vm.ehopaddressempty.DateofIssue = '01/01/1990';
            vm.ehopaddressempty.MasterId = UtilityFactory.getMasterId($routeParams.guid);
            vm.ehopaddressempty.Type = "EHOPADDRESS";
            vm.ehopaddressempty.IsDataChange = true;
        }

        function InsertEhopToDB(data) {
            return requestService.FetchedDataToDb(data);
        }

        function InsertionResult(response) {
            if (response.data) {
                vm.navToRegisteredAgent();
            }
        }

        function ErrorWhileProcessing(response) {
            console.log("Error");
        }

        vm.getrelatedAddress = function (address) {
            vm.ehopaddress = angular.copy(MAR_validation_service.getTotalAddress(address));
            vm.fieldsDisable = true;
            if (vm.previousObj.Street != vm.ehopaddress.Street) {
                vm.ehopaddress.IsDataChange = true;
            } else {
                vm.ehopaddress.IsDataChange = false;
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
                            vm.ehopaddress = MAR_validation_service.invalid_DC_address(street);
                            vm.not_valid_Address = true;
                        }
                    }, ErrorWhileProcessing);
                    break;
                case "smallerstring":
                    vm.Address = [];
                    vm.ehopaddress = MAR_validation_service.invalid_DC_address(street);
                    vm.not_valid_Address = false;
                    break;
                case "morethenrequired":
                    if ($filter('filter')(vm.Address, { FullAddress: angular.copy(vm.ehopaddress.Street) }, vm.startsWith).length == 0) {
                        vm.not_valid_Address = true;
                        vm.ehopaddress = MAR_validation_service.invalid_DC_address(street);
                    }
                    break;
            }
        }

        vm.startsWith = function (fulladdress, viewValue) {
            return fulladdress.substr(0, viewValue.length).toLowerCase() == viewValue.toLowerCase();
        }
        vm.navToBack = function () {
            $window.history.back();
        }

        vm.navToRegisteredAgent = function () {
            SessionFactory.setSessionAsClear();
            if (vm.navigationPath == '') {
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
            } else {
                $location.path('/' + vm.navigationPath);
            }
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
        }

        vm.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#error_msg').html('');
        }

        vm.checkAndExit = function (path) {
            vm.submitted = true;
            if (path.indexOf('app') != -1) {
                vm.navigationPath = path + '/' + $routeParams.guid;
            } else {
                vm.navigationPath = path
            }

            if (vm.ehophomeadd_form.$invalid) {
                if (SessionFactory.isFormEmpty(vm.ehophomeadd_form)) {
                    $('#dvLoadingSection').css('display', 'block');
                    $("#dvMainsection").css("display", "none");
                    deletionEhopAddress();
                    DeleteEhopAddressToDB(vm.ehopaddressempty).then(function () {
                        SessionFactory.setSessionAsClear();
                        $location.path('/' + vm.navigationPath);
                    }, ErrorWhileProcessing);
                } else {
                    SessionFactory.setSessionAsDirty();
                    popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigationPath);
                }
            } else {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                submitEhopHomeAdd();
            }
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                SessionFactory.compareObjectsInCurrentSession(angular.copy(vm.ehopaddress), vm.previousObj);
                if (SessionFactory.isSessionDirty()) {
                    vm.navigationPath = next.split('#')[1].slice(1);
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.navigateaway, vm.navigationPath);
                    return;
                }
            }
        });
    }
})();