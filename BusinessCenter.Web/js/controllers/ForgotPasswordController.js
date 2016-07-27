(function () {

    'use strict';

    var controllerId = 'ForgotPasswordController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', 'authService', 'popupFactory', ForgotPasswordController]);

    function ForgotPasswordController($scope, $rootScope, $location, requestService, authService, popupFactory) {

        var error_messages = {
            "subject1": "Please provide an answer to all security questions",
            "subject2": "Please provide an answer to all security questions",
            "subject3": "Please provide an answer to all security questions"
        };

        init();

        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load. 
        */
        function init() {
            authService.freeToken().then(function () {
                $scope.ValidateUser = "Email";
                $("#dvMainsection").css("display", "none");
                $("#dvLoadingSection").css("display", "block");
                $("#SequrityQuestion_block").css("display", "none");
                var resp = requestService.GetQuestions();
                $("#mainFPassword").css("display", "block");
                resp.success(function (data) {
                    $scope.Questions = data;
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                });
                resp.error(function (data) {
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                });
            }, function () {
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
        }

        $scope.checkrightClick = function (url, e) {
            e.preventDefault();
            (e.which === 3) ? e.target.href = url : e.target.href = 'javascript:void(0)';
        }

        $scope.navToQuickSearchResult = function () {
            $location.path('/quicksearchresult');
        };

        $scope.navToRegister = function () {
            $location.path('/register');
        }

        $scope.navToforgotusername = function () {
            $location.path('/forgotusername');
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is validating Username and is navigated to the security questions page or sending email whether user is selected radio button of security question (or) email.
        // Last Modification: if...else condition for not accessing "admin/superadmin" to retrieve "Password" in user portal.

        //------------------------------------------------------------------  

        $scope.navToforgotpassword = function () {
            $("#headingPassword1").css("display", "none");
            $("#headingPassword").css("display", "block");
            $("#divUserQuestion").css("display", "none");
            $("#login_block").css("display", "block");
            $("#classrow1").css("display", "none");
            $("#title1").css("display", "none");
            if ($scope.forgotpass != undefined)
                $scope.forgotpass.username = '';
            $("#pagetitle").css("display", "block");
            $("#classrow").css("display", "block");
            $("#classrow1").css("display", "block");
            $("#getStartLink").css("display", "block");
            $("#SequrityQuestion_block").css("display", "none");
            $("#divForgotpasswordBymail").css("display", "block");
            $("#divbutton1").css("display", "block");
            $('#licenseQYes').addClass('checked');
            $('#licenseQNo').removeClass('checked');
            $('#RadioEmailValue').val('0');
            $('#RadioSecuValue').val('1');
            $location.path('/forgotpassword');
        }

        $scope.setPwdErrorMsg = function (id) {
            $('#' + id).html('');
            $('#usernameValid').html('');
            $('#checkAnswers').html('');
        }

        $scope.forgotpassword_form_validation = function () {
            $('#error_msg').html('');

            if ($scope.forgotpassword_form.$invalid) {
                $('#username').html("Username field is required");
            } else {
                $("#dvMainsection").css("display", "none");
                $("#dvLoadingSection").css("display", "block");
                $scope.navToValidateUser();
            }
        };

        $scope.navToValidateUser = function () {

            //-------------------------------------------------------------------

            // Created By       : CodeIT DevTeam
            // Api-Method       : Account/ForgotValidation
            // Last Update date : 26-07-2015
            // Description      : This Method is validating Username and sending link to the user Email for changing the password.
            // Last Modification:

            //------------------------------------------------------------------  

            if ($('#RadioEmailValue').val() == '0') {
                localStorage.forgotpass = $scope.forgotpass.username;
                $scope.ValidateUser = "Email";
                var resp123 = requestService.ForgotPassword($scope.forgotpass);
            }
            else {
                //-------------------------------------------------------------------

                // Created By       : CodeIT DevTeam
                // Api-Method       : Account/NewForgotPassword
                // Last Update date : 26-07-2015
                // Description      : This Method is validating Username and navigated to the security questions page.
                // Last Modification: if...else condition for not accessing "admin/superadmin" to "login" in user portal.
                //                    when role number is 3 then it will be user .if it is 1 or 2 then we need to rise error.
                //------------------------------------------------------------------   
                $scope.ValidateUser = "SQ";
                localStorage.forgotpass = $scope.forgotpass.username;
                var resp123 = requestService.NewForgotPassword($scope.forgotpass);
            }

            resp123.success(function (data) {

                if (data.Rcount == "3") {
                    if (data.status == "success") {
                        localStorage.question1 = data.question1;
                        $scope.SecurityQuestion1 = data.question1;
                        $rootScope.mailid = data.userMail;
                        if ($('#RadioEmailValue').val() == '0') {
                            $location.path('/forgotpasswordstatus');
                        }
                        else {
                            $("#SequrityQuestion_block").css("display", "block");
                            $("#divForgotpasswordBymail").css("display", "none");
                            $("#headingPassword1").css("display", "block");
                            $("#headingPassword").css("display", "none");
                            $location.path('/forgotpassword');
                            $("#divUserQuestion").css("display", "block");
                            $("#divbutton2").css("display", "block");
                            $('.login_wrapper').hide();
                            $("#divbutton1").css("display", "none");
                            $("#getStartLink").css("display", "none");
                            $('.login_wrapper').hide();
                            $('#title1').css("display", "block");
                            $('#pagetitle').css("display", "none");
                            $('#classrow').hide();
                            $('#classrow1').hide();
                            $('.success_msg').hide();
                            $('.error_msg').hide();
                            $('#forgotclass').hide();
                        }
                    } else if (data.status == "Delete") {
                        $("#divUserQuestion").css("display", "none");
                        $("#divbutton2").css("display", "none");
                        $("#divbutton1").css("display", "block");
                        $("#getStartLink").css("display", "block");
                        $location.path('/deletestatus');
                    } else if (data.status == "In-Activate") {
                        $("#divUserQuestion").css("display", "none");
                        $("#divbutton2").css("display", "none");
                        $("#divbutton1").css("display", "block");
                        $("#getStartLink").css("display", "block");
                        $('#usernameValid ').html("Your account is not yet activated. Please activate it.");
                    } else if (data.status == "Re-Register") {
                        $("#divUserQuestion").css("display", "none");
                        $("#divbutton2").css("display", "none");
                        $("#divbutton1").css("display", "block");
                        $("#getStartLink").css("display", "block");
                        $('#usernameValid ').html("Your account is expired. Please re-register.");
                    } else if (data.status == "Lockout") {
                        $("#divUserQuestion").css("display", "none");
                        $("#divbutton2").css("display", "none");
                        $("#divbutton1").css("display", "block");
                        $("#getStartLink").css("display", "block");
                        if ($scope.ValidateUser == 'Email') {
                            $location.path('/lockoutfrgtpwd');
                        } else {
                            $location.path('/lockoutfrgtpwd');
                        }
                    }
                    else if (data.status == "NullInput") {
                        $("#divUserQuestion").css("display", "none");
                        $("#divbutton2").css("display", "none");
                        $("#divbutton1").css("display", "block");
                        $("#getStartLink").css("display", "block");
                    }
                    else {
                        $("#divUserQuestion").css("display", "none");
                        $("#divbutton2").css("display", "none");
                        $("#divbutton1").css("display", "block");
                        $("#getStartLink").css("display", "block");
                        $('#usernameValid ').html("The username you entered is not registered in our database. Double check the username and try again.");
                    }
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                }
                else if (data.Rcount == "4") {
                    $("#divUserQuestion").css("display", "none");
                    $("#divbutton2").css("display", "none");
                    $("#divbutton1").css("display", "block");
                    $("#getStartLink").css("display", "block");
                    $('#usernameValid ').html("The username you entered is not registered in our database. Double check the username and try again.");
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                } else if (data.Rcount == "0") {
                    $("#divUserQuestion").css("display", "none");
                    $("#divbutton2").css("display", "none");
                    $("#divbutton1").css("display", "block");
                    $("#getStartLink").css("display", "block");
                    $('#usernameValid ').html("Access is denied for the above user into this portal.");
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                }
            });

            resp123.error(function (data) {
                $("#divUserQuestion").css("display", "none");
                //console.log(JSON.stringify(data));
                $('pwderror_msg').hide();
                $('#usernameValid ').html("The username you entered is not registered in our database. Double check the username and try again.");
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
        }


        $scope.setErrorMsg = function (id) {
            $('#' + id).html('');
        }

        $scope.security_form_validation = function () {
            $('#error_msg').html('');

            if ($scope.security_form.$invalid) {
                $('#error_msg').html('Please answer the above Security Question');

            } else {
                $('#error_msg').html('');
                $('#usernameValid').html('');
                $('.error_text').hide();
                $("#dvMainsection").css("display", "none");
                $("#dvLoadingSection").css("display", "block");
                $scope.forgotPassword();
            }
        };

        //-------------------------------------------------------------------/

        // Created By       : CodeIT DevTeam
        // Api-Method       : Account/ForgotValidation
        // Last Update date : 26-07-2015
        // Description      : This Method is validating SecurityQuestions of User then Redircet to New Password Page.
        // Last Modification: Team is Add New Type for SecurityQuestions of Type:s.

        //-------------------------------------------------------------------

        $scope.forgotPassword = function () {

            $scope.forgotpass.SecurityQuestion1 = localStorage.question1;

            var resp = requestService.ForgotPassword($scope.forgotpass);
            resp.success(function (data) {
                if (data.status == "success") {
                    $rootScope.mailid = data.userMail;
                    localStorage.forgotpass = $scope.forgotpass.username;
                    $("#dvLoadingSection").css("display", "none");
                    $location.path('/forgotpasswordstatus');
                }
                else if (data.status == "movenewpassword") {
                    $("#dvMainsection").css("display", "none");
                    $location.path('/getnewpassword').search({ 'userId': data.UserId, 'code': data.code, 'type': 'S' });
                }
                else if (data.status == "Delete") {
                    $location.path('/deletestatus');
                }
                else if (data.status == "In-Active") {
                    $('#checkAnswers').html("Your account is not yet activated. Please activate it.");
                }
                else if (data.status == "Re-Register") {
                    $('#checkAnswers').html("Your account is expired. Please re-register.");
                }
                else if (data.status == "Lockout") {
                    $("#dvLoadingSection").css("display", "block");
                    $("#dvMainsection").css("display", "none");
                    $location.path('/lockoutfrgtpwd');
                }
                else if (data.status == "invalidans") {
                    $("#dvMainsection").css("display", "block");
                    $("#dvLoadingSection").css("display", "none");
                    $("#divUserQuestion").css("display", "block");
                    $("#divbutton2").css("display", "block");
                    $("#divbutton1").css("display", "none");
                    $("#getStartLink").css("display", "none");
                    $('#checkAnswers').css("display", "block");
                    $('#checkAnswers').html("The following security answer(s) are incorrect");
                    if (data.failCount == 3) {
                        $('#checkAnswers').html("Security answer is incorrect. You currently have two (2) attempts left before your account will be frozen for five (5) minutes");
                        $scope.status = data.status;
                    } else if (data.failCount == 4) {
                        $('#checkAnswers').html("Security answer is incorrect. You currently have one (1) attempt left before your account will be frozen for five (5) minutes");
                        $scope.status = data.status;
                    }
                    else if (data.failCount == 0) {
                        $("#dvLoadingSection").css("display", "block");
                        $("#dvMainsection").css("display", "none");
                        $location.path('/lockoutSQ');
                    }
                    else {
                        $('#checkAnswers').html("The following security answer is incorrect.");
                        $scope.status = data.status;
                    }
                }
                else {
                    $('#checkAnswers').html("The username you entered is not registered in our database. Double check the username and try again.");
                }
            });
            resp.error(function (data) {
                //console.log(JSON.stringify(data));
                $('#checkAnswers').html("The username you entered is not registered in our database. Double check the username and try again.");
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
        }

        //-------------------------------------------------------------------/

        // Created By       : CodeIT DevTeam
        // Api-Method       : Account/ForgotValidation
        // Last Update date : 26-07-2015
        // Description      : This Method is used to resend the email for the user.
        // Last Modification: 

        //-------------------------------------------------------------------

        $scope.resendPassword = function () {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            var resp1 = requestService.ForgotPassword({ UserName: localStorage.forgotpass });
            resp1.success(function (data) {
                if (data.status == "success") {
                    $('#password_title').html('Resent Email');
                    $('#password_status').html('Resent Email for a new password');
                    $('#resend_Email_text').html('');
                    $('#hide_or').hide();
                    $('#resend_Email_link').hide();
                }
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
            resp1.error(function (data) {
                
                popupFactory.showpopup("Problem in sending Email.", "", { config: { buttons: '1' } });
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for checking the which radio button is needed to be checked.  
        // Last Modification: 

        //------------------------------------------------------------------

        $scope.licenseQSpanClick = function (var_id_checked, var_id, radio_id, radio_id_unselect) {

            var spanClassChecked = $('#' + var_id_checked).attr('class');
            var spanClass = $('#' + var_id).attr('class');

            if (spanClassChecked == '') {
                $('#' + var_id_checked).addClass('checked');
                $('#' + var_id).removeClass('checked');
                $("#" + radio_id).val('0');
                $("#" + radio_id_unselect).val('1');
            }
            else {
                $('#' + var_id_checked).removeClass('checked');
                $('#' + var_id).addClass('checked');
                $("#" + radio_id).val('1');
                $("#" + radio_id_unselect).val('0');
            }
        };
    }

})();