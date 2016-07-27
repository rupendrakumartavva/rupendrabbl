(function () {
    'use strict';
    var controllerId = 'IndividualBusinessLicenseController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', '$routeParams', 'UtilityFactory', 'BBLSubmissionFactory', 'SessionFactory', 'popupFactory', 'errorFactory', 'authService', '$window', IndividualBusinessLicenseController]);

    function IndividualBusinessLicenseController($scope, $rootScope, $location, requestService, $routeParams, UtilityFactory, BBLSubmissionFactory, SessionFactory, popupFactory, errorFactory, authService, $window) {

        var vm = this;
        vm.businessLicenseData = '';
        vm.navigationPath = '';
        vm.navigate = false;
        vm.BusinessNameDisable = false;
        vm.prevObj = {};
        vm.individual = {};
        vm.validateCountry = true;
        vm.businessname = null;
        SessionFactory.setSessionAsDirty();
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
                    submissionStatus().then(function (response) {
                        vm.businessname = response.data.BusinessName;
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        } else {
                            StreetsDropDown().then(function (response) {
                                vm.StreetTypes = response.data.StreetList;
                                vm.Countries = response.data.CountryList;
                                getStatesList().then(function (response) {
                                    vm.individual.Country = "US";
                                    vm.StatesList = response.data.Status;
                                    getPreviousData().then(function (response) {
                                        vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                                        if (angular.isDefined(response.data[0])) {
                                            vm.individual = response.data[0];
                                        }
                                        vm.selectcountryoption(true);
                                        vm.prevObj = angular.copy(vm.individual);
                                        $('#dvLoadingSection').css('display', 'none');
                                        $("#dvMainsection").css("display", "block");
                                    }, ErrorWhileProcessing);
                                }, ErrorWhileProcessing);
                            }, ErrorWhileProcessing);
                        }
                    }, ErrorWhileProcessing);
                } else {
                    SessionFactory.setSessionAsClear();
                    BBLSubmissionFactory.noGuidAvailable();
                }
            }, function () {
                $location.path("/login");
            });
        };

        function getStatesList() {
            return requestService.getStateList({ CountryCode: "US" });
        }

        function submissionStatus() {
            return requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid));
        }

        function StreetsDropDown() {
            return requestService.streetsDropDown();
        }

        function getPreviousData() {
            return requestService.GetSubIndividuals({ MasterId: UtilityFactory.getMasterId($routeParams.guid) });
        }

        function ErrorWhileProcessing(response) {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error");
        }

        vm.ChcekingLicenceNumber = function (id) {
            $('#' + id).html('');
            // $('#testID').prop('disabled', false);
            //$('#CompanyNameinfo').prop('disabled', true);
        }

        $scope.$watch('vm.individual.DateofBirth', function () {
            if (angular.isUndefined(vm.individual.DateofBirth)) {
                vm.update_form.datepicker.$setValidity("invalidDate", true);
            } else {
                var tempDate = "";
                var now = new Date();
                if (now.getDate() == 31) {
                    tempDate = new Date(now.getMonth() + 2 + '/' + parseInt(1) + '/' + parseInt(now.getFullYear() - 21));
                } else {
                    tempDate = new Date(now.getMonth() + 1 + '/' + parseInt(now.getDate() + 1) + '/' + parseInt(now.getFullYear() - 21));
                }
                var enteredDate = new Date(vm.individual.DateofBirth);
                vm.update_form.datepicker.$setValidity("invalidDate", enteredDate <= tempDate);
            }
        });

        $scope.$watch('vm.individual.ExpirationDate', function () {
            var currDate = new Date(new Date().getMonth() + 1 + '/' + parseInt(new Date().getDate()) + '/' + parseInt(new Date().getFullYear()));
            var nextMonth = new Date();
            var now = new Date();
            if (now.getMonth() == 11) {
                var nextMonth = new Date(new Date(1 + '/' + parseInt(new Date().getDate() + 1) + '/' + parseInt(now.getFullYear() + 1)));
            } else {
                var nextMonth = new Date(new Date().getMonth() + 2 + '/' + parseInt(new Date().getDate() + 1) + '/' + parseInt(new Date().getFullYear()));
            }

            var enteredDate = new Date(vm.individual.ExpirationDate);
            if (enteredDate < nextMonth && enteredDate > currDate) {
                $("#warning").html('License/Identification Card # will expire within the next 30 days. Your submitted application must contain a valid license.');
            } else if (enteredDate < currDate) {
                $("#warning").html('License/Identification Card # has expired. You may not complete the application with an expired license.');
            } else if (enteredDate.toString() == currDate.toString()) {
                $("#warning").html('License/Identification Card # is expiring today.  Your submitted application must contain a valid license.');
            } else {
                $("#warning").html('');
            }
        });

        $scope.beforeRender = function ($view, $dates, $leftDate, $upDate, $rightDate) {
            var tempDate = new Date(new Date().getMonth() + 1 + '/' + parseInt(new Date().getDate() + 1) + '/' + parseInt(new Date().getFullYear() - 21));
            var localOffset = tempDate.getTimezoneOffset() * 60000;
            var utcDateValue = tempDate.getTime();
            for (var i = 0; i < $dates.length; i++) {
                if ($dates[i].utcDateValue >= utcDateValue) {
                    $dates[i].selectable = false;
                }
            }
        }

        $scope.beforeExpirationRender = function ($view, $dates, $leftDate, $upDate, $rightDate) {
            var tempDate = new Date(new Date().getMonth() + 1 + '/' + parseInt(new Date().getDate() + 1) + '/' + parseInt(new Date().getFullYear()));
            var localOffset = tempDate.getTimezoneOffset() * 60000;
            var utcDateValue = tempDate.getTime();
            for (var i = 0; i < $dates.length; i++) {
                if ($dates[i].utcDateValue <= utcDateValue) {
                    $dates[i].selectable = false;
                }
                var tempval = $upDate.display.split('-');
                if (tempval.length > 1) {
                    if (!isNaN(tempval[1])) {
                        if (new Date($dates[i].utcDateValue).getFullYear() >= tempDate.getFullYear()) {
                            $dates[i].selectable = true;
                        }
                    }
                } else {
                    if (new Date($dates[i].utcDateValue).getMonth() >= tempDate.getMonth()) {
                        $dates[i].selectable = true;
                    }
                }
            }
        }

        vm.setErrorMsg = function (id) {
            vm.businessLicenseData = ''
            $('#' + id).html('');
        }

        function validateLicenceNumber() {
            return requestService.ValidateLicenceNum({ CompanyBusinessLicense: vm.individual.CompanyBusinessLicense });
        }

        vm.checklicense = function () {
            if (vm.individual.CompanyBusinessLicense != undefined && angular.isDefined(vm.individual) && vm.individual.CompanyBusinessLicense.length >= 2) {
                validateLicenceNumber().then(function (response) {
                    if (response.data.Status == "NoData" || response.data.Status == "InActive" || response.data.Status == '') {
                        vm.businessLicenseData = angular.copy(vm.individual.CompanyBusinessLicense);
                        delete vm.individual;
                    }
                    else {
                        vm.businessLicenseData = '';
                        vm.BusinessNameDisable = true;
                        vm.individual.CompanyName = response.data.Status;
                    }
                }, ErrorWhileProcessing);
            }
        }

        $scope.$watch('vm.individual.CompanyBusinessLicense', function () {
            if (angular.isDefined(vm.individual) && angular.isUndefined(vm.individual.CompanyBusinessLicense)) {
                vm.individual.CompanyName = undefined;
            }
        });



        function submissionIndividualDelete() {
            return requestService.SubmissionIndividualDelete({ MasterId: UtilityFactory.getMasterId($routeParams.guid) });
        }

        function submissionIndividualBusiness() {
            return requestService.SubmissionIndividualBusiness(vm.individual);
        }

        vm.selectcountryoption = function (frominit) {
            if (!frominit) {
                vm.individual.State_Province = "";
                vm.individual.City = null;
            }
            if (vm.individual.Country != 'US') {
                vm.validateCountry = false;
            } else {
                vm.validateCountry = true;
            }
        };

        function isSessionIncomplete() {
            if (SessionFactory.isSessionDirty()) {
                if (angular.isDefined(vm.individual) && !angular.equals(JSON.stringify(vm.individual), JSON.stringify(vm.prevObj))) {
                    SessionFactory.setSessionAsDirty();
                } else {
                    SessionFactory.setSessionAsClear();
                }
            }
        }

        vm.checkAndExit = function (path) {
            vm.submitted = true;
            if (path.indexOf('app') != -1) {
                vm.navigationPath = path + '/' + $routeParams.guid;
            } else {
                vm.navigationPath = path
            }
            if (vm.update_form.$invalid) {
                if (SessionFactory.isFormEmpty(vm.update_form)) {
                    $('#dvLoadingSection').css('display', 'block');
                    $("#dvMainsection").css("display", "none");
                    SessionFactory.setSessionAsClear();
                    submissionIndividualDelete().then(function (response) {
                        $location.path('/' + vm.navigationPath);
                    });
                } else {
                    popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigationPath);
                }
            } else {
                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                validateLicenceNumber().then(function (response) {
                    if (response.data.Status == "NoData" || response.data.Status == "InActive") {
                        vm.businessLicenseData = angular.copy(vm.individual.CompanyBusinessLicense);
                        delete vm.individual;
                        $location.path('/' + vm.navigationPath);
                    } else if (vm.update_form.$invalid) {
                        $('#dvLoadingSection').css('display', 'none');
                        $("#dvMainsection").css("display", "block");
                        popupFactory.showpopup(vm.currentpage_errors.incompleteData, vm.navigationPath);
                        return;
                    } else {
                        $('#dvLoadingSection').css('display', 'block');
                        $("#dvMainsection").css("display", "none");
                        SessionFactory.setSessionAsClear();
                        vm.individual.MasterId = UtilityFactory.getMasterId($routeParams.guid);
                        vm.individual.UserId = localStorage.userId;
                        vm.individual.FirstName = vm.businessname;
                        submissionIndividualBusiness().then(function (response) {
                            if (response.data > 0) {
                                $('#dvLoadingSection').css('display', 'none');
                                $("#dvMainsection").css("display", "block");
                                $location.path('/' + vm.navigationPath);
                            }
                        }, ErrorWhileProcessing);
                    }
                }, ErrorWhileProcessing);
            }
        }
        vm.navToBack = function () {
            $window.history.back();
        }
        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                isSessionIncomplete();
                if (SessionFactory.isSessionDirty()) {
                    vm.navigationPath = next.split('#')[1].slice(1);
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.navigateaway, vm.navigationPath);
                    return;
                }

            }
        });

    }

})();