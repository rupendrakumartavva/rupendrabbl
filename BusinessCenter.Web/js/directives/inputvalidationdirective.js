'use strict'

angular.module('DCRA').directive('ngUniqueInput', function () {
    return {
        restrict: 'AE',
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            elem.on('blur', function (evt) {
                scope.$apply(function () {
                    if (elem.val() != "") {
                        ctrl.$setValidity('uniqueCompare', false);
                    }
                });
            })
            elem.on('focus', function (evt) {
                scope.$apply(function () {
                    ctrl.$setValidity('uniqueCompare', true);
                });
            });
        }
    }
});