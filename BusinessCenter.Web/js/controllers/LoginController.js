(function () {

    'use strict';

    var controllerId = 'LoginController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', 'authService', 'TimerFactory', 'errorFactory', LoginController]);

    function LoginController($scope, $rootScope, $location, requestService, authService, TimerFactory, errorFactory) {
        $scope.lockout = 0;
        $scope.userData = {};
        init();

        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load. 
        */
        function init() {
            $("#dvLoadingSection").css("display", "block");
            $rootScope.LoginType = '';
            if (authService.authentication.isAuth) {
                var authExpiration = JSON.parse(localStorage.getItem('ls.authorizationData')).expires_fulldate;
                if (moment().unix() < moment(authExpiration).unix()) {
                    $location.path('/dashboard');
                } else {
                    authService.logOut();
                    authService.freeToken();
                }
            } else {
                authService.freeToken();
            }
            $("#dvLoadingSection").css("display", "none");
            $("#dvMainsection").css("display", "block");
        }

        $scope.navToRegister = function () {
            $location.path('/register')
        };


        $scope.navToForgotPassword = function () {
            $location.path('/forgotpassword');
        };

        $scope.navToForgotUsername = function () {
            $location.path('/forgotusername');
        };

        $scope.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#Password').html('');
            $('#name').html('');
        }

        $scope.form_validate = function () {
            $('#error_msg').html('');

            if ($scope.login_form.$invalid) {
                $('#error_msg').html("Please complete all required fields");
                if ($scope.contact_us.$invalid && $scope.contact_us.$dirty) {
                    $('#error_text').html("");
                }
            } else {
                loginDetails();
            }
        };

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : Account/Login
        // Last Update date : 28-07-2015
        // Description      : This Method is validating User Credentials and navigating to the dashboard page.
        // Last Modification: if...else condition for not accessing "admin/superadmin" to "login" in user portal.
        //                    when role number is 3 then it will be user .if it is 1 or 2 then we need to rise error.
        //IMP Note          : If the role Count is 3 or rolecount=0 and rolecount undefined then user log in. if role count=1 then 
        //                  : it should be admin or superadmin.
        //------------------------------------------------------------------


        function loginDetails() {
            $("#dvLoadingSection").css("display", "block");
            $("#dvMainsection").css("display", "none");


            //authService.logOut();
            localStorage.removeItem('ls.authorizationData');
            $scope.userCredentials.useRefreshTokens = true;
            authService.login($scope.userCredentials).then(function (data) {
                //var resp = requestService.UserLogin($scope.userCredentials);
                //resp.success(function (data) {
                authService.authentication.isAuth = false;
                if (data.RoleCount == "3" || data.RoleCount == undefined || data.RoleCount == "0") {

                    if (data.status == "Success") {
                        $rootScope.LoginType = 'Login';
                        $scope.UserName = data.userFullName;
                        $rootScope.username = data.userFullName;
                        localStorage.loggedin = 2;
                        localStorage.userId = data.userID;
                        localStorage.username = data.userFullName;
                        localStorage.userFirstName = data.FirstName;
                        localStorage.userLastName = data.LastName;
                        localStorage.setItem('loggedout', 'false');
                        $scope.status = data.status;
                        authService.authentication.isAuth = true;
                        errorFactory.loadvalidationmessages();
                        TimerFactory.startTimer();
                        (localStorage.path != undefined && localStorage.path != "undefined") ? $location.path(localStorage.path) : $location.path('/dashboard');
                    } else if (data.status == "In-Activate") {
                        $('#error_msg').html("Your account is not yet activated. Please activate it to Login.");
                        $("#dvLoadingSection").css("display", "none");
                        $("#dvMainsection").css("display", "block");
                        $scope.status = data.status;
                    } else if (data.status == "expireuser") {
                        $('#error_msg').html("Your account is not yet activated. Please activate it to Login.");

                        $("#dvLoadingSection").css("display", "none");
                        $("#dvMainsection").css("display", "block");
                        $scope.status = data.status;
                    } else if (data.status == "linkExpire") {
                        $scope.status = data.status;
                        $('#error_msg').html("Your Email validation link has been expired.Please register again.");

                        $("#dvLoadingSection").css("display", "none");
                        $("#dvMainsection").css("display", "block");
                    }
                    else if (data.status == "LockedOut") {
                        $scope.status = data.status;

                        $location.path('/lockout')
                    }
                    else if (data.status == "Failure") {
                        if (data.failCount == "3") {
                            $('#name').html('');
                            $('#Password').html("Either Username or Password are incorrect. You currently have two (2) attempts left before your account will be frozen for five (5) minutes");
                            $scope.status = data.status;

                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                        } else if (data.failCount == "4") {
                            $('#name').html('');
                            $('#Password').html("Either Username or Password are incorrect. You currently have one (1) attempt left before your account will be frozen for five (5) minutes");
                            $scope.status = data.status;

                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                        } else {
                            $('#Password').html('');
                            $('#name').html('');
                            $('#error_msg').html("Either Username or Password Are Incorrect");
                            $scope.status = data.status;

                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                        }
                    } else if (data.status == "No User") {
                        $('#Password').html('');
                        $('#error_msg').html('');
                        $('#name').html("This username is not currently registered. Please double check the username or create a new account by clicking the   &quot; Get Started  &quot; link below");
                        $scope.status = data.status;

                        $("#dvLoadingSection").css("display", "none");
                        $("#dvMainsection").css("display", "block");
                    } else if (data.status == "Delete") {
                        $scope.status = data.status;
                        $location.path('/deletestatus');
                    }
                } else if (data.RoleCount == "4") {
                    $('#Password').html('');
                    $('#error_msg').html('');
                    $('#error_msg').html("This username is not currently registered. Please double check the username or create a new account by clicking the   &quot; Get Started  &quot; link below");
                    $scope.status = data.status;

                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                }
                else if (data.RoleCount == "1") {
                    $('#Password').html('');
                    $('#error_msg').html('');
                    $('#error_msg').html("Access is denied for the above user into this portal.");

                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                }
                //  });
            });
        }
    }
})();