(function () {

    'use strict';
    var controllerId = 'CorpReqRegwithTradeController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', '$routeParams', 'UtilityFactory', 'BBLSubmissionFactory', 'authService', CorpReqRegwithTradeController]);

    function CorpReqRegwithTradeController($scope, $rootScope, $location, requestService, $routeParams, UtilityFactory, BBLSubmissionFactory, authService) {
        var vm = this;
        init();

        /*
         * Function: init
         * init (initialize) method: first method to be executed on controller load.
         */

        function init() {
            authService.refreshToken().then(function () {
                if (UtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid)).then(function (response) {
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        }
                    });
                } else {
                    BBLSubmissionFactory.noGuidAvailable();
                }
            }, function () {
                $location.path("/login");
            });
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "physicallocation/corpreg" page

        //------------------------------------------------------------------

        vm.navToCorpRegistration = function () {
            $location.path('/physicallocation/corpreg/' + $routeParams.guid);
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "appchecklist" page

        //------------------------------------------------------------------

        vm.navToChecklist = function () {
            $location.path('/appchecklist/' + $routeParams.guid);
        }

        vm.navToNewBblWelcome = function () {
            $location.path('/mybbl');
        }
    }
})();