(function () {
    'use strict';

    var controllerId = 'HeaderController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', 'appConstants', 'authService', 'SessionFactory', '$window', HeaderController]);

    function HeaderController($scope, $rootScope, $location, requestService, appConstants, authService, SessionFactory, $window) {
        init();

        /*
		 * Function: init
		 * init (initialize) method: first method to be executed on controller load.
		 */
        function init() {
            //$scope.home_navigateUrl = appConstants.stagingUrl;
            $rootScope.userFirstName = localStorage.userFirstName;
            $rootScope.userLastName = localStorage.userLastName;
            $rootScope.logoutMsg = "Are you sure you wish to log out?";
            var pagePath = $location.path();
            if ((pagePath === '/home') || (pagePath === '/lookup') || (pagePath === '/aboutus')) {
                $scope.mainmenu1 = true;
                $scope.mainmenu2 = false;
                $scope.mainmenu3 = false;
                if (authService.authentication.isAuth) {
                    $scope.islogin = true;
                } else { $scope.islogin = false; }
            }
            else if (authService.authentication.isAuth) {
                $scope.mainmenu3 = true;
                $scope.mainmenu1 = false;
                $scope.mainmenu2 = false;
                $scope.islogin = true;
            }
            else if (pagePath === '/login') {
                $scope.mainmenu2 = true;
                $scope.mainmenu1 = false;
                $scope.mainmenu3 = false;
            }
            else if ((pagePath === '/profile')) {
                $scope.mainmenu3 = true;
                $scope.mainmenu1 = false;
                $scope.mainmenu2 = false;
            }
            else {
                $scope.mainmenu2 = true;
                $scope.mainmenu1 = false;
                $scope.mainmenu3 = false;
            }
        }

        $scope.$on('$changeHeader', function () {
            if (authService.authentication.isAuth) {
                $scope.mainmenu3 = true;
                $scope.mainmenu1 = false;
                $scope.mainmenu2 = false;
            }
            else {
                $scope.mainmenu2 = true;
                $scope.mainmenu1 = false;
                $scope.mainmenu3 = false;
            }
        });

        $scope.navToSearchPage = function () {
            $location.path('/searchresult');
        };

        $scope.navToSearch = function () {
            $('#search_icon').addClass('active_search');
            $('.search_box .btn.btn-default').fadeIn(300);
        };

        $scope.showMenu = function () {
            if ($('.mobile-menu').hasClass('show-menu')) {
                $('.mobile-menu').removeClass('show-menu');
                $('.activemenu span').addClass('glyphicon-chevron-up').removeClass('glyphicon-chevron-down');
            }
            else {
                $('.mobile-menu').addClass('show-menu');
                $('.activemenu span').addClass('glyphicon-chevron-down').removeClass('glyphicon-chevron-up');
            }
        };

        $scope.showQuickMenu = function () {
            if ($('.save-search').hasClass('show-menu')) {
                $('.save-search').removeClass('show-menu');
                $('.activemenuquick span').addClass('glyphicon-chevron-up').removeClass('glyphicon-chevron-down');
            }
            else {
                $('.save-search').addClass('show-menu');
                $('.activemenuquick span').addClass('glyphicon-chevron-down').removeClass('glyphicon-chevron-up');
            }
        };

        $scope.navToHelp = function () {
            $location.path('/help');
        };

        $scope.navToProfile = function () {
            $location.path('/profile');
        };

        $scope.menuClick = function () {
            window.setTimeout(function () {
                $('#homeModal').modal('show');
            }, 100);
        };

        $scope.checkrightClick = function (url, e) {
            e.preventDefault();
            console.log(e);
            if ((e.which === 3) || (e.ctrlKey)) {
                $scope.navToCorp();
            } else {
                e.target.href = 'javascript:void(0)';
            }
        }

        $scope.navToCorp = function (e) {
            $("#corpmodel").modal('show');
        }

        $scope.navigateAnyWay = function () {
            $("#corpmodel").modal('hide');
            $window.location.href = "https://corp.dcra.dc.gov/";
        }

        $scope.cancelNavigation = function () {
            $("#corpmodel").modal('hide');
        }

        $scope.logoutRedirect = function () {
            $('#homeModal').modal('hide');
            var authData = JSON.parse(localStorage.getItem('ls.authorizationData'));
            var tokenData = { RefreshTokenId: authData.refreshToken };
            window.setTimeout(function () {
                var response = requestService.UserLogout(tokenData);
                response.success(function (data) {
                    if (data.status == "True") {
                        $rootScope.filterByType = undefined;
                        SessionFactory.setSessionAsClear();
                        authService.logOut();
                        $location.path('/login');
                    }
                });
            }, 400);
        };

        $scope.CancellogoutRedirect = function () {
            $('#homeModal').modal('hide');
        }

        $scope.navToTop = function () {
            angular.element('html, body').animate({ scrollTop: 0 }, '500', 'swing');
        }

        if ($location.path().indexOf("home") > -1) {
            $(".smallMenu li:first-child > a").addClass("active");
        }
        else if ($location.path().indexOf("aboutus") > -1) {
            $(".smallMenu li > a").removeClass("active");
            $(".smallMenu li:nth-child(3) > a").addClass("active");
        }
        else if ($location.path().indexOf("lookup") > -1) {
            $("#lookup").addClass("active");
        }
        else if (($location.path() === '/login') || ($location.path() === 'accountrecovery') || ($location.path() === 'securityquestion') || ($location.path() === '/forgotpassword') || ($location.path() === '/forgotusername')) {
            $(".mainNavigation .global-menu li.login-menu").addClass("active");
            $(".smallMenu li:nth-child(2) > a").addClass("active");
        }
        else if (($location.path() === '/register') || ($location.path() === '/verifyemail') || ($location.path() === '/termsofservices')) {
            $(".register").addClass("active");
            $(".smallMenu li:nth-child(2) > a").addClass("active");
        }
        else if (($location.path() === '/quicksearch') || ($location.path() === '/quicksearchsave') || ($location.path() === '/quicksearchresult')) {
            $(".quicksearch").addClass("active");
            $(".smallMenu li:nth-child(2) > a").addClass("active");
        }
        else if (($location.path() == '/mybbl') || ($location.path() == '/associatebbl') || ($location.path() == '/verifybusinessbeforeassociation') || ($location.path() == '/mybbl') || ($location.path() == '/newbblwelcome')
            || ($location.path() == '/preappquestions/step1') || ($location.path() == '/preappquestions/step2') || ($location.path() == '/preappquestions/step3') || ($location.path() == '/preappquestions/step4')
            || ($location.path() == '/preappquestions/step5') || ($location.path().indexOf('/appchecklist') != -1) || ($location.path() == '/supportingdocs/step1') || ($location.path() == '/preappquestions/step5')
            || ($location.path().indexOf('/reviewchecklist') != -1) || ($location.path() == '/preappquestions/step6')
            || ($location.path().indexOf('/individualbusinesslic') != -1) || ($location.path().indexOf('/taxrevenue') != -1) || ($location.path().indexOf('/corpreqregwithtradesecond') != -1) || ($location.path().indexOf('/corpreqregisterfirst') != -1)
            || ($location.path().indexOf('/corpnotregisteredagent') != -1) || ($location.path().indexOf('ehopehomeaddress') != -1) || ($location.path().indexOf('/ehopeligibility') != -1) || ($location.path().indexOf('/physicallocation/corpreg') != -1)
            || ($location.path().indexOf('/physicallocation/address') != -1) || ($location.path().indexOf('/physicallocation/agent') != -1) || ($location.path().indexOf('/physicallocation/hop') != -1) || ($location.path().indexOf('/physicallocation/cofo') != -1) || ($location.path().indexOf('/corpnotregisteredaddress') != -1) || ($location.path().indexOf('/nopobox') != -1)
            || ($location.path().indexOf('/bblrenewalconfirm') != -1) || ($location.path().indexOf('receipt') != -1) || ($location.path().indexOf('payment') != -1) || ($location.path().indexOf('/infoverification') != -1) || ($location.path().indexOf('/renewalreceipt') != -1)
            || ($location.path().indexOf('/physicallocation/corpbussagent') != -1) || ($location.path().indexOf('/supportingdocs/step2/') != -1) || ($location.path().indexOf('/supportingdocs/') != -1) || ($location.path() == '/renewalpayment') || ($location.path() == '/paymentfailure') || ($location.path().indexOf('/emailconfirmation') != -1)
            || ($location.path().indexOf('/bblrenewalpayment') != -1) || ($location.path().indexOf('/bblrenewalpayment/b') != -1) || ($location.path() == '/bblrenewalpayment/t')
            || ($location.path().indexOf('/renewal/cleanhands') != -1)
            || ($location.path() == '/paymentfailure/apply')) {
            $(".mybbl").addClass("active");
            $(".smallMenu li:nth-child(2) > a").addClass("active");
        }
        else if (window.location.href.indexOf("help") > -1) {
            $(".help").addClass("active");
            $(".smallMenu li:nth-child(2) > a").addClass("active");
        }
        else {
            if (($location.path() === '/dashboard')) {
                $(".dashboard").addClass("active");
                $(".smallMenu li:nth-child(2) > a").addClass("active");
            }
            if (($location.path() === '/profile')) {
                $(".profile-menu").addClass("active");
                $(".smallMenu li:nth-child(2) > a").addClass("active");
            }
        }
    }
})();