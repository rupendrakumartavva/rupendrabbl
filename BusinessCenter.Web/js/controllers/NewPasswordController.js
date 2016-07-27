(function () {

    'use strict';
    var controllerId = "NewPasswordController";
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', 'authService', 'popupFactory', NewPasswordController]);
    function NewPasswordController($scope, $rootScope, $location, requestService, authService, popupFactory) {
        //$scope.slides = [];
        if (localStorage.getItem('ls.authorizationData') == null) {
            authService.freeToken().then(function () {
                init();
            });
        } else {
            init();
        }



        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load. 
        */
        function init() {
            $("#dvLoadingSection").css("display", "block");
            $("#dvMainsection").css("display", "none");
            var reg = $location.search();
            reg.code = unescape(reg.code);
            reg.Password = "Dcra@123";
            reg.ConfirmPassword = "Dcra@123";
            reg.SelectedType = unescape(reg.type);
            var resp = requestService.PasswordCheck(reg);

            resp.success(function (data) {
                if (data.status == "success") {
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                    $("#dvTitle").css("display", "block");
                }
                else if (data.status == "nrfp") {
                    $location.path("/passwordrecheck");
                    $("#dvLoadingSection").css("display", "none");
                }
                else if (data.status == "Lockout") {
                    $location.path("/lockout");
                    $("#dvLoadingSection").css("display", "none");
                }
                else if (data.status == "linkExpire") {
                    $location.path("/passwordexpiry");
                    $("#dvLoadingSection").css("display", "none");
                }
            });
        }

        $scope.setErrorMsg = function (id) {
            $('#' + id).html('');
        }

        //-------------------------------------------------------------------/

        // Created By       : CodeIT DevTeam
        // Api-Method       : Account/ConfirmForgotPassword
        // Last Update date : 26-07-2015
        // Description      : This Method is used to update New Password for user.
        // Comments         : Team is Add New Type for SecurityQuestions of Type:s and for Email link No Type is added.Based on Type update
        //                    column field is update . if Type:s  isforgot is not update.others it will update                    

        //-------------------------------------------------------------------

        $scope.newpassword = function () {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            $('#error_msg').html('');

            if ($scope.contact_us.$invalid) {
                $('#error_text').html("Please fill  all required password fields");
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
                $("#dvTitle").css("display", "block");
                if ($scope.register.ConfirmPassword == undefined && $scope.register.Password != undefined) {
                    $('#error_text').html("Password and Confirm Password does not match!");
                }

            } else {
                var reg = $location.search();
                reg.userId = unescape(reg.userId);
                reg.code = unescape(reg.code);
                reg.Password = $scope.register.Password;
                reg.ConfirmPassword = $scope.register.ConfirmPassword;
                reg.SelectedType = unescape(reg.type);
                requestService.checkPreviousPasswords(reg).then(function (response) {
                    if (response.data == "true") {
                        $('#error_text').html("New password cannot be same as your last (3) passwords.");
                        $("#dvLoadingSection").css("display", "none");
                        $("#dvMainsection").css("display", "block");
                    }

                    else {
                        var resp = requestService.NewPassword(reg);
                        resp.success(function (data) {

                            if (data.status == "success") {
                                $("#dvLoadingSection").css("display", "none");
                                $location.path('/resetsuccessful');

                            }
                            else if (data.status == "nrfp") {
                                $location.path("/passwordrecheck");
                                $("#dvLoadingSection").css("display", "none");
                            }
                            else {
                                popupFactory.showpopup("Problem in password reset.", "", { config: { buttons: '1' } });
                            }
                        });
                        resp.error(function () {
                            console.log("error");
                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                            $("#dvTitle").css("display", "block");
                        });
                    }
                }, function (response) {
                    console.log("error");
                });
            }
        }

        $scope.navToRegister = function () {
            $location.path('/register')
        };

        $scope.navToForgotPassword = function () {
            $location.path('/forgotpassword')
        };

        $scope.$on('$locationChangeStart', function (event, next, current) {
            $location.search('code', null);
            $location.search('userId', null);
            $location.search('Password', null);
            $location.search('ConfirmPassword', null);
        });

    }
})();