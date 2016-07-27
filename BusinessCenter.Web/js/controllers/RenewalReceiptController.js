(function () {
    'use strict';
    var controllerId = 'RenewalReceiptController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', '$routeParams', 'requestService', 'RenewalUtilityFactory', 'appConstants', 'SessionFactory','authService', RenewalReceiptController]);

    function RenewalReceiptController($scope, $rootScope, $location, $routeParams, requestService, RenewalUtilityFactory, appConstants, SessionFactory, authService) {
        var vm = this;
        vm.DocType = '';
        vm.emailconfirm = '';
        vm.printmasterid = '';
        vm.contentSection = [
            { cId: 'c01', num: 10 },
            { cId: 'c02', num: 25 },
            { cId: 'c02', num: 50 }
        ];
        SessionFactory.setSessionAsDirty();
        vm.itemPage = 50;
        vm.currentPage = 1;

        $('.panel-body >h1').each(function (ndx, ele) {
            if (ele.offsetWidth < ele.scrollWidth) {
                $(ele).css('font-size', Math.ceil($(ele).width() / 10) + 5);
            }
        });

        init();

        function getSubmissionMaster() {
            return requestService.getSubmissionMasterWithbbl(RenewalUtilityFactory.getRenewalObject($routeParams.guid));
        }

        function confirmPayment(masterid) {
            return requestService.getPaymentDetails({ MasterId: masterid, PaymentId: localStorage.getItem("PaymentId"), UserId: localStorage.userId, SubmitTypeFrom: "RENEW" });
        }

        function init() {
            $("#dvLoadingSection").css("display", "block");
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                if (RenewalUtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    getSubmissionMaster().then(function (resp) {
                        vm.DocType = resp.data.Result[0].DocSubmType;
                        vm.printmasterid = resp.data.Result[0].MasterId;
                        confirmPayment(resp.data.Result[0].MasterId).then(function (response) {
                            vm.ServiceCheckList = response.data.ServiceCheckList;
                            vm.documenttype = response.data.DocType;
                            vm.IsBackgroundInvestigation = response.data.IsBackgroundInvestigation;
                            vm.DocumentList = response.data.DocumentList;
                            vm.feeandDetails = response.data;
                            SessionFactory.setSessionAsClear();
                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                        }, function (response) {
                            SessionFactory.setSessionAsClear();
                            $("#dvLoadingSection").css("display", "none");
                            $("#dvMainsection").css("display", "block");
                        });
                    });
                    vm.emailconfirm = localStorage.getItem("EmailConfirm");
                } else {
                    SessionFactory.setSessionAsClear();
                    RenewalUtilityFactory.noGuid();
                }
            }, function () {
                $location.path('/login');
            })
            
        }

        vm.printreceiptcontents = function () {
            downloadPdf(vm.printmasterid);
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
                window.open(appConstants.apiServiceBaseUri + 'api/Download/SubmissionReceipt_GeneratedDocument/?reft=' + encryRefreshToken + '&masterid=' + val + "&userId=" + localStorage.userId
                    + "&type=RENEW", '_self', '');
            }
        };

        vm.selectOption = function () {
            vm.itemPage = $scope.confirmed.num;
        };

        vm.showdescription = function () {
            $('#description').modal('show');
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

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigated to "mybbl" page.

        //------------------------------------------------------------------

        vm.navToMyBBLsList = function () {
            $location.path('/mybbl');
        }

        vm.navTodashboard = function () {
            $location.path('/dashboard');
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!SessionFactory.isSessionDirty()) {
                RenewalUtilityFactory.removeGuid($routeParams.guid);
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