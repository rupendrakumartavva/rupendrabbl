'use strict';

/* App Module */
/*
 * Application Main Module - Angular initializes from here. DCRA is the name of our module.
 * There are other modules like angular-flexslider,angularUtils.directives.dirPagination,smart-table,ui.bootstrap etc were integrated into our module in the way shown below.
 */
var ffaApp = angular.module('DCRA', ['ngRoute', 'angular-flexslider', 'angularUtils.directives.dirPagination', 'smart-table', 'ui.bootstrap', 'ui.bootstrap.datetimepicker', 'maskedinput']).
   /*
	 * Config   : Application configurations, like partials (html DIV blocks) and their
	 *            respective controllers, etc. This configuration section helps us to configure the Url routing etc to the Angular App.

       $routeProvider : This is used for routing. It will load html pages as per the url routes. we will specify the configuration of html partials and respective 
                        JS files as per the url with bblrenewalinfoconfirm method called when.
	 */
	config(['$locationProvider', '$routeProvider', '$httpProvider', function ($locationProvider, $routeProvider, $httpProvider) {

	    //$locationProvider.hashPrefix('!');
	    $routeProvider.
			when('/aboutus', { title: 'About Us', templateUrl: 'partials/aboutus.html', controller: 'AboutUsController' }).
			when('/', { title: 'Login', templateUrl: 'partials/login.html', controller: 'LoginController' }).
            when('/login', { title: 'Login', templateUrl: 'partials/login.html', controller: 'LoginController' }).
			when('/register', { title: 'Register', templateUrl: 'partials/register.html', controller: 'RegisterController' }).
			when('/termsofservices', { title: 'Term of Services', templateUrl: 'partials/terms.html', controller: 'TermsServicesController' }).
			when('/verifyemail', { title: 'Verify Email', templateUrl: 'partials/verifyemail.html', controller: 'VerifyEmailController' }).
			when('/quicksearch', { title: 'Quick Search', templateUrl: 'partials/quicksearch.html', controller: 'QuickSearchController' }).
			when('/forgotpassword', { title: 'Forgot Password', templateUrl: 'partials/forgotpassword.html', controller: 'ForgotPasswordController' }).
			when('/forgotusername', { title: 'Forgot Username', templateUrl: 'partials/forgotusername.html', controller: 'ForgotUsernameController' }).
			when('/profile', { title: 'Profile', templateUrl: 'partials/profile.html', controller: 'ProfileController', requireLogin: true }).
			when('/quicksearchresult', { title: 'Quick Search Results', templateUrl: 'partials/quicksearchresult.html', controller: 'QuickSearchResultController' }).
			when('/quicksearchsave', { title: 'My Saved Search Results', templateUrl: 'partials/quicksearchsave.html', controller: 'QuickSearchSaveController', requireLogin: true }).
			when('/help', { title: 'Help', templateUrl: 'partials/help.html', controller: 'HelpController' }).
			when('/dashboard', { title: 'Dashboard', templateUrl: 'partials/dashboard.html', controller: 'DashboardController', requireLogin: true }).
			when('/lookup', { title: 'Lookup', templateUrl: 'partials/lookup.html', controller: 'LookupController' }).
			when('/securityquestion', { title: 'Security Questions', templateUrl: 'partials/securityquestion.html', controller: 'SecurityQuestionController' }).
            when('/forgotusernamedisplay', { title: 'Forgot Username Display', templateUrl: 'partials/forgotusernamedisplay.html', controller: 'ForgotUsernameController' }).
			when('/accountrecovery', { title: 'Account Recovery', templateUrl: 'partials/accountrecovery.html', controller: 'AccountRecoveryController' }).
            when('/deletestatus', { title: 'Delete Status', templateUrl: 'partials/deletestatusmessage.html', controller: 'LoginController' }).
            when('/lockout', { title: 'Lockout', templateUrl: 'partials/Lockout.html', controller: 'LoginController' }).
            when('/lockoutSQ', { title: 'Lockout', templateUrl: 'partials/lockoutSQ.html', controller: 'ForgotPasswordController' }).
            when('/lockoutfrgtpwd', { title: 'Lockout', templateUrl: 'partials/lockoutfrgtpwd.html', controller: 'ForgotPasswordController' }).
            when('/lockoutfrgtuser', { title: 'Lockout', templateUrl: 'partials/lockoutfrgtuser.html', controller: 'ForgotUsernameController' }).
            when('/deleteaccount', { title: 'DeleteAccount', templateUrl: 'partials/deleteaccount.html', controller: 'DeleteAccountController' }).
            when('/emailverified', { title: 'Email Verified', templateUrl: 'partials/Success.html', controller: 'EmailVerification' }).
            when('/resetsuccessful', { title: 'Reset Successful', templateUrl: 'partials/profilelogout.html', controller: 'ProfileLogoutController' }).
            when('/profileemailchange', { title: 'Profile Logout', templateUrl: 'partials/profileemailchanged.html', controller: 'ProfileLogoutController' }).
            when('/forgotpasswordstatus', { title: 'Password Status', templateUrl: 'partials/passwordstatus.html', controller: 'ForgotPasswordController' }).
            when('/getnewpassword', { title: 'Get New Password', templateUrl: 'partials/newpassword.html', controller: 'NewPasswordController' }).
            when('/pagenotfound', { title: 'Page Not Found', templateUrl: 'partials/pagenotfound.html', controller: 'PageNotfoundController' }).
            when('/passwordrecheck', { title: 'Password Recheck', templateUrl: 'partials/Passwordresendmailverification.html', controller: 'NewPasswordController' }).
            when('/PreviousEmailVerification', { title: 'Email Verification', templateUrl: 'partials/PreviousEmailVerification.html', controller: 'UserPreviousEmailConfirmationController' }).
			when('/searchresult', { title: 'Search Result', templateUrl: 'partials/searchresult.html', controller: 'SearchResultController' }).
            when('/passwordexpiry', { title: 'PasswordExpiry', templateUrl: 'partials/PasswordExpiry.html', controller: 'ForgotPasswordController' }).
			when('/associatebbl', { title: 'Associate a Basic Business License', templateUrl: 'partials/associatebbl.html', requireLogin: true }).
			when('/verifybusinessbeforeassociation/:guid', { title: 'Verify Business Before Association', templateUrl: 'partials/verifybusinessbeforeassociation.html', requireLogin: true }).
			when('/mybbl', { title: 'My Basic Business License', templateUrl: 'partials/mybbl.html', requireLogin: true }).
			when('/newbblwelcome', { title: 'Welcome', templateUrl: 'partials/newbblwelcome.html', controller: 'NewBBLWelcomeController', requireLogin: true }).
			when('/preappquestions/step1', { title: 'Pre-Application Screening Questions', templateUrl: 'partials/preappquestions_step1.html', requireLogin: true }).
			when('/preappquestions/step2', { title: 'Pre-Application Screening Questions', templateUrl: 'partials/preappquestions_step2.html', requireLogin: true }).
			when('/preappquestions/step3', { title: 'Pre-Application Screening Questions', templateUrl: 'partials/preappquestions_step3.html', requireLogin: true }).
            when('/preappquestions/step4', { title: 'Pre-Application Screening Questions', templateUrl: 'partials/preappquestions_step4.html', requireLogin: true }).
			when('/preappquestions/step5', { title: 'Pre-Application Screening Questions', templateUrl: 'partials/preappquestions_step5.html', requireLogin: true }).
			when('/preappquestions/step6', { title: 'Pre-Application Screening Questions', templateUrl: 'partials/preappquestions_step6.html', requireLogin: true }).
			when('/appchecklist/:guid', { title: 'Application Checklist', templateUrl: 'partials/applicationchecklist.html', requireLogin: true }).
			when('/supportingdocs/step1/:guid', { title: 'Supporting Document Upload', templateUrl: 'partials/supportingdocsupload_step1.html', requireLogin: true }).
			when('/supportingdocs/step2/ON/:guid', { title: 'Supporting Document Upload', templateUrl: 'partials/supportingdocsupload_step2.html', requireLogin: true }).
            when('/renewal/supportingdocs/step1/:guid', { title: 'Supporting Document Upload', templateUrl: 'partials/renewalsupportingdocsupload_step1.html', requireLogin: true }).
            when('/renewal/supportingdocs/step2/:guid', { title: 'Supporting Document Upload', templateUrl: 'partials/renewalsupportingdocs_step2.html', requireLogin: true }).
			when('/physicallocation/cofo/:guid', { title: 'Physical Location and Business Structure', templateUrl: 'partials/physicallocationcofo.html', requireLogin: true }).
            when('/physicallocation/hop/:guid', { title: 'Physical Location and Business Structure', templateUrl: 'partials/physicallocationhop.html', requireLogin: true }).
			when('/physicallocation/address/:guid', { title: 'Physical Location and Business Structure', templateUrl: 'partials/physicallocation_mailing_address.html', requireLogin: true }).
			when('/physicallocation/corpreg/:guid', { title: 'Physical Location and Business Structure', templateUrl: 'partials/physicallocation_corp_registration.html', requireLogin: true }).
            when('/physicallocation/corpbussagent/:guid', { title: 'Physical Location and Business Structure', templateUrl: 'partials/physicallocation_corp_bussagent.html', requireLogin: true }).
			when('/taxrevenue/:guid', { title: 'Tax and Revenue', templateUrl: 'partials/taxrevenue.html', requireLogin: true }).
			when('/infoverification/:guid', { title: 'Information Verificaation', templateUrl: 'partials/informationverification.html', requireLogin: true }).
			when('/payment/:guid', { title: 'Payment', templateUrl: 'partials/payment.html', requireLogin: true }).
			when('/payment/:bblapptype/:guid', { title: 'Payment', templateUrl: 'partials/payment.html', requireLogin: true }).
            when('/renewalpayment/:guid', { title: 'Payment', templateUrl: 'partials/renewalpayment.html', requireLogin: true }).
            when('/paymentfailure/:paymenttype/:guid', { title: 'Payment Failure', templateUrl: 'partials/paymentfailure.html', requireLogin: true }).
            when('/emailconfirmation', { title: 'EmailConfirmation', templateUrl: 'partials/emailconfirmation.html' }).
			when('/receipt/:guid', { title: 'Receipt', templateUrl: 'partials/receipt.html', requireLogin: true }).
			when('/receipt/:bblapptype', { title: 'Receipt', templateUrl: 'partials/receipt.html', controller: 'ReceiptController', requireLogin: true }).
            when('/renewalreceipt/:guid', { title: 'Receipt', templateUrl: 'partials/renewalreceipt.html', requireLogin: true }).
			when('/bblrenewalconfirm/:guid', { title: 'BBL Renewal Information Confirmation', templateUrl: 'partials/bblrenewalinfoconfirm.html', requireLogin: true }).
            when('/bblrenewalpayment/:verify/:guid', { title: 'BBL Renewal Payment Confirmation', templateUrl: 'partials/bblrenewalpayment.html', requireLogin: true }).
			when('/individualbusinesslic/:guid', { title: 'Individual Business License', templateUrl: 'partials/individualbusinesslicense.html', requireLogin: true }).
			when('/ehopeligibility/:guid', { title: 'Determine eHOP Eligibility', templateUrl: 'partials/ehopeligibility.html', requireLogin: true }).
			when('/ehopehomeaddress/:guid', { title: 'Physical Location and Business Structure', templateUrl: 'partials/ehophomeaddress.html', requireLogin: true }).
            when('/nopobox/:guid', { title: 'Physical Location and Business Structure', templateUrl: 'partials/nopobox.html', requireLogin: true }).
            when('/corpnotregisteredaddress/:guid', { title: 'Corp Not Registered', templateUrl: 'partials/corpnotregisteredaddress.html', requireLogin: true }).
            when('/corpreqregisterfirst/:guid', { title: 'Corp Req Register', templateUrl: 'partials/corpreqregisterfirst.html', requireLogin: true }).
            when('/corpreqregwithtradesecond/:guid', { title: 'Corp Req Register ', templateUrl: 'partials/corpreqregwithtradesecond.html', requireLogin: true }).
            when('/corpnotregisteredagent/:guid', { title: 'Corp Req Register ', templateUrl: 'partials/corpnotregisteredagent.html', requireLogin: true }).
            when('/reviewchecklist/:guid', { title: 'Review CheckList', templateUrl: 'partials/reviewchecklist.html', requireLogin: true }).
            when('/renewal/cleanhands/:guid', { title: 'Renewal clean hands', templateUrl: 'partials/renewalcleanhandscertification.html', requireLogin: true }).
            when('/selfcertification/:guid', { title: 'One Family Self Cerfication', templateUrl: 'partials/onefamilyrentalselfcetification.html', requireLogin: true }).
            //when('/inconvenience', { title: 'Inconvenience', templateUrl: 'partials/inconvenience.html' }).
			otherwise({ redirectTo: '/pagenotfound' });
	    $httpProvider.interceptors.push('authInterceptorService');
	}]).


	/*

	 * Run: Runtime configurations for the application, event handlers
	 */
	run(['$rootScope', '$location', '$log', '$window', '$interval', 'requestService', '$document', 'authService', 'appConstants', 'TimerFactory', 'errorFactory',
        function ($rootScope, $location, $log, $window, $interval, requestService, $document, authService, appConstants, TimerFactory, errorFactory) {
            if (localStorage.getItem("ls.authorizationData") != null) {
                authService.fillAuthData();
                if (authService.authentication.isAuth) {
                    errorFactory.loadvalidationmessages();
                    TimerFactory.startTimer();
                }
            }
            TimerFactory.refreshTokenWithTime();
            TimerFactory.generatefreetoken();
            $rootScope.$on('$routeChangeStart', function (evt, next, current) {
                if (next && next.$$route.requireLogin && !authService.authentication.isAuth) {
                    evt.preventDefault();
                    $location.path("/login");
                } else {
                    localStorage.setItem("routechanged", Date.now());
                    $rootScope.title = next.$$route.title;
                }
                $('.modal-backdrop').remove();
                $('body').removeClass('.modal-open');
                TimerFactory.update_lastlytouched_time();
            });

            //monitoring events whether keyboard keys are pressed or mouse events occured on body
            $document.find('body').on('keydown DOMMouseScroll mousewheel mousedown touchstart', function () {
                TimerFactory.update_lastlytouched_time();
            });

            $rootScope.navtoHome = function (e) {
                if (authService.authentication.isAuth)
                    e.target.href = '#/dashboard';
                else
                    e.target.href = '#/login';
            }

            $rootScope.navToMyDCBC = function (e) {
                if (authService.authentication.isAuth)
                    e.target.href = '#/dashboard';
                else
                    e.target.href = '#/login';
            }
        }]).
	/*
	 * Constant : Application constants, global across the entire
	 *            application and it's supporting modules
	 */
	constant('appConstants', {
	    apiServiceBaseUri: 'http://localhost:40001/',
	    stagingUrl: 'http://staging.mybusiness.dc.gov/',
	    clientId: '352549070fb44ce793a5343a5f846dcc',
	    marstringlength: 4,
	    patternvalidations: {
	        "country_US": {
	            zip: {
	                pattern: /^[0-9]+$/,
	                maxlength: 5,
	                minlength: 5
	            },
	            telephone: {
	                pattern: /^[0-9]+$/,
	                maxlength: 10,
	                minlength: 10
	            }
	        },
	        "country_not_US": {
	            zip: {
	                pattern: /^[a-zA-Z0-9 -]*$/,
	                maxlength: 100,
	                minlength: 1
	            },
	            telephone: {
	                pattern: /^[0-9 +-]*$/,
	                maxlength: 100,
	                minlength: 1
	            }
	        }
	    }
	});