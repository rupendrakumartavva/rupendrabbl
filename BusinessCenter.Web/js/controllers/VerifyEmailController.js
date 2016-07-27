(function () {
    'use strict';
    var controllerId = 'VerifyEmailController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', 'authService', VerifyEmailController]);

    function VerifyEmailController($scope, $rootScope, $location, requestService, authService) {

        init();

        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load. 
        */
        function init() {
            authService.freeToken().then(function () {
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
                $('[data-toggle="tooltip"]').tooltip();
            }, function () {
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
        }

        $scope.navToQuickSearchResult = function () {
            $location.path('/quicksearchresult')
        };

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : UserAccounts/ResendMail
        // Last Update date : 26-07-2015
        // Description      : This Method is used to resend the email for confirming the registered user.
        // Last Modification: 

        //-------------------------------------------------------------------

        $scope.resendEmail = function () {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");

            var resp = requestService.resendEmail(JSON.parse(localStorage.usermailinfo));
            resp.success(function (data) {
                $('#loading').css('display', 'none');
                $('#email_title').html('Resent Email');
                $('#email_breadcrum').html('Resent Email');
                $('#email_heading').html('Resent Verification Mail to your registered account');
                $('#email_message').html('Please check your email and follow the link to finish creating your DC Business Center account. Once you verify your email address, you will be able to create your profile and save records to your account. The validation link in your email will expire after 24 hours.');
                $('#resend_email').html(' Still can\'t find the email?');
                $('#hide_or').hide();
                $('#resend_Email_link').hide();
                $scope.status = data.status;
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
            resp.error(function (data) {
                console.log(JSON.stringify("error"));
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
        }

        $scope.navToHelp = function () {
            $location.path('/help');
        };

        $scope.navToRegister = function () {
            $location.path('/register');
        };

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : UserAccounts/ResendMail
        // Last Update date : 26-07-2015
        // Description      : This Method is used to resend the email for confirming the registered user.
        // Last Modification: 

        //-------------------------------------------------------------------

        $scope.navToverifyModal = function () {
            var resp = requestService.resendEmail(JSON.parse(localStorage.usermailinfo));
            resp.success(function (data) {
                $('#loading').css('display', 'none');
                $('#verificationModal').modal('show');
            });
        }
    }

})();