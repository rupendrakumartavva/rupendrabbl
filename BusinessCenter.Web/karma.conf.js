

// Karma configuration
// Generated on Thu Feb 04 2016 18:21:57 GMT+0530 (India Standard Time)

module.exports = function (config) {
    config.set({

        // base path that will be used to resolve all patterns (eg. files, exclude)
        basePath: '',


        // frameworks to use
        // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
        frameworks: ['jasmine'],


        // list of files / patterns to load in the browser
        files: [
            "libs/angularjs/angular.min.js",
                "libs/angularjs/**.js",
                "libs/jquery/**.js",
                "js/**.js",
                "js/**/**.js",

         // JSON fixture
       {
           pattern: 'js/*.json',
           watched: true,
           served: true,
           included: false
       }
        ],


        // list of files to exclude
        exclude: [
                    "js/specs/AboutUsControllerSpec.js",
                    "js/specs/AccountRecoveryControllerSpec.js",
                    "js/specs/ApplicationChecklistControllerSpec.js",
                    "js/specs/AssociateBasicBusinessLicenseControllerSpec.js",
                    "js/specs/BBLRenewalInfoConfirmControllerSpec.js",
                    "js/specs/CorpNotRegisteredAddressControllerSpec.js",
                    "js/specs/CorpNotRegisteredAgentControllerSpec.js",
                    "js/specs/CorpReqRegisterControllerSpec.js",
                    "js/specs/CorpReqRegwithTradeControllerSpec.js",
                    "js/specs/DashboardControllerSpec.js",
                    "js/specs/DeleteAccountControllerSpec.js",
                    //"js/specs/eHOPEligibilityControllerSpec.js",
                    "js/specs/eHOPHomeAddressControllerSpec.js",
                    "js/specs/EmailConfirmationControllerSpec.js",
                    "js/specs/EmailVerificationControllerSpec.js",
                    "js/specs/ForgotPasswordControllerSpec.js",
                    "js/specs/ForgotUsernameControllerSpec.js",
                    "js/specs/HeaderControllerSpec.js",
                    "js/specs/HelpControllerSpec.js",
                    "js/specs/HomeControllerSpec.js",
                    "js/specs/IndividualBusinessLicenseControllerSpec.js",
                    "js/specs/InformationVerificationControllerSpec.js",
                    "js/specs/LoginControllerSpec.js",
                    "js/specs/LookupControllerSpec.js",
                    "js/specs/MyBBLControllerSpec.js",
                    "js/specs/NewBBLWelcomeControllerSpec.js",
                    "js/specs/NewPasswordControllerSpec.js",
                    "js/specs/NoPoBoxControllerSpec.js",
                    "js/specs/OnefamilyRentalSelfCetificationControllerSpec.js",
                    "js/specs/PageNotfoundControllerSpec.js",
                    "js/specs/PaymentControllerSpec.js",
                    "js/specs/PaymentFailureControllerSpec.js",
                    "js/specs/PhysicalLocationCofoControllerSpec.js",
                    "js/specs/PhysicalLocationCorpBussAgentControllerSpec.js",
                    "js/specs/PhysicalLocationHopControllerSpec.js",
                    "js/specs/PhysicallocMailingControllerSpec.js",
                    "js/specs/PreAppQuestionsControllerSpec.js",
                    "js/specs/ProfileControllerSpec.js",
                    "js/specs/ProfileLogoutControllerSpec.js",
                    "js/specs/QuickSearchControllerSpec.js",
                    "js/specs/QuickSearchResultControllerSpec.js",
                    "js/specs/QuickSearchSaveControllerSpec.js",
                    "js/specs/ReceiptControllerSpec.js",
                    "js/specs/RegisterControllerSpec.js",
                    "js/specs/RenewalCleanHandsControllerSpec.js",
                    "js/specs/RenewalPaymentControllerSpec.js",
                    "js/specs/RenewalReceiptControllerSpec.js",
                    "js/specs/RenewalSupportingDocsControllerSpec.js",
                    "js/specs/ReviewCheckListControllerSpec.js",
                    "js/specs/SecurityQuestionControllerSpec.js",
                    "js/specs/SessionFactorySpec.js",
                    "js/specs/SupportingDocsUploadControllerSpec.js",
                    "js/specs/TaxRevenueControllerSpec.js",
                    "js/specs/TermsServicesControllerSpec.js",
                    "js/specs/UserPreviousEmailConfirmationControllerSpec.js",
                    "js/specs/VerifyBusinessBeforeAssociationControllerSpec.js",
                    "js/specs/VerifyEmailControllerSpec.js",
                    "js/controllers/SearchResultController.js"

        ],


        // preprocess matching files before serving them to the browser
        // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
        //preprocessors: {
        //    'js/**/**.js': 'coverage'
        //},

        preprocessors: {

        },


        // test results reporter to use
        // possible values: 'dots', 'progress'
        // available reporters: https://npmjs.org/browse/keyword/karma-reporter
        //reporters: ['progress','coverage'],

        reporters: ['progress'],

        //coverageReporter: {
        //    type: 'html',
        //    dir: 'coverage/'
        //},

        // web server port
        port: 9876,


        // enable / disable colors in the output (reporters and logs)
        colors: true,


        // level of logging
        // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
        logLevel: config.LOG_INFO,


        // enable / disable watching file and executing tests whenever any file changes
        autoWatch: true,


        // start these browsers
        // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher

        browsers: ['Chrome'],


        // Continuous Integration mode
        // if true, Karma captures browsers, runs the tests and exits
        singleRun: false
    });
};
