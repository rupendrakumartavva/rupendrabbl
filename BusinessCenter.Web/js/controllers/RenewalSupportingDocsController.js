'use strict';

angular.module('DCRA').controller('RenewalSupportingDocs', ['$scope', '$rootScope', '$location', '$routeParams',
    'requestService', 'appConstants', 'UtilityFactory', 'RenewalUtilityFactory', 'SessionFactory', 'popupFactory', 'errorFactory', 'authService', RenewalSupportingDocsController]);

function RenewalSupportingDocsController($scope, $rootScope, $location, $routeParams, requestService, appConstants, UtilityFactory, RenewalUtilityFactory, SessionFactory, popupFactory, errorFactory, authService) {

    var vm = this;
    vm.DocSubmType = '';
    vm.noDocumentsUploaded = false;
    $scope.notuploaded = [];
    SessionFactory.setSessionAsDirty();
    init();

    function init() {
        $('#dvLoadingSection').css('display', 'block');
        $("#dvMainsection").css("display", "none");
        authService.refreshToken().then(function () {

            //getSubmissionMaster().then(function (response) {
            //vm.DocSubmType = response.data.Result[0].DocSubmType;
            //vm.masterId = response.data.Result[0].MasterId;
            if (RenewalUtilityFactory.ifGuidAvailable($routeParams.guid)) {
                getDocuments(RenewalUtilityFactory.getRenewalObject($routeParams.guid)).then(function (response) {
                    vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                    vm.DocumentList = response.data.DocumentList;
                    $(document).ready(function () {
                        setTimeout(function () {
                            window.scrollTo(0, -400);
                            $('#HeadingText').focus();
                        }, 0);
                    });
                    $('#dvLoadingSection').css('display', 'none');
                    $("#dvMainsection").css("display", "block");
                }, onFetchingDataError);
            } else {
                RenewalUtilityFactory.noGuid();
            }
            // }, onFetchingDataError);
        }, function () {
            $location.path("/login");
        });
    }

    function getSubmissionMaster() {
        return requestService.getSubmissionMasterWithbbl(RenewalUtilityFactory.getRenewalObject($routeParams.guid));
    }
    function onFetchingDataError(response) {
        console.log("Error");
        $('#dvLoadingSection').css('display', 'none');
        $("#dvMainsection").css("display", "block");
    }

    vm.selectDropOption = function () {
        if (vm.DocSubmType != '') {
            if (vm.DocSubmType.toUpperCase() == 'IN') {
                $("#selectionIn").modal('show');
                return;
            }
        }
    };

    //vm.navToUploadDocs_Step2 = function () {
    //    //requestService.deleteRenewalData({ MasterId: vm.masterId }).then(function (response) {
    //    vm.navigate = true;
    //    $('#dvLoadingSection').css('display', 'block');
    //    $("#dvMainsection").css("display", "none");
    //    UpdateRenwalDocumentType().then(function (response) {
    //        //CheckDocument().then(function (response) {
    //        if (response.data == "true") {
    //            $location.path('/renewal/supportingdocs/step2/' + $routeParams.guid);
    //        } else
    //            $location.path('/renewalpayment/' + $routeParams.guid);
    //        $('#dvLoadingSection').css('display', 'none');
    //        $("#dvMainsection").css("display", "block");
    //    }, onFetchingDataError);
    //    // }, onFetchingDataError);
    //    //}, onFetchingDataError);
    //}

    function CheckDocument() {
        return requestService.CheckDocumentStatus(checkorUpdate());
    }
    function UpdateRenwalDocumentType() {
        return requestService.UpdateRenwalDocumentType(checkorUpdate());
    }
    function checkorUpdate() {
        return { MasterId: vm.DocumentList[0].MasterId, DocType: "ON", DocumentList: vm.DocumentList };
    }

    function getDocuments(data) {
        return requestService.getDocuments(data);
    }

    function confirmRenuwal(confirmdata) {
        confirmdata.InitalDocumet = "Initial";
        return requestService.confirmRenuwal(confirmdata);
    };

    vm.removeUploadedFile = function (ndx) {
        removeDocument(ndx).then(function (response) {
            if (response.data == 'true') {
                angular.element(document.getElementById('r' + ndx)).html('');
                $("#icon" + ndx).addClass('glyphicon-unchecked').removeClass('glyphicon-ok');
                $('#f' + ndx).show();
            }
        });
    }

    function removeDocument(ndx) {
        var data = {};
        data.MasterId = vm.DocumentList[ndx].MasterId;
        data.SubmissionCategoryID = vm.DocumentList[ndx].SubmissionCategoryID;
        data.Description = vm.DocumentList[ndx].Description;
        data.DocRequired = vm.DocumentList[ndx].DocRequired;
        return requestService.removeUpladedFile(data);
    }

    vm.navToRenewalPayment = function () {
        CheckDocument().then(function (resp) {
            if (resp.data == "false") {
                vm.noDocumentsUploaded = true;
                window.scrollTo(0, 200);
                $("#nodocumentsupload").html('Please upload the required documents to proceed further.').focus();
            }
            else {
                SessionFactory.setSessionAsClear();
                $location.path('/renewalpayment/' + $routeParams.guid);
            }

        }, onFetchingDataError);
    }

    vm.navToPrevious = function () {
        SessionFactory.setSessionAsClear();
        checkIfUploadedPartially();
        $location.path('/renewal/cleanhands/' + $routeParams.guid);
    };

    vm.navToBBL = function () {
        $location.path('/mybbl');
    };

    function checkIfUploadedPartially() {
        if ($scope.notuploaded.length > 0) {
            SessionFactory.setSessionAsDirty();
        } else {
            SessionFactory.setSessionAsClear();
        }
    }

    $scope.$on('$locationChangeStart', function (event, next, current) {
        if (!angular.equals(next, current)) {
            vm.navigationPath = next.split('#')[1].slice(1);
            if (SessionFactory.isSessionDirty()) {
                event.preventDefault();
                if ($scope.notuploaded.length > 0) {
                    popupFactory.showpopup("One or more of your documents has not been uploaded. Select [CANCEL] to go back and upload or [OK] to exit without saving data on this page.", vm.navigationPath);
                } else
                    popupFactory.showpopup(vm.currentpage_errors.renewalNavigation, vm.navigationPath);
                return;
            }
        }
    });
};