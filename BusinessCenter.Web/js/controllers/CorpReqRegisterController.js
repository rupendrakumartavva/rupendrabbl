(function () {
    'use strict';
    var controllerId = 'CorpReqRegisterController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', 'appConstants', '$timeout', 'SessionFactory', '$routeParams', 'UtilityFactory', 'BBLSubmissionFactory', 'errorFactory', 'popupFactory', 'authService', '$window', CorpReqRegisterController]);

    function CorpReqRegisterController($scope, $rootScope, $location, requestService, appConstants, $timeout, SessionFactory, $routeParams, UtilityFactory, BBLSubmissionFactory, errorFactory, popupFactory, authService, $window) {
        var vm = this;

        //vm.navigate = true;
        vm.prevObj = {};
        vm.corpregistraion = {};
        vm.corpregistraion.FileNumber = undefined;
        vm.submissionStatusData = {};
        SessionFactory.setSessionAsDirty();
        vm.Dcbc_EntityCorpStatus = '';
        vm.CheckCorpanyRadiobutton = false;
        vm.CheckCorHQAddressRadiobutton = false;
        vm.entityStatus = false;
        vm.businessStructureStatus = true;
        vm.IsDataChange = false;
        vm.currentpage_errors = {};
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
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        } else {
                            vm.corpregistraion.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                            vm.corpregistraion.UserType = 'Y-CORPREG';
                            requestService.GetCorporationDetails(vm.corpregistraion).then(function (response) {
                                vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                                vm.corpregisterfirst = response.data[0].BusinessStructure;
                                vm.getCorpDetails = response.data;
                                $('#dvLoadingSection').css('display', 'none');
                                $("#dvMainsection").css("display", "block");
                                if (vm.getCorpDetails[0].FileNumber != null && response.data[0].CorpStatus != "NODATA") {
                                    if (response.data[0].EntityStatus == 'ACTIVE' || response.data[0].EntityStatus == null) {
                                        vm.corpregistraion.FileNumber = response.data[0].FileNumber;
                                        requestService.CorporationSearchFind(vm.corpregistraion).then(function (response) {
                                            $('#dvLoadingSection').css('display', 'block');
                                            $("#dvMainsection").css("display", "none");
                                            vm.Businessdata = response.data;
                                            vm.corporation = vm.submissionStatusData.IsCorporateRegistration;
                                            vm.businessStructureStatus = angular.copy(vm.getCorpDetails[0].BusinessStructureStatus);
                                            vm.Dcbc_EntityCorpStatus = angular.copy(vm.Businessdata[0].EntityStatus.toUpperCase());
                                            if (vm.Businessdata[0].BusinessStructureStatus) {
                                                vm.corpregistraion.foundinfo = true;
                                                if (vm.getCorpDetails[0].CorpStatus == 'True') {
                                                    vm.corpregistraion.addresscorrect = true;
                                                } else {
                                                    if (vm.getCorpDetails[0].CorpStatus == 'False') {
                                                        vm.corpregistraion.addresscorrect = false;
                                                    }
                                                }
                                                if (vm.getCorpDetails[0].CorpStatus == 'True' || vm.getCorpDetails[0].CorpStatus == 'False') {
                                                    if (vm.getCorpDetails[0].HQStatus == 'True') {
                                                        vm.corpregistraion.busiaddresscorrect = true;
                                                    } else {
                                                        vm.corpregistraion.busiaddresscorrect = false;
                                                    }
                                                }

                                                if (vm.corpregistraion.busiaddresscorrect == true && vm.corpregistraion.addresscorrect == true) {
                                                    vm.entityStatus = true;
                                                }
                                                else {
                                                    vm.entityStatus = false;
                                                }
                                            } else {
                                                vm.entityStatus = false;
                                                vm.corpregistraion.foundinfo = true;
                                            }

                                            if (vm.Businessdata[0].EntityStatus == "NODATA" && vm.Businessdata[0].FileNumber != "") {
                                                vm.businessStructureStatus = true;
                                                vm.corpregistraion.foundinfo = true;
                                                vm.Dcbc_EntityCorpStatus = angular.copy(vm.Businessdata[0].EntityStatus.toUpperCase());
                                            }
                                            vm.prevObj = angular.copy(vm.corpregistraion);
                                            $('#dvLoadingSection').css('display', 'none');
                                            $("#dvMainsection").css("display", "block");
                                        }, ErrorWhileProcessing);
                                    } else {
                                        if (response.data[0].FileNumber != null) {
                                            vm.corpregistraion.foundinfo = true;
                                            vm.corpregistraion.FileNumber = response.data[0].FileNumber;
                                            vm.Dcbc_EntityCorpStatus = response.data[0].EntityStatus;
                                            vm.Businessdata = {};
                                            vm.Businessdata[0] = {};
                                            vm.Businessdata[0].EntityStatus = response.data[0].EntityStatus;
                                            requestService.UpdateCorpStatus({ MasterId: vm.corpregistraion.MasterId, FileNumber: vm.corpregistraion.FileNumber }).then(function (response) {
                                            });
                                            if (vm.Dcbc_EntityCorpStatus != 'ACTIVE')
                                                vm.entityStatus = false;
                                        }
                                    }
                                    $('#dvLoadingSection').css('display', 'none');
                                    $("#dvMainsection").css("display", "block");
                                }
                                vm.prevObj = angular.copy(vm.corpregistraion);
                            }, ErrorWhileProcessing);
                        }
                        $("#dvSubLoadingSection").css("display", "none");
                        $('#dvSubMainsection').css('display', 'block');
                    });
                } else {
                    vm.navigate = true;
                    BBLSubmissionFactory.noGuidAvailable();
                }
            }, function () {
                $location.path("/login");
            });
        }

        vm.getCorpRegInfo = function () {
            vm.searchZoningClicked = true;
            vm.submitted = false;
            delete vm.corpregistraion.busiaddresscorrect;
            delete vm.corpregistraion.addresscorrect;
            if (vm.contact_us.$invalid) {
                $('#error_msg').html(vm.currentpage_errors.corpFileNumberError).focus();
            }
            else {
                $("#dvSubLoadingSection").css("display", "block");
                $('#dvSubMainsection').css('display', 'none');
                $("#error_msg").html('');
                vm.corpregistraion.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                requestService.CorporationSearchFind(vm.corpregistraion).then(function (response) {
                    vm.corpregistraion.foundinfo = true;
                    vm.Businessdata = response.data;
                    vm.Dcbc_EntityCorpStatus = response.data[0].EntityStatus;
                    vm.entityStatus = false;
                    vm.businessStructureStatus = angular.copy(vm.Businessdata[0].BusinessStructureStatus);
                    //if (!vm.Businessdata[0].BusinessStructureStatus) {
                    //    vm.corpregistraion.addresscorrect = false;
                    //    vm.corpregistraion.busiaddresscorrect = false;
                    //}
                    $("#dvSubLoadingSection").css("display", "none");
                    $('#dvSubMainsection').css('display', 'block');
                }, ErrorWhileProcessing);
            }
        }

        vm.toggleRadio = function (ctlID) {
            SessionFactory.setSessionAsDirty();
            $("#errormsg").html('');
            if (ctlID.indexOf('incorrect') != -1) {
                vm.CheckCorpanyRadiobutton = true;
                vm.corpregistraion.addresscorrect = false;
                $timeout(function () {
                    $('.highlight').focus();
                }, 1);
            } else {
                vm.CheckCorpanyRadiobutton = true;
                vm.corpregistraion.addresscorrect = true;
            }
            if (vm.corpregistraion.busiaddresscorrect == true && vm.corpregistraion.addresscorrect == true) {
                vm.entityStatus = true;
            }
            else {
                vm.entityStatus = false;
            }
        }

        vm.togglebusiness = function (ctlID) {
            SessionFactory.setSessionAsDirty();
            $("#error_msg").html('');
            if (ctlID.indexOf('incorrect') != -1) {
                vm.corpregistraion.busiaddresscorrect = false;
                vm.CheckCorHQAddressRadiobutton = true;
                $timeout(function () {
                    $('.highlight').focus();
                }, 1)
            } else {
                vm.CheckCorHQAddressRadiobutton = true;
                vm.corpregistraion.busiaddresscorrect = true;
            }
            if (vm.corpregistraion.busiaddresscorrect == true && vm.corpregistraion.addresscorrect == true) {
                vm.entityStatus = true;
            }
            else {
                vm.entityStatus = false;
            }
        }

        function ErrorWhileProcessing() {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error");
        }

        vm.checkAndExit = function (path) {
            vm.submitted = true;
            if (path.indexOf('app') != -1) {
                vm.navigationPath = path + '/' + $routeParams.guid;
            } else {
                vm.navigationPath = path
            }
            if (vm.corpregistraion.FileNumber != undefined && vm.corpregistraion.FileNumber != "") {
                if (angular.isDefined(vm.corpregistraion.busiaddresscorrect) && angular.isDefined(vm.corpregistraion.addresscorrect)) {
                    submitCorpDetails();
                } else if (angular.isDefined(vm.Businessdata) && !vm.Businessdata[0].BusinessStructureStatus && vm.Businessdata[0].EntityStatus.toUpperCase() == 'ACTIVE') {
                    SessionFactory.setSessionAsClear();
                    submitCorpDetails();
                }
                else {
                    if (angular.isDefined(vm.Businessdata)) {
                        if (vm.Businessdata[0].EntityStatus.toUpperCase() != 'ACTIVE') {
                            SessionFactory.setSessionAsClear();
                            $location.path('/' + vm.navigationPath);
                            return;
                        }
                    }
                    if (vm.searchZoningClicked == false) {
                        popupFactory.showpopup(vm.currentpage_errors.corpSearchNotClicked, vm.navigationPath);
                    } else {
                        popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigationPath);
                    }
                }
            } else {
                SessionFactory.setSessionAsClear();
                vm.corpregistraion.FileNumber = '';
                vm.corpregistraion.UserType = 'Y-CORPREG';
                vm.corpregistraion.IsDataChange = true;
                requestService.SubmitCorpAgent({ MasterId: UtilityFactory.getMasterId($routeParams.guid), FileNumber: vm.corpregistraion.FileNumber, UserType: vm.corpregistraion.UserType, IsDataChange: vm.corpregistraion.IsDataChange }).then(function (response) {
                    $location.path('/' + vm.navigationPath);
                }, ErrorWhileProcessing)
            }
        }

        vm.navToBusinessAgent = function () {
            vm.navigationPath = '';
            if (angular.isDefined(vm.corpregistraion.busiaddresscorrect) && angular.isDefined(vm.corpregistraion.addresscorrect)) {
                submitCorpDetails();
            } else {
                $('#error_msg').html(vm.currentpage_errors.NextButtonIncompleteData).focus();
            }
        }

        vm.checkSearchZoning = function (id) {
            $('#' + id).html('');
            $('#errorrevoked').html('');
            $('#errormsg').html('');
            $('#erroraddress').html('');
            $('#successmessage').html('');
            $('#error_msg').html('');
            delete vm.corpregistraion.addresscorrect;
            delete vm.corpregistraion.busiaddresscorrect;
            delete vm.corpregistraion.foundinfo;
            vm.searchZoningClicked = false;
            vm.entityStatus = false;
            vm.businessStructureStatus = true;
            SessionFactory.setSessionAsDirty();
            delete vm.Businessdata;
        }

        function submitCorpDetails() {
            SessionFactory.setSessionAsClear();
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            vm.Businessdata[0].UserType = 'Y-CORPREG';
            vm.Businessdata[0].MasterId = UtilityFactory.getMasterId($routeParams.guid);
            vm.Businessdata[0].CorpStatus = vm.corpregistraion.addresscorrect;
            vm.Businessdata[0].HQStatus = vm.corpregistraion.busiaddresscorrect;

            vm.Businessdata[0].BusinessName = vm.Businessdata[0].CBusinessName;
            vm.Businessdata[0].TradeName = "NA";

            if ((vm.prevObj.FileNumber != vm.corpregistraion.FileNumber) || (!vm.corpregistraion.addresscorrect) || (!vm.corpregistraion.busiaddresscorrect)) {
                vm.IsDataChange = true;
            }
            vm.Businessdata[0].IsDataChange = vm.IsDataChange;
            requestService.SubmitCorpAgent(vm.Businessdata[0]).then(function (response) {
                if (response.data) {
                    if (vm.navigationPath == 'appchecklist/' + $routeParams.guid || vm.navigationPath == 'mybbl') {
                        $location.path('/' + vm.navigationPath);
                        return;
                    }
                    vm.submissionStatusData.IsCorporateRegistration = true;

                    if (vm.submissionStatusData.BusinessStructure.toUpperCase() != 'SOLE PROPRIETORSHIP') {
                        $location.path('/physicallocation/corpbussagent/' + $routeParams.guid);
                    } else {
                        if (vm.submissionStatusData.BusinessStructure.toUpperCase() == 'SOLE PROPRIETORSHIP' && vm.Businessdata[0].BusinessState.toUpperCase() == 'DC') {
                            $location.path('/physicallocation/corpbussagent/' + $routeParams.guid);
                        }
                        else {
                            $location.path('/corpnotregisteredagent/' + $routeParams.guid);
                        }
                    }
                }
            }, ErrorWhileProcessing);
        }

        //------ corp rq reg first page-----------//

        vm.navToChecklist = function () {
            $location.path('/appchecklist/' + $routeParams.guid);
        }

        vm.navToNewBblWelcome = function () {
            $location.path('/mybbl');
        }

        vm.corpErrorMsg = function (id) {
            $('#' + id).html('');
            $('#error_msg').html('');
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                if (SessionFactory.isSessionDirty() && (!angular.equals(vm.prevObj, vm.corpregistraion))) {
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