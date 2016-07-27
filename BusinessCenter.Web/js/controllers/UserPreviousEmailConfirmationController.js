
//-------------------------------------------------------------------

// $scope           : Scopes provide separation between the model and the view, via a mechanism for watching the model for changes.
// $rootScope       : All other scopes are descendant scopes of the root scope.
// $location        : The $location service parses the URL in the browser address bar (based on the window.location) and makes the URL available to your application.
// requestService   : It is used for making API calls.

//------------------------------------------------------------------

(function () {

    'use strict';
    var controllerId = 'UserPreviousEmailConfirmationController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', UserPreviousEmailConfirmationController]);

    function UserPreviousEmailConfirmationController($scope, $rootScope, $location, requestService) {


        init();
        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load. 
        */
        function init() {
            //-------------------------------------------------------------------

            // Created By       : CodeIT DevTeam
            // Api-Method       : UserAccounts/UserPreviousEmail
            // Last Update date : 26-07-2015
            // Description      : This Method is used for changing the primary email. 
            // Last Modification: if...else condition for not accessing "admin/superadmin" to "login" in user portal.
            //                    when role number is 3 then it will be user .if it is 1 or 2 then we need to rise error.
            //------------------------------------------------------------------
            var response = requestService.UserPreviousEmailConfirmation($location.search());
            
            response.success(function (data) {
                if ((data.status === "alreadyExists") && (data.type === "P")) {
                    $('#email_breadcrum').html('Previous Email Update Status');
                    $("#email_title").html('This link is already used to change your email id.');
                    $("#email_message").html('You have already confirmed changing your email Id from this account. Please check your new email Id to confirm from it, if it is not done.If you want to change to another email Id, you have to do it from your User profile of My DCBC. ');
                    $scope.status = data.status;
                }
                else if ((data.status === "success") && (data.type === "P")) {
                    $('#email_breadcrum').html('Previous Email Update Status');
                    $("#email_title").html('Changing primary email is Confirmed');
                    $("#email_message").html('The change to your primary email is completed, if you have already confirmed it from your new email id as well. If not, please confirm by clicking the link sent to your new Email Id to see the change.');
                    $scope.status = data.status;
                }
                else if ((data.status === "linkExpire") && (data.type === "P")) {
                    $('#email_breadcrum').html('Previous Email Update Status');
                    $("#email_title").html('You link is  Expired.');
                    $("#email_message").html('Sorry!.. Your update new email link expired.Please update your profile with new email then activate again.');
                    $scope.status = data.status;
                }
                else if ((data.status === "success") && (data.type === "N")) {
                    $('#email_breadcrum').html('New Email Update Status');
                    $("#email_title").html('Changing primary email is Confirmed.');
                    $("#email_message").html('The change to your new email address is successfully  completed.');
                    $scope.status = data.status;
                }
                else if ((data.status === "alreadyExists") && (data.type === "N")) {

                    $('#email_breadcrum').html('New Email Update Status');
                    $("#email_title").html('  This link is already used to change your email id.');
                    $("#email_message").html('You have already confirmed changing your email id ');
                    $scope.status = data.status;
                }
                else if ((data.status === "linkExpire") && (data.type === "N")) {
                    $('#email_breadcrum').html('New Email Update Status');
                    $("#email_title").html('You link is  Expired.');
                    $("#email_message").html('Sorry!.. Your update new email link expired.Please update your profile with new email then activate again.');
                    $scope.status = data.status;
                }
            });
            response.error(function (data) {
                console.log(JSON.stringify(data));
            });
        }
       
        $scope.navToLogin = function () {
            if (localStorage.loggedin != undefined && localStorage.loggedin != 0) {
                $location.path('/dashboard');
            } else {
                $location.path('/login');
            }
        }

        $scope.navToVerifyEmail = function () {
            $location.path("/verifyemail");
        }
        
    }

})();