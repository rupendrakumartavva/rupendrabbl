(function () {

    'use strict';
    var controllerId = 'EmailConfirmationController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', '$routeParams', 'UtilityFactory', EmailConfirmationController]);

    function EmailConfirmationController($scope, $rootScope, $location, requestService, $routeParams, UtilityFactory) {


        var vm = this;


        //-------------------------------------------------------------------
        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigated to "mybbl" page. 
        //------------------------------------------------------------------
        $scope.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#error_msg').html('');
        }

        // Created By       : CodeIT DevTeam
        // Last Update date : 10-09-2015
        // Description      :  This Method is user for selecting the checkbox.

        //------------------------------------------------------------------
        vm.toggleCheckbox = function (ctlID) {
            var controlID = "#c" + ctlID;
            var controlElement = angular.element(document.querySelector(controlID)).parent();
            if (controlElement.hasClass('checked')) {
                controlElement.removeClass('checked');
            }
            else {
                controlElement.addClass('checked');
            }
        }

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigated to "receipt" page. 

        //------------------------------------------------------------------
        vm.navToReceipt = function () {
            vm.diffbillingaddress = true;
            vm.emailconfirmForm.$invalid ? $('#error_msg').html("Please fill all the required data.") : redirectToReceipt();
        }

        function redirectToReceipt() {

            vm.paymentData = {
                MasterId: UtilityFactory.getMasterId($routeParams.guid),
                Email: vm.emailconfirm,
                EmailConfirmation: vm.signature
            };

            requestService.submitPayment(vm.paymentData).then(function (response) {
                if (response.data) {
                    vm.PaymentStatus = response.data.trasactionresult.Success;
                    localStorage.setItem("PaymentId", response.data.paymentId);
                    if (vm.PaymentStatus == true) {
                        $location.path('/receipt/' + $routeParams.guid);
                    }
                    else {
                        $location.path('/paymentfailure/' + $routeParams.guid);
                    }
                }
            }, function (response) {
                console.log("Error");
            });
        }

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigated to "appchecklist" page. 

        //------------------------------------------------------------------
        vm.navToChecklist = function () {
            $location.path('/appchecklist/' + $routeParams.guid);
        }
    }


})();