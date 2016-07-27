(function () {

    'use strict';
    var controllerId = 'PaymentFailureController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$window', '$location', '$routeParams', 'requestService', 'appConstants', 'UtilityFactory', 'SessionFactory', 'errorFactory', 'authService', PaymentFailureController]);

    function PaymentFailureController($scope, $rootScope, $window, $location, $routeParams, requestService, appConstants, UtilityFactory, SessionFactory, errorFactory, authService) {
        var vm = this;
        vm.renewalBreadcrum = false;
        vm.navigate = false;
        vm.submissionStatusData = {};
        SessionFactory.setSessionAsClear();
        init();


        /*
    * Function: init
    * init (initialize) method: first method to be executed on controller load. 
    */
        function init() {
            authService.refreshToken().then(function () {
                vm.guid = $routeParams.guid;
                submissionStatus().then(function (response) {
                    vm.submissionStatusData = response.data;
                    if ($routeParams.paymenttype == 'renewal') {
                        vm.renewalBreadcrum = true;
                    }
                    else {
                        vm.renewalBreadcrum = false;
                    }

                    if (vm.submissionStatusData.CorporationStatus != 'ACTIVE') {
                        vm.corpFailure = 'true';
                        vm.paymentFailure = 'false';
                    } else {
                        if (vm.submissionStatusData.PaymentStatus == 'TRUE') {
                            vm.paymentFailure = 'false';
                        } else {
                            vm.paymentFailure = 'true';
                        }
                    }
                    vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                })
            }, function () {
                $location.path("/login");
            });
        }

        function submissionStatus() {
            return requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid));
        }

        vm.dontNavigate = function () {
            vm.navigate = false;
            $('#paymentFailure').modal('hide');
        }

        vm.navigateAnyway = function () {
            $('#paymentFailure').modal('hide');
            vm.navigate = true;
            // if (vm.next.split('#')[1] == '/mybbl') {
            // $location.path(vm.next.split('#')[1]);
            // } else {
            $location.path(vm.next.split('#')[1] + '/' + $routeParams.guid);
            // }

        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 10-09-2015
        // Description      : This Method is navigating to "appchecklist" page

        //------------------------------------------------------------------

        vm.navToChecklist = function () {
            vm.navigate = true;
            $location.path('/appchecklist/' + $routeParams.guid);
        }

        vm.navToMyBBL = function () {
            vm.navigate = true;
            $location.path('/mybbl');
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 10-09-2015
        // Description      : This Method is navigating to "appchecklist" page

        //------------------------------------------------------------------

        vm.navToPayment = function () {
            vm.navigate = true;
            $window.history.back();
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                vm.navigationPath = next.split('#')[1].slice(1) + '/' + $routeParams.guid;
                if (current.indexOf('renewal') != -1) {
                    if (!vm.navigate) {
                        vm.next = next;
                        event.preventDefault();
                        $('#paymentFailure .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.navigateaway + "</h3>");
                        $('#paymentFailure').modal('show');
                        return;
                    }
                }
            }
        });
    }
})();