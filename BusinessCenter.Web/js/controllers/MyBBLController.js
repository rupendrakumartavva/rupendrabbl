(function () {
    'use strict';
    var controllerId = 'MyBBLController';
    angular.module('DCRA').controller(controllerId,
        ['$scope', '$rootScope', '$location', 'requestService', '$timeout', '$http', 'UtilityFactory', 'RenewalUtilityFactory', 'appConstants', '$filter', 'SessionFactory', 'authService', 'popupFactory', MyBBLController]);

    function MyBBLController($scope, $rootScope, $location, requestService, $timeout, $http, UtilityFactory, RenewalUtilityFactory, appConstants, $filter, SessionFactory, authService, popupFactory) {
        var vm = this;

        vm.itemPage = 10;

        vm.filterByStatusKeyword = '';
        vm.previousElement = '';
        vm.contentSection = [
            { cId: 'c01', num: 10 },
            { cId: 'c02', num: 25 },
            { cId: 'c03', num: 50 }
        ];
        vm.confirmed = {};
        vm.currentPage = 1;
        SessionFactory.setSessionAsClear();
        init();

        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                vm.filterByStatusKeyword = localStorage.filterByStatusKeyword;
                getBblServiceList().then(function (response) {
                    vm.businessList = response.data.Result;

                    //console.log(vm.businessList[0].BblServiceList[0].UserAssociateType);
                    $('#dvLoadingSection').css('display', 'none');
                    $("#dvMainsection").css("display", "block");
                    setRecordsPerPage();

                    if (angular.isDefined(vm.filterByStatusKeyword)) {
                        //filterByEhopExpand(vm.filterByStatusKeyword);
                    } else {
                        vm.filterByStatusKeyword = '';
                        //filterByEhopExpand(vm.filterByStatusKeyword);
                    }
                }, errorWhileProcessing);
                vm.currentPage = localStorage.pagenumber;
            }, function () {
                $location.path("/login");
            });
        }

        function getBblServiceList() {
            return requestService.BblServiceList({ UserID: localStorage.userId });
        }

        function errorWhileProcessing(response) {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error Occured");
        }

        function setRecordsPerPage() {
            if (localStorage.getItem('itemsperpage') != null) {
                var itemsperpage = JSON.parse(localStorage.getItem('itemsperpage'));
                vm.confirmed = vm.contentSection[parseInt(itemsperpage.cId.substring(2)) - 1];
                vm.itemPage = itemsperpage.num;
            } else {
                vm.confirmed = vm.contentSection[0];
                vm.itemPage = 10;
                vm.currentPage = 1;
            }
        }

        vm.checkrightClick = function (e, masterid) {
            //   $('#dvLoadingSection').css('display', 'block'); $("#dvMainsection").css("display", "none");
            e.preventDefault();
            if ((e.which === 3) || (e.ctrlKey)) {
                e.target.href = "#" + getPathWithGuid(masterid);
            } else
                e.target.href = 'javascript:void(0)';
        }

        function getPathWithGuid(masterid) {
            if (!UtilityFactory.containsMasterId(masterid)) {
                UtilityFactory.addMasterId(UtilityFactory.getGuid(), masterid);
                UtilityFactory.updateMasterIdsList();
            }
            return '/appchecklist/' + UtilityFactory.getGuidByMasterId(masterid);
        }

        vm.navToAppChecklist = function (masterid) {
            $('#dvLoadingSection').css('display', 'block'); $("#dvMainsection").css("display", "none");

            $location.path(getPathWithGuid(masterid));
        }

        function getPathWithRenewalId(UserBblServiceId) {
            if (!RenewalUtilityFactory.containsRenewalServiceId(UserBblServiceId)) {
                RenewalUtilityFactory.addRenewalApplication(RenewalUtilityFactory.getGuid(), UserBblServiceId);
                RenewalUtilityFactory.updateRenewals();
            }
            return '/bblrenewalconfirm/' + RenewalUtilityFactory.getGuidByRenewalServiceId(UserBblServiceId);
        }

        vm.navToRenewalConfirm = function (entity, license, UserBblServiceId) {
            $('#dvLoadingSection').css('display', 'block'); $("#dvMainsection").css("display", "none");

            requestService.deleteRenewalData({ UserId: localStorage.getItem('userId'), UserBblAssociateId: UserBblServiceId }).then(function (response) {
                if (response.data == "true") {
                    $location.path(getPathWithRenewalId(UserBblServiceId));
                }
            }, errorWhileProcessing);
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is used for selecting an option in dropdown, to show the records according to the selected option.

        //------------------------------------------------------------------

        vm.selectOption = function () {
            vm.currentPage = 1;
            vm.itemPage = vm.confirmed.num;
            localStorage.setItem('itemsperpage', JSON.stringify(vm.confirmed));
        };

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is used for

        //------------------------------------------------------------------

        //vm.filterByStatus = function (type) {
        //    vm.filterByStatusKeyword = type;
        //}

        vm.filterByStatus = function (type, id) {
            localStorage.pagenumber = 1;
            vm.currentPage = 1;
            if (vm.previousElement != '') {
                vm.previousElement.removeClass('activebtn');
            }
            var clickedElement = angular.element(document.querySelector('#' + id));
            vm.previousElement = angular.element(document.querySelector('#' + id));
            clickedElement.addClass('activebtn');
            localStorage.filterByStatusKeyword = type;
            vm.filterByStatusKeyword = type;
            $timeout(function () {
                filterByEhopExpand(vm.filterByStatusKeyword);
            }, 100);
        }

        vm.mybblfilter = function (bblobj) {
            var item = angular.copy(bblobj);
            if (!(item.LrenNumber != "" && (item.Status != "Under Review" || item.Status != "Active"))) {
                delete item.LrenNumber;
            }
            if (item.SubCategory == "" || item.SubCategory == "NA" || item.SubCategory == null) {
                delete item.SubCategory;
            }
            delete item.MasterId;
            delete item.GrandTotal;
            delete item.EntityId;
            delete item.UserBblServiceId;
            delete item.UserName;
            delete item.IsEhop;
            delete item.CategoryStatus;
            delete item.ShowActivePdf;

            switch (vm.filterByStatusKeyword) {
                case "Eligible for Renewal":
                    return item.Status == 'Expired' || item.Status == 'Expiring Soon' || item.Status == 'Lapsed';
                case "Under Review":
                    return item.Status == 'Under Review';
                case "Active":
                    return item.Status == 'Active';
                case "Draft Application In Progress":
                    return item.Status == 'Draft Application In Progress';
                case "eHOP":
                    return item.EhopNumber;
                default:
                    return $filter('filter')(new Array(item), vm.filterByStatusKeyword).length > 0;
            }
        }

        $scope.$watch('vm.currentPage', function () {
            $timeout(function () {
                filterByEhopExpand(vm.filterByStatusKeyword);
            }, 100);
        });

        function filterByEhopExpand(type) {

            if (vm.filterByStatusKeyword == 'eHOP') {
                $('.arrow-right').addClass('hide-details').removeClass('see-details');
                $(".container.ehop.details.no-pad").css("display", "block");
            } else {
                $('.arrow-bottom').addClass('see-details').removeClass('hide-details');
                $('.arrow-right').addClass('see-details').removeClass('hide-details');
                $(".container.ehop.details.no-pad").css("display", "none");
            }
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is used for selecting an option in dropdown, to show the records according to the selected option.

        //------------------------------------------------------------------

        vm.expandCollapse = function (event, type) {

            if ($(event.target).hasClass('see-details')) {
                $(event.target).addClass('hide-details').removeClass('see-details');
                $(event.target).parent().parent().css('border-bottom', '0px solid');
                $(event.target).parent().parent().parent().parent().next().show();
            }
            else {
                $(event.target).addClass('see-details').removeClass('hide-details');
                $(event.target).parent().parent().parent().parent().next().hide();
            }
        }

        vm.setDisabled = function (data, id) {
            if (data == 0) {
                $('#' + id).css('background-color', '#1268ac');
                return true;
            }
            return false;
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "newbblwelcome" page

        //------------------------------------------------------------------

        vm.navToApplyNewLicense = function () {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            $location.path('/newbblwelcome');
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "associatebbl" page

        //------------------------------------------------------------------

        vm.navToAssociateBusinessLicense = function () {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            $location.path('/associatebbl');
        };

        vm.delete_record = function () {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            confirmDelete(vm.SubmissionLicense).then(function (response) {
                if (response.data.Result) {
                    getBblServiceList().then(function (response) {
                        vm.businessList = response.data.Result;
                        setRecordsPerPage();
                        $('#dvLoadingSection').css('display', 'none');
                        $("#dvMainsection").css("display", "block");
                    }, errorWhileProcessing);
                } else {
                    console.log("Error");
                }
            });
        }

        function confirmDelete(entityid) {
            return requestService.BblRemoveServiceList({ DCBC_ENTITY_ID: entityid, UserID: localStorage.userId });
        }

        vm.navToRemove = function (entityid, licensenum) {
            vm.SubmissionLicense = entityid;
            vm.licensenumber = licensenum;
            if (localStorage.showMessagePopup == undefined || localStorage.showMessagePopup == "false" || localStorage.showMessagePopup == false || localStorage.showMessagePopup == 'undefined') {
                $("#dvMainsection").css("display", "block");
                $("#dvLoadingSection").css("display", "none");
                $('#modelMessage').html("Selected License # :  " + licensenum + "  will no longer be associated with your My DC Business Center account and will not be shown in your My Basic Business Licenses page. Are you sure you wish to continue?");
                $('#delete_message').modal('show');
            } else {
                vm.delete_record(entityid);
            }
        }

        vm.cancel_delete = function () {
            localStorage.showMessagePopup = false;
            $("#delete_status").attr('checked', false);
            $('#uniform-delete_status span').removeClass('checked');
            $("#dvLoadingSection").css("display", "none");
            $("#dvMainsection").css("display", "block");
            $('#delete_message').modal('hide');
        }

        vm.ShowMessage = function () {
            localStorage.pagenumber = 1;
            vm.currentPage = 1;
            if (vm.filterByStatusKeyword.length < 100) {
                localStorage.filterByStatusKeyword = vm.filterByStatusKeyword;
                if (vm.previousElement != '') {
                    vm.previousElement.removeClass('activebtn');
                }
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
                window.open(appConstants.apiServiceBaseUri + 'api/Download/EHOP_GeneratedDocument/?reft=' + encryRefreshToken + '&masterid=' + val + "&userId=" + localStorage.userId, '_self', '');
            }
        };

        vm.navToEhopConfirm = function (masterId) {
            downloadPdf(masterId);
        }

        vm.lookupData = {
            "default": '<ul style="color:#000">' +
		'<li><strong>Active:</strong> 760-91 Days prior to Expiration Date (For a two year license) OR 1,460-91 Days prior to Expiration Date (For a four year license).</li>' +
        '<li><strong>Expiring Soon:</strong> 90-0 Days PRIOR to Expiration Date.</li>' +
        '<li><strong>Lapsed:</strong> 1-30 Days AFTER Expiration Date.</li>' +
        '<li><strong>Expired:</strong> 31-180 Days AFTER Expiration Date.</li>' +
        '<li><strong>Renewal Not Allowed:</strong> 181 or more days AFTER Expiration Date.</li>' +
	'</ul>'
        }

        vm.lookUpStatus = function () {
            popupFactory.showpopup(vm.lookupData.default, '', { config: { buttons: 0, isHtmlString: true } });
        }

        function submissionOrderPdf(val) {
            //var authData = JSON.parse(localStorage.getItem('ls.authorizationData'));
            //var token = 'Bearer ' + authData.token;
            //var params= {Authorization:token };
            //var url = [appConstants.apiServiceBaseUri + 'api/Download/SubmissionsActive_GeneratedDocument/?masterid=' + val + "&userId=" + localStorage.userId, $.param(params)].join('?');
            //console.log(url);

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

                window.open(appConstants.apiServiceBaseUri + 'api/Download/SubmissionsActive_GeneratedDocument/?reft=' + encryRefreshToken + '&masterid=' + val + "&userId=" + localStorage.userId, '_self', '');
            }
        };

        vm.navToBblSubmissionOrder = function (masterId) {
            submissionOrderPdf(masterId);
        }

        function renewelOrderPdf(val) {
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
                window.open(appConstants.apiServiceBaseUri + 'api/Download/BblRenewelOrder/?reft=' + encryRefreshToken + '&masterid=' + val + "&userId=" + localStorage.userId, '_self', '');
            }
        };

        vm.navToBblRenewelOrder = function (masterId) {
            renewelOrderPdf(masterId);
        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                if (next.indexOf('receipt') == -1) {
                    vm.next = next;
                    vm.current = current;
                    localStorage.pagenumber = 1;
                    localStorage.removeItem('itemsperpage');
                    vm.filterByStatusKeyword = '';
                    delete localStorage.filterByStatusKeyword;
                    localStorage.filterByStatusKeyword = '';
                } else {
                    event.preventDefault();
                }
            }
        });
    }
})();