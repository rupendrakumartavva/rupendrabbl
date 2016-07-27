angular.module('DCRA').directive('validPasswordC', function () {
    return {
        require: 'ngModel',
        scope: {

            reference: '=validPasswordC'

        },
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$setValidity('noMatch', true);
            $('#hideerror').show();
            ctrl.$parsers.unshift(function (viewValue, $scope) {
                if (elm.val() != '') {
                    if ((elm.val() != null) && (viewValue === undefined)) {
                        var noMatch = viewValue != scope.reference
                        ctrl.$setValidity('noMatch', !noMatch)
                        //console.log(ctrl.$setValidity('noMatch', !noMatch))
                    } else {

                        scope.$watch("reference", function (value) {
                            if (ctrl.$viewValue == '') {
                                ctrl.$setValidity('noMatch', true);
                            } else
                                ctrl.$setValidity('noMatch', value === ctrl.$viewValue);

                        });
                    }
                }
                else {
                    ctrl.$setValidity('noMatch', true);
                }

            });
        }
    }
});