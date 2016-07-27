(function () {

    'use strict';
    var controllerId = 'PageNotfoundController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', PageNotfoundController]);

    function PageNotfoundController($scope, $rootScope, $location) {

        $scope.PageNotfound = function () {
            console.log("Page Not Found");
        }
    }

})();
