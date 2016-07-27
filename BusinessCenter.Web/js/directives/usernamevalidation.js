angular.module('DCRA').directive('checkUserOnBlur', function () {
    // var USER_REGX = /^(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,250})$/;
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elm, attr, ctrl) {

            if (attr.type === 'radio' || attr.type === 'checkbox') return;
            elm.unbind('input').unbind('keydown').unbind('change');

            elm.bind('blur', function () {

                scope.$apply(dovalidation);
            });
            scope.$on('kickOffValidations', dovalidation)

            function dovalidation() {
                if (elm.val()) {
                    ctrl.$setValidity('username', true);
                } else {
                    ctrl.$setValidity('username', false);
                }
            }
        }
    };

});

