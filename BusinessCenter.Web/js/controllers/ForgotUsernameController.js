(function () {

    'use strict';
    var controllerId = 'ForgotUsernameController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', 'authService', 'popupFactory', ForgotUsernameController]);

    function ForgotUsernameController($scope, $rootScope, $location, requestService, authService, popupFactory) {

        init();
        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load. 
        */
        function init() {
            $("#dvLoadingSection").css("display", "block");
            $("#dvMainsection").css("display", "none");
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

        $scope.navToRegister = function () {
            $location.path('/register');
        }

        $scope.navToforgotusername = function () {
            $location.path('/forgotusername');
        }

        $scope.navTosecurityquestion = function () {
            $location.path('/securityquestion');
        }

        $scope.setErrorMsg = function (id) {
            $('#' + id).html('');
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : Account/ForgotUserName
        // Last Update date : 28-07-2015
        // Description      : This Method is validating UserMail and sending Mail to the user. 
        // Last Modification: removed unwanted code and displayed the messages according to role counts.
        //                    when role number is 3 then it will be user .if it is 1 or 2 then we need to raise error.
        //IMP Note          : If the role Count is 3 or rolecount=0 and rolecount undefined then user log in. if role count=1 then 
        //                  : it should be admin or superadmin.
        //------------------------------------------------------------------

        $scope.CheckUserEmail = function () {
            var namesReg = /^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$/;
            $('#error_msg').html('');
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            //console.log($scope.forgotusername);
            if ($scope.forgotusername == undefined || $scope.forgotusername.email == '') {
                $('#error_msg').html("Please enter the Email Id associated with your DC Business Center account.");
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            } else if (!namesReg.test($scope.forgotusername.email)) {
                $('#error_msg').html("Please enter a valid email address.");
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            } else {

                var resp = requestService.UserEmailvalidation($scope.forgotusername);
                resp.success(function (data) {
                    if (data.Rcount == "3" || data.Rcount == "0") {
                        if (data.status == "success") {
                            $rootScope.forgotusername = data;
                            localStorage.question1 = data.question1;
                            localStorage.question2 = data.question2;
                            localStorage.question3 = data.question3;
                            localStorage.userID = data.userID;
                            localStorage.fullName = data.FullName;
                            localStorage.userName = data.UserName;
                            $rootScope.fullName = localStorage.fullName;
                            $rootScope.UserName1 = data.UserName;
                            $scope.forgotusername.email = '';
                            popupFactory.showpopup("An e-mail has been sent to your provided e-mail address with your username.", "", { config: { buttons: '1' } });
                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                        } else if (data.status == "Delete") {
                            $location.path('/deletestatus');
                        }
                        else if (data.status == "In-Activate") {
                            $('#error_msg').html("Your account is not yet activated. Please activate it."); $("#dvLoadingSection").css("display", "none");
                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                        }
                        else if (data.status == "Re-Register") {
                            $('#error_msg').html("Your account is expired. Please re-register.");
                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                        }
                        else if (data.status == "Lockout") {
                            $location.path('/lockoutfrgtuser');
                        }
                        else {
                            $('#error_msg').html("The email you entered is not registered in our database. Double check the email and try again.");
                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                        }
                    }
                    else if (data.Rcount == "1") {
                        $('#error_msg').html("Access is denied for the above user into this portal.");
                        $("#dvLoadingSection").css("display", "none");
                        $("#dvMainsection").css("display", "block");
                    }
                    else if (data.Rcount == "4") {
                        $('#error_msg').html("The email you entered is not registered in our database. Double check the email and try again.");
                        $("#dvLoadingSection").css("display", "none");
                        $("#dvMainsection").css("display", "block");
                    }
                });
                resp.error(function (data) {
                    // console.log(JSON.stringify("error"));
                    $('#error_msg').html("Problem in Fetching data.");
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                });
            };
        }
    }

})();