'use strict'

angular.module('DCRA').directive('ngUserunique', function (requestService, authService) {
    return {
        restrict: 'AE',
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            elem.on('blur', function (evt) {
                scope.$apply(function () {
                    if (elem.val() != "") {
                       // authService.freeToken();
                        var resp = requestService.checkUserNameAvailabilty({ UserName: elem.val() })
                        resp.success(function (data, status, headers, config) {
                            $('#loading').css('display', 'none');
                            if (data.status == "False")
                            {
                                ctrl.$setValidity('userunique', false);
                            }
                            else
                            {
                                ctrl.$setValidity('userunique', true);
                            }
                        });
                    }
                });
            })
            elem.on('focus', function (evt) {
                scope.$apply(function () {
                    ctrl.$setValidity('userunique', true);
                });
            });
        }
    }
});