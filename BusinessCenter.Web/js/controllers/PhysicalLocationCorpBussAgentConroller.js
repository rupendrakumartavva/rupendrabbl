(function () {

    'use strict';
    var controllerId = 'PhysicalLocationCorpBussAgentConroller';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', 'appConstants', '$timeout', '$routeParams', 'UtilityFactory', 'BBLSubmissionFactory', 'SessionFactory', 'popupFactory', 'errorFactory', 'authService', '$window', PhysicalLocationCorpBussAgentConroller]);

    function PhysicalLocationCorpBussAgentConroller($scope, $rootScope, $location, requestService, appConstants, $timeout, $routeParams, UtilityFactory, BBLSubmissionFactory, SessionFactory, popupFactory, errorFactory, authService, $window) {

        var vm = this;
        vm.navigatepath = '';
        vm.submissionStatusData = {};
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
                    vm.Businessdata = {};
                    vm.Businessdata.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                    requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid)).then(function (response) {
                        vm.submissionStatusData = response.data;
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        }
                        else {
                            getagentdetails().then(function (response) {
                                vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                                vm.Businessdata.FileNumber = response.data[0].FileNumber;
                                vm.Businessdata = response.data;
                                vm.Businessdata[0].BusinessCountry = 'United States';
                                if (vm.Businessdata[0].IsValid == true) {
                                    vm.busiaddresscorrect = true;
                                }
                                if (vm.Businessdata[0].HQStatus == 'False') {
                                    vm.busiaddresscorrect = false;
                                }
                                else if (vm.Businessdata[0].HQStatus == 'True') {
                                    vm.busiaddresscorrect = true;
                                }
                                else {
                                    vm.busiaddresscorrect = undefined;
                                }
                                if (vm.submissionStatusData.IsCoporateRegistration == 'false') {
                                    vm.busiaddresscorrect = undefined;
                                }
                                if (vm.submissionStatusData.IsResidentAgent == 'false' && vm.busiaddresscorrect == false) {
                                    vm.busiaddresscorrect = false;
                                } else if (vm.submissionStatusData.IsResidentAgent == 'false' && vm.busiaddresscorrect == true) {
                                    vm.busiaddresscorrect = undefined;
                                } else if (vm.submissionStatusData.IsResidentAgent == 'true' && vm.busiaddresscorrect == true) {
                                    vm.busiaddresscorrect = true;
                                }
                                SessionFactory.setSessionAsClear();
                            }, ErrorWhileProcessing);
                            $('#dvLoadingSection').css('display', 'none');
                            $("#dvMainsection").css("display", "block");
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

        function getagentdetails() {
            return requestService.GetAgentDetails(vm.Businessdata);
        }

        function ErrorWhileProcessing() {
            console.log("Error");
        }

        vm.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#error_msg').html('');
        }

        vm.togglebusiness = function (ctlID) {
            SessionFactory.setSessionAsDirty();
            $("#error_msg").html('');
            if (ctlID.indexOf('incorrect') != -1) {
                vm.busiaddresscorrect = false;

            } else {
                vm.busiaddresscorrect = true;
            }
            $timeout(function () {
                $('.pre-checkquestion').focus();
            }, 1);
        }

        vm.checkAndExit = function (path) {
            if (path.indexOf('app') != -1) {
                vm.navigatepath = path + '/' + $routeParams.guid;
            } else {
                vm.navigatepath = path
            }
            if (vm.busiaddresscorrect == undefined) {
                SessionFactory.setSessionAsClear();
                $location.path('/' + vm.navigatepath);
            } else {
                SubmitData();
            }
        }

        vm.navToMailingAddress = function () {
            if (vm.submissionStatusData.IsCategorySelfCertification) {
                vm.navigatepath = 'selfcertification/' + $routeParams.guid;
            } else {
                vm.navigatepath = 'physicallocation/address/' + $routeParams.guid;
            }
            if (vm.busiaddresscorrect == undefined) {
                $('#error_msg').html(vm.currentpage_errors.NextButtonIncompleteData).focus();
            }
            else {
                if (vm.busiaddresscorrect) {
                    SubmitData();
                }
            }
        }

        function subitcorpagent() {
            vm.Businessdata[0].UserType = 'Y-CORPAGENT';
            vm.Businessdata[0].MasterId = UtilityFactory.getMasterId($routeParams.guid);
            vm.Businessdata[0].CorpStatus = true;
            vm.Businessdata[0].HQStatus = vm.busiaddresscorrect;
            vm.Businessdata[0].AddressNumber = vm.Businessdata[0].AddressNumber;
            return requestService.SubmitCorpAgent(vm.Businessdata[0]);
        }

        function SubmitData() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            SessionFactory.setSessionAsClear();
            subitcorpagent().then(function (response) {
                $location.path('/' + vm.navigatepath);
            }, ErrorWhileProcessing);
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                vm.navigatepath = next.split('#')[1].slice(1);
                if (SessionFactory.isSessionDirty()) {
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.navigateaway, vm.navigatepath);
                    return;
                }
            }
        });

        vm.navToBack = function () {
            $window.history.back();
        }
    }
})();