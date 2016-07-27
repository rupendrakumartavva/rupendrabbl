describe('it should test supporting documents controller', function () {


    var $scope, controller, httpBackend, mockservice, supportingdocscontroller, localStore, appconstants, routeParams;
    var basePath, utilityfactory, sessionfactory, bblsubmissionfactory;
    var bblrequireddocuments = {
        "result": [{
            "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999",
            "BblServiceDoc": [{
                "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "SubmissionId": "DAPP16000008", "SubmissionCategoryID": 1, "ApprovedBy": "",
                "DocRequired": "Certificate of Liability Insurance", "Agency": "APPLICANT", "Division": "NA", "Div": "NA", "FileName": "", "FileLocation": "",
                "DocStatus": "Open", "Description": "Applicant shall furnish a certificate of insurance for the license period; therefore, each applicant must secure commercial general liability insuranceCertificate of Insurance: Certificate Holder: DCRA with current Address ;  Insured:  Business name, premise address , city, state and zip code.  Description of Operations:  Gen Contr/Construction Mngr in Washington, DC .   Current one year Certificate.Certificate of Liability Insurance in the amount of - Property Damage ($50,000), Public Liability ($100,000)", "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "111", "ShortName": "Insurance", "IsUpload": false, "UploadFileName": "", "CheckListType": "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000008"
            }, { "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "SubmissionId": "DAPP16000008", "SubmissionCategoryID": 1, "ApprovedBy": "", "DocRequired": "Police Criminal History Report", "Agency": "APPLICANT", "Division": "NA", "Div": "NA", "FileName": "", "FileLocation": "", "DocStatus": "Open", "Description": "DC Residents:  Form PD 70 from the Metropolitan Police Department.Non-DC Residents:  A certified copy of a Police Criminal History Report from the applicant's jurisdiction of residence dated within 30 days of submission.", "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "112", "ShortName": "CrimHistory", "IsUpload": false, "UploadFileName": "", "CheckListType": "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000008" }, { "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "SubmissionId": "DAPP16000008", "SubmissionCategoryID": 1, "ApprovedBy": "", "DocRequired": "Designation Letter", "Agency": "DCRA", "Division": "Business Licensing Division (BLD)", "Div": "BLD", "FileName": "", "FileLocation": "", "DocStatus": "Open", "Description": "A form document that lists salespersons employed with Home Improvement Contactor's business (HIC).", "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "113", "ShortName": "EmployeeList", "IsUpload": false, "UploadFileName": "", "CheckListType": "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000008" }, { "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "SubmissionId": "DAPP16000008", "SubmissionCategoryID": 1, "ApprovedBy": "", "DocRequired": "Surety Bond - Home Improvement Surety Bond", "Agency": "DCRA", "Division": "Business Licensing Division (BLD)", "Div": "BLD", "FileName": "", "FileLocation": "", "DocStatus": "Open", "Description": "A bond obtained by applicant from an insurance company, surety company or post the full amount with DCRA, if/of their choosing to cover the full duration of the license period (two years) in the amount of $25,000.", "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "114", "ShortName": "Surety Bond", "IsUpload": false, "UploadFileName": "", "CheckListType": "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000008" }, { "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "SubmissionId": "DAPP16000008", "SubmissionCategoryID": 1, "ApprovedBy": "", "DocRequired": "Three (3) Contract Samples", "Agency": "DCRA", "Division": "Business Licensing Division (BLD)", "Div": "BLD", "FileName": "", "FileLocation": "", "DocStatus": "Open", "Description": "This is a typeset document that the applicant provides and is used in the operation of business", "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "115", "ShortName": "SampleContract3", "IsUpload": false, "UploadFileName": "", "CheckListType": "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000008" }, { "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "SubmissionId": "DAPP16000008", "SubmissionCategoryID": 999999, "ApprovedBy": "", "DocRequired": "Home Occupancy Permit (HOP) Document", "Agency": "DCRA", "Division": "Zoning", "Div": "Zoning", "FileName": "", "FileLocation": "", "DocStatus": "Open", "Description": "DCRA", "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "327", "ShortName": "HOP", "IsUpload": false, "UploadFileName": "", "CheckListType": "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000008" }, { "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "SubmissionId": "DAPP16000008", "SubmissionCategoryID": 1, "ApprovedBy": "", "DocRequired": "Notice of BusinessTax Registration", "Agency": "OTR", "Division": "Business Tax Service Center", "Div": "BTSC", "FileName": "", "FileLocation": "", "DocStatus": "Open", "Description": "A document indicating the business has completed the Combined Business Tax form (FR-500)", "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "116", "ShortName": "BusTaxRegistration", "IsUpload": false, "UploadFileName": "", "CheckListType": "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000008" }], "IsIndividual": false, "IsFEIN": false, "DocSubType": "ON", "IsHop": true, "IsCof": false, "AppType": "B", "BusinessStructure": "", "TradeName": "", "CategoryName": "Home Improvement Contractor", "IsCorporationDivision": true, "ISFEINSSN": true, "IsCleanHandsVerify": true, "IsCorporateRegistration": true, "IsBHAddress": true, "IsBPAddress": true, "IsMailAddress": true, "IsResidentAgent": true, "IsDocforCleanHands": false, "IsDocforCofo": false, "IsDocforHop": true, "IsDocforEhop": false, "IsSubmissionCofo": false, "IsSubmissionHop": true, "IsSubmissioneHop": false, "CheckedStatus": false, "IsSubmissionCorpReg": false, "IsSubmissionAgent": false, "IsHomeBased": true, "IsBusinessMustbeinDC": true, "IsMcofo": false, "CategoryCode": "4202", "IsIndividualValid": false, "IsValidateTextRevenue": false, "IsCategorySelfCertification": false, "IsSelfCertification": false
        }], "validateCorpFileStatus": "Active", "CorpChangeStatus": null
    },
    bblservicedocument = { "Status": true, "FileName": "testuploadpdfhit.pdf" },
    submissionstatusdata = { "Status": "Draft", "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "TradeName": "", "BusinessStructure": "", "IsCorporationDivision": true, "IsCoporateRegistration": true, "IsResidentAgent": true, "IsIndividual": false, "IsBusinessMustbeinDc": true, "AppType": "B", "IsFEIN": true, "DocSubmType": "", "PaymentId": null, "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "AGENT FOR SERVICE", "CurrentYear": "2016", "CreatedDate": "02/08/2016", "SelectedMailType": "NEWMAIL", "PremisesAddress": "1523  3RD Street NW  2 Washington District of Columbia United States 20001", "BusinessName": "AGENT FOR SERVICE", "IsCategorySelfCertification": false };


    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $routeParams, UtilityFactory, SessionFactory, BBLSubmissionFactory, appConstants) {
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
        utilityfactory = UtilityFactory;
        sessionfactory = SessionFactory;
        bblsubmissionfactory = BBLSubmissionFactory;
        appconstants = appConstants;
        basePath = appconstants.apiServiceBaseUri;
    }));

    describe('test cases when the accessed application is in guid is not available', function () {

        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
            supportingdocscontroller = controller('SupportingDocsUploadController',
                {
                    $scope: $scope, $rootScope: rootscope,
                    $location: $location, $routeParams: routeParams, mockservice: mockservice,
                    appConstants: appconstants, 
                    UtilityFactory: utilityfactory, SessionFactory: sessionfactory,
                    BBLSubmissionFactory: bblsubmissionfactory
                });
        });

        it('should test init with no guid available', function () {
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('test cases when the accessed application is in guid is available and status is draft', function () {

        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            submissionstatusdata.Status = "underreview";
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/BblRequiredDocuments').respond(bblrequireddocuments);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/BblServiceDocument').respond(bblservicedocument);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/DeleteDocument').respond(true);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionUpdate').respond('true');
            supportingdocscontroller = controller('SupportingDocsUploadController',
                {
                    $scope: $scope, $rootScope: rootscope,
                    $location: $location, $routeParams: routeParams, mockservice: mockservice,
                    appConstants: appconstants,
                    UtilityFactory: utilityfactory, SessionFactory: sessionfactory,
                    BBLSubmissionFactory: bblsubmissionfactory
                });
        });

        //it('should test navToCheckList', function () {
        //    supportingdocscontroller.navToCheckList();
        //    expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        //});

        it('should test navToSaveandExitfromDoc', function () {
            supportingdocscontroller.checkListData = bblrequireddocuments;
            supportingdocscontroller.navToSaveandExitfromDoc();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navigateAnyWay', function () {
            supportingdocscontroller.checkListData = bblrequireddocuments;
            supportingdocscontroller.navigateAnyWay();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        });

        it('should test removeUploadedFile', function () {
            supportingdocscontroller.supportingDocsData = bblrequireddocuments.result[0].BblServiceDoc;
            supportingdocscontroller.removeUploadedFile("1");
            httpBackend.flush();
        });

        it('should test navToUploadDocs_Step2', function () {
            supportingdocscontroller.checkListData = bblrequireddocuments;
            supportingdocscontroller.bblapptype = "ON";
            supportingdocscontroller.navToUploadDocs_Step2();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/supportingdocs/step2/ON/guid');
        });

        it('should test selectDropOption', function () {
            supportingdocscontroller.bblapptype = "IN";
            supportingdocscontroller.selectDropOption();
            httpBackend.flush();
        });

        it('should test stayOnThisPage', function () {
            supportingdocscontroller.stayOnThisPage();
            httpBackend.flush();
        });

    });


});