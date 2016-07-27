(function () {

    'use strict';

    var controllerId = 'RegisterController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', 'vcRecaptchaService', 'appConstants', 'authService', 'popupFactory', RegisterController]);

    function RegisterController($scope, $rootScope, $location, requestService, vcRecaptchaService, appConstants, authService, popupFactory) {

        var error_messages = {
            "subject": "Please provide an answer to all security questions",
            "subject2": "Please provide an answer to all security questions",
            "subject3": "Please provide an answer to all security questions",
            "securityquestion1": "Please select a security question",
            "securityquestion2": "Please select a security question",
            "securityquestion3": "Please select a security question"
        };
        $scope.response = null;
        $scope.widgetId = null;
        $scope.responseValue = null;
        $scope.model = {
            key: '6LfgBAYTAAAAAObIh2cyLjvW5v5U34sjoBjqafwJ'
        };

        init();

        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load.
        */
        function init() {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");

            authService.freeToken().then(function () {
                var resp = requestService.GetQuestions();
                resp.success(function (data) {
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                    $scope.Questions = data;
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


        $scope.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#error_msg').html('');
        }

        $scope.setResponse = function (response) {
            $('#recaptcha').html('');
            $scope.responseValue = response;
        };

        $scope.setWidgetId = function (widgetId) {
            $scope.widgetId = widgetId;
        };

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is validating all fields and to create user. 
        // Last Modification: 

        //------------------------------------------------------------------

        $scope.reg_form_validate = function (register) {
            $('#error_msg').html('');
            $('#recaptcha').html('');
            if ($scope.registration_form.$invalid) {
                $('#error_msg').html("Please complete all required fields");
                window.scrollTo(0, 0);
                var validations = $scope.registration_form.$error.required;
                for (var i = 0; i < validations.length; i++) {
                    $('#' + validations[i].$name).html(error_messages[validations[i].$name]);
                }
                if ($scope.registration_form.$error[recaptcha] != false && $scope.responseValue == null) {
                    $('#recaptcha').html("ReCaptcha has not been completed");
                }
            } else {
                $("#dvLoadingSection").css("display", "none");
                createUser();
            }
        };

        function createUser() {
            $scope.register.Title = "MR.";
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            var resp = requestService.validateRecaptcha({ responseValue: $scope.responseValue });
            resp.success(function (data) {
                if (data.status == "success") {
                    var responseval = requestService.createUser($scope.register);
                    responseval.success(function (data) {
                        if (data.status == "success") {
                            localStorage.usermailinfo = JSON.stringify({ EmailAddress: data.mailid, UserId: data.userId, Code: data.code, FullName: data.FullName });
                            localStorage.EmailAddress = data.mailid;
                            localStorage.FullName = data.FullName;
                            $scope.status = data.status;
                            //console.log(JSON.stringify(localStorage.usermailinfo));
                            $("#dvLoadingSection").css("display", "block");
                            $("#dvMainsection").css("display", "none");
                            $location.path("/verifyemail");
                        }
                        else if (data.status == "User Already Exits") {
                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                        }
                    });
                    responseval.error(function (data) {
                        console.log(JSON.stringify(data));
                        $("#dvLoadingSection").css("display", "none");
                        $("#dvMainsection").css("display", "block");
                    });
                }
            });
            resp.error(function (data) {
                console.log("Error");
                $scope.status = "error";
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
        }

        $scope.navToVerifyEmail = function () {
            $location.path("/verifyemail");
        }

        $scope.navToTerms = function () {
            $location.path("/termsofservices");
        }

        $scope.ifQuestionSelected = function (id) {
            var i, ctrl = document.getElementById(id);
            for (i = 1; i <= 3; i++) {
                if (ctrl.id !== "dropdown" + i) {
                    if ($("#dropdown" + i).val() === ctrl.value) {
                        $scope.register["SecurityQuestion" + id.slice(-1)] = undefined;
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