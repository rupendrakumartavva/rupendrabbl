(function () {
    'use strict';
    var controllerId = 'CorpNotRegisteredAgentController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', 'appConstants', '$routeParams', 'UtilityFactory', 'SessionFactory', 'BBLSubmissionFactory', 'errorFactory', 'popupFactory', 'authService', '$window', CorpNotRegisteredAgentController]);

    function CorpNotRegisteredAgentController($scope, $rootScope, $location, requestService, appConstants, $routeParams, UtilityFactory, SessionFactory, BBLSubmissionFactory, errorFactory, popupFactory, authService, $window) {
        var vm = this;
        vm.navigate = false;
        vm.previousObj = {};
        vm.ehopAgentform = {};
        vm.corpagentaddressempty = {};
        vm.submissionStatusData = {};
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
                        vm.submissionStatusData = response.data;
                        vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        } else {
                            StreetsDropDown().then(function (response) {
                                vm.Countries = response.data.CountryList;
                                vm.ehopaddress = {};
                                vm.ehopAgentform.BusinessCity = 'Washington';
                                vm.ehopAgentform.BusinessState = 'District of Columbia';
                                vm.ehopaddress.FileNumber = "NA";
                                vm.ehopaddress.UserType = "N-CORPAGENT";
                                vm.ehopaddress.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                                requestService.GetHQA(vm.ehopaddress).then(function (response) {
                                    vm.ehopAgentform.BusinessCountry = "US";
                                    if (response.data.BusinessName != "" && response.data.BusinessName != null) {
                                        vm.ehopAgentform = response.data;
                                    }
                                    delete vm.ehopaddress;
                                    vm.previousObj = angular.copy(vm.ehopAgentform);
                                    $('#dvLoadingSection').css('display', 'none');
                                    $("#dvMainsection").css("display", "block");
                                });
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

        function StreetsDropDown() {
            return requestService.streetsDropDown();
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "physicallocation/corpreg" page

        //------------------------------------------------------------------

        vm.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#error_msg').html('');
        }

        vm.navToNext = function () {
            if (vm.submissionStatusData.IsCategorySelfCertification) {
                vm.navigationPath = 'selfcertification/' + $routeParams.guid;
            } else {
                vm.navigationPath = 'physicallocation/address/' + $routeParams.guid;
            }
            if (vm.corpnorregadd_form.$invalid) {
                $('#error_msg').html(vm.currentpage_errors.NextButtonIncompleteData).focus();
            }
            else {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                SubmitAgentAddress();
            }
        }

        function SubmitAgentAddress() {
            vm.ehopAgentform.FileNumber = "NA";
            vm.ehopAgentform.UserType = "N-CORPAGENT";
            vm.ehopAgentform.MasterId = UtilityFactory.getMasterId($routeParams.guid);
            vm.ehopAgentform.BusinessStructure = vm.submissionStatusData.BusinessStructure;
            vm.ehopAgentform.TradeName = vm.submissionStatusData.TradeName;
            vm.ehopAgentform.AddressNumber = vm.ehopAgentform.AddressNumber;

            requestService.SubmitCorpAgent(vm.ehopAgentform).then(function (response) {
                SessionFactory.setSessionAsClear();
                SessionFactory.setFormAsSubmitted();
                $location.path('/' + vm.navigationPath);
            }, ErrorWhileProcessing);
        };

        function ErrorWhileProcessing(response) {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error");
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "appchecklist" page

        //------------------------------------------------------------------

        function deletecorpagentaddress(data) {
            return requestService.CorpNotRegEmptyHqAddress(data);
        }

        function emptycorpngentdata() {
            vm.corpagentaddressempty.UserType = "N-CORPAGENT";
            vm.corpagentaddressempty.FileNumber = "NA";
            vm.corpagentaddressempty.MasterId = UtilityFactory.getMasterId($routeParams.guid);
            vm.corpagentaddressempty.BusinessStructure = vm.submissionStatusData.BusinessStructure;
            vm.corpagentaddressempty.TradeName = vm.submissionStatusData.TradeName;
        }

        vm.checkAndExit = function (path) {
            vm.submitted = true;
            vm.navigationPath = path;
            if (path.indexOf('app') != -1) {
                vm.navigationPath = path + '/' + $routeParams.guid;
            } else {
                vm.navigationPath = path;
            }
            if (vm.corpnorregadd_form.$invalid) {
                if (SessionFactory.isFormEmpty(vm.corpnorregadd_form)) {
                    $('#dvLoadingSection').css('display', 'block');
                    $("#dvMainsection").css("display", "none");
                    emptycorpngentdata();
                    deletecorpagentaddress(vm.corpagentaddressempty).then(function (response) {
                        SessionFactory.setSessionAsClear();
                        $location.path('/' + vm.navigationPath);
                    }, ErrorWhileProcessing);
                } else {
                    SessionFactory.setSessionAsDirty();
                    popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigationPath);
                }
            } else {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                SubmitAgentAddress();
            }
        }

        //vm.stayOnThisPage = function () {
        //    $('#ehopAgentMessage').modal('hide');
        //}

        //vm.navigateAnyWay = function () {
        //    $("#ehopAgentMessage").modal('hide');
        //    SessionFactory.setSessionAsClear();
        //    $location.path('/' + vm.navigationPath);
        //}

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                SessionFactory.compareObjectsInCurrentSession(vm.ehopAgentform, vm.previousObj);
                if (SessionFactory.isSessionDirty()) {
                    vm.navigationPath = next.split('#')[1].slice(1);
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.navigateaway, vm.navigationPath);
                    return;
                }
            }
        });
        vm.navToBack = function () {
            $window.history.back();
        }
    }
})();