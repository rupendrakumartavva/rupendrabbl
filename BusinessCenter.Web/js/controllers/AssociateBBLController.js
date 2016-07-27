'use strict';

(function () {

    var controllerId = 'AssociateBBLController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', '$routeParams', 'RenewalUtilityFactory', 'authService', AssociateBBLController]);

    function AssociateBBLController($scope, $rootScope, $location, requestService, $routeParams, RenewalUtilityFactory, authService) {

        var vm = this;

        vm.isvalidated = '';
        vm.type = null;
        init();

        /*
       * Function: init
       * init (initialize) method: first method to be executed on controller load. 
       */

        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                vm.togglepinvalmsg = false;
                $('#dvLoadingSection').css('display', 'none');
                $("#dvMainsection").css("display", "block");
            }, function () {
                $location.path("/login");
            });

        }

        vm.navToBBL = function () {
            $location.path('/mybbl');
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : api/BBLApplication/CheckAssociate
        // Last Update date : 18-08-2015
        // Description      : This Method is validating pin-number and license number and navigating to "verifybusinessbeforeassociation".
        // Last Modified    : Storing the data in localstorage for retaining the data in other pages. 

        //------------------------------------------------------------------

        vm.navToAssociateBBL = function () {
            SendRequest();
        };

        vm.hideError = function () {
            vm.togglepinvalmsg = false;
            if (vm.responseData == 'Already Associate') {
                delete vm.responseData;
            }
        }

        function AssociationRenewalData(response) {
            if (response.data.isValidated) {
                //alert(response.data.SubmissionLicenseNumber, response.data.expDate, response.data.CleanHandsType, response.data.entityId, response.data.B1_Alt_Id);
                //console.log(response.data);
                getServiceId(response.data.SubmissionLicenseNumber, response.data.expDate, response.data.CleanHandsType, response.data.entityId, response.data.B1_Alt_Id).then(function (response) {
                    $location.path(getPathWithRenewalId(response.data.Result));
                }, errorWhileProcessing);
            } else {
                $('#dvLoadingSection').css('display', 'none');
                $("#dvMainsection").css("display", "block");
                if (response.data.isValidated == false) {
                    vm.togglepinvalmsg = true;
                } else if (response.data.status == 'NODATA') {
                    $('#dvLoadingSection').css('display', 'none');
                    $("#dvMainsection").css("display", "block");
                    vm.togglepinvalmsg = true;
                }
                else {
                    vm.responseData = response.data.status;
                }
            }

        }

        function getPathWithRenewalId(UserBblServiceId) {
            if (!RenewalUtilityFactory.containsRenewalServiceId(UserBblServiceId)) {
                RenewalUtilityFactory.addRenewalApplication(RenewalUtilityFactory.getGuid(), UserBblServiceId);
                RenewalUtilityFactory.updateRenewals();
            }
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            return '/verifybusinessbeforeassociation/' + RenewalUtilityFactory.getGuidByRenewalServiceId(UserBblServiceId);
        }

        function getServiceId(SubmissionLicenseNumber, expdate, cleanHandsType, entityId, B1_Alt_Id) {
            return requestService.AssociateBblService({
                SubmissionLicense: SubmissionLicenseNumber,
                UserID: localStorage.userId,
                PinNumber: vm.pinnumber,
                LicenseExpirationDate: expdate,
                Status: false,
                CleanHandsType: cleanHandsType,
                DCBC_ENTITY_ID: entityId,
                B1_ALT_ID: B1_Alt_Id
            });
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "mybbl" page.
        // Last Modified    : 

        //------------------------------------------------------------------
        vm.cancel = function () {
            $location.path('/mybbl');
        }

        var errorWhileProcessing = function () {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error");
        }

        function SendRequest() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            requestService.validateLicense({ PinNumber: vm.pinnumber, LicenseNumber: vm.licnumber, TaxNumber: vm.taxnumber, UserId: localStorage.userId, CleanHandsType: vm.type }).then(AssociationRenewalData, errorWhileProcessing);
        }

        $scope.$watch('vm.taxnumber', function () {
            if (angular.isDefined(vm.taxnumber)) {
                if (vm.taxnumber.indexOf('-') == 2) {
                    if (vm.taxnumber.lastIndexOf('-') != 2 || vm.taxnumber.length != 10) {
                        vm.associatebblform.taxnumber.$setValidity('improperformat', false);
                        if (vm.taxnumber.length >= 10) {
                            vm.taxnumber = vm.taxnumber.substring(0, 10);
                        }
                    } else {
                        vm.type = 'fein';
                        vm.associatebblform.taxnumber.$setValidity('improperformat', true);
                    }

                } else if (vm.taxnumber.indexOf('-') == 3) {
                    if (vm.taxnumber.lastIndexOf('-') != 6 || vm.taxnumber.length != 11) {
                        vm.associatebblform.taxnumber.$setValidity('improperformat', false);
                        if (vm.taxnumber.length > 11) {
                            vm.taxnumber = vm.taxnumber.substring(0, 11);
                        }
                    } else {
                        vm.type = 'ssn';
                        vm.associatebblform.taxnumber.$setValidity('improperformat', true);
                    }
                } else {
                    vm.associatebblform.taxnumber.$setValidity('improperformat', false);
                    if (vm.taxnumber.length >= 11) {
                        vm.taxnumber = vm.taxnumber.substring(0, 11);
                    }
                }
            } else {
                vm.associatebblform.taxnumber.$setValidity('improperformat', true);
            }
        });

    }
})();