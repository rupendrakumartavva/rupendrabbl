(function () {

    'use strict';
    var controllerId = 'CorpNotRegisteredAddressController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', 'appConstants', '$routeParams', 'UtilityFactory', 'SessionFactory', 'errorFactory', 'BBLSubmissionFactory', 'popupFactory', 'authService', '$window', CorpNotRegisteredAddressController]);

    function CorpNotRegisteredAddressController($scope, $rootScope, $location, requestService, appConstants, $routeParams, UtilityFactory, SessionFactory, errorFactory, BBLSubmissionFactory, popupFactory, authService, $window) {
        var vm = this;
        vm.navigatePath = '';
        vm.previousObj = {};
        vm.ehopaddress = {};
        vm.submissionStatusData = {};
        vm.Countries = {};
        vm.validations_wrt_contry = errorFactory.isCountryUS(true);
        vm.showStateAsDropdown = false;
        vm.IsDataChange = false;
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
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        } else {
                            StreetsDropDown().then(function (response) {
                                vm.StreetTypes = response.data.StreetList;
                                vm.Countries = response.data.CountryList;
                                requestService.getStateList({ CountryCode: "US" }).then(function (response) {
                                    vm.StatesList = response.data.Status;
                                    vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                                    var data = {};
                                    data.FileNumber = "NA";
                                    data.UserType = "N-CORPREG";
                                    data.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                                    data.BusinessStructure = vm.submissionStatusData.BusinessStructure;
                                    data.TradeName = vm.submissionStatusData.TradeName;
                                    data.Telphone = data.Telphone;
                                    getHQaddress(data).then(function (response) {
                                        if (response.data.BusinessName != null) {
                                            vm.ehopaddress = response.data;
                                        }
                                        else {
                                            vm.ehopaddress.BusinessCountry = "US";
                                        }
                                        vm.countryChanged(true);
                                        vm.previousObj = angular.copy(vm.ehopaddress);
                                        $('#dvLoadingSection').css('display', 'none');
                                        $("#dvMainsection").css("display", "block");
                                    }, ErrorWhileProcessing);
                                }, ErrorWhileProcessing);
                            });
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



        function StreetsDropDown() {
            return requestService.streetsDropDown();
        }

        function getHQaddress(data) {
            return requestService.GetHQA(data);
        }

        function ErrorWhileProcessing() {
            console.log("Error");
        }

        vm.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#error_msg').html('');
        }

        function savecorpdata() {
            vm.ehopaddress.UserType = "N-CORPREG";
            vm.ehopaddress.FileNumber = "NA";
            vm.ehopaddress.MasterId = UtilityFactory.getMasterId($routeParams.guid);
            vm.ehopaddress.BusinessStructure = vm.submissionStatusData.BusinessStructure;
            vm.ehopaddress.TradeName = vm.submissionStatusData.TradeName;
            vm.ehopaddress.BusinessCountry = vm.ehopaddress.BusinessCountry;
            vm.ehopaddress.CBusinessName = vm.ehopaddress.BusinessName;

            if (vm.previousObj.BusinessName != (vm.ehopaddress.BusinessName)) {
                vm.IsDataChange = true;
            }

            vm.ehopaddress.IsDataChange = vm.IsDataChange;
        }

        function submitcorpaddress() {
            return requestService.SubmitCorpAgent(vm.ehopaddress);
        }

        function deletecorpaddress(data) {
            return requestService.CorpNotRegEmptyHqAddress(data);
        }

        function emptycorpdata() {
            vm.corpaddressempty = {};
            vm.corpaddressempty.UserType = "N-CORPREG";
            vm.corpaddressempty.FileNumber = "NA";
            vm.corpaddressempty.MasterId = UtilityFactory.getMasterId($routeParams.guid);
            vm.corpaddressempty.BusinessStructure = vm.submissionStatusData.BusinessStructure;
            vm.corpaddressempty.TradeName = vm.submissionStatusData.TradeName;
            vm.corpaddressempty.IsDataChange = true;
            deletecorpaddress(vm.corpaddressempty).then(deletedFromDb, ErrorWhileProcessing);
        }

        function deletedFromDb() {
            SessionFactory.setSessionAsClear();
            $location.path('/' + vm.navigatePath);
        }

        vm.navToAgent = function () {
            if (vm.corpnorregadd_form.$invalid) {
                SessionFactory.setSessionAsDirty();
                $('#error_msg').html(vm.currentpage_errors.NextButtonIncompleteData).focus();
            }
            else {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                savecorpdata();
                submitcorpaddress().then(function (response) {
                    SessionFactory.setSessionAsClear();
                    $location.path('/corpnotregisteredagent/' + $routeParams.guid);
                }, ErrorWhileProcessing);
            }
        }

        vm.checkAndExit = function (path) {
            vm.submitted = true;
            vm.navigatePath = path;
            if (path.indexOf('app') != -1) {
                vm.navigatePath = path + '/' + $routeParams.guid;
            } else {
                vm.navigatePath = path;
            }

            if (vm.corpnorregadd_form.$invalid) {
                if (SessionFactory.isFormEmpty(vm.corpnorregadd_form)) {
                    $('#dvLoadingSection').css('display', 'block');
                    $("#dvMainsection").css("display", "none");
                    emptycorpdata();
                } else {
                    SessionFactory.setSessionAsDirty();
                    popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigatePath);
                }
            } else {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                savecorpdata();
                submitcorpaddress().then(function (response) {
                    SessionFactory.setSessionAsClear();
                    $location.path('/' + vm.navigatePath);
                }, ErrorWhileProcessing);
            }

            //vm.ehopaddress=SessionFactory.removeUndefinedProperties(angular.copy(vm.ehopaddress));
        }

        vm.countryChanged = function (cameFrominit) {
            if (!cameFrominit) {
                var country = vm.ehopaddress.BusinessCountry;
                errorFactory.setAllFormControlsEmpty(vm.corpnorregadd_form);
                vm.ehopaddress = {};
                vm.ehopaddress.BusinessCountry = country;
            }
            vm.corpnorregadd_form.ZipCode.$setValidity('customminlength', true);
            vm.corpnorregadd_form.ZipCode.$setValidity('customlength', true);
            vm.corpnorregadd_form.Telephone.$setValidity('customlength', true);
            vm.corpnorregadd_form.Telephone.$setValidity('customminlength', true);
            if (vm.ehopaddress.BusinessCountry != "US") {
                vm.showStateAsDropdown = true;
            } else {
                vm.showStateAsDropdown = false;
            }
            vm.validations_wrt_contry = errorFactory.isCountryUS(!angular.copy(vm.showStateAsDropdown));
        }

        vm.checkZipMaxLength = function () {
            if (vm.ehopaddress.ZipCode != undefined) {
                if (vm.ehopaddress.ZipCode.length > vm.validations_wrt_contry.zip.maxlength) {
                    vm.corpnorregadd_form.ZipCode.$setValidity('customlength', false);
                } else {
                    vm.corpnorregadd_form.ZipCode.$setValidity('customlength', true);
                }
                if (vm.ehopaddress.ZipCode.length < vm.validations_wrt_contry.zip.minlength) {
                    vm.corpnorregadd_form.ZipCode.$setValidity('customminlength', false);
                } else {
                    vm.corpnorregadd_form.ZipCode.$setValidity('customminlength', true);
                }
            } else {
                vm.corpnorregadd_form.ZipCode.$setValidity('customlength', true);
                vm.corpnorregadd_form.ZipCode.$setValidity('customminlength', true);
            }
        }

        vm.checkTelephoneMaxLength = function () {
            if (vm.ehopaddress.Telphone != "") {
                if (vm.ehopaddress.Telphone.length > vm.validations_wrt_contry.telephone.maxlength) {
                    vm.corpnorregadd_form.Telephone.$setValidity('customlength', false);
                } else {
                    vm.corpnorregadd_form.Telephone.$setValidity('customlength', true);
                }
                if (vm.ehopaddress.Telphone.length < vm.validations_wrt_contry.telephone.minlength) {
                    vm.corpnorregadd_form.Telephone.$setValidity('customminlength', false);
                } else {
                    vm.corpnorregadd_form.Telephone.$setValidity('customminlength', true);
                }
            } else {
                vm.corpnorregadd_form.Telephone.$setValidity('customminlength', true);
                vm.corpnorregadd_form.Telephone.$setValidity('customlength', true);
            }
        }


        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                SessionFactory.compareObjectsInCurrentSession(angular.copy(vm.ehopaddress), vm.previousObj);
                if (SessionFactory.isSessionDirty()) {
                    vm.navigatePath = next.split('#')[1].slice(1);
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.navigateaway, vm.navigatePath);
                    return;
                }
            }
        });

        vm.navToBack = function () {
            $window.history.back();
        }
    }
})();