(function () {

    'use strict';
    var controllerId = 'NewBBLWelcomeController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'SessionFactory', 'authService', NewBBLWelcomeController]);

    function NewBBLWelcomeController($scope, $rootScope, $location, SessionFactory, authService) {

        var vm = this;
        SessionFactory.setSessionAsClear();
        init();
        function init() {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            authService.refreshToken().then(function () {
                $("#dvMainsection").css("display", "block");
                $("#dvLoadingSection").css("display", "none");
            }, function () {
                $location.path("/login");
            });
        }
        $('.panel-body >h1').each(function (ndx, ele) {
            if (ele.offsetWidth < ele.scrollWidth) {
                $(ele).css('font-size', Math.ceil($(ele).width() / 10) + 5);
            }
        });
        localStorage.removeItem('preAppQuestionsData');
        localStorage.removeItem("subcategoryFor");
        $rootScope.preAppQuestions = {};

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "login" page.
        // Last Modified    : 

        //------------------------------------------------------------------

        vm.navToLogin = function () {
            // $location.path("/login");
            if (authService.authentication.isAuth) {
                $location.path('/dashboard');
            } else {
                $location.path('/login');
            }

        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "mybbl" page.
        // Last Modified    : 

        //------------------------------------------------------------------

        vm.navToMyBBL = function () {
            $location.path('/mybbl');
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "/preappquestions/step1" page.
        // Last Modified    : 

        //------------------------------------------------------------------

        vm.navToPreAppQues = function () {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            $location.path("/preappquestions/step1");
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "mybbl" page.
        // Last Modified    : 

        //------------------------------------------------------------------

        vm.navToExit = function () {
            $location.path('/mybbl');
        }
    }

})();