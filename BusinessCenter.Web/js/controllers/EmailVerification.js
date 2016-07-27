(function () {

    'use strict';

    var controllerId = 'EmailVerification';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', 'authService', EmailVerification]);

    function EmailVerification($scope, $rootScope, $location, requestService, authService) {

        init();

        /*
      * Function: init
      * init (initialize) method: first method to be executed on controller load. 
      */
        function init() {

            $scope.loadingdata = "Please wait...";
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");

            //-------------------------------------------------------------------

            // Created By       : CodeIT DevTeam
            // Api-Method       : UserAccounts/ConfirmEmail
            // Last Update date : 26-07-2015
            // Description      : This Method is used for activation of the account by the link which is sent to the email after registration. 
            // Last Modification: 

            //------------------------------------------------------------------
            authService.freeToken().then(function () {
                var response = requestService.EmailConfirmation($location.search());
                response.success(function (data) {
                    if (data.status === "UserActive") {
                        $('#headermessage').html('Account Already Activated');
                        $("#message").html('Email validation is already completed for this user.');
                        $('#registerlink').hide();
                        $('#loginlink').show();
                        $scope.status = data.status;
                    }
                    else if (data.status === "success") {
                        $('#headermessage').html('Account Has Been Activated Successfully');
                        $("#message").html('Thank you for confirming your email. Please');
                        $('#registerlink').hide();
                        $('#loginlink').show();
                        $scope.status = data.status;
                    }
                    else if (data.status === "AccountExpired") {
                        $('#headermessage').html('Expired');
                        $("#message").html('Your Email validation link has been expired.Please register again.');
                        $('#loginlink').hide();
                        $('#registerlink').show();
                        $scope.status = data.status;
                    }
                    else if (data.status === "") {
                        $('#headermessage').html('Account is not registered');
                        $("#message").html('This username is not currently registered. Please double check the username or create a new account.');
                        $('#loginlink').hide();
                        $('#registerlink').show();
                        $scope.status = data.status;
                    }
                    $("#dvMainsection").css("display", "block");
                    $("#dvLoadingSection").css("display", "none");
                });
                response.error(function (data) {
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");

                });
            }, function () {
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            $location.search('code', null);
            $location.search('userId', null);
            $location.search('Password', null);
            $location.search('ConfirmPassword', null);
        });

        $scope.navToLogin = function () {
            if (authService.authentication.isAuth) {
                $location.path('/dashboard');
            } else {
                $location.path('/login');
            }
        }

        $scope.navToRegister = function () {
            $location.path("/register");
        }
    }

})();