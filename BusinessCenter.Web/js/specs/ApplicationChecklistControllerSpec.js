describe('Application CheckList Controller Spec', function () {


    var $scope, controller, httpBackend, mockservice, ApplicationChecklistController, localStore, basePath, appconstants, utilityfactory, routeParams, bblsubmissionfac;
    var supportingDocsData = {
        "result": [{
            "MasterId": "eb08c8b1-f439-4397-ae44-e72a75c7141a", "BblServiceDoc": [{
                "MasterId": "eb08c8b1-f439-4397-ae44-e72a75c7141a"
        , "SubmissionId": "DAPP16000245", "SubmissionCategoryID": 2, "ApprovedBy": "", "DocRequired": "Certificate of Liability Insurance", "Agency": "APPLICANT", "Division": "NA", "Div": "NA", "FileName": "", "FileLocation": ""
        , "DocStatus": "Open", "Description": "Applicant shall furnish a certificate of insurance for the license period; therefore, each applicant must secure commercial general liability insurance.  Certificate of Insurance with DCRA-Business Licensing Division and address listed as the Certificate Holder.  Guidelines for Class A, Class B, Class C, Class G and Class H licenses are listed in the Information Packet for this License Category or at dcra.dc.gov.",
                "Endorsement": "General Service and Repair",
                "License": "Home Improvement Contractor", "CategoryID": "116", "ShortName": "Insurance", "IsUpload": false, "UploadFileName"
                   : "", "CheckListType": "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000245"
            }, {
                "MasterId": "eb08c8b1-f439-4397-ae44-e72a75c7141a"
        , "SubmissionId": "DAPP16000245", "SubmissionCategoryID": 2, "ApprovedBy": "", "DocRequired": "Police Criminal History Report", "Agency": "APPLICANT", "Division": "NA", "Div": "NA", "FileName": "", "FileLocation": "",
                "DocStatus": "Open", "Description": "DC Residents must submit a certified copy of Form PD 70 from the Metropolitan  Police Department.  Non-DC Residents must submit a certified copy of a Police Criminal History Report from the applicant's jurisdiction of residence dated within 30 days of submission.", "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "117", "ShortName": "CrimHistory"
        , "IsUpload": false, "UploadFileName": "", "CheckListType": "Document", "CategoryCode": "4202", "LicenseName"
        : "DAPP16000245"
            }, {
                "MasterId": "eb08c8b1-f439-4397-ae44-e72a75c7141a", "SubmissionId": "DAPP16000245",
                "SubmissionCategoryID": 2, "ApprovedBy": "", "DocRequired": "Designation Letter", "Agency": "DCRA", "Division": "Business Licensing Division (BLD)", "Div": "BLD", "FileName": "", "FileLocation": "", "DocStatus": "Open", "Description": "A form document that lists salespersons employed with Home Improvement Contactor's business (HIC).", "Endorsement"
                : "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "118", "ShortName"
                : "EmployeeList", "IsUpload": false, "UploadFileName": "", "CheckListType": "Document", "CategoryCode": "4202"
            , "LicenseName": "DAPP16000245"
            }, {
                "MasterId": "eb08c8b1-f439-4397-ae44-e72a75c7141a", "SubmissionId": "DAPP16000245"
            , "SubmissionCategoryID": 2, "ApprovedBy": "", "DocRequired": "Surety Bond - Home Improvement Surety Bond"
            , "Agency": "DCRA", "Division": "Business Licensing Division (BLD)", "Div": "BLD", "FileName": "", "FileLocation"
        : "", "DocStatus": "Open", "Description": "A bond obtained by applicant from an insurance company, surety company or post the full amount with DCRA, if/of their choosing to cover the full duration of the license period (two years) in the amount of $25,000.", "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "119", "ShortName": "Surety Bond", "IsUpload": false, "UploadFileName"
        : "", "CheckListType": "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000245"
            }, {
                "MasterId": "eb08c8b1-f439-4397-ae44-e72a75c7141a"
        , "SubmissionId": "DAPP16000245", "SubmissionCategoryID": 2, "ApprovedBy": "", "DocRequired": "Three (3) Contract Samples", "Agency": "DCRA", "Division": "Business Licensing Division (BLD)", "Div": "BLD", "FileName": "", "FileLocation": "", "DocStatus": "Open", "Description": "This is a typeset document that the applicant provides and is used in the operation of business", "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "120", "ShortName": "SampleContract3", "IsUpload": false, "UploadFileName": "", "CheckListType"
            : "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000245"
            }, {
                "MasterId": "eb08c8b1-f439-4397-ae44-e72a75c7141a"
            , "SubmissionId": "DAPP16000245", "SubmissionCategoryID": 2, "ApprovedBy": "",
                "DocRequired": "Notice of BusinessTax Registration", "Agency": "OTR", "Division": "Business Tax Service Center", "Div": "BTSC", "FileName": "", "FileLocation"
            : "", "DocStatus": "Open", "Description": "A document indicating the business has completed the Combined Business= Tax form (FR-500).", "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor"
        , "CategoryID": "1366", "ShortName": "BusTaxRegistration", "IsUpload": false, "UploadFileName": "", "CheckListType"
        : "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000245"
            }], "IsIndividual": false, "IsFEIN": false
    , "DocSubType": "", "IsHop": true, "IsCof": false, "AppType": "B", "BusinessStructure": "Corporation (For Profit)", "TradeName": "asdasfsfaf", "CategoryName": "Home Improvement Contractor", "IsCorporationDivision": true, "ISFEINSSN"
    : false, "IsCleanHandsVerify": false, "IsCorporateRegistration": false, "IsBHAddress": false, "IsBPAddress": false
    , "IsMailAddress": false, "IsResidentAgent": false, "IsDocforCleanHands": false, "IsDocforCofo": false, "IsDocforHop"
    : false, "IsDocforEhop": false, "IsSubmissionCofo": false, "IsSubmissionHop": false, "IsSubmissioneHop": false
    , "CheckedStatus": false, "IsSubmissionCorpReg": false, "IsSubmissionAgent": false, "IsHomeBased": true, "IsBusinessMustbeinDC"
    : true, "IsMcofo": false, "CategoryCode": "4202", "IsIndividualValid": false, "IsValidateTextRevenue": false, "IsCategorySelfCertification"
    : false, "IsSelfCertification": false, "BusinessName": "", "PremisesAddress": "", "physicalLocationValidate"
    : false, "IsTaxReattested": false
        }], "validateCorpFileStatus": "ACTIVE", "CorpChangeStatus": null
    }


    var submissionstatus_data = {
        "Status": "Draft", "MasterId": "eb08c8b1-f439-4397-ae44-e72a75c7141a", "TradeName": "NA", "BusinessStructure"
    : "Corporation (For Profit)", "IsCorporationDivision": true, "IsCoporateRegistration": false, "IsResidentAgent"
    : false, "IsIndividual": false, "IsBusinessMustbeinDc": true, "AppType": "B", "IsFEIN": true, "DocSubmType": ""
, "PaymentId": null, "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "", "CurrentYear"
: "2016", "CreatedDate": "03/18/2016", "SelectedMailType": "", "PremisesAddress": "", "BusinessName": "", "IsCategorySelfCertification"
: false
    }

    var submissionstatus_data_check_physicalloc_data = {
        "Status": "Draft", "MasterId": "eb08c8b1-f439-4397-ae44-e72a75c7141a", "TradeName": "asdf", "BusinessStructure"
    : "Corporation (For Profit)", "IsCorporationDivision": true, "IsCoporateRegistration": false, "IsResidentAgent"
    : false, "IsIndividual": false, "IsBusinessMustbeinDc": true, "AppType": "B", "IsFEIN": true, "DocSubmType": ""
, "PaymentId": null, "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "", "CurrentYear"
: "2016", "CreatedDate": "03/18/2016", "SelectedMailType": "", "PremisesAddress": "1100 1/2 E SW", "BusinessName": "Corporation", "IsCategorySelfCertification"
: false
    }, sessionfactory, authservice, localstorageservice;

    var errormessages = [{ "ShortName": "incompleteData", "ErrrorMessage": "All requested information is required in order to save the data you entered.  Please select [OK] to exit without saving or [CANCEL] to stay on the page." }, { "ShortName": "ehop_inEligible", "ErrrorMessage": "Given your response, you are ineligible for an Expedited Home Occupancy Permit.  Please select [Return to Checklist] and visit the DC Office of Zoning at 1100 4th St., SW or call 202-442-4400 to obtain an HOP." }, { "ShortName": "verifyandcontinuemessage", "ErrrorMessage": "To revise any of your responses, select [Cancel] and then select the [Return to Checklist] button. To proceed, select [Confirm]." }, { "ShortName": "corpnodata", "ErrrorMessage": "The File Number you entered does not match the Corporations records. Please check your entry and try again. If your entry does not match, please select [Return to Checklist] and visit the DCRA Corporations Division at 1100 4th St., SW or call  202-442-4400." }, { "ShortName": "feinssnNonCompliance", "ErrrorMessage": "According to Office of Tax and Revenue (OTR) records, the FEIN you entered is not in compliance with the District of Columbia's Clean Hands requirements. Please click on Tax and Revenue link below to know how to proceed further." }, { "ShortName": "corpSearchNotClicked", "ErrrorMessage": "You must select [Search Corp Online].  Please click [OK] to proceed without saving the entered data or click [CANCEL] to stay on the page." }, { "ShortName": "allfieldsNotFilled", "ErrrorMessage": "Please complete all the required fields." }, { "ShortName": "renewalNavigation", "ErrrorMessage": "To save any updates/changes, please select [CANCEL] to stay on the page or [OK] to leave without saving." }, { "ShortName": "navigateaway", "ErrrorMessage": "To save any updates/changes, please select [CANCEL] to stay on the page or [OK] to leave without saving." }, { "ShortName": "hopallfieldsNotFilled", "ErrrorMessage": "Please complete all fields." }, { "ShortName": "corpFileNumberError", "ErrrorMessage": "Please enter your Corporate Registration File Number." }, { "ShortName": "createChecklistnavigation", "ErrrorMessage": "The data you have selected/entered so far will be lost. You must complete all of the pre-application questions and create a checklist to save your data. Do you want to exit without saving?" }, { "ShortName": "NextButtonIncompleteData", "ErrrorMessage": "Please provide all requested data and select [Next]." }, { "ShortName": "ehopSelectionErrorMsg", "ErrrorMessage": "You must select [Confirm eHOP Eligibility].  Please click [OK] to proceed without saving the entered data or click [CANCEL] to stay on the page." }, { "ShortName": "corp_number_failedstatus", "ErrrorMessage": "According to the Corporations Division's files, the Status of your Corporate Registration is {0}. You must resolve any  issues prior to submitting your Business License application. Please select Return to Checklist and visit the DCRA Corporations Division at 1100 4th St., SW, or call 202-442-4400." }, { "ShortName": "createCheckList", "ErrrorMessage": "Making changes after your Checklist is created will require you to discard your Application and start a new Application from the beginning. To proceed and create your Checklist select [Confirm]. To review and revise your responses select [Cancel] and select the Revise button on the bottom of the page." }, { "ShortName": "corp_businessstructure_mismatch", "ErrrorMessage": "The Business Structure you selected in the Pre-Application Questions does not match the Corporations records. Please check your entry and try again. If your entry does not match, please select [Return to Checklist] and visit the DCRA Corporations Division at 1100 4th St., SW or call 202-442-4400." }, { "ShortName": "donothaveCofo", "ErrrorMessage": "The data selected/entered for this CofO will not be retained. Select [OK] to proceed with this option or [Cancel] to retain the data as presented." }, { "ShortName": "searchNotClicked", "ErrrorMessage": "Please select [Search Zoning], or click [OK] to proceed without saving, or select [CANCEL] to stay on the page." }, { "ShortName": "renewalpaymentallfieldsNotFilled", "ErrrorMessage": "Please provide all requested data." }]

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $routeParams, appConstants, UtilityFactory, BBLSubmissionFactory, SessionFactory, authService, localStorageService) {
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
        routeParams = $routeParams;
        routeParams.guid = 'guid';
        appconstants = appConstants;
        basePath = appconstants.apiServiceBaseUri;
        utilityfactory = UtilityFactory;
        bblsubmissionfac = BBLSubmissionFactory;
        sessionfactory = SessionFactory;
        authservice = authService;
        localstorageservice = localStorageService;
    }));

    describe('test submission status is in underreview and no guid available', function () {

        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            ApplicationChecklistController = controller('ApplicationChecklistController',
            {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, $routeParams: routeParams,
                mockservice: mockservice, utilityfactory: utilityfactory,
                appConstants: appconstants, BblsubmissionFactory: bblsubmissionfac,
                SessionFactory: sessionfactory, authService: authservice
            });
        });

        it('should test getCheckListData under review', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('test submission status is in underreview', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            ApplicationChecklistController = controller('ApplicationChecklistController',
            {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, $routeParams: routeParams,
                mockservice: mockservice, utilityfactory: utilityfactory,
                appConstants: appconstants, BblsubmissionFactory: bblsubmissionfac
            });
        });
        it('should test getCheckListData under review', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('test cases where submission status is not in underreview', function () {
        var data;
        var check_physicalloc_data_false_data;

        beforeEach(function () {
            data = {
                "result": [{
                    "MasterId": "31163aec-7ead-4dae-94be-81aefcc0ead1",
                    "BblServiceDoc": [], "IsIndividual": false, "IsFEIN": true, "DocSubType": "", "IsHop": false, "IsCof": true, "AppType": "B",
                    "BusinessStructure": "For-ProfitCorporation", "TradeName": "", "CategoryName": "Secondhand Dealers (C)", "IsCorporationDivision": true,
                    "ISFEINSSN": false, "IsCleanHandsVerify": false, "IsCorporateRegistration": true, "IsBHAddress": true, "IsBPAddress": true,
                    "IsMailAddress": false, "IsResidentAgent": true, "IsDocforCleanHands": false, "IsDocforCofo": false,
                    "IsDocforHop": false, "IsDocforEhop": false, "IsSubmissionCofo": true, "IsSubmissionHop": false, "IsSubmissioneHop": false,
                    "CheckedStatus": false, "IsSubmissionCorpReg": false, "IsSubmissionAgent": false, "IsHomeBased": false, "IsBusinessMustbeinDC": true,
                    "IsMcofo": true, "CategoryCode": "6020", "IsIndividualValid": false, "IsValidateTextRevenue": false
                }], "masterTextRevenue": false, "validateCorpFileStatus": "ACTIVE"
            };
            check_physicalloc_data_false_data = {
                "result": [{
                    "MasterId": "31163aec-7ead-4dae-94be-81aefcc0ead1",
                    "BblServiceDoc": [], "IsIndividual": false, "IsFEIN": true, "DocSubType": "", "IsHop": false, "IsCof": true, "AppType": "B",
                    "BusinessStructure": "For-ProfitCorporation", "TradeName": "asdf", "CategoryName": "Secondhand Dealers (C)", "IsCorporationDivision": true,
                    "ISFEINSSN": false, "IsCleanHandsVerify": false, "IsCorporateRegistration": true, "IsBHAddress": true, "IsBPAddress": true,
                    "IsMailAddress": false, "IsResidentAgent": true, "IsDocforCleanHands": false, "IsDocforCofo": false,
                    "IsDocforHop": false, "IsDocforEhop": false, "IsSubmissionCofo": true, "IsSubmissionHop": true, "IsSubmissioneHop": false,
                    "CheckedStatus": false, "IsSubmissionCorpReg": false, "IsSubmissionAgent": false, "IsHomeBased": false, "IsBusinessMustbeinDC": true,
                    "IsMcofo": true, "CategoryCode": "6020", "IsIndividualValid": false, "IsValidateTextRevenue": false
                }], "masterTextRevenue": false, "validateCorpFileStatus": "ACTIVE"
            };

            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond(submissionstatus_data);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/BblRequiredDocuments').respond(supportingDocsData);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionUpdate').respond(data);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            ApplicationChecklistController = controller('ApplicationChecklistController',
            {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, $routeParams: routeParams,
                mockservice: mockservice, utilityfactory: utilityfactory,
                appConstants: appconstants, BblsubmissionFactory: bblsubmissionfac
            });
        });


        it('should test getCheckListData not in review', function () {
            httpBackend.flush();
            expect(ApplicationChecklistController.supportingDocsData).toBeDefined();
            expect(ApplicationChecklistController.supportingDocsData.length).toBe(1);
        });


        localStorage.setItem("preAppQuestionsData", "");
        localStorage.setItem('MasterId', 'sasdf');

        it('should test save and exit', function () {
            ApplicationChecklistController.navSaveandExit();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navToUploadDocs', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.navToUploadDocs();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/supportingdocs/step2/ON/guid');
        });

        it('should test navToTaxRevenue', function () {
            ApplicationChecklistController.navToTaxRevenue();
            expect($location.path).toHaveBeenCalledWith('/taxrevenue/guid');
        });

        it('should test navToInfoVerification', function () {
            ApplicationChecklistController.navToInfoVerification();
            expect($location.path).toHaveBeenCalledWith('/infoverification/guid');
        });

        it('should test navToPreAppQues', function () {
            ApplicationChecklistController.navToPreAppQues();
            expect($location.path).toHaveBeenCalledWith('/reviewchecklist/guid');
        });

        it('should test navToIndividualBusinessLic', function () {
            ApplicationChecklistController.navToIndividualBusinessLic();
            expect($location.path).toHaveBeenCalledWith('/individualbusinesslic/guid');
        });

        it('should test check_physicalloc_data', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.check_physicalloc_data();
            expect(ApplicationChecklistController.check_physicalloc_data()).toBeTruthy();
        });

        it('should test check_physicalloc_data for returning false', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.submissionStatusData = submissionstatus_data_check_physicalloc_data;
            ApplicationChecklistController.check_physicalloc_data();
            expect(ApplicationChecklistController.check_physicalloc_data()).toBeTruthy();
        });

        it('should test navToPhysicalLoc when IsSubmissioneHop is true', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.supportingDocsData[0].IsSubmissioneHop = true;
            ApplicationChecklistController.navToPhysicalLoc();
            expect($location.path).toHaveBeenCalledWith('/ehopeligibility/guid');
        });

        it('should test navToPhysicalLoc when IsSubmissioneHop,IsHop are false, IsBusinessMustbeinDC,IsHomeBased are true', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.supportingDocsData[0].IsSubmissioneHop = false;
            ApplicationChecklistController.supportingDocsData[0].IsHop = false;
            ApplicationChecklistController.supportingDocsData[0].IsBusinessMustbeinDC = true;
            ApplicationChecklistController.supportingDocsData[0].IsHomeBased = true;
            ApplicationChecklistController.navToPhysicalLoc();
            expect($location.path).toHaveBeenCalledWith('/ehopeligibility/guid');
        });

        it('should test navToPhysicalLoc when IsHomeBased,IsCof,IsHop are false, IsIndividual is true', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.supportingDocsData[0].IsHomeBased = false;
            ApplicationChecklistController.supportingDocsData[0].IsCof = false;
            ApplicationChecklistController.supportingDocsData[0].IsHop = false;
            ApplicationChecklistController.supportingDocsData[0].IsIndividual = true;
            ApplicationChecklistController.navToPhysicalLoc();
            expect($location.path).toHaveBeenCalledWith('/nopobox/guid');
        });

        it('should test navToPhysicalLoc when IsHomeBased,IsCof,IsHop are false, IsIndividual is false', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.supportingDocsData[0].IsHomeBased = false;
            ApplicationChecklistController.supportingDocsData[0].IsCof = false;
            ApplicationChecklistController.supportingDocsData[0].IsHop = false;
            ApplicationChecklistController.supportingDocsData[0].IsIndividual = false;
            ApplicationChecklistController.navToPhysicalLoc();
            expect($location.path).toHaveBeenCalledWith('/nopobox/guid');
        });

        it('should test navToPhysicalLoc when IsIndividual,IsHop are false, CategoryName is Solicitor', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.supportingDocsData[0].IsIndividual = true;
            ApplicationChecklistController.supportingDocsData[0].CategoryName = "Solicitor";
            ApplicationChecklistController.supportingDocsData[0].IsHop = true;
            ApplicationChecklistController.navToPhysicalLoc();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/hop/guid');
        });

        it('should test navToPhysicalLoc when IsIndividual,IsHop are false, CategoryName is Solicitor', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.supportingDocsData[0].IsIndividual = true;
            ApplicationChecklistController.supportingDocsData[0].CategoryName = "Solicitor";
            ApplicationChecklistController.supportingDocsData[0].IsHop = true;
            ApplicationChecklistController.navToPhysicalLoc();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/hop/guid');
        });

        it('should test navToPhysicalLoc when IsCof is true', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.supportingDocsData[0].IsCof = true;
            ApplicationChecklistController.navToPhysicalLoc();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/cofo/guid');
        });

        it('should test navToPhysicalLoc when IsHop is true', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.supportingDocsData[0].IsHop = true;
            ApplicationChecklistController.navToPhysicalLoc();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/hop/guid');
        });

        it('should test navToPhysicalLoc when IsCorporationDivision is true, IsCof,IsHop are false', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.supportingDocsData[0].IsCorporationDivision = true;
            ApplicationChecklistController.supportingDocsData[0].IsCof = false;
            ApplicationChecklistController.supportingDocsData[0].IsHop = false;
            ApplicationChecklistController.navToPhysicalLoc();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/corpreg/guid');
        });

        it('should test navToPhysicalLoc when IsCorporationDivision,IsCof,IsHop are false and TradeName not null,BusinessStructure not equal to SOLE PROPRIETORSHIP', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.supportingDocsData[0].IsCorporationDivision = false;
            ApplicationChecklistController.supportingDocsData[0].IsCof = false;
            ApplicationChecklistController.supportingDocsData[0].IsHop = false;
            ApplicationChecklistController.supportingDocsData[0].TradeName = "asdf";
            ApplicationChecklistController.supportingDocsData[0].BusinessStructure = "GENERAL PARTNERSHIP";
            ApplicationChecklistController.navToPhysicalLoc();
            expect($location.path).toHaveBeenCalledWith('/corpreqregwithtradesecond/guid');
        });

        it('should test navToPhysicalLoc when IsCorporationDivision,IsCof,IsHop are false and TradeName equal to null,BusinessStructure equal to GENERAL PARTNERSHIP', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.supportingDocsData[0].IsCorporationDivision = false;
            ApplicationChecklistController.supportingDocsData[0].IsCof = false;
            ApplicationChecklistController.supportingDocsData[0].IsHop = false;
            ApplicationChecklistController.supportingDocsData[0].TradeName = "";
            ApplicationChecklistController.supportingDocsData[0].BusinessStructure = "GENERAL PARTNERSHIP";
            ApplicationChecklistController.navToPhysicalLoc();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it('should test navToPhysicalLoc when IsCorporationDivision,IsCof,IsHop are false and TradeName equal to null,BusinessStructure not equal to GENERAL PARTNERSHIP', function () {
            ApplicationChecklistController.supportingDocsData[0] = data;
            ApplicationChecklistController.supportingDocsData[0].IsCorporationDivision = false;
            ApplicationChecklistController.supportingDocsData[0].IsCof = false;
            ApplicationChecklistController.supportingDocsData[0].IsHop = false;
            ApplicationChecklistController.supportingDocsData[0].TradeName = "";
            ApplicationChecklistController.supportingDocsData[0].BusinessStructure = "Corporation(For Profit)";
            ApplicationChecklistController.navToPhysicalLoc();
            expect($location.path).toHaveBeenCalledWith('/corpreqregisterfirst/guid');
        });

    });
});