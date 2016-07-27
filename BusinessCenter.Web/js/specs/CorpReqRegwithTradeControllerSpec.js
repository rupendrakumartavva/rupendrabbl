describe('CorpReqRegWithTrade Controller Spec', function () {

    var $scope, controller, httpBackend, mockservice, CorpReqRegWithTradeController, localStore, basePath, routeparams, utilityfactory, bblsubmissionfactory, authservice, localstorageservice;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants,
        $routeParams, UtilityFactory, BBLSubmissionFactory, authService, localStorageService) {
        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');

        localStore = (function () {
            var store = {};
            return {
                getItem: function (key) {
                    return store[key];
                },
                setItem: function (key, value) {
                    store[key] = value.toString();
                },
                removeItem: function (key) {
                    delete store[key];
                },
                clear: function () {
                    store = {};
                },
                loggedin: '2'
            };
        })();

        Object.defineProperty(window, 'localStorage', { value: localStore });
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        basePath = appConstants.apiServiceBaseUri;
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        utilityfactory = UtilityFactory;
        bblsubmissionfactory = BBLSubmissionFactory;
        authservice = authService;
        localstorageservice = localStorageService;
    }));
    var errormessages = [
       { "ShortName": "incompleteData", "ErrrorMessage": "All requested information is required in order to save the data you entered.  Please select [OK] to exit without saving or [CANCEL] to stay on the page." },
       { "ShortName": "ehop_inEligible", "ErrrorMessage": "Given your response, you are ineligible for an Expedited Home Occupancy Permit.  Please select [Return to Checklist] and visit the DC Office of Zoning at 1100 4th St., SW or call 202-442-4400 to obtain an HOP." },
       { "ShortName": "verifyandcontinuemessage", "ErrrorMessage": "To revise any of your responses, select [Cancel] and then select the [Return to Checklist] button. To proceed, select [Confirm]." },
       { "ShortName": "corpnodata", "ErrrorMessage": "The File Number you entered does not match the Corporations records. Please check your entry and try again. If your entry does not match, please select [Return to Checklist] and visit the DCRA Corporations Division at 1100 4th St., SW or call  202-442-4400." },
       { "ShortName": "feinssnNonCompliance", "ErrrorMessage": "According to Office of Tax and Revenue (OTR) records, the FEIN you entered is not in compliance with the District of Columbia's Clean Hands requirements. Please click on Tax and Revenue link below to know how to proceed further." },
       { "ShortName": "corpSearchNotClicked", "ErrrorMessage": "You must select [Search Corp Online].  Please click [OK] to proceed without saving the entered data or click [CANCEL] to stay on the page." },
       { "ShortName": "allfieldsNotFilled", "ErrrorMessage": "Please complete all the required fields." },
       { "ShortName": "renewalNavigation", "ErrrorMessage": "To save any updates/changes, please select [CANCEL] to stay on the page or [OK] to leave without saving." },
       { "ShortName": "navigateaway", "ErrrorMessage": "To save any updates/changes, please select [CANCEL] to stay on the page or [OK] to leave without saving." },
       { "ShortName": "hopallfieldsNotFilled", "ErrrorMessage": "Please complete all fields." }, { "ShortName": "corpFileNumberError", "ErrrorMessage": "Please enter your Corporate Registration File Number." },
       { "ShortName": "createChecklistnavigation", "ErrrorMessage": "The data you have selected/entered so far will be lost. You must complete all of the pre-application questions and create a checklist to save your data. Do you want to exit without saving?" },
       { "ShortName": "NextButtonIncompleteData", "ErrrorMessage": "Please provide all requested data and select [Next]." },
       { "ShortName": "ehopSelectionErrorMsg", "ErrrorMessage": "You must select [Confirm eHOP Eligibility].  Please click [OK] to proceed without saving the entered data or click [CANCEL] to stay on the page." },
       { "ShortName": "corp_number_failedstatus", "ErrrorMessage": "According to the Corporations Division's files, the Status of your Corporate Registration is {0}. You must resolve any  issues prior to submitting your Business License application. Please select Return to Checklist and visit the DCRA Corporations Division at 1100 4th St., SW, or call 202-442-4400." },
       { "ShortName": "createCheckList", "ErrrorMessage": "Making changes after your Checklist is created will require you to discard your Application and start a new Application from the beginning. To proceed and create your Checklist select [Confirm]. To review and revise your responses select [Cancel] and select the Revise button on the bottom of the page." },
       { "ShortName": "corp_businessstructure_mismatch", "ErrrorMessage": "The Business Structure you selected in the Pre-Application Questions does not match the Corporations records. Please check your entry and try again. If your entry does not match, please select [Return to Checklist] and visit the DCRA Corporations Division at 1100 4th St., SW or call 202-442-4400." },
       { "ShortName": "donothaveCofo", "ErrrorMessage": "The data selected/entered for this CofO will not be retained. Select [OK] to proceed with this option or [Cancel] to retain the data as presented." },
       { "ShortName": "searchNotClicked", "ErrrorMessage": "Please select [Search Zoning], or click [OK] to proceed without saving, or select [CANCEL] to stay on the page." }, { "ShortName": "renewalpaymentallfieldsNotFilled", "ErrrorMessage": "Please provide all requested data." }
    ];

    describe('test cases when guid is not available and status is underreview', function () {
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            CorpReqRegWithTradeController = controller('CorpReqRegwithTradeController',
               {
                   $scope: $scope, $rootScope: rootscope,
                   $location: $location, mockservice: mockservice,
                   routeParams: routeparams, utilityFactory: utilityfactory,
                   BBLSubmissionfactory: bblsubmissionfactory, authService: authservice
               });
        });

        it('should test init with underreivew status', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('test cases when guid is available and status is underreview', function () {
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            CorpReqRegWithTradeController = controller('CorpReqRegwithTradeController',
               {
                   $scope: $scope, $rootScope: rootscope,
                   $location: $location, mockservice: mockservice,
                   routeParams: routeparams, utilityFactory: utilityfactory,
                   BBLSubmissionfactory: bblsubmissionfactory, authService: authservice
               });
        });

        it('should test init with underreivew status', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('test cases when guid is available and status is underreview', function () {
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "draft" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            CorpReqRegWithTradeController = controller('CorpReqRegwithTradeController',
               {
                   $scope: $scope, $rootScope: rootscope,
                   $location: $location, mockservice: mockservice,
                   routeParams: routeparams, utilityFactory: utilityfactory,
                   BBLSubmissionfactory: bblsubmissionfactory, authService: authservice
               });
        });

        it('should test navToChecklist method', function () {
            CorpReqRegWithTradeController.navToChecklist();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        });

        it('should test navToCorpRegistration method', function () {
            CorpReqRegWithTradeController.navToCorpRegistration();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/corpreg/guid');
        });

        it('should test navToNewBblWelcome method', function () {
            CorpReqRegWithTradeController.navToNewBblWelcome();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });


});