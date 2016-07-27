(function () {

    'use strict';
    var controllerId = 'eHOPEligibilityController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', 'appConstants', '$routeParams', 'UtilityFactory', 'BBLSubmissionFactory', 'errorFactory', 'SessionFactory', 'popupFactory', 'authService','$window', eHOPEligibilityController]);

    function eHOPEligibilityController($scope, $rootScope, $location, requestService, appConstants, $routeParams, UtilityFactory, BBLSubmissionFactory, errorFactory, SessionFactory, popupFactory, authService, $window) {
        var vm = this;
        vm.ehopeligibility = {};
        vm.successMsg = false;
        vm.isValidateConformiedEhop = false;
        vm.searchZoningClicked = true;
        vm.attestation = false;
        vm.navigatePath = '';
        vm.confirmEhop_Eligibility = false;
        vm.bindData = false;
        vm.currentpage_errors = {};
        SessionFactory.setSessionAsDirty();
        init();

        /*
         * Function: init
         * init (initialize) method: first method to be executed on controller load.
         */

        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                if (UtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid)).then(function (response) {
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        } else {
                            vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                            vm.ehopeligibility.type = '';
                            insertOrBindData().then(function (response) {
                                vm.categories = response.data;
                                vm.togglepinvalmsg = false;
                                if (response.data[0].getChcekedItem == true) {
                                    vm.confirmEhop_Eligibility = true;
                                    vm.checked = true;
                                    vm.EditDisable = true;
                                    vm.isValidateConformiedEhop = true;
                                    vm.ehopeligibility.type = response.data[0].typeId;
                                    SessionFactory.setSessionAsClear();
                                    vm.successMsg = true;
                                } else {
                                    vm.EditDisable = false;
                                    //vm.ehopeligibility.type = null;
                                }
                                $('#dvLoadingSection').css('display', 'none');
                                $("#dvMainsection").css("display", "block");
                            }, ErrorWhileProcessing);
                        }
                    });
                } else {
                    SessionFactory.setSessionAsClear();
                    BBLSubmissionFactory.noGuidAvailable();
                }
            }, function () {
                $location.path("/login");
            });
        }

        function insertOrBindData() {
            return requestService.GetMasterEhop({ MasterId: UtilityFactory.getMasterId($routeParams.guid), EhopIds: 0 });
        }

        function ErrorWhileProcessing(response) {
            console.log("Error");
        };

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "ehopehomeaddress" page

        //------------------------------------------------------------------

        vm.navToeHOPBusinessAddress = function () {
            var elementsChecked = angular.element(document.querySelectorAll('.checked'));
            if ((parseInt(elementsChecked.length)) > vm.categories.length) {
                vm.togglepinvalmsg = false;
                var list = [];
                for (var i = 0; i < elementsChecked.length; i++) {
                    list.push(elementsChecked[i].id);
                }
                var result = list.toString();
                vm.isValidateConformiedEhop = true;
                if (vm.isValidateConformiedEhop && elementsChecked.length == 13 && vm.attestation == false && vm.confirmEhop_Eligibility) {
                    $('#dvLoadingSection').css('display', 'block');
                    $("#dvMainsection").css("display", "none");
                    requestService.Ehopeligible({ MasterId: UtilityFactory.getMasterId($routeParams.guid), EhopIds: result, TypeId: vm.ehopeligibility.type, UserId: localStorage.userId }).then(function (response) {
                        confirmEhopEligibility().then(function () {
                            SessionFactory.setSessionAsClear();
                            $location.path('/ehopehomeaddress/' + $routeParams.guid);
                        });
                    }, ErrorWhileProcessing);
                } else {
                    $('#error_msg').html(vm.currentpage_errors.NextButtonIncompleteData).focus();
                }
            } else {
                $('#error_msg').html(vm.currentpage_errors.NextButtonIncompleteData).focus();
            }

        }

        vm.toggleCheckbox = function (ctlID) {

            $('#error_msg').html('');
            var controlID = "#c" + ctlID;
            var controlElement = angular.element(document.querySelector(controlID)).parent();
            vm.successMsg = false
            vm.togglepinvalmsg = false;
            vm.attestation = false;
            vm.bindData = false;
            if (controlElement.hasClass('checked')) {
                controlElement.removeClass('checked');
                vm.confirmEhop_Eligibility = false;
                vm.attestationchecked = false;
                angular.element(document.querySelector("#cattestation")).parent().removeClass('checked');
            }
            else {
                vm.confirmEhop_Eligibility = false;
                vm.attestationchecked = false;
                angular.element(document.querySelector("#cattestation")).parent().removeClass('checked');
                controlElement.addClass('checked');
            }
        }

        vm.confirmEhop = function () {

            $('#error_msg').html('');
            if (angular.element(document.querySelector('#cattestation')).parent().hasClass('checked')) {
                vm.attestation = false;
                vm.togglepinvalmsg = false;
            } else {
                vm.attestation = true;
                vm.togglepinvalmsg = false;
                return;
            }

            var elementsChecked = angular.element(document.querySelectorAll('.checked'));

            if ((parseInt(elementsChecked.length)) > vm.categories.length) {
                vm.successMsg = true;
                vm.togglepinvalmsg = false;
                vm.confirmEhop_Eligibility = true;

            } else {
                vm.isValidateConformiedEhop = false;
                vm.successMsg = false;
                vm.bindData = true;
                vm.togglepinvalmsg = true;
            }
        }

        function confirmEhopEligibility() {
            return requestService.SubmitHop({ MasterId: UtilityFactory.getMasterId($routeParams.guid) });
        }

        vm.ehopoption = function () {
            vm.bindData = false;
            vm.isValidateConformiedEhop = false;
            if (vm.ehopeligibility.type == 6) {
                vm.successMsg = false
                vm.attestation = false;
                vm.togglepinvalmsg = true;
                $("#error_msg").html("Given your response, you are ineligible for an Expedited Home Occupancy Permit.  Please select [Return to Checklist] and visit the DC Office of Zoning at 1100 4th St., SW or call 202-442-4400 to obtain an HOP.");

            }
            else if (vm.ehopeligibility.type == '') {
                vm.successMsg = false
                vm.attestation = false;
                vm.togglepinvalmsg = false;
            }
            else {
                if (angular.element(document.querySelector('#cattestation')).parent().hasClass('checked')) {
                    angular.element(document.querySelector('#cattestation')).parent().removeClass('checked');
                    vm.attestationchecked = undefined;
                    vm.submitted = false;
                }
                $("#error_msg").html('');
                insertOrBindData().then(function (response) {
                    vm.categories = response.data;
                    vm.successMsg = false
                    vm.togglepinvalmsg = false;
                    vm.isValidateConformiedEhop = true;
                    vm.attestation = false;
                }, ErrorWhileProcessing);

            }
        }

        vm.navToBack = function () {
            $window.history.back();
        }
        vm.checkAndExit = function (path) {

            if (path.indexOf('app') != -1) {
                vm.navigatePath = path + '/' + $routeParams.guid;
            } else {
                vm.navigatePath = path
            }

            $('#error_msg').html('');
            var elementsChecked = angular.element(document.querySelectorAll('.checked'));
            if ((vm.ehopeligibility.type == '' || vm.ehopeligibility.type == 6 || vm.togglepinvalmsg)) {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                requestService.updatenoneEhop({ MasterId: UtilityFactory.getMasterId($routeParams.guid) }).then(function (response) {
                    if (response.data == 'true') {
                        SessionFactory.setSessionAsClear();
                        $location.path('/' + vm.navigatePath);
                    }
                }, ErrorWhileProcessing);
            }
            else {
                if (vm.bindData == true) {
                    confirmEhopEligibility();
                }
                else if (vm.isValidateConformiedEhop == false || elementsChecked.length != 13 || vm.attestation) {
                    SessionFactory.setSessionAsDirty();
                    if (angular.element(document.getElementById('cattestation')).parent().hasClass('checked')) {
                        popupFactory.showpopup(vm.currentpage_errors.ehopSelectionErrorMsg, vm.navigatePath);
                    } else {
                        popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigatePath);
                    }
                }
                else {

                    if (vm.confirmEhop_Eligibility) {
                        $('#dvLoadingSection').css('display', 'block');
                        $("#dvMainsection").css("display", "none");
                        var list = [];
                        for (var i = 0; i < elementsChecked.length; i++) {
                            list.push(elementsChecked[i].id);
                        }
                        var result = list.toString();

                        requestService.Ehopeligible({ MasterId: UtilityFactory.getMasterId($routeParams.guid), EhopIds: result, TypeId: vm.ehopeligibility.type, UserId: localStorage.userId }).then(function (response) {
                            SessionFactory.setSessionAsClear();
                            $location.path('/' + vm.navigatePath);
                        }, ErrorWhileProcessing);
                    } else {
                        SessionFactory.setSessionAsDirty();
                        popupFactory.showpopup(vm.currentpage_errors.ehopSelectionErrorMsg, vm.navigatePath);
                    }
                }
            }
        }

        function IsFormDirty() {
            if (SessionFactory.isSessionDirty()) {
                var elementsChecked = angular.element(document.querySelectorAll('.checked'));
                if ((elementsChecked.length <= 13 && vm.ehopeligibility.type != '' && vm.ehopeligibility.type != 6) || (vm.successMsg == true)) {
                    SessionFactory.setSessionAsDirty();
                } else {
                    SessionFactory.setSessionAsClear();
                }
            }
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                vm.navigatePath = next.split('#')[1].slice(1);
                IsFormDirty();
                if (SessionFactory.isSessionDirty()) {
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.navigateaway, vm.navigatePath);
                    return;
                }
            }

        });
    }
})();