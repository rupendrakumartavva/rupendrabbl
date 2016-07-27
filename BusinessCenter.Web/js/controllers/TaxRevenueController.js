(function () {

    'use strict';
    var controllerId = 'TaxRevenueController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', 'appConstants', '$routeParams', 'UtilityFactory', 'SessionFactory', 'BBLSubmissionFactory', 'popupFactory', 'errorFactory', 'authService','$window', TaxRevenueController]);

    function TaxRevenueController($scope, $rootScope, $location, requestService, appConstants, $routeParams, UtilityFactory, SessionFactory, BBLSubmissionFactory, popupFactory, errorFactory, authService, $window) {

        var vm = this;
        vm.taxrevenue = {};
        vm.redirectionpath = '';
        vm.verifybutton = false;
        vm.cleanhandsverified = '';
        vm.isSubmitionType = false;
        vm.DocumentsUploadType = false;
        vm.signatureMismatch = false;
        vm.prevData = {};
        vm.SelfCertificationDate = null;
        SessionFactory.setSessionAsDirty();
        init();


        //---masking FEIN and SSN Numbers---

        //$("#feinnumber").mask("99-9999999");
        //$("#ssnnumber").mask("999-99-9999");

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
                            $location.path('/mybbl');
                        } else {
                            vm.isfein = response.data.IsFEIN;
                            vm.applicationType = response.data.AppType;
                            vm.SelfCertificationDate = response.data.CreatedDate;
                            vm.bOwnerName = response.data.BusinessName;
                            getTaxrevenueData().then(function (response) {
                                vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                                //vm.bOwnerName = response.data.BusinessName;
                                vm.tradeName = response.data.tradeName;
                                vm.PremisesAddress = response.data.primisessAddress;
                                if (response.data.taxrevenue.length > 0) {
                                    vm.taxrevenue.FullName = response.data.bOwnerName;
                                    vm.taxrevenue.declaration = response.data.taxrevenue[0].IsIAgree;
                                    vm.taxrevenue.number = response.data.taxrevenue[0].TaxRevenueNumber;
                                    vm.taxrevenue.FullName = response.data.taxrevenue[0].FullName;

                                    vm.taxrevenue.BusinessOwnerRoles = response.data.taxrevenue[0].BusinessOwnerRoles;
                                    vm.taxrevenue.signature = response.data.taxrevenue[0].FullName;
                                }
                                vm.prevData = angular.copy(vm.taxrevenue);
                                $('#dvLoadingSection').css('display', 'none');
                                $("#dvMainsection").css("display", "block");
                            }, ErrorWhileProcessing);
                        }
                    });
                } else {
                    BBLSubmissionFactory.invalidSubmission();
                }
            }, function () {
                $location.path("/login");
            });
        }

        function getTaxrevenueData() {
            return requestService.getTaxRevenuData({ 'MasterId': UtilityFactory.getMasterId($routeParams.guid) });
        }

        var numberlength = {
            's': 11,
            'f': 10
        };

        var regex = new RegExp(/^[0-9-]+$/);



        vm.applyMask = function (e, type) {
            if (vm.taxrevenue.number != undefined) {
                delete vm.taxrevenue.isnumbervalid;
                if (type == 's') {
                    if ((vm.taxrevenue.number.length == 3 || vm.taxrevenue.number.length == 6) && e.keyCode != 8)
                        vm.taxrevenue.number += '-';
                } else {
                    if (vm.taxrevenue.number.length == 2 && e.keyCode != 8)
                        vm.taxrevenue.number += '-';
                }
            }
        }

        $scope.$watch('vm.taxrevenue.number', function () {
            if (angular.isDefined(vm.taxrevenue.number)) {
                if (regex.test(vm.taxrevenue.number)) {
                    vm.taxrevenueform.taxnumber.$setValidity('pattern', true);

                    if (vm.isfein) {
                        if (vm.taxrevenue.number.lastIndexOf('-') != 2 || vm.taxrevenue.number.length != 10) {
                            vm.taxrevenueform.taxnumber.$setValidity('minlength', false);
                        } else {
                            if ((vm.taxrevenue.number.match(/-/g) || []).length > 1) {
                                vm.taxrevenueform.taxnumber.$setValidity('minlength', false);
                            } else {
                                vm.taxrevenueform.taxnumber.$setValidity('minlength', true);
                            }
                        }
                    } else {
                        if (vm.taxrevenue.number.indexOf('-') != 3 || vm.taxrevenue.number.lastIndexOf('-') != 6 || vm.taxrevenue.number.length != 11) {
                            vm.taxrevenueform.taxnumber.$setValidity('minlength', false);
                        } else {
                            if ((vm.taxrevenue.number.match(/-/g) || []).length > 2) {
                                vm.taxrevenueform.taxnumber.$setValidity('minlength', false);
                            } else
                                vm.taxrevenueform.taxnumber.$setValidity('minlength', true);
                        }
                    }


                    //if (vm.isfein) {
                    //    if (vm.taxrevenue.number.length < 10) {
                    //        vm.taxrevenueform.taxnumber.$setValidity('minlength', false);
                    //    } else {
                    //        vm.taxrevenueform.taxnumber.$setValidity('minlength', true);
                    //    }
                    //} else {
                    //    if (vm.taxrevenue.number.length < 11) {
                    //        vm.taxrevenueform.taxnumber.$setValidity('minlength', false);
                    //    } else {
                    //        vm.taxrevenueform.taxnumber.$setValidity('minlength', true);
                    //    }
                    //}
                } else {
                    vm.taxrevenueform.taxnumber.$setValidity('pattern', false);
                }
            }
        });




        vm.checkAndExit = function (path, submit) {
            vm.redirectionpath = path;
            if (path.indexOf('app') != -1) {
                vm.redirectionpath = path + '/' + $routeParams.guid;
            } else {
                vm.redirectionpath = path;
            }

            if (vm.taxrevenueform.$invalid) {
                if (angular.equals(vm.taxrevenue, vm.prevData)) {
                    if (!vm.taxrevenue.declaration && angular.isDefined(vm.taxrevenue.number)) {
                        popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.redirectionpath);
                    } else {
                        SessionFactory.setSessionAsClear();
                        $location.path('/' + vm.redirectionpath);
                    }
                }
                else {
                    popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.redirectionpath);
                }
            } else {
                submitTaxes(vm.redirectionpath);
            }
        }

        vm.navigateAnyWay = function () {
            SessionFactory.setSessionAsClear();
            $("#taxRevenueMessage").modal('hide');
            $location.path('/' + vm.redirectionpath);
        }

        vm.checkSignWithFullName = function () {
            if (angular.isDefined(vm.taxrevenue.signature)) {
                if (!angular.equals(vm.taxrevenue.signature, vm.taxrevenue.FullName)) {
                    vm.signatureMismatch = true;
                    vm.taxrevenueform.signature.$setValidity('required', false);
                } else {
                    vm.signatureMismatch = false;
                    vm.taxrevenueform.signature.$setValidity('required', true);
                }
            } else {
                if (angular.isUndefined(vm.taxrevenue.FullName)) {
                    vm.signatureMismatch = false;
                }
            }
        }


        function submitTaxes(path) {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            vm.redirectionpath = path;
            var revenueType = '';
            vm.isfein ? revenueType = 'FEIN' : revenueType = 'SSN';

            verifyNumberFromDB({
                "TaxRevenueFFIN": vm.taxrevenue.number,
                MasterId: UtilityFactory.getMasterId($routeParams.guid),
                TaxRevenueType: revenueType,
                "BusinessOwnerRoles": vm.taxrevenue.BusinessOwnerRoles,
                "FullName": vm.taxrevenue.FullName,
                IsIAgree: vm.taxrevenue.declaration
            }).then(VerificationFromDB, ErrorWhileProcessing);
        }

        function VerificationFromDB(response) {
            vm.taxrevenue.isnumbervalid = response.data.status;
            if (vm.redirectionpath != '') {
                SessionFactory.setSessionAsClear();
                $location.path('/' + vm.redirectionpath);
            }
        }

        function ErrorWhileProcessing(response) {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error" + response.status);
        }

        function verifyNumberFromDB(data) {
            return requestService.validateTaxRevenue(data);
        }

        vm.stayOnThisPage = function () {
            $('#taxRevenueMessage').modal('hide');
        }

        vm.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#validatename_error').html('');
        }
        vm.navToBack = function () {
            $window.history.back();
        }
        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (vm.taxrevenue.declaration == false) {
                vm.taxrevenue.declaration = undefined;
            }
            if (!angular.equals(next, current)) {
                SessionFactory.compareObjectsInCurrentSession(vm.taxrevenue, vm.prevData);
                if (SessionFactory.isSessionDirty()) {
                    vm.redirectionpath = next.split('#')[1].slice(1);
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.navigateaway, vm.redirectionpath);
                    return;
                }
            }
        });
    }
})();