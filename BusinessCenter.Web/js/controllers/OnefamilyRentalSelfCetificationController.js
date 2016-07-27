(function () {
    'use strict';
    var controllerId = 'OnefamilyRentalSelfCetificationController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', 'appConstants', '$routeParams', 'UtilityFactory', 'BBLSubmissionFactory', 'errorFactory', 'SessionFactory', 'popupFactory', 'authService', '$window', OnfamilyRentalSelfCetificationController]);

    function OnfamilyRentalSelfCetificationController($scope, $rootScope, $location, requestService, appConstants, $routeParams, UtilityFactory, BBLSubmissionFactory, errorFactory, SessionFactory, popupFactory, authService, $window) {
        var vm = this;
        vm.isPropertyOccupied = '';
        vm.confirm = '';
        vm.selfCertification = {};
        vm.previousObj = {};
        vm.signatureMismatch = false;
        //vm.SelfCertificationDate = new Date().getMonth() + 1 + "/" + new Date().getDate() + "/" + new Date().getFullYear();
        vm.SelfCertificationDate = null;
        vm.IsAgree = false;
        //vm.fullName = '';
        vm.currentpage_errors = {};
        SessionFactory.setSessionAsDirty();
        init();

        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                if (UtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid)).then(function (response) {
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        } else {
                            //vm.selfCertification.FullName = response.data.BusinessOwnerName;
                            //vm.fullName = response.data.BusinessOwnerName;

                            vm.SelfCertificationDate = response.data.CreatedDate;
                            vm.previousObj = angular.copy(vm.selfCertification);
                            requestService.getSelfCertificationDetailsByMasterId(UtilityFactory.getSubmissionStatusObject($routeParams.guid)).then(function (response) {
                                vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                                if (response.data.length > 0) {
                                    vm.selfCertification = response.data[0];
                                    if (vm.selfCertification.FullName != undefined) {
                                        //vm.selfCertification.FullName = vm.fullName;
                                        vm.selfCertification.confirm = response.data[0].IsAgree;
                                        vm.selfCertification.signature = vm.selfCertification.FullName;
                                        if (!response.data[0].IsAgree) {
                                            vm.selfCertification.signature = '';
                                        }
                                    }
                                    vm.previousObj = angular.copy(vm.selfCertification);
                                }
                                $('#dvLoadingSection').css('display', 'none');
                                $("#dvMainsection").css("display", "block");
                            }, onFetchingDataError);
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

        vm.navToMailingAddress = function () {
            if (vm.onefamilyrental.$invalid) {
                $('#error_msg').html('Please fill all the required fields').focus();
            } else {
                vm.checkAndExit('physicallocation/address/' + $routeParams.guid);
            }
        };

        vm.checkSignWithFullName = function () {
            if (angular.isDefined(vm.selfCertification.signature)) {
                if (!angular.equals(vm.selfCertification.signature, vm.selfCertification.FullName)) {
                    vm.signatureMismatch = true;
                    vm.onefamilyrental.signature.$setValidity('required', false);
                } else {
                    vm.signatureMismatch = false;
                    vm.onefamilyrental.signature.$setValidity('required', true);
                }
            } else {
                vm.signatureMismatch = false;
            }
        }

        //function checkIfUndefined() {
        //    var flag = 0;
        //    var keys = Object.keys(vm.selfCertification);
        //    for (var i = 0; i < keys.length; i++) {
        //        if (vm.selfCertification[keys[i]] == undefined)
        //            flag++;
        //    }
        //    if (flag == keys.length)
        //        return true;
        //    return false;
        //}

        vm.checkAndExit = function (path, submit) {
            if (path.indexOf('check') != -1)
                vm.navigationPath = path + "/" + $routeParams.guid;
            else
                vm.navigationPath = path;

            if (vm.onefamilyrental.$invalid) {
                if (SessionFactory.isFormEmpty(vm.onefamilyrental)) {
                    SessionFactory.setSessionAsClear();
                    $location.path("/" + vm.navigationPath);
                } else {
                    SessionFactory.setSessionAsDirty();
                    popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigationPath);
                }
            } else {
                vm.selfCertification.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                vm.selfCertification.SelfCertificationOn = vm.SelfCertificationDate;
                vm.selfCertification.IsAgree = true;
                requestService.submitSelfCertificationDetails(vm.selfCertification).then(function () {
                    SessionFactory.setSessionAsClear();
                    $location.path("/" + vm.navigationPath);
                }, onFetchingDataError);
            }

            //if (Object.keys(vm.selfCertification).length > 1 && (checkIfUndefined() || vm.onefamilyrental.$invalid || vm.selfCertification.IsPropertyOccupied == undefined || vm.selfCertification.confirm == false)) {
            //    $('#onefamilySelfcertification .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.incompleteData + "</h3>");
            //    $('#onefamilySelfcertification').modal('show');
            //} else if (Object.keys(vm.selfCertification).length == 1) {
            //    vm.navigate = true;
            //    $location.path("/" + vm.navigationPath);
            //} else {
            //}
        }

        function onFetchingDataError(response) {
            console.log("Error");
        }

        vm.navigateAnyway = function () {
            vm.navigate = true;
            $("#onefamilySelfcertification").modal('hide');
            $location.path('/' + vm.navigationPath);
        }

        vm.dontNavigate = function () {
            $("#onefamilySelfcertification").modal('hide');
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (vm.selfCertification.confirm == false) {
                vm.selfCertification.confirm = undefined;
            }
            if (!angular.equals(next, current)) {
                SessionFactory.compareObjectsInCurrentSession(vm.selfCertification, vm.previousObj);
                if (SessionFactory.isSessionDirty()) {
                    vm.navigationPath = next.split('#')[1].slice(1);
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.navigateaway, vm.navigationPath);
                }
                //if (!vm.navigate && !angular.equals(JSON.stringify(vm.previousObj), JSON.stringify(vm.selfCertification))) {
                //    $('#onefamilySelfcertification .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.navigateaway + "</h3>");
                //    $("#onefamilySelfcertification").modal('show');
                //    return;
                //}
            }
        });
        vm.navToBack = function () {
            $window.history.back();
        }
    }
})();