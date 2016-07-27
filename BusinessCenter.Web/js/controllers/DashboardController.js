(function () {

    'use strict';

    var controllerId = 'DashboardController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService','$window', DashboardController]);

    function DashboardController($scope, $rootScope, $location, requestService,$window) {
        $scope.Wish = {};
        init();

        /*
       * Function: init
       * init (initialize) method: first method to be executed on controller load. 
       */

        function init() {
            $("#dvLoadingSection").css("display", "none");
            $scope.userFirstName = localStorage.userFirstName;
            getData();
        }


        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : MySavedResults/mycount
        // Last Update date : 26-07-2015
        // Description      : This Method is user for getting count of the saved data of current user. 
        // Last Modification: 

        //------------------------------------------------------------------

        function getData() {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            var resp = requestService.GetUserServiceCount({ userId: localStorage.userId });

            resp.success(function (data) {
                $scope.result = data;
                getRenewalData();
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
            resp.error(function (data) {
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
        }

        function getRenewalData() {
            var resp = requestService.UserBblExpCount({ UserID: localStorage.userId });
            resp.success(function (data) {
                //console.log(data.Result);
                $scope.list = data.Result;
            })
            resp.error(function (data) {
                console.log(JSON.stringify(data));
            })
        }

        $scope.checkrightClick = function (url, e) {
            e.preventDefault();
            (e.which === 3) || (e.ctrlKey) ? e.target.href = url : e.target.href = 'javascript:void(0)';
        }

        $scope.navToCorp = function () {
            $("#dashboardmodal").modal('show');
        }

        $scope.navigateAnyWay = function () {
            $("#dashboardmodal").modal('hide');
            $window.location.href = "https://corp.dcra.dc.gov/";
        }

        $scope.cancelNavigation = function () {
            $("#dashboardmodal").modal('hide');
        }
    }

})();