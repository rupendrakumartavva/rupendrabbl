(function () {
    'use strict';
    var controllerId = 'PaymentController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', '$routeParams', 'requestService', 'appConstants', 'UtilityFactory', 'errorFactory', 'BBLSubmissionFactory', 'SessionFactory', 'popupFactory', 'authService', PaymentController]);

    function PaymentController($scope, $rootScope, $location, $routeParams, requestService, appConstants, UtilityFactory, errorFactory, BBLSubmissionFactory, SessionFactory, popupFactory, authService) {
        var vm = this;
        vm.examptAllfee = false;
        vm.selectStateDropdown = false;
        vm.paymentdetails = {};
        vm.billingAddress = true;
        vm.currentpage_errors = {};
        vm.isPaymentSubmitted = false;
        SessionFactory.setSessionAsClear();
        vm.validations_wrt_contry = errorFactory.isCountryUS(false);
        init();

        vm.declaration = false;

        /*
    * Function: init
    * init (initialize) method: first method to be executed on controller load.
    */
        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                if (UtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    localStorage.setItem("location", $location.path().split("/")[1]);
                    StreetsDropDown().then(function (response) {
                        vm.Countries = response.data.CountryList;
                        requestService.getStateList({ CountryCode: "US" }).then(function (response) {
                            vm.StatesList = response.data.Status;
                        });
                    }, ErrorWhileProcessing);
                    submissionStatus().then(function (response) {
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        } else {
                            vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                            vm.securitycode = '';
                            requestService.GetPaymentDetailsFromVerification({ masterId: UtilityFactory.getMasterId($routeParams.guid) }).then(function (paymentdetails) {
                                paymentdetails.data.TotalFee = parseFloat(paymentdetails.data.TotalFee);
                                if (paymentdetails.data.TotalFee == 0.00) {
                                    vm.examptAllfee = true;
                                    delete vm.year;
                                    delete vm.cardexpdate;
                                    delete vm.currentMonth;
                                    delete vm.currentyear;
                                } else {
                                    vm.examptAllfee = false;
                                    vm.year = 0;
                                    vm.cardexpdate = 0;
                                    vm.currentMonth = response.data.CreatedDate.split('/')[0];
                                    vm.currentyear = response.data.CreatedDate.split('/')[2];
                                }

                                vm.paymentdetails = paymentdetails.data;
                                $('#dvLoadingSection').css('display', 'none');
                                $("#dvMainsection").css("display", "block");
                            });
                        }
                    }, ErrorWhileProcessing);
                } else {
                    SessionFactory.setSessionAsClear();
                    BBLSubmissionFactory.noGuidAvailable();
                }
            }, function () {
                $location.path("/login");
            });
        }

        function submissionStatus() {
            return requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid));
        }

        function StreetsDropDown() {
            return requestService.streetsDropDown();
        }

        function ErrorWhileProcessing(response) {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error :" + JSON.stringify(response.status));
        };

        $scope.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#error_msg').html('');
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigated to "appchecklist" page.

        //------------------------------------------------------------------
        vm.navToChecklist = function () {
            $location.path('/appchecklist/' + $routeParams.guid);
        }

        vm.navToMybbl = function () {
            $location.path('/mybbl');
        }

        function validatemonth(currentmonth, type) {
            if (vm.cardexpdate > 12 || vm.cardexpdate <= 0) {
                vm.paymentform.cardexpdate.$setValidity('validexpirationdate', false);
            } else {
                if (vm.cardexpdate < currentmonth && type != "checkonlygt12") {
                    vm.paymentform.cardexpdate.$setValidity('validexpirationdate', false);
                } else
                    vm.paymentform.cardexpdate.$setValidity('validexpirationdate', true);
            }
        }

        vm.ValidateDate = function () {
            if (vm.year == vm.currentyear) {
                validatemonth(vm.currentMonth);
            } else if (vm.year > vm.currentyear && vm.year < parseInt(vm.currentyear) + 35) {
                vm.paymentform.cardexpdate.$setValidity('validexpirationdate', true);
                validatemonth(vm.currentMonth, "checkonlygt12");
            } else {
                if (vm.year == 0 || vm.year == undefined) {
                    validatemonth(vm.currentMonth, "checkonlygt12");
                } else {
                    vm.paymentform.cardexpdate.$setValidity('validexpirationdate', false);
                }
            }
        };

        vm.creditcardclear = function () {
            if (vm.cardnumber == undefined) {
                vm.cardnumber = '';
            }
        }

        // Created By       : CodeIT DevTeam
        // Last Update date : 11-09-2015
        // Description      :  This Method is user for selecting the radiobutton.

        //------------------------------------------------------------------

        vm.toggleRadio = function () {
            $("#errormsg").html('');
            vm.country = "US";
            vm.validations_wrt_contry = errorFactory.isCountryUS(true);
        }

        vm.selectcountryoption = function () {
            var cardinfo = {};
            cardinfo.CardType = vm.CardType;
            cardinfo.nameoncard = vm.nameoncard;
            cardinfo.cardnumber = vm.cardnumber;
            cardinfo.cardexpdate = vm.cardexpdate;
            cardinfo.year = vm.year;
            cardinfo.securitycode = vm.securitycode;
            errorFactory.setAllFormControlsEmpty(vm.paymentform);
            vm.billingAddress = false;
            vm.CardType = cardinfo.CardType;
            vm.nameoncard = cardinfo.nameoncard;
            vm.cardnumber = cardinfo.cardnumber;
            vm.cardexpdate = cardinfo.cardexpdate;
            vm.year = cardinfo.year;
            vm.securitycode = cardinfo.securitycode;
            vm.ValidateDate();
            vm.paymentform.zip.$setValidity('customminlength', true);
            vm.paymentform.zip.$setValidity('customlength', true);
            vm.paymentform.telephone.$setValidity('customlength', true);
            vm.paymentform.telephone.$setValidity('customminlength', true);
            vm.diffPayment = {};

            if (vm.country != 'US') {
                vm.selectStateDropdown = true;
            } else {
                vm.selectStateDropdown = false;
            }
            vm.validations_wrt_contry = errorFactory.isCountryUS(!angular.copy(vm.selectStateDropdown));
        };

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigated to "receipt" page.

        //------------------------------------------------------------------

        vm.navToReceipt = function () {
            if (vm.paymentform.$invalid) {
                $('#error_msg').html(vm.currentpage_errors.renewalpaymentallfieldsNotFilled).focus();
            } else {
                redirectToReceipt();
            }
        }

        function redirectToReceipt() {
            paymentDetails();
        }

        function paymentDetails() {
            vm.isPaymentSubmitted = true;
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            vm.paymentData = {};
            if (vm.paymentdetails.TotalFee == 0) {
                vm.CardType = "NA";
            }
            if (vm.billingAddress) {
                vm.paymentData = {
                    MasterId: UtilityFactory.getMasterId($routeParams.guid),
                    TotalAmount: vm.paymentdetails.TotalFee,
                    CardType: vm.CardType,
                    CardName: vm.nameoncard,
                    CardNumber: vm.cardnumber,
                    CardExpDate: vm.cardexpdate,
                    CvvNumber: vm.securitycode,
                    GrandTotal: vm.TotalFee,
                    TotalApplicationFee: vm.applicationFee,
                    TotalLicenseFee: vm.CategoryLicenseFee,
                    TotalEndosementFee: vm.EndorsementFee,
                    TotalTechFee: vm.techFee,

                    FullName: vm.paymentdetails.MailingName,
                    BusinessName: vm.paymentdetails.MailingBName,
                    FullAddress: vm.paymentdetails.MailingAddress,
                    ContactFirstName: vm.paymentdetails.MailingFirstName,
                    ContactMiddleName: vm.paymentdetails.MailingMiddleName,
                    ContactLastName: vm.paymentdetails.MailingLastName,
                    Quadrant: vm.paymentdetails.MailingQuadrant,
                    StreetName: vm.paymentdetails.MailingStreetName,
                    StreetNumber: vm.paymentdetails.MailingStreetNumber,
                    StreetType: vm.paymentdetails.MailingStreetType,

                    UnitNumber: vm.paymentdetails.MailingUnit,
                    City: vm.paymentdetails.MailingCity,
                    Zip: vm.paymentdetails.MailingZip,
                    Telephone: vm.paymentdetails.MailingTelePhone,
                    ContactNumber1: vm.paymentdetails.MailingTelePhone,
                    Email: vm.paymentdetails.MailingEmail,
                    State: vm.paymentdetails.MailingState,
                    Country: vm.paymentdetails.MailingCountry,

                    EmailConfirmation: vm.email,
                    PaymentMailAddress: vm.email,
                    AcceptRules: vm.confirmcheckbox,
                    Signature: vm.signature,
                    IsAggree: vm.declaration,
                    CardExpMonth: vm.cardexpdate,
                    CardExpYear: vm.year
                }
            }
            else {
                vm.paymentData = {
                    MasterId: UtilityFactory.getMasterId($routeParams.guid),
                    TotalAmount: vm.paymentdetails.TotalFee,
                    GrandTotal: vm.TotalFee,
                    TotalApplicationFee: vm.applicationFee,
                    TotalLicenseFee: vm.CategoryLicenseFee,
                    TotalEndosementFee: vm.EndorsementFee,
                    TotalTechFee: vm.techFee,
                    CardType: vm.CardType,
                    CardName: vm.nameoncard,
                    CardNumber: vm.cardnumber,
                    CardExpDate: vm.cardexpdate,
                    CvvNumber: vm.securitycode,
                    FullName: vm.diffPayment.firstname + ' ' + vm.diffPayment.middlename + ' ' + vm.diffPayment.lastname,
                    ContactFirstName: vm.diffPayment.firstname,
                    ContactMiddleName: vm.diffPayment.middlename,
                    ContactLastName: vm.diffPayment.lastname,
                    FullAddress: vm.diffPayment.address1 + ' ' + vm.diffPayment.adress2 + ' ' + vm.diffPayment.address3,
                    StreetName: vm.diffPayment.address1,
                    StreetNumber: vm.diffPayment.adress2,
                    StreetType: vm.diffPayment.address3,
                    UnitNumber: "",
                    Quadrant: "",
                    BusinessName: vm.diffPayment.businessname,
                    City: vm.diffPayment.city,
                    Zip: vm.diffPayment.zip,
                    State: vm.diffPayment.state,
                    Country: vm.country,
                    ContactNumber1: vm.diffPayment.Telephone,
                    Telephone: vm.diffPayment.Telephone,
                    Email: vm.email,
                    PaymentMailAddress: vm.email,
                    EmailConfirmation: vm.email,
                    AcceptRules: vm.confirmcheckbox,
                    FNameSign: vm.signature,
                    CardExpMonth: vm.cardexpdate,
                    CardExpYear: vm.year, Signature: vm.signature,
                    IsAggree: vm.declaration
                }
            }
            vm.paymentData.PaymentType = 'submission';

            requestService.submitPayment(vm.paymentData).then(function (response) {
                if (response.data) {
                    vm.PaymentStatus = response.data.trasactionresult.Success;
                    vm.finalsuccess = response.data.finalsuccess;
                    vm.validateCorp = response.data.validateCorpFileStatus;

                    if (vm.finalsuccess == "YES" && (vm.validateCorp.toLowerCase() == 'active')) {
                        localStorage.setItem("EmailConfirm", vm.email);
                        if (vm.PaymentStatus == true) {
                            $location.path('/receipt/' + $routeParams.guid);
                        } else {
                            $location.path('/paymentfailure/apply/' + $routeParams.guid);
                        }
                    }
                    else {
                        vm.PaymentStatus = true;
                        $location.path('/paymentfailure/apply/' + $routeParams.guid);
                    }
                    vm.isPaymentSubmitted = false;
                }
            }, function (response) {
                $('#dvLoadingSection').css('display', 'none');
                $("#dvMainsection").css("display", "block");
                console.log("Error");
            });
        }

        vm.checkTelephoneMaxLength = function () {
            if (vm.diffPayment.Telephone != undefined) {
                if (vm.diffPayment.Telephone.length > vm.validations_wrt_contry.telephone.maxlength) {
                    vm.paymentform.telephone.$setValidity('customlength', false);
                } else {
                    vm.paymentform.telephone.$setValidity('customlength', true);
                }
                if (vm.diffPayment.Telephone.length < vm.validations_wrt_contry.telephone.minlength) {
                    vm.paymentform.telephone.$setValidity('customminlength', false);
                } else {
                    vm.paymentform.telephone.$setValidity('customminlength', true);
                }
            }
        }

        vm.checkZipMaxLength = function () {
            if (vm.diffPayment.zip != undefined) {
                if (vm.diffPayment.zip.length > vm.validations_wrt_contry.zip.maxlength) {
                    vm.paymentform.zip.$setValidity('customlength', false);
                } else {
                    vm.paymentform.zip.$setValidity('customlength', true);
                }
                if (vm.diffPayment.zip.length < vm.validations_wrt_contry.zip.minlength) {
                    vm.paymentform.zip.$setValidity('customminlength', false);
                } else {
                    vm.paymentform.zip.$setValidity('customminlength', true);
                }
            } else {
                vm.paymentform.zip.$setValidity('customlength', true);
                vm.paymentform.zip.$setValidity('customminlength', true);
            }
        }

        vm.emailconfirmcase = function () {
            if (vm.emailconfirm != undefined) {
                if (!angular.equals(vm.emailconfirm.toLowerCase(), vm.email.toLowerCase())) {
                    vm.paymentform.emailconfirm.$setValidity('mismatch', false);
                } else {
                    vm.paymentform.emailconfirm.$setValidity('mismatch', true);
                }
            } else {
                vm.paymentform.emailconfirm.$setValidity('mismatch', true);
            }
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (vm.isPaymentSubmitted) {
                event.preventDefault();
            }
        });
    }
})();