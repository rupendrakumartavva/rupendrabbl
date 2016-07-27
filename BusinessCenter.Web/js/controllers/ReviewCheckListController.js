(function () {

    'use strict';
    var controllerId = 'ReviewCheckListController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', '$routeParams', 'UtilityFactory', 'authService', ReviewCheckListController]);

    function ReviewCheckListController($scope, $rootScope, $location, requestService, $routeParams, UtilityFactory, authService) {
        var vm = this;
        init();

        /*
         * Function: init
         * init (initialize) method: first method to be executed on controller load.
         */

        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                servicechecklist().then(function (response) {
                    vm.reviewcheckList = response.data;
                    for (var i = 0; i < vm.reviewcheckList.SubQuestion.length; i++) {
                        if (vm.reviewcheckList.SubQuestion[i].Type == "RadioButton") {
                            if (vm.reviewcheckList.SubQuestion[i].Answer == "YES" || vm.reviewcheckList.SubQuestion[i].Answer == "NO") {
                                vm.reviewcheckList.SubQuestion[i].rightlabel = "NO"
                                vm.reviewcheckList.SubQuestion[i].leftlabel = "YES"
                            } else if (vm.reviewcheckList.SubQuestion[i].Answer.indexOf('(') != -1) {
                                vm.reviewcheckList.SubQuestion[i].rightlabel = "4 year"
                                vm.reviewcheckList.SubQuestion[i].leftlabel = "2 year"
                            } else {
                                vm.reviewcheckList.SubQuestion[i].leftlabel = "FEIN"
                                vm.reviewcheckList.SubQuestion[i].rightlabel = "SSN"
                            }
                        }
                    }
                    $('#dvLoadingSection').css('display', 'none');
                    $("#dvMainsection").css("display", "block");
                }, errorWhileProcessing);
            }, function () {
                $location.path("/login");
            });
        }

        function errorWhileProcessing(response) {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error Occured:");
        }

        function servicechecklist() {
            return requestService.ServiceCheckList({ MasterId: UtilityFactory.getMasterId($routeParams.guid) });
        }

        vm.navToChecklist = function () {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            $location.path('/appchecklist/' + $routeParams.guid);
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "mybbl" page.
        // Last Modified    : 

        //------------------------------------------------------------------

        vm.navToMyBBL = function () {
            localStorage.removeItem('preAppQuestionsData');
            $location.path("/mybbl");

        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "newbblwelcome" page.
        // Last Modified    : 

        //------------------------------------------------------------------

        vm.navToApply = function () {
            $location.path("/newbblwelcome");
        }


        $('.panel-body >h1').each(function (ndx, ele) {
            if (ele.offsetWidth < ele.scrollWidth) {
                $(ele).css('font-size', Math.ceil($(ele).width() / 10) + 5);
            }
        });

        vm.expandCollapse = function (event) {
            if ($(event.target).hasClass('see-details')) {
                $(event.target).addClass('hide-details').removeClass('see-details');
                $(event.target).parent().parent().css('border-bottom', '0px solid');
                $(event.target).parent().parent().parent().find('.details').show();
            }
            else {
                $(event.target).addClass('see-details').removeClass('hide-details');
                $(event.target).parent().parent().parent().find('.details').hide();
            }
        }
    }
})();