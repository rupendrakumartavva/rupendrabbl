"use strict";

(function () {

    var controllerId = "BBLRenewalInfoConfirmController";
    angular.module("DCRA").controller(controllerId,
    ["$scope", "$rootScope", "$location", "requestService", "$routeParams", 'appConstants', 'RenewalUtilityFactory', '$window', 'SessionFactory', 'popupFactory', 'errorFactory', 'authService', BBLRenewalInfoConfirmController]);

    function BBLRenewalInfoConfirmController($scope, $rootScope, $location, requestService, $routeParams, appConstants, RenewalUtilityFactory, $window, SessionFactory, popupFactory, errorFactory, authService) {
        var vm = this;
        vm.enableButtons = "";
        vm.businessinfo = "";
        vm.corp = {};
        SessionFactory.setSessionAsDirty();
        //vm.navigate = false;

        vm.isActiveCorpNumber = false;
        vm.corpnotregistered = false;
        init();

        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                if (RenewalUtilityFactory.ifGuidAvailable($routeParams.guid)) {
                    getBusinessInfoToVerify().then(function (response) {
                        vm.currentpage_errors = errorFactory.getBBLErrorMessages();
                        vm.businessinfo = response.data;
                        if (!(response.data.corpNumber == "" && response.data.iscorp == false)) {
                            if (response.data.corpNumber == "") {
                                vm.corpnotregistered = true
                            } else {
                                vm.corp.number = response.data.corpNumber;
                                vm.corpnotregistered = false;
                                vm.numberfromdb = response.data.IsBblCorp;
                            }
                            $('#dvLoadingSection').css('display', 'none');
                            $("#dvMainsection").css("display", "block");
                        }
                        if (response.data.Status != '') {
                            vm.isActiveCorpNumber = false;
                            vm.searchcorponlinebuttonclicked = true;
                            vm.corpnotregistered = true;
                            $("#search_corp_online,.single_checkbox_set").hide();
                            $(".input-group").addClass('col-md-12 col-sm-12 col-xs-12');
                            if (response.data.Status != 'Active' && response.data.Status != 'NoData') {
                                $("#error_msg").html("According to DCRA's Corporations Division, your Corporate Registration status is not currently ACTIVE. Please, select Return to My BBLs, and contact DCRA's Corporations Division at 202-442-4400 to resolve any open issues prior to submitting your renewal. <br/><br/> Please allow two full business days after updating your Corporate Registration before attempting the Renewal process again.").removeClass('text-success').addClass('error_text');
                            } else if (response.data.Status == 'NoData') {
                                $("#error_msg").html("The entered Corporate Registration File number did not match CorpOnline records.");
                            }
                            else if (response.data.Status == 'Active') {
                                vm.isActiveCorpNumber = true;
                                $("#error_msg").html("Your Corporate Registration is Active. Please select Next").addClass('text-success').removeClass('error_text');
                            }
                        }
                        $('#dvLoadingSection').css('display', 'none');
                        $("#dvMainsection").css("display", "block");
                    }, ErrorWhileProcessing);
                } else {
                    RenewalUtilityFactory.noGuid();
                }
            }, function () {
                $location.path("/login");
            });
        }

        $("#error_msg").html("");

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "mybbl" page


        //------------------------------------------------------------------

        vm.navToConfirmAssociation = function () {
            $location.path("/mybbl");
        };

        function getBusinessInfoToVerify() {
            return requestService.GetrenuwalLicense(RenewalUtilityFactory.getRenewalObject($routeParams.guid));
        }

        //vm.navToBblsHome = function () {
        //    popupFactory.showpopup(vm.currentpage_errors.renewalNavigation, vm.navigationPath);
        //    //$('#renewPopUp .modal-body').html("<h3 class='error_message'>" + appConstants.errorMessages.renewalNavigation + "</h3>");
        //    //$('#renewPopUp').modal('show');
        //};




        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 14-08-2015
        // Description      : This Method is navigating to "supportingdocs/step1/renewal" page


        //------------------------------------------------------------------

        vm.insertAndnavigate = function () {

            if (vm.corpnotregistered == false && vm.corp.number == undefined) {
                $("#error_msg").html("Please enter the Corporate Registration File # or select the  &quot;I am not registered with the DCRA Corporations Division&quot; checkbox to proceed.").removeClass('text-success').addClass('error_text');
            } else if (vm.corp.number != undefined && vm.isActiveCorpNumber == false) {
                $("#error_msg").html("Please click the Search Corp Online button").removeClass('text-success').addClass('error_text');
            } else {

                $('#dvLoadingSection').css('display', 'block');
                $("#dvMainsection").css("display", "none");
                var confirmbbl = {
                    CategoryName: vm.businessinfo.category,
                    LicenseNumber: vm.businessinfo.licenseNumber,
                    UserId: localStorage.userId,
                    EntityId: vm.businessinfo.entityId,
                    CorpNumber: vm.corp.number,
                    IsCorp: vm.corpnotregistered,
                    SubCategoryName: vm.businessinfo.subcategory,
                    ActivityName: vm.businessinfo.activity,
                    UserBblAssociateId: RenewalUtilityFactory.getRenewalServiceId($routeParams.guid),
                    LicenseDuration: 2
                };
                confirmRenuwal(confirmbbl).then(confirmationResult, ErrorWhileProcessing);
            }
        };

        function ErrorWhileProcessing(response) {
            $('#dvLoadingSection').css('display', 'none');
            $("#dvMainsection").css("display", "block");
            console.log("Error");
        };


        function navToCleanHands() {
            $location.path('/renewal/cleanhands/' + $routeParams.guid);
        }

        function confirmationResult(response) {
            if (response.data.CorpStatus == "ACTIVE" || response.data.IsCorp == true) {
                SessionFactory.setSessionAsClear();
                //vm.navigate = true;
                navToCleanHands();
            }
            //$('#dvLoadingSection').css('display', 'none');
            //$("#dvMainsection").css("display", "block");
        };

        function confirmRenuwal(confirmdata) {
            return requestService.confirmRenuwal(confirmdata);
        };

        vm.navToCancelBBLAssociation = function () {
            if (angular.isDefined(vm.corp.number) && vm.corp.number != undefined) {
                popupFactory.showpopup(vm.currentpage_errors.renewalNavigation, "mybbl");
            } else {
                $location.path("/mybbl");
            }
        };

        vm.cancelNavigation = function () {
            $('#renewPopUp').modal('hide');
        };

        vm.navigateAnyWay = function () {
            $('#renewPopUp').modal('hide');
            //vm.navigate = true;
            SessionFactory.setSessionAsClear();
            $location.path("/mybbl");
        };

        vm.checkCorpRegistration = function () {
            if (vm.IsCorpRegistration) {
                vm.IsCorpRegistration = false;
            } else {
                vm.IsCorpRegistration = true;
            }
        };

        vm.checkboxupdated = function () {
            if (vm.corpnotregistered) {
                vm.isActiveCorpNumber = true;
                $("#error_msg").html("");
            }
        }

        vm.setErrorMsg = function (id) {
            vm.searchcorponlinebuttonclicked = false;
            vm.submitted = false;
            $("#" + id).html("");
            $("#error_msg").html("");
        };

        vm.corpmodalchanged = function () {
            $("#success_msg").html("");
            vm.isActiveCorpNumber = false;
            vm.submitted = false;
        }

        vm.getCorpRegInfo = function () {

            if (vm.corp.number != undefined) {
                var data = { FileNumber: vm.corp.number };
                requestService.RenewalCorporationSearchFind(data).then(function (response) {
                    vm.searchcorponlinebuttonclicked = true;
                    if (response.data.Status != 'Active' && response.data.Status != 'NoData') {
                        vm.isActiveCorpNumber = false;
                        $("#error_msg").html("According to DCRA's Corporations Division, your Corporate Registration status is not currently ACTIVE. Please, select Return to My BBLs, and contact DCRA's Corporations Division at 202-442-4400 to resolve any open issues prior to submitting your renewal. <br/><br/> Please allow two full business days after updating your Corporate Registration before attempting the Renewal process again.").removeClass('text-success').addClass('error_text');
                    } else if (response.data.Status == 'NoData') {
                        $("#error_msg").html("The entered Corporate Registration File number did not match CorpOnline records.");
                    }
                    else {
                        vm.isActiveCorpNumber = true;
                        $("#error_msg").html("Your Corporate Registration is Active. Please select Next").addClass('text-success').removeClass('error_text');
                    }
                    $('#dvLoadingSection').css('display', 'none');
                    $("#dvMainsection").css("display", "block");
                }, ErrorWhileProcessing);
            } else {
                vm.isActiveCorpNumber = false;
                $("#error_msg").html("Please enter the corp number and click search corp online button").removeClass('text-success').addClass('error_text');
            }
        }

        function isSessionDirty() {
            if (SessionFactory.isSessionDirty()) {
                if (angular.isDefined(vm.corp.number) || vm.corpnotregistered) {
                    SessionFactory.setSessionAsDirty();
                } else {
                    SessionFactory.setSessionAsClear();
                }
            }

        }

        $scope.$on('$locationChangeStart', function (event, next, current) {
            if (!angular.equals(next, current)) {
                vm.navigationPath = next.split('#')[1].slice(1);
                isSessionDirty();
                if (SessionFactory.isSessionDirty()) {
                    event.preventDefault();
                    popupFactory.showpopup(vm.currentpage_errors.renewalNavigation, vm.navigationPath);
                }
                return;
            }
        });
    }
})();