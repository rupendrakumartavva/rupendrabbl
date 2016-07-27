(function () {
    'use strict';
    var controllerId = 'ReceiptController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', '$routeParams', 'requestService', 'UtilityFactory', 'appConstants', 'BBLSubmissionFactory', 'SessionFactory', 'authService', ReceiptController]);

    function ReceiptController($scope, $rootScope, $location, $routeParams, requestService, UtilityFactory, appConstants, BBLSubmissionFactory, SessionFactory, authService) {
        var vm = this;
        vm.bblapptype = $routeParams.bblapptype;
        vm.applicationFee = 0;
        vm.CategoryLicenseFee = 0;
        vm.EndorsementFee = 0;
        vm.SubTotal = 0;
        vm.techFee = 0;
        vm.TotalFee = 0;
        vm.isehop = false;
        vm.DocType = '';
        vm.underreview = false;
        vm.emailconfirm = '';
        vm.PaymentId = '';
        vm.PaymentStatus = '';
        vm.Status = '';
        SessionFactory.setSessionAsDirty();
        vm.contentSection = [
            { cId: 'c01', num: 10 },
            { cId: 'c02', num: 25 },
            { cId: 'c02', num: 50 }
        ];

        vm.filterByStatusKeyword = '';
        vm.itemPage = 50;
        vm.currentPage = 1;

        $('.panel-body >h1').each(function (ndx, ele) {
            if (ele.offsetWidth < ele.scrollWidth) {
                $(ele).css('font-size', Math.ceil($(ele).width() / 10) + 5);
            }
        });

        init();

        function init() {
            $("#dvLoadingSection").css("display", "block");
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                if (UtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    submissionStatus().then(function (response) {
                        vm.Status = response.data.Status;
                        vm.DocType = response.data.DocSubmType;
                        vm.PaymentId = response.data.PaymentId;
                        vm.PaymentStatus = response.data.PaymentStatus;
                        vm.emailconfirm = localStorage.getItem("EmailConfirm");
                        confirmPayment().then(function (response) {
                            if (angular.isDefined(response.data.ServiceCheckList)) {
                                vm.ServiceCheckList = response.data.ServiceCheckList;
                                vm.documenttype = response.data.DocType;
                                vm.DocumentList = response.data.DocumentList;
                                vm.feeandDetails = response.data;
                                vm.IsEhopAllowed = response.data.IsEhopAllowed;
                                vm.IsBackgroundInvestigation = response.data.IsBackgroundInvestigation;
                                vm.applicationFee = response.data.ApplicationFee;
                                vm.CategoryLicenseFee = response.data.CategoryLicenseFee;
                                vm.EndorsementFee = response.data.EndorsementFee;
                                vm.SubTotal = response.data.SubTotal;
                                vm.techFee = response.data.TechFee;
                                vm.TotalFee = response.data.TotalFee;
                                vm.isehop = response.data.Isehop;
                                SessionFactory.setSessionAsClear();
                            }
                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                        }, function (response) {
                            SessionFactory.setSessionAsClear();
                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                        });
                    });
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

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is used for selecting an option in dropdown, to show the records according to the selected option.

        //------------------------------------------------------------------

        vm.selectOption = function () {
            vm.itemPage = $scope.confirmed.num;
        };

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is used for filtering the data by selecting the type.

        //------------------------------------------------------------------

        vm.filterByStatus = function (type) {
            vm.filterByStatusKeyword = type;
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is used for selecting an option in dropdown, to show the records according to the selected option.

        //------------------------------------------------------------------

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

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigated to "mybbl" page.

        //------------------------------------------------------------------

        vm.navToMyBBLsList = function () {
            vm.underreview = true;
            $location.path('/mybbl');
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
                window.open(appConstants.apiServiceBaseUri + 'api/Download/SubmissionReceipt_GeneratedDocument/?reft=' + encryRefreshToken + '&masterid=' + val + "&userId=" + localStorage.userId + "&type=submission", '_self', '');
            }
        };
        vm.printreceiptcontents = function () {
            downloadPdf(UtilityFactory.getSubmissionStatusObject($routeParams.guid).masterId);
        }

        function confirmPayment() {
            return requestService.getPaymentDetails({ MasterId: UtilityFactory.getMasterId($routeParams.guid), PaymentId: vm.PaymentId, UserId: localStorage.userId });
        }

        vm.navTodashboard = function () {
            $location.path('/dashboard');
        }

        vm.showdescription = function () {
            $('#description').modal('show');
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!SessionFactory.isSessionDirty()) {
                UtilityFactory.removeGuid($routeParams.guid);
                localStorage.removeItem('PaymentId');
                localStorage.removeItem('EmailConfirm');

                if (next.indexOf('payment') != -1 && next.indexOf('mybbl') == -1) {
                    $location.path('/mybbl').replace();
                }
            } else {
                event.preventDefault();
            }
        });
    }
})();