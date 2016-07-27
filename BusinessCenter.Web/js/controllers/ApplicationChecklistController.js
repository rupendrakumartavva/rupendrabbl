'use strict';

(function () {
    var controllerId = 'ApplicationChecklistController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', '$routeParams', 'requestService', 'UtilityFactory', 'appConstants', 'BBLSubmissionFactory', 'SessionFactory','authService', ApplicationChecklistController]);

    function ApplicationChecklistController($scope, $rootScope, $location, $routeParams, requestService, UtilityFactory, appConstants, BBLSubmissionFactory, SessionFactory, authService) {
        var vm = this;
        vm.supportingDocsData = {};
        vm.submissionStatusData = {};
        vm.isPhysicallocDataFilled = true;
        vm.IsIndividualPhysicallocDataFilled = true;
        SessionFactory.setSessionAsClear();
        init();

        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load.
        */
        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            
            authService.refreshToken().then(function () {
                $(".login_form preapp-checklist").css("background-color", "white");
                if (UtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid)).then(function (response) {
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            $location.path('/mybbl');
                        } else {
                            vm.submissionStatusData = response.data;
                            getCheckListData();
                        }
                    });
                }
                else {
                    BBLSubmissionFactory.invalidSubmission();
                }
            }, function () {
                $location.path('/login');
            })
           
        }

        function getCheckListData() {
            AppCheckListInfo(UtilityFactory.getMasterId($routeParams.guid)).then(function (response) {
                vm.supportingDocsData = response.data.result;
                vm.MasterTextRevenueIsCleanHand = response.data.masterTextRevenue;
                vm.validateCorpFileStatus = response.data.validateCorpFileStatus;
                vm.CorpChangeStatus = response.data.CorpChangeStatus;
                vm.isPhysicallocDataFilled = vm.check_physicalloc_data();
                vm.IsIndividualPhysicallocDataFilled = vm.checkIndivual_physicalloc_data();
                $('#dvLoadingSection').css('display', 'none');
                $("#dvMainsection").css("display", "block");
            }, function (response) {
                console.log("Error");
            });
        }

        function AppCheckListInfo(MasterId) {
            return requestService.BblRequiredDocuments({ MasterId: MasterId });
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "supportingdocs/step1" page

        //------------------------------------------------------------------

        vm.navToUploadDocs = function () {
            updateSubmissionStatus().then(function (response) {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                //if (vm.bblapptype == "ON")
                $location.path('/supportingdocs/step2/ON/' + $routeParams.guid);
                //else
                //    $location.path('/mybbl');
            }, ErrorWhileProcessing);
            //    $location.path('/supportingdocs/step1/' + $routeParams.guid);
            //  $location.path('/supportingdocs/step2/ON/' + $routeParams.guid);
        }

        function updateSubmissionStatus() {
            var obj = angular.copy(vm.supportingDocsData[0]);
            obj.DocSubType = 'ON';
            return requestService.updateSubmissionStatus(obj);
        }

        function ErrorWhileProcessing() {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error");
        }
        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "physicallocation" page

        //------------------------------------------------------------------

        vm.navToPhysicalLoc = function () {
            if (vm.supportingDocsData[0].IsSubmissioneHop == true) {
                $location.path('/ehopeligibility/' + $routeParams.guid);
            } else if (vm.supportingDocsData[0].IsSubmissioneHop == false && vm.supportingDocsData[0].IsBusinessMustbeinDC == true && vm.supportingDocsData[0].IsHomeBased == true && vm.supportingDocsData[0].IsHop == false) {
                $location.path('/ehopeligibility/' + $routeParams.guid);
            } else if (vm.supportingDocsData[0].IsHomeBased == false && vm.supportingDocsData[0].IsCof == false && vm.supportingDocsData[0].IsHop == false) {
                if (vm.supportingDocsData[0].IsIndividual == true) {
                    $location.path('/nopobox/' + $routeParams.guid);
                } else {
                    $location.path('/nopobox/' + $routeParams.guid);
                }
            } else if (vm.supportingDocsData[0].IsIndividual == true && vm.supportingDocsData[0].CategoryName == 'Solicitor' && vm.supportingDocsData[0].IsHop == true) {
                $location.path('/physicallocation/hop/' + $routeParams.guid);
            } else {
                if (vm.supportingDocsData[0].IsCof == true) {
                    $location.path('/physicallocation/cofo/' + $routeParams.guid);
                } else if (vm.supportingDocsData[0].IsHop == true) {
                    $location.path('/physicallocation/hop/' + $routeParams.guid);
                } else if (vm.supportingDocsData[0].IsCorporationDivision == true && vm.supportingDocsData[0].IsHop == false && vm.supportingDocsData[0].IsCof == false) {
                    $location.path('/physicallocation/corpreg/' + $routeParams.guid);
                } else if (vm.supportingDocsData[0].IsCorporationDivision == false && vm.supportingDocsData[0].IsHop == false && vm.supportingDocsData[0].IsCof == false) {
                    if (vm.supportingDocsData[0].TradeName != '') {
                        if (vm.supportingDocsData[0].BusinessStructure.toUpperCase() != 'SOLE PROPRIETORSHIP') {
                            $location.path('/corpreqregwithtradesecond/' + $routeParams.guid);
                        }
                    } else {
                        if ((vm.supportingDocsData[0].BusinessStructure.toUpperCase() == 'SOLE PROPRIETORSHIP' || vm.supportingDocsData[0].BusinessStructure.toUpperCase() == 'GENERAL PARTNERSHIP' || vm.supportingDocsData[0].BusinessStructure.toUpperCase() == 'JOINT VENTURE') && vm.supportingDocsData[0].TradeName == '') {
                            $location.path('/corpnotregisteredaddress/' + $routeParams.guid);
                        } else if (vm.supportingDocsData[0].BusinessStructure.toUpperCase() != 'SOLE PROPRIETORSHIP' || vm.supportingDocsData[0].BusinessStructure.toUpperCase() != 'GENERAL PARTNERSHIP' || vm.supportingDocsData[0].BusinessStructure.toUpperCase() != 'JOINT VENTURE' && vm.supportingDocsData[0].TradeName == '') {
                            $location.path('/corpreqregisterfirst/' + $routeParams.guid);
                        }
                    }
                }
            }
        }

        //vm.supportingDocsData[0].IsResidentAgent &&
        //     vm.supportingDocsData[0].IsMailAddress &&

        vm.check_physicalloc_data = function () {
            if ((vm.supportingDocsData[0].IsSubmissionHop ||
                vm.supportingDocsData[0].IsBPAddress ||
                vm.supportingDocsData[0].IsSubmissionCofo) &&
                vm.supportingDocsData[0].IsCorporateRegistration &&
                vm.submissionStatusData.TradeName.length > 0 &&
                vm.submissionStatusData.PremisesAddress.length > 0 &&
                vm.submissionStatusData.BusinessName.length > 0
                ) {
                return false;
            }
            return true;
        }

        vm.checkIndivual_physicalloc_data = function () {
            if (
                vm.supportingDocsData[0].IsCorporateRegistration &&
                vm.submissionStatusData.BusinessName.length > 0
                ) {
                return false;
            }
            return true;
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "taxrevenue" page

        //------------------------------------------------------------------

        vm.navToTaxRevenue = function () {
            $location.path('/taxrevenue/' + $routeParams.guid)
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "infoverification" page

        //------------------------------------------------------------------

        vm.navToInfoVerification = function () {
            $location.path('/infoverification/' + $routeParams.guid);
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "preappquestions/step1" page

        //------------------------------------------------------------------

        vm.navToPreAppQues = function () {
            $location.path('/reviewchecklist/' + $routeParams.guid);
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "individualbusinesslic" page

        //------------------------------------------------------------------

        vm.navToIndividualBusinessLic = function () {
            $location.path('/individualbusinesslic/' + $routeParams.guid);
        }

        vm.navSaveandExit = function () {
            localStorage.removeItem('preAppQuestionsData');
            localStorage.removeItem("genBuss");
            //  $location.path("/mybbl");
            localStorage.removeItem('MasterId');
            $location.path('/mybbl');
        }

        function downloadPdf(val) {
            var authData = JSON.parse(localStorage.getItem('ls.authorizationData'));

            if (authData) {
                var token = 'Bearer ' + authData.token;
                var refreshtoken = authData.guiToken;
                var key = CryptoJS.enc.Utf8.parse('8080808080808080');
                var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
                var encrymasterId = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(val), key,
             {
                 keySize: 128 / 8,
                 iv: iv,
                 mode: CryptoJS.mode.CBC,
                 padding: CryptoJS.pad.Pkcs7
             });
                var encryRefreshToken = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(refreshtoken), key,
                               {
                                   keySize: 128 / 8,
                                   iv: iv,
                                   mode: CryptoJS.mode.CBC,
                                   padding: CryptoJS.pad.Pkcs7
                               });

                window.open(appConstants.apiServiceBaseUri + 'api/Download/applicationChecklist_GeneratedDocument/?reft=' + encryRefreshToken + '&masterid=' + val, '_self', '');
            }
            //    window.open(appConstants.apiServiceBaseUri + 'api/Download/applicationChecklist_GeneratedDocument/?masterid=' + val + '&token=' + token + '&refreshtoken=' + refreshtoken, '_self', '');
        };

        vm.printChecklist = function () {
            downloadPdf(UtilityFactory.getSubmissionStatusObject($routeParams.guid).masterId);
        }

        vm.printChcekListWithJson = function () {
            AppCheckListInfo1(UtilityFactory.getMasterId($routeParams.guid)).then(function (response) {
                //  alert(response.data);
                var pdfFile = new Blob([response.data], { type: 'application/pdf' });
                var pdfFileUrl = URL.createObjectURL(pdfFile);
                window.open(pdfFileUrl);
            }, function (response) {
                console.log("Error");
            });
        }

        function AppCheckListInfo1(MasterId) {
            var submissionEntityModel = {};
            submissionEntityModel.MasterId = MasterId;
            return requestService.ApplicationChcekListPdf(submissionEntityModel);
        }
    }
})();