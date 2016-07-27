'use strict';

(function () {

    var controllerId = 'SupportingDocsUploadController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', '$routeParams', 'requestService', 'appConstants', 'UtilityFactory', 'SessionFactory', 'BBLSubmissionFactory', 'authService', '$window', 'errorFactory', 'popupFactory', SupportingDocsUploadController]);

    function SupportingDocsUploadController($scope, $rootScope, $location, $routeParams, requestService, appConstants, UtilityFactory, SessionFactory, BBLSubmissionFactory, authService, $window, errorFactory, popupFactory) {

        var vm = this;
        vm.bblapptype = '';
        $scope.notuploaded = [];
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
                    AppCheckListInfo(UtilityFactory.getMasterId($routeParams.guid)).then(function (response) {
                        vm.checkListData = response.data.result[0];
                        vm.supportingDocsData = response.data.result[0].BblServiceDoc;
                        vm.bblapptype = vm.checkListData.DocSubType;
                        vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                        $(document).ready(function () {
                            setTimeout(function () {
                                window.scrollTo(0, -400);
                                $('#HeadingText').focus();
                            }, 0);
                        });
                        $('#dvLoadingSection').css('display', 'none');
                        $("#dvMainsection").css("display", "block");
                    }, ErrorWhileProcessing);
                } else {
                    SessionFactory.setSessionAsClear();
                    BBLSubmissionFactory.invalidSubmission();
                }
            }, function () {
                $location.path("/login");
            });
        }

        function AppCheckListInfo(MasterId) {
            return requestService.BblRequiredDocuments({ MasterId: MasterId });
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigated to "supportingdocs/step2" page for uploading the docs  

        //------------------------------------------------------------------

        vm.navToUploadDocs_Step2 = function () {
            updateSubmissionStatus().then(function (response) {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                if (vm.bblapptype == "ON")
                    $location.path('/supportingdocs/step2/ON/' + $routeParams.guid);
                else
                    $location.path('/mybbl');
            }, ErrorWhileProcessing);
        }

        vm.navToCheckList = function () {
            if ($location.path().indexOf("step1") != -1) {
                updateSubmissionStatus().then(function (response) {
                    $location.path('/appchecklist/' + $routeParams.guid);
                }, ErrorWhileProcessing);
            } else {
                $location.path('/appchecklist/' + $routeParams.guid);
            }
        }

        function ErrorWhileProcessing() {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error");
        }

        function updateSubmissionStatus() {
            var obj = angular.copy(vm.checkListData);
            obj.DocSubType = vm.bblapptype;
            return requestService.updateSubmissionStatus(obj);
        }

        vm.stayOnThisPage = function () {
            $('#selectionIn').modal('hide');
            return;
        }

        vm.navigateAnyWay = function () {
            $('#selectionIn').modal('hide');
            updateSubmissionStatus().then(function (response) {
                SessionFactory.setSessionAsClear();
                $location.path('/appchecklist/' + $routeParams.guid);
            }, function (response) {
                console.log("error");
            });
        }

        vm.navToSaveandExitfromDoc = function () {
            updateSubmissionStatus().then(function (response) {
                $location.path('/mybbl');
            }, ErrorWhileProcessing);
        }

        vm.removeUploadedFile = function (ndx) {
            removeDocument(ndx).then(function (response) {
                if (response.data == 'true') {
                    angular.element(document.getElementById('r' + ndx)).html('');
                    $("#icon" + ndx).addClass('glyphicon-unchecked').removeClass('glyphicon-ok');
                    $('#f' + ndx).show();
                }
            });
        }

        vm.selectDropOption = function () {
            if (vm.bblapptype == "IN") {
                $("#selectionIn").modal('show');
                return;
            }
        };

        function removeDocument(ndx) {
            var data = {};
            data.MasterId = vm.supportingDocsData[ndx].MasterId;
            data.SubmissionCategoryID = vm.supportingDocsData[ndx].SubmissionCategoryID;
            data.Description = vm.supportingDocsData[ndx].Description;
            data.DocRequired = vm.supportingDocsData[ndx].DocRequired;
            return requestService.removeUpladedFile(data);
        }

        vm.navToBack = function () {
            $window.history.back();
        }

       

        function checkIfUploadedPartially() {
            if (SessionFactory.isSessionDirty() && $scope.notuploaded.length > 0) {
                SessionFactory.setSessionAsDirty();
            } else {
                SessionFactory.setSessionAsClear();
            }
        }

        
        $scope.$on('$locationChangeStart', function (event, next, current) {
            checkIfUploadedPartially();
            if (SessionFactory.isSessionDirty()) {
                vm.navigationPath = next.split('#')[1].slice(1);
                event.preventDefault();
                popupFactory.showpopup("One or more of your documents has not been uploaded. Select [CANCEL] to go back and upload or [OK] to exit without saving data on this page.", vm.navigationPath);
                return;
            }
        });

    }
})();