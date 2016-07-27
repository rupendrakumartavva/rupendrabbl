(function () {

    'use strict';
    var controllerId = 'RenewalPaymentController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', '$routeParams', 'requestService', 'appConstants', 'RenewalUtilityFactory', 'errorFactory', 'popupFactory', 'SessionFactory', 'authService', RenewalPaymentController]);

    function RenewalPaymentController($scope, $rootScope, $location, $routeParams, requestService, appConstants, RenewalUtilityFactory, errorFactory, popupFactory, SessionFactory, authService) {
        var vm = this;
        vm.paymentBillingDetails = {};
        vm.examptAllfee = false;
        vm.declaration = false;
        vm.selectStateDropdown = false;
        vm.isPaymentSubmitted = false;
        SessionFactory.setSessionAsDirty();
        vm.validations_wrt_contry = errorFactory.isCountryUS(false);

        init();


        /*
    * Function: init
    * init (initialize) method: first method to be executed on controller load. 
    */
        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                localStorage.setItem("location", $location.path().split("/")[1]);

                if (RenewalUtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    StreetsDropDown().then(function (response) {
                        vm.Countries = response.data.CountryList;
                        requestService.getStateList({ CountryCode: "US" }).then(function (response) {
                            vm.StatesList = response.data.Status;
                            requestService.checkAmount(RenewalUtilityFactory.getRenewalObject($routeParams.guid)).then(function (resp) {
                                vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                                vm.billingaddress = true;
                                vm.paymentBillingDetails = resp.data;
                                vm.securitycode = '';
                                vm.paymentBillingDetails.GrandTotalAmount = angular.copy(parseFloat(vm.paymentBillingDetails.GrandTotalAmount));
                                if (vm.paymentBillingDetails.GrandTotalAmount == 0) {
                                    vm.examptAllfee = true;
                                } else {
                                    vm.examptAllfee = false;
                                    vm.year = 0;
                                    vm.cardexpdate = 0;
                                    vm.currentMonth = resp.data.CurrentDate.split('/')[0];
                                    vm.currentyear = resp.data.CurrentDate.split('/')[2];
                                }
                                $('#dvLoadingSection').css('display', 'none');
                                $("#dvMainsection").css("display", "block");
                            }, ErrorWhileProcessing);
                        });
                    }, ErrorWhileProcessing);
                }
                else {
                    RenewalUtilityFactory.noGuid();
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
        // Description      : This Method is navigated to "mybbl" page. 
        //------------------------------------------------------------------
        vm.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#error_msg').html('');
        }

        vm.creditcardclear = function () {
            if (vm.cardnumber == undefined) {
                vm.cardnumber = '';
            }
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

        //-------------------------------------------------------------------
        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigated to "mybbl" page. 

        //------------------------------------------------------------------
        vm.navToMyBBLs = function () {
            $location.path('/mybbl');
        }


        vm.cancelNavigation = function () {
            $('#renewPopUp').modal('hide');
        }

        vm.navigateAnyWay = function () {
            SessionFactory.setSessionAsClear();
            $('#renewPopUp').modal('hide');
            $location.path("/mybbl");
        }

        function ErrorWhileProcessing(response) {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error");
        }

        vm.navToBblsHome = function () {
            $location.path('/mybbl');
        }

        // Created By       : CodeIT DevTeam
        // Last Update date : 11-09-2015
        // Description      :  This Method is user for selecting the radiobutton.

        //------------------------------------------------------------------
        vm.toggleRadio = function (result) {
            $("#errormsg").html('');
            vm.country = "US";
            vm.validations_wrt_contry = errorFactory.isCountryUS(true);
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigated to "receipt" page. 

        //------------------------------------------------------------------
        vm.navToReceipt = function () {
            if (vm.paymentform.$invalid)
                $('#error_msg').html(vm.currentpage_errors.renewalpaymentallfieldsNotFilled).focus();
            else
                redirectToReceipt();
        }

        function redirectToReceipt() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            vm.isPaymentSubmitted = true;
            vm.paymentData = {};
            if (vm.paymentBillingDetails.GrandTotalAmount == 0) {
                vm.CardType = "NA";
            }
            if (vm.billingaddress) {
                vm.paymentData = vm.paymentBillingDetails;
                vm.paymentData = {
                    MasterId: vm.paymentBillingDetails.MasterId,
                    TotalAmount: vm.paymentBillingDetails.GrandTotalAmount,
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
                    ContactFirstName: vm.paymentData.ContactFirstName,
                    ContactMiddleName: vm.paymentData.ContactMiddleName,
                    ContactLastName: vm.paymentData.ContactLastName,
                    FullName: vm.paymentData.NameofLicense,
                    BusinessName: vm.paymentData.BusinessOwnerName,
                    FullAddress: vm.paymentData.BblAddress,
                    State: vm.paymentData.BblState,
                    City: vm.paymentData.BblCity,
                    Zip: vm.paymentData.BblZip,
                    StreetName: vm.paymentData.MailingStreetName,
                    StreetNumber: vm.paymentData.MailingStreetNumber,
                    StreetType: vm.paymentData.MailingStreetType,
                    Telephone: vm.paymentData.MailingTelePhone,
                    Email: vm.paymentData.MailingEmail,
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
                    MasterId: vm.paymentBillingDetails.MasterId,
                    TotalAmount: vm.paymentBillingDetails.GrandTotalAmount,
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
                    FullName: vm.diffRenewalPayment.firstname + ' ' + vm.diffRenewalPayment.middlename + ' ' + vm.diffRenewalPayment.lastname,
                    FullAddress: vm.diffRenewalPayment.address1 + ' ' + vm.diffRenewalPayment.adress2 + ' ' + vm.diffRenewalPayment.address3,
                    BusinessName: vm.diffRenewalPayment.businessname,
                    ContactFirstName: vm.diffRenewalPayment.firstname,
                    ContactMiddleName: vm.diffRenewalPayment.middlename,
                    ContactLastName: vm.diffRenewalPayment.lastname,
                    StreetName: vm.diffRenewalPayment.address1,
                    StreetNumber: vm.diffRenewalPayment.adress2,
                    StreetType: vm.diffRenewalPayment.address3,
                    City: vm.diffRenewalPayment.city,
                    Zip: vm.diffRenewalPayment.zip,
                    State: vm.diffRenewalPayment.state,
                    Country: vm.country,
                    ContactNumber1: vm.diffRenewalPayment.Telephone,
                    Email: vm.email,
                    Telephone: vm.diffRenewalPayment.Telephone,
                    PaymentMailAddress: vm.email,
                    EmailConfirmation: vm.email,
                    AcceptRules: vm.confirmcheckbox,
                    FNameSign: vm.signature,
                    CardExpMonth: vm.cardexpdate,
                    Signature: vm.signature,
                    CardExpYear: vm.year
                }
            }
            vm.paymentData.PaymentType = 'renewal';
            requestService.submitPayment(vm.paymentData).then(function (response) {
                SessionFactory.setSessionAsClear();
                if (response.data) {
                    vm.PaymentStatus = response.data.trasactionresult.Success;
                    localStorage.setItem("PaymentId", response.data.paymentId);
                    if (vm.PaymentStatus == true) {
                        localStorage.setItem("EmailConfirm", vm.email);
                        $location.path('/renewalreceipt/' + $routeParams.guid);
                    }
                    else {
                        $location.path('/paymentfailure/renewal/' + $routeParams.guid);
                    }
                    vm.isPaymentSubmitted = false;
                }
            }, ErrorWhileProcessing);

        }

        vm.emailconfirmcase = function () {
            if (vm.emailconfirm != undefined) {
                if (!angular.equals(vm.emailconfirm.toLowerCase(), vm.email.toLowerCase())) {
                    vm.paymentform.confirmemail.$setValidity('mismatch', false);
                } else {
                    vm.paymentform.confirmemail.$setValidity('mismatch', true);
                }
            } else {
                vm.paymentform.confirmemail.$setValidity('mismatch', true);
            }
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
            vm.CardType = cardinfo.CardType;
            vm.nameoncard = cardinfo.nameoncard;
            vm.cardnumber = cardinfo.cardnumber;
            vm.cardexpdate = cardinfo.cardexpdate;
            vm.year = cardinfo.year;
            vm.securitycode = cardinfo.securitycode;
            vm.billingaddress = false;
            vm.diffRenewalPayment = {};
            vm.ValidateDate();
            vm.paymentform.zip.$setValidity('customminlength', true);
            vm.paymentform.zip.$setValidity('customlength', true);
            vm.paymentform.telephone.$setValidity('customlength', true);
            vm.paymentform.telephone.$setValidity('customminlength', true);

            if (vm.country != 'US') {
                vm.selectStateDropdown = true;
            } else {
                vm.selectStateDropdown = false;
            }
            vm.validations_wrt_contry = errorFactory.isCountryUS(!angular.copy(vm.selectStateDropdown));
        };

        vm.checkZipMaxLength = function () {
            if (vm.diffRenewalPayment.zip != undefined) {
                if (vm.diffRenewalPayment.zip.length > vm.validations_wrt_contry.zip.maxlength) {
                    vm.paymentform.zip.$setValidity('customlength', false);
                } else {
                    vm.paymentform.zip.$setValidity('customlength', true);
                }
                if (vm.diffRenewalPayment.zip.length < vm.validations_wrt_contry.zip.minlength) {
                    vm.paymentform.zip.$setValidity('customminlength', false);
                } else {
                    vm.paymentform.zip.$setValidity('customminlength', true);
                }
            } else {
                vm.paymentform.zip.$setValidity('customlength', true);
                vm.paymentform.zip.$setValidity('customminlength', true);
            }
        }

        vm.checkTelephoneMaxLength = function () {
            if (vm.diffRenewalPayment.Telephone != undefined) {
                if (vm.diffRenewalPayment.Telephone.length > vm.validations_wrt_contry.telephone.maxlength) {
                    vm.paymentform.telephone.$setValidity('customlength', false);
                } else {
                    vm.paymentform.telephone.$setValidity('customlength', true);
                }
                if (vm.diffRenewalPayment.Telephone.length < vm.validations_wrt_contry.telephone.minlength) {
                    vm.paymentform.telephone.$setValidity('customminlength', false);
                } else {
                    vm.paymentform.telephone.$setValidity('customminlength', true);
                }
            }
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (vm.isPaymentSubmitted) {
                event.preventDefault();
            } else {
                if (!angular.equals(next, current)) {
                    vm.navigationPath = next.split('#')[1].slice(1);
                    if (SessionFactory.isSessionDirty()) {
                        event.preventDefault();
                        popupFactory.showpopup(vm.currentpage_errors.renewalNavigation, vm.navigationPath);
                        return;
                    }
                }
            }
        });
    }
})();