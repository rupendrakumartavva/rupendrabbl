(function () {

    'use strict';
    var controllerId = 'SecurityQuestionController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', 'authService', 'popupFactory', SecurityQuestionController]);

    function SecurityQuestionController($scope, $rootScope, $location, requestService, authService, popupFactory) {
        //$scope.slides = [];

        init();

        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load. 
        */
        function init() {
            authService.freeToken().then(function () {
                var resp = requestService.GetQuestions();
                $("#mainFPassword").css("display", "block");
                resp.success(function (data) {
                    $('#loading').css('display', 'none');
                    $scope.Questions = data;
                });

                resp.error(function (data) {
                    console.log(JSON.stringify(data));
                });
            }, function () {

            });
        }

        $scope.navToQuickSearchResult = function () {
            $location.path('/quicksearchresult')
        };

        $scope.navToRegister = function () {
            $location.path('/register');
        }

        $scope.navToLogin = function () {
            if (authService.authentication.isAuth) {
                $location.path('/dashboard');
            } else {
                $location.path('/login');
            }
        }

        $scope.navToforgotpassword = function () {
            $("#headingPassword1").css("display", "none");
            $("#headingPassword").css("display", "block");
            $("#divUserQuestion").css("display", "none");
            $("#login_block").css("display", "block");
            $("#classrow1").css("display", "none");
            $("#title1").css("display", "none");
            $scope.forgotpass.username = '';
            $("#pagetitle").css("display", "block");
            $("#classrow").css("display", "block");
            $("#classrow1").css("display", "block");
            $("#RadioEmailValue").prop("checked", true);
            $("#RadioSecuValue").prop("checked", false);
            $location.path('/forgotpassword');

        }

        //navToValidateUser

        $scope.navToValidateUser = function () {
            $scope.loadingdata = "Please wait...";
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            if ($scope.forgotpass == undefined || $scope.forgotpass.username == undefined || $scope.forgotpass.username == '') {
                popupFactory.showpopup("UserName field cannot be blank.", "", { config: { buttons: '1' } });
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            }

            else {
                if ($('input[name="group1"]:checked', '#contact_us').val() == '0') {

                    localStorage.forgotpass = $scope.forgotpass.username;

                    var resp123 = requestService.ForgotPassword($scope.forgotpass);
                }
                else {
                    localStorage.forgotpass = $scope.forgotpass.username;
                    var resp123 = requestService.NewForgotPassword($scope.forgotpass);
                }
                resp123.success(function (data) {
                    $('#loading').css('display', 'none');
                    if (data.status == "success") {

                        localStorage.question1 = data.question1;
                        localStorage.question2 = data.question2;
                        localStorage.question3 = data.question3;
                        $scope.SecurityQuestion1 = data.question1;
                        $scope.SecurityQuestion2 = data.question2;
                        $scope.SecurityQuestion3 = data.question3;
                        $rootScope.mailid = data.userMail;
                        if ($('input[name="group1"]:checked', '#contact_us').val() == '0') {

                            $location.path('/forgotpasswordstatus');
                        }
                        else {

                            $("#headingPassword1").css("display", "block");

                            $("#headingPassword").css("display", "none");




                            $location.path('/securityquestion');
                            $("#divUserQuestion").css("display", "block");
                            $("#divbutton2").css("display", "block");
                            $('.login_wrapper').hide();
                            $("#divbutton1").css("display", "none");
                            $('.login_wrapper').hide();
                            $('#title1').css("display", "block");
                            $('#pagetitle').css("display", "none");
                            $('#classrow').hide();
                            $('#classrow1').hide();
                            $('.success_msg').hide();
                            $('#forgotclass').hide();
                        }
                    } else if (data.status == "Delete") {
                        $("#divUserQuestion").css("display", "none");
                        $("#divbutton2").css("display", "none");
                        $("#divbutton1").css("display", "block");
                        popupFactory.showpopup("Your account has been deleted. Please try to contact administrator.", "", { config: { buttons: '1' } });
                    } else if (data.status == "In-Activate") {
                        $("#divUserQuestion").css("display", "none");
                        $("#divbutton2").css("display", "none");
                        $("#divbutton1").css("display", "block");
                        popupFactory.showpopup("Your account is not yet activated. Please activate it.", "", { config: { buttons: '1' } });
                    } else if (data.status == "Re-Register") {
                        $("#divUserQuestion").css("display", "none");
                        $("#divbutton2").css("display", "none");
                        $("#divbutton1").css("display", "block");
                        popupFactory.showpopup("Your account is expired. Please re-register.", "", { config: { buttons: '1' } });
                    } else if (data.status == "Lockout") {
                        $("#divUserQuestion").css("display", "none");
                        $("#divbutton2").css("display", "none");
                        $("#divbutton1").css("display", "block");
                        $location.path('/lockout');
                    }
                    else if (data.status == "NullInput") {
                        $("#divUserQuestion").css("display", "none");
                        $("#divbutton2").css("display", "none");
                        $("#divbutton1").css("display", "block");
                    }
                    else {
                        $("#divUserQuestion").css("display", "none");
                        $("#divbutton2").css("display", "none");
                        $("#divbutton1").css("display", "block");
                        popupFactory.showpopup("The username you entered is not registered in our database. Double check the username and try again.", "", { config: { buttons: '1' } });
                    }
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                });
                resp123.error(function (data) {
                    $("#divUserQuestion").css("display", "none");
                    popupFactory.showpopup("The username you entered is not registered in our database. Double check the username and try again.", "", { config: { buttons: '1' } });
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                });
            }
        }


        $scope.forgotPassword = function () {

            if (($scope.forgotpass == undefined) || ($scope.forgotpass.securityanswer1 == undefined) || ($scope.forgotpass.securityanswer1 == '')
                || (($scope.forgotpass.securityanswer2 == undefined) || ($scope.forgotpass.securityanswer2 == ''))
                || (($scope.forgotpass.securityanswer3 == undefined) || ($scope.forgotpass.securityanswer3 == ''))) {
                popupFactory.showpopup("Please answer all security questions.", "", { config: { buttons: '1' } });
            }
            else {

                $scope.forgotpass.SecurityQuestion1 = localStorage.question1;
                $scope.forgotpass.SecurityQuestion2 = localStorage.question3;
                $scope.forgotpass.SecurityQuestion3 = localStorage.question3;
                var resp = requestService.ForgotPassword($scope.forgotpass);

                resp.success(function (data) {
                    $('#loading').css('display', 'none');
                    if (data.status == "success") {
                        $rootScope.mailid = data.userMail;
                        localStorage.forgotpass = $scope.forgotpass.username;
                        $location.path('/forgotpasswordstatus');
                    }
                    else if (data.status == "movenewpassword") {
                        $location.path('/getnewpassword').search({ 'userId': data.UserId, 'code': data.code });
                    }
                    else if (data.status == "Delete") {
                        popupFactory.showpopup("Your account is in delete state.Contact administrator.", "", { config: { buttons: '1' } });
                    }
                    else if (data.status == "In-Active") {
                        popupFactory.showpopup("Your account is not yet activated. Please activate it.", "", { config: { buttons: '1' } });
                    }
                    else if (data.status == "Re-Register") {
                        popupFactory.showpopup("Your account is expired. Please re-register.", "", { config: { buttons: '1' } });
                    }
                    else if (data.status == "Lockout") {
                        $location.path('/lockout');
                    }
                    else if (data.status == "invalidans") {
                        $("#divUserQuestion").css("display", "block");
                        $("#divbutton2").css("display", "block");
                        $("#divbutton1").css("display", "none");
                        if (data.failCount == 3) {
                            popupFactory.showpopup("Security answers are incorrect. You currently have two (2) attempts left before youraccount will be frozen for five (5) minutes.", "", { config: { buttons: '1' } });
                            $scope.status = data.status;
                        } else if (data.failCount == 4) {
                            popupFactory.showpopup("Security answers are incorrect. You currently have one (1) attempts left before youraccount will be frozen for five (5) minutes.", "", { config: { buttons: '1' } });
                            $scope.status = data.status;
                        }
                        else if (data.failCount == 0) {
                            $location.path('/lockout');
                        }
                        else {
                            popupFactory.showpopup("The following security answer(s) are incorrect.", "", { config: { buttons: '1' } });
                            $scope.status = data.status;
                        }
                    }
                    else {
                        popupFactory.showpopup("The username you entered is not registered in our database. Double check the username and try again.", "", { config: { buttons: '1' } });
                    }
                });
                resp.error(function (data) {
                    popupFactory.showpopup("The username you entered is not registered in our database. Double check the username and try again.", "", { config: { buttons: '1' } });
                });
            }
        }

        $scope.resendPassword = function () {



            var resp1 = requestService.ForgotPassword({ UserName: localStorage.forgotpass });
            resp1.success(function (data) {
                $('#loading').css('display', 'none');
                if (data.status == "success") {
                    $('#password_title').html('Resent Email');
                    $('#password_status').html('Resent Email for a new password');
                    // $('#resend_Email_text').html('Still can\'t find the email?');
                    $('#hide_or').hide();
                    $('#resend_Email_link').hide();
                }
            });
            resp1.error(function (data) {
                console.log(JSON.stringify(data));
                popupFactory.showpopup("Problem in sending Email.", "", { config: { buttons: '1' } });
            });
        }
    }

})();