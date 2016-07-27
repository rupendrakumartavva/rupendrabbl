'use strict'

angular.module('DCRA').directive('ngUniqueemailp', function (requestService) {

    return {
        restrict: 'AE',
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            elem.on('blur', function (evt) {
                scope.$apply(function () {
                    if (elem.val() != "") {
                        var resp = requestService.checkEmailAvailabilityInProfile({ Email: elem.val(), UserId: localStorage.userId });
                        resp.success(function (data, status, headers, config) {
                            $('#loading').css('display', 'none');
                            if (data.status == "False") {

                                ctrl.$setValidity('unique', false);
                            }
                            else {
                                ctrl.$setValidity('unique', true);
                            }
                        });
                    }
                });
            })
            elem.on('focus', function (evt) {
                scope.$apply(function () {
                    ctrl.$setValidity('unique', true);
                });
            });
        }
    }
});