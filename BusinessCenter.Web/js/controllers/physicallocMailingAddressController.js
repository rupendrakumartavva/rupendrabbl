(function () {

    'use strict';
    var controllerId = 'PhysicallocMailingController';
    angular.module('DCRA').controller(controllerId,
    ['$scope', '$rootScope', '$location', 'requestService', 'appConstants', '$routeParams', 'UtilityFactory', '$timeout', 'SessionFactory', 'errorFactory', 'BBLSubmissionFactory', 'popupFactory', 'authService','$window', PhysicallocMailingController]);

    function PhysicallocMailingController($scope, $rootScope, $location, requestService, appConstants, $routeParams, UtilityFactory, $timeout, SessionFactory, errorFactory, BBLSubmissionFactory, popupFactory, authService, $window) {

        var vm = this;
        vm.navigate = false;
        vm.prevObj = {};
        vm.previousType = '';
        vm.address = {};
        vm.country = null;
        vm.validateCountry = true;
        SessionFactory.setSessionAsDirty();
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
                    requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid)).then(function (response) {

                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        } else {
                            vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                            vm.diffmail = {};
                            vm.address.type = 3;
                            StreetsDropDown().then(function (streetresponse) {
                                vm.StreetTypes = streetresponse.data.StreetList;
                                vm.Countries = streetresponse.data.CountryList;

                                requestService.getStateList({ CountryCode: "US" }).then(function (response) {
                                    vm.diffmail.BusinessCountry = "US";
                                    vm.StatesList = response.data.Status;
                                    vm.diffmail.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                                    BindData();
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

        function StreetsDropDown() {
            return requestService.streetsDropDown();
        }

        function BindData() {
            vm.address.MasterId = UtilityFactory.getMasterId($routeParams.guid);
            vm.address.UserSelectTpe = "NEWMAIL";
            vm.address.UserType = "NEWMAIL";
            requestService.GetHQA(vm.address).then(function (response) {
                vm.diffmail.FileNumber = response.data.FileNumber;
                vm.diffmail = response.data;
                if (vm.diffmail.Telphone == null || vm.diffmail.Telphone == "") {
                    vm.diffmail.Telphone = undefined;
                }
                if (response.data.BusinessCountry == "" || response.data.BusinessCountry == null) {
                    vm.diffmail.BusinessCountry = "US";
                } else {
                    vm.diffmail.BusinessCountry = response.data.BusinessCountry;
                }

                if (vm.diffmail.BusinessCountry == "US") {
                    vm.validateCountry = true;
                } else {
                    vm.validateCountry = false;
                }
                vm.validations_wrt_contry = errorFactory.isCountryUS(angular.copy(vm.validateCountry));
                vm.prevObj = angular.copy(vm.diffmail);
                $('#dvLoadingSection').css('display', 'none');
                $("#dvMainsection").css("display", "block");
            }, ErrorWhileProcessing);
        }

        function ErrorWhileProcessing(response) {
            console.log("Error");
        }

        vm.selectOption = function () {
            SessionFactory.setSessionAsDirty();
            $('#err_msg').html('');
            BindData(vm.address.type);

        };

        vm.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#mail_msg').html('');
        }

        vm.checkAndExit = function (path) {
            vm.submitted = true;
            if (path.indexOf('app') != -1) {
                vm.navigationPath = path + '/' + $routeParams.guid;
            } else {
                vm.navigationPath = path
            }
            vm.address.MasterId = UtilityFactory.getMasterId($routeParams.guid);

            if (vm.prefer_mail.$invalid) {
                if (SessionFactory.isFormEmpty(vm.prefer_mail)) {
                    $('#dvLoadingSection').css('display', 'block');
                    $("#dvMainsection").css("display", "none");
                    vm.diffmail.UserSelectTpe = "NEWMAIL";
                    vm.diffmail.UserType = "NEWMAIL";
                    vm.diffmail.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                    vm.diffmail.CBusinessName = vm.diffmail.BusinessName;
                    requestService.SubmitCorpAgent(vm.diffmail).then(function (response) {
                        SessionFactory.setSessionAsClear();
                        $location.path('/' + vm.navigationPath);
                    });
                } else {
                    popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigationPath);
                }
            } else {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                vm.diffmail.UserSelectTpe = "NEWMAIL";
                vm.diffmail.UserType = "NEWMAIL";
                vm.diffmail.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                vm.diffmail.CBusinessName = vm.diffmail.BusinessName;
                requestService.SubmitCorpAgent(vm.diffmail).then(function (response) {
                    vm.diffmail.FileNumber = response.data.FileNumber;
                    SessionFactory.setSessionAsClear();
                    $location.path('/' + vm.navigationPath);
                }, ErrorWhileProcessing);
            }
        }

        vm.countryChanged = function () {
            vm.country = vm.diffmail.BusinessCountry;
            errorFactory.setAllFormControlsEmpty(vm.prefer_mail);
            vm.diffmail = {};
            vm.prefer_mail.Zipcode.$setValidity('customminlength', true);
            vm.prefer_mail.Zipcode.$setValidity('customlength', true);
            vm.prefer_mail.telephone.$setValidity('customlength', true);
            vm.prefer_mail.telephone.$setValidity('customminlength', true);
            vm.diffmail.BusinessCountry = vm.country;

            if (vm.diffmail.BusinessCountry != 'US') {
                vm.validateCountry = false;
            } else {
                vm.validateCountry = true;
            }
            vm.validations_wrt_contry = errorFactory.isCountryUS(angular.copy(vm.validateCountry));
        };

        vm.checkZipMaxLength = function () {
            if (vm.diffmail.ZipCode != undefined) {
                if (vm.diffmail.ZipCode.length > vm.validations_wrt_contry.zip.maxlength) {
                    vm.prefer_mail.Zipcode.$setValidity('customlength', false);
                } else {
                    vm.prefer_mail.Zipcode.$setValidity('customlength', true);
                }
                if (vm.diffmail.ZipCode.length < vm.validations_wrt_contry.zip.minlength) {
                    vm.prefer_mail.Zipcode.$setValidity('customminlength', false);
                } else {
                    vm.prefer_mail.Zipcode.$setValidity('customminlength', true);
                }
            } else {
                vm.prefer_mail.Zipcode.$setValidity('customlength', true);
                vm.prefer_mail.Zipcode.$setValidity('customminlength', true);
            }
        }

        vm.checkTelephoneMaxLength = function () {
            if (vm.diffmail.Telphone != "" && vm.diffmail.Telphone != undefined) {
                if (vm.diffmail.Telphone.length > vm.validations_wrt_contry.telephone.maxlength) {
                    vm.prefer_mail.telephone.$setValidity('customlength', false);
                } else {
                    vm.prefer_mail.telephone.$setValidity('customlength', true);
                }
                if (vm.diffmail.Telphone.length < vm.validations_wrt_contry.telephone.minlength) {
                    vm.prefer_mail.telephone.$setValidity('customminlength', false);
                } else {
                    vm.prefer_mail.telephone.$setValidity('customminlength', true);
                }
            } else {
                vm.prefer_mail.telephone.$setValidity('customminlength', true);
                vm.prefer_mail.telephone.$setValidity('customlength', true);
            }
        }
        vm.navToBack = function () {
            $window.history.back();
        }
        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                vm.navigationPath = next.split('#')[1].slice(1);
                SessionFactory.compareObjectsInCurrentSession(angular.copy(vm.diffmail), vm.prevObj);
                if (SessionFactory.isSessionDirty()) {
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.navigateaway, vm.navigationPath);
                    return;
                }
            }
        });
    }

}());