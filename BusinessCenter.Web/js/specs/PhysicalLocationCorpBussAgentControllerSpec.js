describe('Physical Location Corp Buss Agent Controller Spec', function () {


    var scope, bblsubmissionfactory,controller, httpBackend, mockservice, PhysicalLocationCorpBussAgentController, localStore, appConstants, timeout, routeparams, utilityfac;
    var sessionfactory, popupfactory, errorfactory, authservice, localstorageservice;
    beforeEach(module('DCRA'));

    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, $timeout, $routeParams, UtilityFactory,
        BBLSubmissionFactory, SessionFactory, popupFactory, errorFactory, authService, localStorageService) {
        rootscope = $rootScope.$new();
        $location = _$location_;
        scope = $rootScope.$new();
        spyOn($location, 'path');
        localStore = (function () {
            var store = {};
            return {
                getItem: function (key) {
                    return store[key];
                },
                setItem: function (key, value) {
                    store[key] = value;
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
        appConstants = appConstants;
        basePath = appConstants.apiServiceBaseUri;
        timeout = $timeout;
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        utilityfac = UtilityFactory;
        errorfactory = errorFactory;
        authservice = authService;
        localstorageservice = localStorageService;
        bblsubmissionfactory = BBLSubmissionFactory;
        sessionfactory = SessionFactory;
        popupfactory = popupFactory;
    }));
    var business_data = [];
    business_data[0] = {
        BusinessAddressLine1: "5225 WISCONSIN AVENUE NW",
        BusinessAddressLine2: null,
        BusinessAddressLine3: null,
        BusinessAddressLine4: null,
        BusinessCity: "WASHINGTON",
        BusinessCountry: "",
        BusinessName: "A ABLE ACCIDENT ADVOCATE",
        BusinessState: "DC",
        BusinessStructure: null,
        CBusinessName: null,
        CorpStatus: true,
        DonothaveCof: false,
        Dropdownlist: null,
        Email: null,
        EntityStatus: null,
        FileNumber: "C880040",
        FirstName: "A ABLE ACCIDENT ADVOCATE",
        HQStatus: true,
        IsValid: true,
        LastName: null,
        MasterId: "12a73156-c14f-4685-9ba5-fcb4bd9559d5",
        MiddleName: null,
        OccupancyAddssValidate: null,
        Quardrant: null,
        SubCorporationRegId: 0,
        Telphone: "123456789",
        TradeName: null,
        Unit: null,
        UnitType: "",
        UserSelectTpe: null,
        UserType: "Y-CORPAGENT",
        ZipCode: "20015"
    };
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

    describe('it should test when no guid available', function () {
        beforeEach(function () {
            spyOn(utilityfac, 'ifGuidAvailable').and.returnValue(false);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            PhysicalLocationCorpBussAgentController = controller('PhysicalLocationCorpBussAgentConroller', {
                $scope: scope, $rootScope: rootscope,
                $location: $location, mockservice: mockservice,
                appConstants: appConstants, timeout: timeout,
                routeParams: routeparams, utilityfactory: utilityfac,
                errorfactory: errorfactory, SessionFactory: sessionfactory,
                popupFactory: popupfactory, BBLSubmissionFactory: bblsubmissionfactory,
                authService: authservice
            });
        });

        it('should test when no guid is avalable', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

    });

    describe('it should test when guid is available and status is underreview', function () {
        beforeEach(function () {
            spyOn(utilityfac, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            httpBackend.when('POST', basePath + 'api/BBLApplication/GetCorpAgent').respond(business_data);
            PhysicalLocationCorpBussAgentController = controller('PhysicalLocationCorpBussAgentConroller', {
                $scope: scope, $rootScope: rootscope,
                $location: $location, mockservice: mockservice,
                appConstants: appConstants, timeout: timeout,
                routeParams: routeparams, utilityfactory: utilityfac,
                errorfactory: errorfactory, SessionFactory: sessionfactory,
                popupFactory: popupfactory, BBLSubmissionFactory: bblsubmissionfactory,
                authService: authservice
            });
        });

        it('should test when guid is avalable', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('it should test when guid is available and status is draft', function () {
        beforeEach(function () {
            spyOn(utilityfac, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "Draft" });
            httpBackend.when('POST', basePath + 'api/BBLApplication/GetCorpAgent').respond(business_data);
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCorpAgent').respond(true);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            PhysicalLocationCorpBussAgentController = controller('PhysicalLocationCorpBussAgentConroller', {
                $scope: scope, $rootScope: rootscope,
                $location: $location, mockservice: mockservice,
                appConstants: appConstants, timeout: timeout,
                routeParams: routeparams, utilityfactory: utilityfac,
                errorfactory: errorfactory, SessionFactory: sessionfactory,
                popupFactory: popupfactory, BBLSubmissionFactory: bblsubmissionfactory,
                authService: authservice
            });
        });

        it('should test when guid is avalable', function () {
            httpBackend.flush();
            //expect(PhysicalLocationCorpBussAgentController.busiaddresscorrect).toBeTruthy();
        });

        it('should test setErrorMsg', function () {
            PhysicalLocationCorpBussAgentController.setErrorMsg();
            //expect(PhysicalLocationCorpBussAgentController.busiaddresscorrect).toBeTruthy();
        });

        it('should test togglebusiness', function () {
            PhysicalLocationCorpBussAgentController.togglebusiness("incorrect");
            expect(PhysicalLocationCorpBussAgentController.busiaddresscorrect).toBeFalsy();
        });

        it('should test togglebusiness when address is correct', function () {
            PhysicalLocationCorpBussAgentController.togglebusiness("correct");
            expect(PhysicalLocationCorpBussAgentController.busiaddresscorrect).toBeTruthy();
        });

        //it('should test navigateAnyway', function () {
        //    PhysicalLocationCorpBussAgentController.navigatepath="testpath"
        //    PhysicalLocationCorpBussAgentController.navigateAnyway();
        //    expect($location.path).toHaveBeenCalledWith('/testpath');
        //});

        //it('should test dontNavigate', function () {
        //   // PhysicalLocationCorpBussAgentController.navigatepath = "testpath"
        //    PhysicalLocationCorpBussAgentController.dontNavigate();
        //    //expect($location.path).toHaveBeenCalledWith('/testpath');
        //});

        it('should test checkAndExit when path is other than mybbl', function () {
            PhysicalLocationCorpBussAgentController.checkAndExit("app");
            expect($location.path).toHaveBeenCalledWith('/app/guid');
        });

        it('should test checkAndExit when path is mybbl', function () {
            PhysicalLocationCorpBussAgentController.checkAndExit("mybbl");
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test checkAndExit when path is mybbl', function () {
            PhysicalLocationCorpBussAgentController.Businessdata = business_data;
            PhysicalLocationCorpBussAgentController.busiaddresscorrect = true;
            PhysicalLocationCorpBussAgentController.checkAndExit("mybbl");
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navToMailingAddress when IsCategorySelfCertification is true', function () {
            PhysicalLocationCorpBussAgentController.submissionStatusData = {};
            PhysicalLocationCorpBussAgentController.submissionStatusData.IsCategorySelfCertification = true;
            PhysicalLocationCorpBussAgentController.Businessdata = business_data;
            PhysicalLocationCorpBussAgentController.busiaddresscorrect = true;
            PhysicalLocationCorpBussAgentController.navToMailingAddress();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/selfcertification/guid');
        });

        it('should test navToMailingAddress when IsCategorySelfCertification is false', function () {
            PhysicalLocationCorpBussAgentController.submissionStatusData = {};
            PhysicalLocationCorpBussAgentController.submissionStatusData.IsCategorySelfCertification = false;
            PhysicalLocationCorpBussAgentController.Businessdata = business_data;
            PhysicalLocationCorpBussAgentController.busiaddresscorrect = true;
            PhysicalLocationCorpBussAgentController.navToMailingAddress();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/address/guid');
        });
    });
});