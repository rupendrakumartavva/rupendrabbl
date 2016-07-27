(function () {

    'use strict';

    var controllerId = 'AccountRecoveryController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'authService', AccountRecoveryController]);

    function AccountRecoveryController($scope, $rootScope, $location, authService) {

        init();

        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load. 
        */
        function init() {
            $scope.username = localStorage.username;
        }

        $scope.navToLogin = function () {
            if (authService.authentication.isAuth) {
                $location.path('/dashboard');
            } else {
                $location.path('/login');
            }
        };
        $scope.navToforgotusername = function () {
            $location.path('/forgotusername');
        }
        $scope.navTosecurityquestion = function () {
            $location.path('/securityquestion');
        }
    }
})();

