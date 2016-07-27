(function () {
    'use strict';
    var controllerId = 'InformationVerificationController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', '$routeParams', 'UtilityFactory', 'appConstants', 'BBLSubmissionFactory', 'errorFactory', 'SessionFactory', 'authService', InformationVerificationController]);

    function InformationVerificationController($scope, $rootScope, $location, requestService, $routeParams, UtilityFactory, appConstants, BBLSubmissionFactory, errorFactory, SessionFactory, authService) {
        var vm = this;
        $('.panel-body >h1').each(function (ndx, ele) {
            if (ele.offsetWidth < ele.scrollWidth) {
                $(ele).css('font-size', Math.ceil($(ele).width() / 10) + 5);
            }
        });
        vm.showNote = '';
        vm.SubVerfication = {};
        vm.addressType = false;
        vm.addressname = false;
        vm.AddressType = 0;
        vm.submissionStatusData = {};
        vm.currentpage_errors = {};
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
                if (UtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    requestService.SubmissionStatus(UtilityFactory.getSubmissionStatusObject($routeParams.guid)).then(function (response) {
                        vm.submissionStatusData = response.data;
                        vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                        if (BBLSubmissionFactory.checkSubmissionStatus(response.data)) {
                            BBLSubmissionFactory.invalidSubmission();
                        } else {
                            TotalInformation();
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

        vm.expandCollapse = function (event) {
            if ($(event.target).hasClass('see-details')) {
                $(event.target).addClass('hide-details').removeClass('see-details');
                $(event.target).parent().parent().css('border-bottom', '0px solid');
                $(event.target).parent().parent().parent().find('.details').show();
            }
            else {
                $(event.target).addClass('see-details').removeClass('hide-details');
                $(event.target).parent().parent().parent().find('.details').hide();
            }
        }

        function downloadPdf(val) {
            var authData = JSON.parse(localStorage.getItem('ls.authorizationData'));

            if (authData) {
                var token = 'Bearer ' + authData.token;
                var refreshtoken = authData.guiToken;
                var key = CryptoJS.enc.Utf8.parse('8080808080808080');
                var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
                var encryRefreshToken = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(refreshtoken), key,
                              {
                                  keySize: 128 / 8,
                                  iv: iv,
                                  mode: CryptoJS.mode.CBC,
                                  padding: CryptoJS.pad.Pkcs7
                              });
                window.open(appConstants.apiServiceBaseUri + 'api/Download/InformationVerification_GeneratedDocument/?reft=' + encryRefreshToken + '&masterid=' + val, '_self', '');
            }
        };

        vm.printcontents = function () {
            downloadPdf(UtilityFactory.getSubmissionStatusObject($routeParams.guid).masterId);
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 09-09-2015
        // Description      : This Method is used to get Total Information to display in corresponding page.

        //------------------------------------------------------------------
        function TotalInformation() {
            requestService.InfoVerification({ masterId: UtilityFactory.getMasterId($routeParams.guid) }).then(function (response) {
                vm.SubVerfication = response.data;
                if (vm.SubVerfication.DocType == "IN") {
                    vm.SubVerfication.DocType = "In-Person";
                    $("#submission-type").css("display", "block");
                }
                else if (vm.SubVerfication.DocType == "ON") {
                    vm.SubVerfication.DocType = "Online";
                    $("#submission-type").css("display", "block");
                }
                else if (vm.SubVerfication.DocType == "") {
                    $("#submission-type").css("display", "none");
                }
                vm.AddressType = vm.submissionStatusData.SelectedMailType;
                if (vm.AddressType == "Primses Address") {
                    vm.addressType = true;
                }
                if (vm.AddressType == "NEWMAIL") {
                    vm.addressname = true;
                }
                if (vm.AddressType == "HQ Address") {
                }
                localStorage.setItem("totalFee", vm.SubVerfication.TotalFee);

                vm.MailingDetails = {
                    MailingBName: vm.SubVerfication.MailingBName,
                    MailingName: vm.SubVerfication.MailingName,
                    MailingAddress: vm.SubVerfication.MailingAddress,

                    MailingFirstName: vm.SubVerfication.MailingFirstName,
                    MailingMiddleName: vm.SubVerfication.MailingMiddleName,
                    MailingLastName: vm.SubVerfication.MailingLastName,

                    MailingStreetName: vm.SubVerfication.MailingStreetName,
                    MailingStreetNumber: vm.SubVerfication.MailingStreetNumber,
                    MailingStreetType: vm.SubVerfication.MailingStreetType,

                    MailingCity: vm.SubVerfication.MailingCity,
                    MailingState: vm.SubVerfication.MailingState,
                    MailingCountry: vm.SubVerfication.MailingCountry,

                    MailingQuadrant: vm.SubVerfication.MailingQuadrant,
                    MailingUnit: vm.SubVerfication.MailingUnit,
                    MailingZip: vm.SubVerfication.MailingZip,
                    MailingEmail: vm.SubVerfication.MailingEmail,
                    MailingTelePhone: vm.SubVerfication.MailingTelePhone
                };

                localStorage.setItem("MailingDetails", JSON.stringify(new Array(vm.MailingDetails)));
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            }, ErrorWhileProcessing);
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "mybbl" page.
        // Last Modified    :

        //------------------------------------------------------------------

        vm.navToMyBBL = function () {
            localStorage.removeItem('preAppQuestionsData');
            $location.path('/mybbl');
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "appchecklist" page

        //------------------------------------------------------------------

        vm.navToChecklist = function () {
            $location.path('/appchecklist/' + $routeParams.guid);
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "payment" page

        //------------------------------------------------------------------

        vm.navToPayment = function () {
            $('#infoverificationPopup .modal-body').html("<h3 class='error_message'>" + vm.currentpage_errors.verifyandcontinuemessage + "</h3>");
            $('#infoverificationPopup').modal('show');
        }

        vm.continueToNavigate = function () {
            $('#infoverificationPopup').modal('hide');
            $location.path("/payment/" + $routeParams.guid);
        }

        vm.stayonthePage = function () {
            $('#infoverificationPopup').modal('hide');
        }

        function ErrorWhileProcessing(response) {
            $("#dvLoadingSection").css("display", "none");
            $("#dvMainsection").css("display", "block");
            console.log("Error");
        }
    }
})();