'use strict'

angular.module('DCRA').directive('ngUniquepwd121', function (requestService) {
    return {
        restrict: 'AE',
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            elem.on('blur', function (evt) {
                scope.$apply(function () {
                    if (elem.val() != "") {
                        var resp = requestService.ValidatePassword({ Password: elem.val(), userId: localStorage.userId });
                        resp.success(function (data, status, headers, config) {
                            $('#loading').css('display', 'none');
                            if (data.status == "False") {
                                //$('#password_error_msg').html("To change your password, please complete Current Password, New Password and Confirm New Password fields.");
                                ctrl.$setValidity('unique121', true);
                            }
                            else {
                                ctrl.$setValidity('unique121', false);
                            }
                            //window.scrollTo(0, 400);
                        });
                    }
                });
            });
            elem.on('focus', function (evt) {
                scope.$apply(function () {
                    ctrl.$setValidity('unique121', true);
                });
            });
        }
    }
});