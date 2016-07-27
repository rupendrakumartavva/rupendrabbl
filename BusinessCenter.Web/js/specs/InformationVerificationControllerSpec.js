describe('Information Verification Controller Spec', function () {
    var $scope, controller, httpBackend, mockservice, infoVerifyController, localStore, basePath, routeparams, utilityfactory, appconstants, bblsubmissionfac, errorfactory;
    var authservice, localstorageservice, sessionfactory;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, $routeParams, UtilityFactory,
        BBLSubmissionFactory, errorFactory, SessionFactory, authService, localStorageService) {
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
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        utilityfactory = UtilityFactory;
        basePath = appConstants.apiServiceBaseUri;
        bblsubmissionfac = BBLSubmissionFactory;
        appconstants = appConstants;
        errorfactory = errorFactory;
        authservice = authService;
        localstorageservice = localStorageService;
        sessionfactory = SessionFactory;
    }));

    var data = {
        "MasterID": "39d78d76-8ab7-4a4b-9843-18aabfb91508",
        "OccupanyNumber": "E1591557",
        "DateofIssue": "01/01/1990",
        "CorpFileNo": "C880040",
        "OrgType": "Corporation (Non-Profit)",
        "BusinessOwner": "DESO & BUCKLEY",
        "FienNumber": "22-1234567",
        "TradeName": "",
        "DocType": "IN",
        "GrandTotal": 72.6000,
        "IsCofo": false,
        "IsHomeBased": true,
        "IsSubmissionCofo": false,
        "IsSubmissionHop": false,
        "IsSubmissioneHop": true,
        "FEIN": "",
        "IsFEIN": true,
        "HeadQName": "  ",
        "HeadQBName": "",
        "HeadQAddress": "1828 L ST., N.W.,#270  ",
        "HeadQCity": "Washington",
        "HeadQState": "DC",
        "HeadQCountry": "",
        "HeadQZip": "20036",
        "HeadQEmail": "",
        "HeadQTelePhone": "",
        "PremiseBName": "",
        "PremiseName": "",
        "PremiseAddress": "1123 1/2 EAST CAPITOL STREET SE EAST CAPITOL   Street",
        "PremiseCity": "WASHINGTON",
        "PremiseState": "DC",
        "PremiseCountry": "",
        "PremiseZip": "20003",
        "PremiseEmail": "",
        "PremiseTelePhone": "12345",
        "PremiseQuadrant": "SE",
        "PremiseUnitType": "",
        "PremiseUnit": "",
        "MailingBName": "",
        "MailingName": "",
        "MailingAddress": "1123 1/2 EAST CAPITOL STREET SE EAST CAPITOL Street",
        "MailingCity": "WASHINGTON",
        "MailingState": "DC",
        "MailingCountry": "",
        "MailingZip": "20003",
        "MailingEmail": "",
        "MailingTelePhone": "12345",
        "MailingQuadrant": "SE",
        "MailingUnitType": "",
        "MailingUnit": "",
        "AgentBName": "A ABLE ACCIDENT ADVOCATE",
        "AgentName": "A ABLE ACCIDENT ADVOCATE  ",
        "AgentAddress": "5225 WISCONSIN AVENUE NW  ",
        "AgentCity": "WASHINGTON",
        "AgentState": "DC",
        "AgentCountry": "",
        "AgentZip": "20015",
        "AgentEmail": "",
        "AgentTelePhone": "123456789",
        "IsRaoFeeApplied": false,
        "ApplicationFee": 0.0,
        "CategoryLicenseFee": 0.0000,
        "EndorsementFee": 0.0000,
        "SubTotal": 0.0000,
        "TechFee": 0.0000,
        "TotalFee": 72.6000,
        "Isehop": true,
        "ServiceCheckList": [
           {
               "Endorsement": "General Business",
               "CategoryId": "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",
               "LicenseCategory": "Charitable Exempt",
               "Units": "NA ",
               "ApplicationFee": 0.0,
               "CategoryLicenseFee": 0.0000,
               "EndorsementFee": 0.0000,
               "SubTotal": 0.0000,
               "TechFee": 0.0000,
               "TotalFee": 0.0000,
               "CategoryCode": "4001",
               "IsRaoFeeApplied": false,
               "LicenseDuration": "TWO (2) YEAR"
           },
           {
               "Endorsement": "General Business",
               "CategoryId": "1B69B449-ECAB-41AE-89A9-E2F76329A52B",
               "LicenseCategory": "General Business Licenses",
               "Units": "NA ",
               "ApplicationFee": 0.0,
               "CategoryLicenseFee": 0.0000,
               "EndorsementFee": 0.0000,
               "SubTotal": 0.0000,
               "TechFee": 0.0000,
               "TotalFee": 0.0000,
               "CategoryCode": "4003",
               "IsRaoFeeApplied": false,
               "LicenseDuration": "TWO (2) YEAR"
           },
           {
               "Endorsement": "General Business",
               "CategoryId": "F8DD1CE1-6645-4A58-8739-0B018565B20C",
               "LicenseCategory": "Online Music Business",
               "Units": "NA",
               "ApplicationFee": 0.0,
               "CategoryLicenseFee": 0.0,
               "EndorsementFee": 0.0,
               "SubTotal": 0.0,
               "TechFee": 0.0,
               "TotalFee": 0.0,
               "CategoryCode": "4003",
               "IsRaoFeeApplied": false,
               "LicenseDuration": "TWO (2) YEAR"
           }
        ],
        "DocumentList": [
           {
               "MasterId": "39d78d76-8ab7-4a4b-9843-18aabfb91508",
               "SubmissionId": "DAPP15969136",
               "SubmissionCategoryID": 4,
               "ApprovedBy": "",
               "DocRequired": "Affidavit",
               "Agency": "APPLICANT",
               "Division": "NA",
               "Div": "NA",
               "FileName": "",
               "FileLocation": "",
               "DocStatus": "Open",
               "Description": "A written and sworn version of declaration/letter stating that exemption status is/was in place on the date of the submission of application",
               "Endorsement": "General Business",
               "License": "Charitable Exempt",
               "CategoryID": "81",
               "ShortName": "Affidavit",
               "IsUpload": false,
               "UploadFileName": "",
               "CheckListType": "Document",
               "CategoryCode": "4001",
               "LicenseName": "DAPP15969136"
           },
           {
               "MasterId": "39d78d76-8ab7-4a4b-9843-18aabfb91508",
               "SubmissionId": "DAPP15969136",
               "SubmissionCategoryID": 4,
               "ApprovedBy": "",
               "DocRequired": "IRS Determination Letter",
               "Agency": "IRS",
               "Division": "Federal",
               "Div": "FED",
               "FileName": "",
               "FileLocation": "",
               "DocStatus": "Open",
               "Description": "An official ruling letter issued by the Internal Revenue Service that allows the organization to claim/have exempt status.",
               "Endorsement": "General Business",
               "License": "Charitable Exempt",
               "CategoryID": "83",
               "ShortName": "IRSDetermLtr",
               "IsUpload": false,
               "UploadFileName": "",
               "CheckListType": "Document",
               "CategoryCode": "4001",
               "LicenseName": "DAPP15969136"
           }
        ],
        "LicenseNumber": "DAPP15969136"
    }

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


    describe('when guid is not available', function () {

        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            infoVerifyController = controller('InformationVerificationController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, mockservice: mockservice,
                routeParams: routeparams, utilityFac: utilityfactory,
                appConstants: bblsubmissionfac, BBLSubmissionFactory: bblsubmissionfac
            });
        });

        it('should test status with underreview', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('when application status is underreview', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            infoVerifyController = controller('InformationVerificationController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, mockservice: mockservice,
                routeParams: routeparams, utilityFac: utilityfactory,
                appConstants: bblsubmissionfac, BBLSubmissionFactory: bblsubmissionfac,
                errorFactory: errorfactory
            });
        });

        it('should test status with underreview', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('when application status is draft', function () {
        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "DRAFT" });
            httpBackend.when('POST', basePath + 'api/BBLApplication/VerficationDetails').respond(data);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            infoVerifyController = controller('InformationVerificationController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, mockservice: mockservice,
                routeParams: routeparams, utilityFac: utilityfactory,
                appConstants: bblsubmissionfac, BBLSubmissionFactory: bblsubmissionfac,
                errorFactory: errorfactory
            });
        });
        it('should test status with draft', function () {
            httpBackend.flush();
            expect(infoVerifyController.SubVerfication).toBeDefined();
            expect(infoVerifyController.MailingDetails).toBeDefined();
            expect(JSON.parse(localStorage.getItem("MailingDetails")).length).toEqual(1);
        });

        it('should test navToMyBBL method', function () {
            infoVerifyController.navToMyBBL();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navToChecklist method', function () {
            infoVerifyController.navToChecklist();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        });

        it('should test continueToNavigate method', function () {
            infoVerifyController.continueToNavigate();
            expect($location.path).toHaveBeenCalledWith('/payment/guid');
        });

        it('should test navToPayment  method', function () {
            infoVerifyController.navToPayment();
        });

        it('should test stayonthePage   method', function () {
            infoVerifyController.stayonthePage();
        });

        it('should test expandCollapse', function () {
            var event = { target: "" };
            infoVerifyController.expandCollapse(event);
        });

    });
});