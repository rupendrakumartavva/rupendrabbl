(function () {

    'use strict';
    var controllerId = 'VerifyBusinessBeforeAssociationController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', '$routeParams', 'RenewalUtilityFactory', 'authService',VerifyBusinessBeforeAssociationController]);

    function VerifyBusinessBeforeAssociationController($scope, $rootScope, $location, requestService, $routeParams, RenewalUtilityFactory, authService) {
        var vm = this;

        init();

        /*
       * Function: init
       * init (initialize) method: first method to be executed on controller load. 
       */

        function init() {
            authService.refreshToken().then(function () {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            getBusinessInfoToVerify().then(function (response) {
                vm.business = response.data;
            }, onFetchingDataError);
            //localStorage.setItem("ExpDate", vm.businessinfo[0].expDate);
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            }, function () {
                $location.path("/login");
            });
        }

        function onFetchingDataError() {
            console.log("Error");
        }

        vm.navToBBL = function () {
            $location.path('/mybbl');
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "mybbl" page.
        // Last Modified    : 

        //------------------------------------------------------------------

        vm.navToConfirmAssociation = function () {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            confirmAssociation().then(function (response) {
                if (response.data.Result) {
                    $location.path('/mybbl');
                }
                else {
                    $('#dvLoadingSection').css('display', 'none');
                    $("#dvMainsection").css("display", "block");
                    console.log("Error");
                }
            });
        }

        function confirmAssociation() {
            return requestService.AssociateBblService({
                SubmissionLicense: vm.business.SubmissionLicenseNumber,
                UserID: localStorage.userId,                
                LicenseExpirationDate: vm.business.expDate,
                DCBC_ENTITY_ID: vm.business.entityId,
                B1_ALT_ID: vm.business.B1_Alt_Id,
                Status: true
            });
        }
        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "associatebbl" page.
        // Last Modified    : 

        //------------------------------------------------------------------

        vm.cancel = function () {
            $location.path('/associatebbl');
        }

        function getBusinessInfoToVerify() {
            return requestService.UserServiceDetailsOnId(RenewalUtilityFactory.getRenewalObject($routeParams.guid));
        }

    }

}());