(function () {

    'use strict';
    var controllerId = 'DeleteAccountController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', DeleteAccountController]);

    function DeleteAccountController($scope, $rootScope, $location) {

        $scope.navToRegister = function () {
            $location.path("/register");
        }
    }
})();
