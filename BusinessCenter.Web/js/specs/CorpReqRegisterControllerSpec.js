﻿describe('CorpReqRegister Controller Spec', function () {
    var $scope, controller, httpBackend, mockservice, corp_reqRegistercontroller, localStore, timeout, appconstants, sessionFactory, session, routeparams, utilityfactory, bblsubmissionfactory, errorfactory;
    var basePath, popupfactory, authservice, localstorageservice;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, localStorageService, $rootScope, _$location_, requestService, $httpBackend, appConstants, $timeout, SessionFactory, $routeParams, popupFactory, UtilityFactory, BBLSubmissionFactory, errorFactory, authService) {
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
        timeout = $timeout;
        controller = $controller;
        appconstants = appConstants;
        basePath = appConstants.apiServiceBaseUri;
        sessionFactory = SessionFactory;
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        utilityfactory = UtilityFactory;
        bblsubmissionfactory = BBLSubmissionFactory;
        errorfactory = errorFactory;
        popupfactory = popupFactory;
        authservice = authService;
        localstorageservice = localStorageService;
    }));

    var corp_response = [{
        "UserType": "Y-CORPREG", "FileNumber": "C262873", "MasterId": "5cebf728-9a09-4e68-9fec-c885db83955a", "CBusinessName"
    : "DISTRICT REGISTERED AGENT SERVICES", "TradeName": "", "BusinessStructure": "Corporation (Non-Profit)", "FirstName"
    : "", "MiddleName": "", "LastName": "", "BusinessName": "", "BusinessAddressLine1": "1025 CONNECTICUT AVENUE,N.W. STE. 615", "BusinessAddressLine2": "", "BusinessAddressLine3": "", "BusinessAddressLine4": null, "BusinessCity"
    : "Washington", "BusinessState": "DC", "BusinessCountry": "", "ZipCode": "20036", "Email": "", "EntityStatus": "ACTIVE"
    , "SubCorporationRegId": 0, "UserSelectTpe": null, "Quardrant": null, "UnitType": null, "Unit": null, "Telphone"
    : "", "IsValid": false, "Dropdownlist": null, "OccupancyAddssValidate": null, "DonothaveCof": false, "CorpStatus"
    : "True", "HQStatus": "True"
    }],

    submissionstatus_data = {
        "Status": "Draft", "MasterId": "b101d779-f91a-40e5-a9e0-00a52edf308d", "TradeName": "NA",
        "BusinessStructure": "Corporation (For Profit)", "IsCorporationDivision": true, "IsCoporateRegistration": true,
        "IsResidentAgent": false, "IsIndividual": false, "IsBusinessMustbeinDc": true, "AppType": "B", "IsFEIN": true,
        "DocSubmType": "", "PaymentId": null, "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "COMMUNITY INVESTMENT",
        "CurrentYear": "2016", "CreatedDate": "04/14/2016", "SelectedMailType": "", "PremisesAddress": "1234  29TH Street NW   Washington DC United States 20007",
        "BusinessName": "COMMUNITY INVESTMENT", "IsCategorySelfCertification": false
    },

    businessDataList = [{
        "StreetNumber": null, "AddressID": null, "AddressNumber": null, "AddressNumberSufix": null,
        "Anc": null, "Cluster": null, "Latitude": null, "Longitude": null, "Vote_Prcnct": null, "Ward": null,
        "Xcoord": null, "Ycoord": null, "UserType": "Y-CORPREG", "FileNumber": "900180", "MasterId": "b101d779-f91a-40e5-a9e0-00a52edf308d",
        "CBusinessName": "COMMUNITY INVESTMENT", "TradeName": "NA", "BusinessStructure": "Corporation (For Profit)", "FirstName": "",
        "MiddleName": "", "LastName": "", "BusinessName": "", "BusinessAddressLine1": "1101 30th Street NW",
        "BusinessAddressLine2": "4th Floor", "BusinessAddressLine3": "", "BusinessAddressLine4": "", "BusinessCity": "Washington",
        "BusinessState": "DC", "BusinessCountry": "United States", "ZipCode": "20007", "Email": "", "EntityStatus": "ACTIVE",
        "SubCorporationRegId": 0, "UserSelectTpe": null, "Quardrant": null, "UnitType": null, "Unit": null, "Telphone": "",
        "IsValid": false, "Dropdownlist": null, "OccupancyAddssValidate": "", "DonothaveCof": false, "CorpStatus": null,
        "HQStatus": null, "Zone": null, "Smd": null, "SSL": null, "BusinessStructureStatus": true, "IsDataChange": false
    }];
    var errormessages = [{ "ShortName": "incompleteData", "ErrrorMessage": "All requested information is required in order to save the data you entered.  Please select [OK] to exit without saving or [CANCEL] to stay on the page." }, { "ShortName": "ehop_inEligible", "ErrrorMessage": "Given your response, you are ineligible for an Expedited Home Occupancy Permit.  Please select [Return to Checklist] and visit the DC Office of Zoning at 1100 4th St., SW or call 202-442-4400 to obtain an HOP." }, { "ShortName": "verifyandcontinuemessage", "ErrrorMessage": "To revise any of your responses, select [Cancel] and then select the [Return to Checklist] button. To proceed, select [Confirm]." }, { "ShortName": "corpnodata", "ErrrorMessage": "The File Number you entered does not match the Corporations records. Please check your entry and try again. If your entry does not match, please select [Return to Checklist] and visit the DCRA Corporations Division at 1100 4th St., SW or call  202-442-4400." }, { "ShortName": "feinssnNonCompliance", "ErrrorMessage": "According to Office of Tax and Revenue (OTR) records, the FEIN you entered is not in compliance with the District of Columbia's Clean Hands requirements. Please click on Tax and Revenue link below to know how to proceed further." }, { "ShortName": "corpSearchNotClicked", "ErrrorMessage": "You must select [Search Corp Online].  Please click [OK] to proceed without saving the entered data or click [CANCEL] to stay on the page." }, { "ShortName": "allfieldsNotFilled", "ErrrorMessage": "Please complete all the required fields." }, { "ShortName": "renewalNavigation", "ErrrorMessage": "To save any updates/changes, please select [CANCEL] to stay on the page or [OK] to leave without saving." }, { "ShortName": "navigateaway", "ErrrorMessage": "To save any updates/changes, please select [CANCEL] to stay on the page or [OK] to leave without saving." }, { "ShortName": "hopallfieldsNotFilled", "ErrrorMessage": "Please complete all fields." }, { "ShortName": "corpFileNumberError", "ErrrorMessage": "Please enter your Corporate Registration File Number." }, { "ShortName": "createChecklistnavigation", "ErrrorMessage": "The data you have selected/entered so far will be lost. You must complete all of the pre-application questions and create a checklist to save your data. Do you want to exit without saving?" }, { "ShortName": "NextButtonIncompleteData", "ErrrorMessage": "Please provide all requested data and select [Next]." }, { "ShortName": "ehopSelectionErrorMsg", "ErrrorMessage": "You must select [Confirm eHOP Eligibility].  Please click [OK] to proceed without saving the entered data or click [CANCEL] to stay on the page." }, { "ShortName": "corp_number_failedstatus", "ErrrorMessage": "According to the Corporations Division's files, the Status of your Corporate Registration is {0}. You must resolve any  issues prior to submitting your Business License application. Please select Return to Checklist and visit the DCRA Corporations Division at 1100 4th St., SW, or call 202-442-4400." }, { "ShortName": "createCheckList", "ErrrorMessage": "Making changes after your Checklist is created will require you to discard your Application and start a new Application from the beginning. To proceed and create your Checklist select [Confirm]. To review and revise your responses select [Cancel] and select the Revise button on the bottom of the page." }, { "ShortName": "corp_businessstructure_mismatch", "ErrrorMessage": "The Business Structure you selected in the Pre-Application Questions does not match the Corporations records. Please check your entry and try again. If your entry does not match, please select [Return to Checklist] and visit the DCRA Corporations Division at 1100 4th St., SW or call 202-442-4400." }, { "ShortName": "donothaveCofo", "ErrrorMessage": "The data selected/entered for this CofO will not be retained. Select [OK] to proceed with this option or [Cancel] to retain the data as presented." }, { "ShortName": "searchNotClicked", "ErrrorMessage": "Please select [Search Zoning], or click [OK] to proceed without saving, or select [CANCEL] to stay on the page." }, { "ShortName": "renewalpaymentallfieldsNotFilled", "ErrrorMessage": "Please provide all requested data." }];

    describe('test cases when the accessed application is in guid is not available', function () {

        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            corp_reqRegistercontroller = controller('CorpReqRegisterController',
                {
                    $scope: $scope,
                    $rootScope: rootscope,
                    $location: $location,
                    mockservice: mockservice,
                    appConstants: appconstants,
                    timeout: timeout,
                    SessionFactory: sessionFactory,
                    routeParams: routeparams,
                    UtilityFactory: utilityfactory,
                    BBLSubmissionFactory: bblsubmissionfactory,
                    errorFactory: errorfactory,
                    popupFactory: popupfactory,
                    authService: authservice
                });
        });

        it('should test init with no guid available', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('when status is under review', function () {
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            corp_reqRegistercontroller = controller('CorpReqRegisterController',
            {
                $scope: $scope,
                $rootScope: rootscope,
                $location: $location,
                mockservice: mockservice,
                appConstants: appconstants,
                timeout: timeout,
                SessionFactory: sessionFactory,
                routeParams: routeparams,
                UtilityFactory: utilityfactory,
                BBLSubmissionFactory: bblsubmissionfactory,
                errorFactory: errorfactory,
                popupFactory: popupfactory,
                authService: authservice
            });
        });

        it("should test init", function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('when status is draft', function () {

        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            spyOn(popupfactory, 'showpopup').and.returnValue(true);

            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond(submissionstatus_data);
            httpBackend.when('POST', basePath + 'api/BBLApplication/GetCorpDetails').respond(corp_response);
            httpBackend.when('POST', basePath + 'api/BBLApplication/BusinessDataList').respond(businessDataList);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);

            localStorage.setItem("MasterId", "12345678");
            corp_reqRegistercontroller = controller('CorpReqRegisterController',
             {
                 $scope: $scope,
                 $rootScope: rootscope,
                 $location: $location,
                 mockservice: mockservice,
                 appConstants: appconstants,
                 timeout: timeout,
                 SessionFactory: sessionFactory,
                 routeParams: routeparams,
                 UtilityFactory: utilityfactory,
                 BBLSubmissionFactory: bblsubmissionfactory,
                 errorFactory: errorfactory,
                 popupFactory: popupfactory,
                 authService: authservice
             });
        });

        it("should test init with draft submissions", function () {
            httpBackend.flush();
            expect(corp_reqRegistercontroller.corpregistraion.foundinfo).toBeTruthy();
        });

        it("should test getCorpRegInfo method when form is invalid", function () {
            corp_reqRegistercontroller.contact_us = {}
            corp_reqRegistercontroller.contact_us.$invalid = true;
            corp_reqRegistercontroller.getCorpRegInfo();
            httpBackend.flush();
            expect(corp_reqRegistercontroller.searchZoningClicked).toBeTruthy();
        });

        it("should test getCorpRegInfo method when form is valid", function () {
            corp_reqRegistercontroller.contact_us = {}
            corp_reqRegistercontroller.contact_us.$invalid = false;
            corp_reqRegistercontroller.getCorpRegInfo();
            httpBackend.flush();
            expect(corp_reqRegistercontroller.searchZoningClicked).toBeTruthy();
            expect(corp_reqRegistercontroller.corpregistraion.foundinfo).toBeTruthy();
            expect(angular.equals(corp_reqRegistercontroller.Businessdata, businessDataList)).toBeTruthy();
        });

        it('should test toggleRadio when correct selected', function () {
            corp_reqRegistercontroller.toggleRadio('testcorrect');
            httpBackend.flush();
            timeout.flush();
            expect(corp_reqRegistercontroller.corpregistraion.addresscorrect).toBeTruthy();
        });

        it('should test toggleRadio when incorrect selected', function () {
            httpBackend.flush();
            timeout.flush();
            corp_reqRegistercontroller.toggleRadio('testincorrect');
            expect(corp_reqRegistercontroller.corpregistraion.addresscorrect).toBeFalsy();
        });

        it('should test togglebusiness when correct selected', function () {
            httpBackend.flush();
            timeout.flush();
            corp_reqRegistercontroller.togglebusiness('testcorrect');
            expect(corp_reqRegistercontroller.corpregistraion.busiaddresscorrect).toBeTruthy();
        });

        it('should test togglebusiness when incorrect selected', function () {
            timeout.flush();
            httpBackend.flush();
            corp_reqRegistercontroller.togglebusiness('testincorrect');
            expect(corp_reqRegistercontroller.corpregistraion.busiaddresscorrect).toBeFalsy();
        });

        it('should test navToChecklist', function () {
            corp_reqRegistercontroller.navToChecklist();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        });

        it('should test navToNewBblWelcome', function () {
            httpBackend.flush();
            corp_reqRegistercontroller.navToNewBblWelcome();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test checkSearchZoning', function () {
            corp_reqRegistercontroller.checkSearchZoning();
            httpBackend.flush();
            expect(corp_reqRegistercontroller.searchZoningClicked).toBeFalsy();
        });

        it('should test checkAndExit when file name is undefined and path is mybbl', function () {
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCorpAgent').respond({ "Result": "true" });

            corp_reqRegistercontroller.corpregistraion.busiaddresscorrect = true;
            corp_reqRegistercontroller.corpregistraion.addresscorrect = true;
            corp_reqRegistercontroller.checkAndExit('mybbl');
            timeout.flush();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test checkAndExit when file name is defined and path is mybbl', function () {
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCorpAgent').respond({ "Result": "true" });
            corp_reqRegistercontroller.corpregistraion.FileNumber = "testnum";
            corp_reqRegistercontroller.Businessdata = businessDataList;
            corp_reqRegistercontroller.checkAndExit('mybbl');
            timeout.flush();
            httpBackend.flush();
        });

        it('should test checkAndExit when file name is defined and path is mybbl', function () {
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCorpAgent').respond({ "Result": "true" });
            corp_reqRegistercontroller.corpregistraion.busiaddresscorrect = true;
            corp_reqRegistercontroller.corpregistraion.FileNumber = "testnum";
            corp_reqRegistercontroller.Businessdata = businessDataList;
            corp_reqRegistercontroller.corpregistraion.addresscorrect = true;
            corp_reqRegistercontroller.checkAndExit('mybbl');
            timeout.flush();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test checkAndExit when file name is undefined and path is appchecklist', function () {
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCorpAgent').respond({ "Result": "true" });
            corp_reqRegistercontroller.corpregistraion.busiaddresscorrect = true;
            corp_reqRegistercontroller.corpregistraion.addresscorrect = true;
            corp_reqRegistercontroller.checkAndExit('appchecklist');
            httpBackend.flush();
            timeout.flush();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        });

        it('should test checkAndExit when file name is undefined and when cilcked next', function () {
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCorpAgent').respond({ "Result": "true" });
            timeout.flush();
            corp_reqRegistercontroller.submissionStatusData = submissionstatus_data;
            corp_reqRegistercontroller.navToBusinessAgent();
        });

        it('should test checkAndExit when file name is undefined and when cilcked next', function () {
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCorpAgent').respond({ "Result": "true" });
            timeout.flush();
            corp_reqRegistercontroller.Businessdata = [];
            corp_reqRegistercontroller.Businessdata[0] = {};
            corp_reqRegistercontroller.corpregistraion.busiaddresscorrect = true;
            corp_reqRegistercontroller.corpregistraion.addresscorrect = true;
            corp_reqRegistercontroller.submissionStatusData = submissionstatus_data;
            corp_reqRegistercontroller.navToBusinessAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/corpbussagent/guid');
        });

        it('should test checkAndExit when file name is undefined', function () {
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCorpAgent').respond({ "Result": "true" });
            timeout.flush();
            corp_reqRegistercontroller.corpregistraion.FileNumber = undefined;
            corp_reqRegistercontroller.checkAndExit('testpath');
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/testpath');
        });

        it('should test corpErrorMsg', function () {
            corp_reqRegistercontroller.corpErrorMsg();
        });

    });
});