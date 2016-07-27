'use strict';
angular.module('maskedinput', []).directive('mask', function () {
    return {
        restrict: 'A',
        link: function (scope, el, attrs) {
            if (attrs.mask)
                el.mask(attrs.mask, { placeholder: attrs.maskPlaceholder });
            (el).on('keypress', function () {
                scope.$eval(attrs.ngModel + "='" + el.val() + "'");
            });
        }
    };
});