'use strict';
(function () {

    

    var controllerId = 'AboutUsController';
    angular.module('DCRA').controller(controllerId,['$scope', '$rootScope','$location', AboutUsController]);

    function AboutUsController($scope, $rootScope, $location) {
        //$scope.slides = [];

        
        $scope.navToHome = function () {
            $location.path('/home');
        };
    }
})();



