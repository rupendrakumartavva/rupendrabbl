(function () {
    'use strict';

    var controllerId = 'ProfileController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', '$timeout', 'authService', 'popupFactory', ProfileController]);

    function ProfileController($scope, $rootScope, $location, requestService, $timeout, authService, popupFactory) {

        var profileObject = '';
        $scope.sendRequest = false;
        var currentEmail = '';
        var currentpassword = 0, newpassword = 0, confirmpassword = 0;
        $scope.currentPassword = '';

        init();
        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load. 
        */
        function init() {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            authService.refreshToken().then(function () {
                $scope.loadingdata = "Please wait...";

                //-------------------------------------------------------------------

                // Created By       : CodeIT DevTeam
                // Api-Method       : UserAccounts/Questions
                // Last Update date : 26-07-2015
                // Description      : This Method is used to get the security questions from database. 
                // Last Modification: 

                //------------------------------------------------------------------

                var resp = requestService.GetQuestions();
                resp.success(function (data) {
                    $scope.Questions = data;
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                });

                resp.error(function (data) {
                    console.log("Error");
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                });

                //-------------------------------------------------------------------

                // Created By       : CodeIT DevTeam
                // Api-Method       : UserAccounts/UserDetails
                // Last Update date : 26-07-2015
                // Description      : This Method is used to get the user details. 
                // Last Modification: 

                //------------------------------------------------------------------

                var response = requestService.UserDetails({ UserId: localStorage.userId });
                response.success(function (data) {
                    $scope.oldEmail = data.email;
                    $scope.profile = data.userDetails.Result;
                    $scope.passwordcopy = data.userDetails.Result.Password;
                    $scope.Result = data.Result;
                    profileObject = angular.copy($scope.profile);
                    currentEmail = $scope.profile.Email;
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                });
            }, function () {
                $location.path("/login");
            });
        }

        $scope.navToRegister = function () {
            $location.path('/register');
        };

        $scope.navToLogin = function () {

            if (!authService.authentication.isAuth) {
                $location.path('/login');
            } else {
                $location.path('/dashboard');
            }
        };

        $scope.navToDashboard = function () {
            $location.path('/dashboard');
        };

        $scope.replaceemail = function () {
            $scope.currentPassword = '';
            $('#validatepassword').modal('hide');
            $('#validationerror').html('');
            $scope.profile.Email = currentEmail;
        }
        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used to delete the account 
        // Last Modification: 

        //------------------------------------------------------------------
        $scope.deleteUser = function () {
            $scope.loadingdata = "Please wait...";
            var userInput = '';
            $('#deleteaccount_perm').hide();
            $('#delete_message').show();
            $('#deleteDialog .modal-body').html("<h3 class='success'>Are you sure you want to delete your profile?</h3>");
            $('#deleteDialog .modal-body').append("<div class='row text-center'><div class='col-md-4 col-md-offset-4' ><input type='text' class='success' id='confirmtext' name='confirmtext' style='margin-top:10px'></div><div class='col-md-4 col-md-offset-4' id='hintMsg'>Please enter yes/no</div></div>");
            $('#deleteDialog').modal('show');
            $('#delete_message').on('click', function (e) {
                userInput = $('#confirmtext').val();
                if (userInput.toLowerCase() == "yes") {
                    $('#confirmtext').val('');
                    userInput = '';
                    $('#deleteDialog .modal-body').html("<h3 class='success'>Do you understand that you will lose all of your information and not be able to recover it?</h3>");
                    $('#deleteDialog .modal-body').append("<div class='row text-center'><div class='col-md-4 col-md-offset-4'><input type='text' class='success' id='confirmtext' name='confirmtext' style='margin-top:10px'></div><div class='col-md-4 col-md-offset-4' id='error_msg'>Please enter yes/no</div></div>");
                    $('#deleteaccount_perm').show();
                    $('#delete_message').hide();
                } else if (userInput.toLowerCase() == "no") {
                    $("#dvMainsection").css("display", "block");
                    $("#dvLoadingSection").css("display", "none");
                    $('#deleteDialog').modal('hide');
                } else {
                    $('#confirmtext').val('');
                    $('#hintMsg').html('Invalid text.Please enter yes/no.');
                    $('#hintMsg').css('color', 'red');
                    $('#hintMsg').addClass('col-md-6 col-md-offset-3').removeClass('col-md-4 col-md-offset-4');
                    $('#deleteDialog').modal('show');
                }
            });

            $scope.closemodal = function () {
                $("#dvMainsection").css("display", "block");
                $("#dvLoadingSection").css("display", "none");
            }
            $('#deleteaccount_perm').click(function (e) {
                $("#dvMainsection").css("display", "none");
                $("#dvLoadingSection").css("display", "block");
                userInput = $('#confirmtext').val();

                //-------------------------------------------------------------------

                // Created By       : CodeIT DevTeam
                // Api-Method       : UserAccounts/delete
                // Last Update date : 26-07-2015
                // Description      : This Method is used to delete the account after confirming the two pop-ups. 
                // Last Modification: 

                //------------------------------------------------------------------

                if (userInput.toLowerCase() == "yes") {
                    var resp = requestService.deleteUser({ UserId: localStorage.userId, DeleteComment: "yes" });
                    resp.success(function (data) {
                        if (data.status == "success") {
                            $("#dvMainsection").css("display", "none");
                            $("#dvLoadingSection").css("display", "block");
                            $('#deleteDialog').modal('hide');
                            localStorage.clear();
                            localStorage.loggedin = 0;
                            localStorage.username = null;
                            $timeout(function () {
                                $("#dvMainsection").css("display", "none");
                                $("#dvLoadingSection").css("display", "block");
                                $location.path('/deleteaccount');
                            }, 350);
                        }
                        $("#dvLoadingSection").css("display", "block");
                    });
                    resp.error(function (data) {
                        popupFactory.showpopup("Problem in deleting please try again.", "", { config: { buttons: '1' } });
                        $("#dvLoadingSection").css("display", "none");
                        $("#dvMainsection").css("display", "block");
                    });
                } else if (userInput.toLowerCase() == "no") {
                    $("#dvMainsection").css("display", "block");
                    $("#dvLoadingSection").css("display", "none");
                    $('#deleteDialog').modal('hide');
                } else {
                    $('#confirmtext').val('');
                    $("#dvMainsection").css("display", "block");
                    $("#dvLoadingSection").css("display", "none");
                    $('#error_msg').html('Invalid text.Please enter yes/no.');
                    $('#error_msg').css('color', 'red');
                    $('#error_msg').addClass('col-md-6 col-md-offset-3').removeClass('col-md-4 col-md-offset-4');
                    $('#deleteDialog').modal('show');
                }
            });
        }

        $scope.getText = function () {
        }

        $scope.setErrorMsg = function () {
            $('#password_error_msg').html('');
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : UserAccounts/ValidatePassword
        // Last Update date : 26-07-2015
        // Description      : This Method is validating User old Password for changing the password.  
        // Last Modification: 

        //------------------------------------------------------------------

        $scope.validatePassword = function () {

            if ($scope.currentPassword != '') {
                var resp = requestService.ValidatePassword({ Password: $scope.currentPassword, userId: localStorage.userId });
                resp.success(function (data) {
                    if (data.status == "True") {
                        $('#validationerror').html('Incorrect Password.Please try again');
                    } else if (data.status == "False") {
                        $('#validatepassword').modal('hide');
                        updateUserProfile();
                        $scope.currentPassword = '';
                        $('#validationerror').html('');
                    }
                });
                resp.error(function (data) {
                    console.log("error");
                });
            }
            else {
                $('#validationerror').html('Please enter your password.');
            }
        }

        $scope.cancel_validation = function () {
            $scope.currentPassword = '';
            $('#validatepassword').modal('hide');
            $('#validationerror').html('');
            $scope.profile.Email = currentEmail;
        }

        $scope.clearErrorMsg = function () {
            $('#emailerror').html('');

        }

        $scope.clearPasswordMsg = function () {
            $('#password_error_msg').html('');
        }

        $scope.clearConfirm = function () {
            $('#confPassword').html('');
            //$('#password_error_msg').html('');
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for updating the profile  
        // Last Modification: 

        //------------------------------------------------------------------

        $scope.updateProfile = function () {
            currentpassword = $scope.profile.Password == undefined ? 0 : $scope.profile.Password;
            newpassword = $scope.profile.NewPassword == undefined ? 0 : $scope.profile.NewPassword;
            confirmpassword = $scope.profile.ConfirmPassword == undefined ? 0 : $scope.profile.ConfirmPassword;
            $('#emailerror').html('');
            // $scope.sendRequest = true;
            $('#answer_msg').html('');
            $('#password_error_msg').html('');
            if ($scope.update_form.$invalid) {
                if ($scope.update_form.subject.$error.required || $scope.update_form.subject2.$error.required || $scope.update_form.subject3.$error.required) {
                    $('#answer_msg').html('Please answer all security questions');
                }
            } else {
                if (currentpassword == 0 && newpassword == 0 && confirmpassword == 0) {
                    $scope.sendRequest = true;
                } else {
                    if (currentpassword != 0 && newpassword != 0 && confirmpassword != 0) {
                        if (currentpassword == '' && newpassword == '' && confirmpassword == '') {
                            $scope.sendRequest = true;
                        } else {
                            $scope.sendRequest = false;
                            // $('#password_error_msg').html("To change your password, please complete Current Password, New Password and Confirm New Password fields.");
                            if (newpassword != confirmpassword) {
                                $('#confPassword').html('New password doesn\'t match');
                                $('#cnpassword').blur();

                            } else {
                                $scope.sendRequest = true;
                            }
                            if (angular.equals(currentpassword, newpassword)) {
                                requestService.checkPreviousPasswords({ userId: localStorage.userId, Password: newpassword }).then(function (response) {
                                    if (response.data == "true") {
                                        $scope.sendRequest = false;
                                        $('#password_error_msg').html("Current password and New password are same. Please enter new one.");
                                        // $scope.profile.Password = undefined;
                                        $scope.profile.NewPassword = undefined;
                                        $scope.profile.ConfirmPassword = undefined;
                                    }
                                }, function (response) {
                                    console.log("error");
                                });
                            }
                            window.scrollTo(0, 400);
                        }
                    } else {
                        $scope.sendRequest = false;
                        $('#password_error_msg').html("To change your password, please complete Current Password, New Password and Confirm New Password fields.");
                        $scope.profile.ConfirmPassword = undefined;
                        window.scrollTo(0, 400);
                    }
                }

                if ($scope.sendRequest == true) {

                    if (!angular.equals(JSON.stringify(profileObject), JSON.stringify($scope.profile))) {

                        if (!angular.equals(currentEmail, $scope.profile.Email)) {
                            //old functionality  var validateEmail = requestService.checkEmailAvailability({ Email: $scope.profile.Email });
                            //changed by rupendra(17th july 2015)
                            //check for email with user id while change primary email id changing
                            var validateEmail = requestService.checkEmailAvailabilityInProfile({ Email: $scope.profile.Email, UserId: localStorage.userId });
                            validateEmail.success(function (data1) {

                                if (data1.status == "False") {
                                    $('#emailerror').html('This email is already in our system. Please select another email address.');
                                    $scope.update_form.$setValidity('unique', true);
                                    $('#validatepassword').modal('hide');
                                } else {
                                    $("#validatepassword").modal('show');
                                }
                            });
                        } else
                            updateUserProfile();

                    } else {
                        popupFactory.showpopup("You have not made any changes to your profile. If needed, please do changes and then click on [SAVE & UPDATE].", "", { config: { buttons: '1' } });
                    }
                }
            }
        }
        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : UserAccounts/ProfileUpdate
        // Last Update date : 26-07-2015
        // Description      : This Method is used for updating the password field.  
        // Last Modification: 

        //------------------------------------------------------------------
        function updateUserProfile() {
            $('#password_error_msg').html('');
            $scope.profile.UserId = localStorage.userId;
            $scope.loadingdata = "Please wait...";
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            // $scope.profile.Email = currentEmail;
            var updateresponse = requestService.UpdateUserProfile($scope.profile);

            updateresponse.success(function (data) {
                profileObject = angular.copy($scope.profile);
                $scope.sendRequest = false;
                if (data.status == "success" && data.logout == "logout") {
                    $scope.profile.Password = '';
                    localStorage.userFirstName = data.FirstName;
                    localStorage.userLastName = data.LastName;
                    $rootScope.userFirstName = localStorage.userFirstName;
                    $rootScope.userLastName = localStorage.userLastName;
                    popupFactory.showpopup("Password is changed successfully. Please use your New Password while logging in next time.", "", { config: { buttons: '1' } });
                    $scope.profile.ConfirmPassword = '';
                    $scope.profile.NewPassword = '';
                    localStorage.username = $scope.profile.FirstName + " " + $scope.profile.LastName;
                } else if (data.status == "success" && data.Email == "EmailChange") {
                    localStorage.userLastName = data.LastName;
                    $rootScope.userFirstName = localStorage.userFirstName;
                    $rootScope.userLastName = localStorage.userLastName;
                    $scope.profile.Email = currentEmail;
                    $scope.currentPassword = '';
                    $scope.sendRequest = false;
                    localStorage.username = $scope.profile.FirstName + " " + $scope.profile.LastName;

                    profileObject.Email = currentEmail;
                    currentEmail = $scope.profile.Email;
                    popupFactory.showpopup("Email is changed successfully.Please confirm  by clicking the link sent to the new email address to see the change.", "", { config: { buttons: '1' } });

                } else if (data.status == "success" && data.logout == undefined) {
                    localStorage.userFirstName = data.FirstName;
                    localStorage.userLastName = data.LastName;
                    $rootScope.userFirstName = localStorage.userFirstName;
                    $rootScope.userLastName = localStorage.userLastName;
                    popupFactory.showpopup("Your profile is updated.", "", { config: { buttons: '1' } });
                    localStorage.username = $scope.profile.FirstName + " " + $scope.profile.LastName;
                    $('#oldPassword').html('');
                    $('#oldPassword').hide();
                } else if (data.status == "InvalidOldPassword" && data.logout == undefined) {
                    window.scrollTo(0, 400);
                    $("#pwd").focus();
                    $("#pwd").val('');
                    $scope.profile.Password = '';
                    $('#oldPassword').html("<h3 class='success_message' style='color:red;'>Current password is incorrect</h3>");
                    $('#password_error_msg').css('display', 'block');
                    $('#password_error_msg').html("To change your password, please complete Current Password, New Password and Confirm New Password fields.");
                } else if (data.status == "User Email already exists") {
                    window.scrollTo(0, 300);
                    popupFactory.showpopup("This email is already in our system. Please select another email address.", "", { config: { buttons: '1' } });
                }
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");

            });
            updateresponse.error(function (data) {
                popupFactory.showpopup("Problem in updating your profile.", "", { config: { buttons: '1' } });
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            })
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for validating existing password with new password.  
        // Last Modification: 

        //------------------------------------------------------------------

        $scope.checkExisting = function (key) {
            $('#password_error_msg').css('display', 'block');
            if ($scope.profile.Password == undefined || $scope.profile.Password == '') {
                $('#password_error_msg').html("To change your password, please complete Current Password, New Password and Confirm New Password fields.");
                window.scrollTo(0, 400);
                $scope.profile.NewPassword = undefined;
            } else {
                if (key == 'newpassword') {
                    if (angular.equals($scope.profile.Password, $scope.profile.NewPassword)) {
                        $('#password_error_msg').html("Current password and New password are same. Please enter new one.");
                        window.scrollTo(0, 400);
                        $scope.profile.NewPassword = undefined;
                        $scope.profile.ConfirmPassword = undefined;
                    } else {
                        requestService.checkPreviousPasswords({ userId: localStorage.userId, Password: $scope.profile.NewPassword }).then(function (response) {
                            if (response.data == "true") {
                                $('#password_error_msg').html("New password cannot be same as your last (3) passwords.");
                                window.scrollTo(0, 400);
                                $scope.profile.NewPassword = undefined;
                                $scope.profile.ConfirmPassword = undefined;
                            }

                        }, function (response) {
                            console.log("error");
                        });
                    }
                }
            }
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : Account/Logout
        // Last Update date : 26-07-2015
        // Description      : This Method is used for logging out of the account and redirected to the login page.  
        // Last Modification: 

        //------------------------------------------------------------------

        $scope.menuClick = function () {
            if (logoutcall()) {
                localStorage.loggedin = 0;
                $location.path('/login');
            }
            else
                console.log("Error");
        };
        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam   
        // Last Update date : 31-07-2015
        // Description      : This Method is used for when changing old email to new email, password authentication pop up will be displayed.
        //                    when we press esc button old email should be displayed.
        // Last Modification: 

        //------------------------------------------------------------------
        $scope.esc = function (e) {
            if (e.keyCode == 27) {
                $scope.profile.Email = currentEmail;
            }

        }

        function logoutcall() {
            $scope.loadingdata = "Please wait...";
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            var response = requestService.UserLogout();
            var res = response.success(function (data) {

                if (data.status == "True") {
                    $scope.profile = "";
                    localStorage.clear();
                    res = true;
                }
                else {
                    popupFactory.showpopup("Problem in logging out.", "", { config: { buttons: '1' } });
                }
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
            response.error(function (data) {
                res = false;
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
            return res;
        }

        $scope.cnfmerror = function () {
            $('#password_error_msg').html('');
            if ($scope.profile.NewPassword != $scope.profile.ConfirmPassword) {
                $('#confPassword').html('New password doesn\'t match');
                //$scope.profile.ConfirmPassword = undefined;
            }
        }

        $scope.ifQuestionSelected = function (id) {
            var i, ctrl = document.getElementById(id);
            for (i = 1; i <= 3; i++) {
                if (ctrl.id !== "dropdown" + i) {
                    if ($("#dropdown" + i).val() === ctrl.value) {
                        $scope.profile["SecurityQuestion" + id.slice(-1)] = undefined;
                        popupFactory.showpopup("Please select another security question as it is already selected.", "", { config: { buttons: '1' } });
                        i = 1;
                        ctrl.selectedIndex = 0;
                        return false;
                    }
                }
            }
        }

    }


})();