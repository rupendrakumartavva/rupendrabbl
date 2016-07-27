(function () {

    'use strict';
    var controllerId = 'RenewalCleanHandsController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', 'appConstants', '$routeParams', 'RenewalUtilityFactory', '$timeout', 'popupFactory', 'SessionFactory', 'errorFactory', 'authService', RenewalCleanHandsController]);

    function RenewalCleanHandsController($scope, $rootScope, $location, requestService, appConstants, $routeParams, RenewalUtilityFactory, $timeout, popupFactory, SessionFactory, errorFactory, authService) {

        var vm = this;
        vm.taxrevenue = {};
        vm.FullAddress = '';
        vm.BusinessOwner = '';
        vm.signatureMismatch = false;
        vm.tradeName = '';
        vm.SelfCertificationDate = null;
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
                if (RenewalUtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    BblFullAddress(RenewalUtilityFactory.getRenewalObject($routeParams.guid)).then(function (response) {
                        vm.FullAddress = response.data.FullAddress;
                        vm.BusinessOwner = response.data.BusinessOwner;
                        if (vm.BusinessOwner == null || vm.BusinessOwner == '') {
                            vm.BusinessOwner = 'NA';
                        }
                        vm.taxrevenue.number = response.data.TaxNumber;
                        vm.SelfCertificationDate = response.data.CreatedDate;
                        vm.applicationType = response.data.BusinessType;
                        vm.cleanHandsType = response.data.TaxType;//'FEIN'
                        vm.tradeName = response.data.tradeName;//'FEIN'
                        getPreviousData().then(function (response) {
                            vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                            //response.data.length == 0 || 
                            if (response.data != "false") {
                                if (response.data.length > 0) {
                                    vm.taxrevenue.FullName = response.data[0].FullName;
                                    vm.taxrevenue.declaration = response.data[0].IsIAgree;
                                    //vm.declaration = true;
                                    vm.taxrevenue.signature = response.data[0].FullName;
                                    vm.taxrevenue.BusinessOwnerRoles = response.data[0].BusinessOwnerRoles;
                                    vm.taxrevenue.number = response.data[0].TaxRevenueNumber;

                                }
                            }
                            $('#dvLoadingSection').css('display', 'none');
                            $("#dvMainsection").css("display", "block");
                        }, onFetchingDataError);
                    }, onFetchingDataError);
                } else {
                    RenewalUtilityFactory.noGuid();
                }
                // }, onFetchingDataError);
            }, function () {
                $location.path("/login");
            });
        }

        function getPreviousData() {

            return requestService.getRenewalCleanhandsData(RenewalUtilityFactory.getRenewalObject($routeParams.guid));
        }

        //function confirmRenuwal(confirmdata) {
        //    confirmdata.InitalDocumet = "Initial";
        //    return requestService.confirmRenuwal(confirmdata);
        //};

        function BblFullAddress(data) {

            return requestService.BblFulladdress(data);
        };

        //function getSubmissionMaster() {
        //    return requestService.getSubmissionMasterWithbbl(RenewalUtilityFactory.getRenewalObject($routeParams.guid));
        //}

        function onFetchingDataError() {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error");
        }

        vm.navToSupportingDocs = function () {
            if (vm.renewalcleanhands.$invalid) {
                vm.fieldsmissed = true;
                $timeout(function () {
                    $("#fieldsmissed").focus();
                }, 1);
            } else
                submitTaxes();
        }

        function submitTaxes() {

            var data = {};
            data = RenewalUtilityFactory.getRenewalObject($routeParams.guid);
            data.TaxRevenueFFIN = vm.taxrevenue.number;
            data.MasterId = vm.masterId;
            data.TaxRevenueType = 'FEIN/SSN';
            data.BusinessOwnerRoles = vm.taxrevenue.BusinessOwnerRoles;
            data.FullName = vm.taxrevenue.FullName;
            data.CleanHandsType_SSN_FEIN = vm.cleanHandsType;
            data.IsIAgree = vm.taxrevenue.declaration;
            verifyNumberFromDB(data).then(VerificationFromDB, onFetchingDataError);
        }

        function VerificationFromDB(response) {
            SessionFactory.setSessionAsClear();
            //if (response.data.DocumentList) {
            if (response.data.DocumentList.length > 0) {
                //UpdateRenwalDocumentType().then(function (response) {
                //CheckDocument().then(function (response) {
                //if (response.data == "true") {
                $location.path('/renewal/supportingdocs/step2/' + $routeParams.guid);
                //}
                //}, onFetchingDataError);
                //   $location.path('/renewal/supportingdocs/step1/' + $routeParams.guid);
            }
            else {
                $location.path('/renewalpayment/' + $routeParams.guid);
            }



            // }
        }

        function verifyNumberFromDB(data) {
            return requestService.renewTaxValidation(data);
        }

        vm.navToMyBBL = function () {
            $location.path('/mybbl');
        }

        vm.navToPrevious = function () {
            SessionFactory.setSessionAsClear();
            $location.path('/bblrenewalconfirm/' + $routeParams.guid);
        };

        vm.restrictMaxLength = function () {
            if (vm.taxrevenue.number.length > 11) {
                vm.taxrevenue.number = vm.taxrevenue.number.substring(0, 11);
            }
        }

        vm.checkSignWithFullName = function () {
            if (angular.isDefined(vm.taxrevenue.signature)) {
                if (!angular.equals(vm.taxrevenue.signature, vm.taxrevenue.FullName)) {
                    vm.signatureMismatch = true;
                    vm.renewalcleanhands.signature.$setValidity('required', false);
                } else {
                    vm.signatureMismatch = false;
                    vm.renewalcleanhands.signature.$setValidity('required', true);
                }
            } else {
                if (angular.isUndefined(vm.taxrevenue.FullName)) {
                    vm.signatureMismatch = false;
                }
            }
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                vm.navigationPath = next.split('#')[1].slice(1);
                if (SessionFactory.isSessionDirty()) {
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.renewalNavigation, vm.navigationPath);
                    return;
                }
            }
        });


    }
})();