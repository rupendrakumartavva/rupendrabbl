describe('it should test renewal supporting documents controller', function () {


    var $scope, controller, httpBackend, mockservice, supportingdocscontroller, localStore, appconstants, routeParams;
    var basePath, renewalutilityfactory, sessionfactory, utilityfactory, windowobj;
    var documentlist = {
        "DocumentList":
            [{
                "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999",
                "SubmissionId": "DAPP16000008", "SubmissionCategoryID": 1, "ApprovedBy": "", "DocRequired": "Certificate of Liability Insurance",
                "Agency": "APPLICANT", "Division": "NA", "Div": "NA", "FileName": "", "FileLocation": "", "DocStatus": "Open",
                "Description": "Applicant shall furnish a certificate of insurance for the ate.)",
                "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor",
                "CategoryID": "111", "ShortName": "Insurance", "IsUpload": false, "UploadFileName": "",
                "CheckListType": "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000008"
            },
                    {
                        "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "SubmissionId": "DAPP16000008",
                        "SubmissionCategoryID": 1, "ApprovedBy": "", "DocRequired": "Police Criminal History Report",
                        "Agency": "APPLICANT", "Division": "NA", "Div": "NA", "FileName": "", "FileLocation": "",
                        "DocStatus": "Open", "Description": "DC Residents:  Form PD 70 from the Metropolitan Poli the withn.",
                        "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "112",
                        "ShortName": "CrimHistory", "IsUpload": false, "UploadFileName": "", "CheckListType": "Document",
                        "CategoryCode": "4202", "LicenseName": "DAPP16000008"
                    },
                    {
                        "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "SubmissionId": "DAPP16000008",
                        "SubmissionCategoryID": 1, "ApprovedBy": "", "DocRequired": "Designation Letter", "Agency": "DCRA",
                        "Division": "Business Licensing Division (BLD)", "Div": "BLD", "FileName": "", "FileLocation": "",
                        "DocStatus": "Open", "Description": "A form document that lists salespersons employed with Home Improvement Contactor's business (HIC).",
                        "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "113", "ShortName": "EmployeeList",
                        "IsUpload": false, "UploadFileName": "", "CheckListType": "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000008"
                    },
                    {
                        "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "SubmissionId":
                          "DAPP16000008", "SubmissionCategoryID": 1, "ApprovedBy": "", "DocRequired": "Surety Bond - Home Improvement Surety Bond",
                        "Agency": "DCRA", "Division": "Business Licensing Division (BLD)", "Div": "BLD", "FileName": "", "FileLocation": "",
                        "DocStatus": "Open", "Description": "A bond obtained by applicant(two years) in the amount of $25,000.",
                        "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "114",
                        "ShortName": "Surety Bond", "IsUpload": false, "UploadFileName": "", "CheckListType": "Document",
                        "CategoryCode": "4202", "LicenseName": "DAPP16000008"
                    },
                    {
                        "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "SubmissionId":
                          "DAPP16000008", "SubmissionCategoryID": 1, "ApprovedBy": "", "DocRequired": "Three (3) Contract Samples", "Agency": "DCRA",
                        "Division": "Business Licensing Division (BLD)", "Div": "BLD", "FileName": "", "FileLocation": "", "DocStatus": "Open",
                        "Description": "This is a typeset document that the applicant provides and is used in the operation of business",
                        "Endorsement": "General Service and Repair", "License": "Home Improvement Contractor", "CategoryID": "115",
                        "ShortName": "SampleContract3", "IsUpload": false, "UploadFileName": "", "CheckListType": "Document",
                        "CategoryCode": "4202", "LicenseName": "DAPP16000008"
                    }, {
                        "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "SubmissionId": "DAPP16000008", "SubmissionCategoryID": 999999,
                        "ApprovedBy": "", "DocRequired": "Home Occupancy Permit (HOP) Document", "Agency": "DCRA", "Division": "Zoning", "Div": "Zoning",
                        "FileName": "", "FileLocation": "", "DocStatus": "Open", "Description": "DCRA", "Endorsement": "General Service and Repair",
                        "License": "Home Improvement Contractor", "CategoryID": "327", "ShortName": "HOP", "IsUpload": false, "UploadFileName": "",
                        "CheckListType": "Document", "CategoryCode": "4202", "LicenseName": "DAPP16000008"
                    }]
    },
    bblservicedocument = { "Status": true, "FileName": "testuploadpdfhit.pdf" },
    submissionstatusdata = {
        "Status": "Draft", "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999",
        "TradeName": "", "BusinessStructure": "", "IsCorporationDivision": true, "IsCoporateRegistration": true,
        "IsResidentAgent": true, "IsIndividual": false, "IsBusinessMustbeinDc": true, "AppType": "B", "IsFEIN": true, "DocSubmType": "",
        "PaymentId": null, "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "AGENT FOR SERVICE",
        "CurrentYear": "2016", "CreatedDate": "02/08/2016", "SelectedMailType": "NEWMAIL",
        "PremisesAddress": "1523  3RD Street NW  2 Washington District of Columbia United States 20001",
        "BusinessName": "AGENT FOR SERVICE", "IsCategorySelfCertification": false
    };


    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $routeParams, appConstants, UtilityFactory, RenewalUtilityFactory, $window) {
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
        renewalutilityfactory = RenewalUtilityFactory;
        appconstants = appConstants;
        utilityfactory = UtilityFactory;
        windowobj = $window;
        basePath = appconstants.apiServiceBaseUri;
    }));

    //describe('test cases when the accessed application is in guid is not available', function () {

    //    beforeEach(function () {
    //        spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
    //        supportingdocscontroller = controller('SupportingDocsUploadController',
    //            {
    //                $scope: $scope, $rootScope: rootscope,
    //                $location: $location, $routeParams: routeParams, mockservice: mockservice,
    //                appConstants: appconstants,
    //                UtilityFactory: utilityfactory, SessionFactory: sessionfactory
    //            });
    //    });

    //    it('should test init with no guid available', function () {
    //        expect($location.path).toHaveBeenCalledWith('/mybbl');
    //    });
    //});

    describe('test cases when the accessed application is in guid is available', function () {

        beforeEach(function () {
            submissionstatusdata.Status = "underreview";
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/Renew/CheckDocuments').respond(documentlist);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/BblServiceDocument').respond(bblservicedocument);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/DeleteDocument').respond(true);
            httpBackend.when('POST', basePath + 'api/Renew/RenewalStatus').respond('true');
            httpBackend.when('POST', basePath + 'api/Renew/CheckDocumentStatus').respond('true');
            httpBackend.when('POST', basePath + 'api/Renew/UpdateRenwalDocumentType').respond('true');

            renewalsupportingdocscontroller = controller('RenewalSupportingDocs',
                {
                    $scope: $scope, $rootScope: rootscope,
                    $location: $location, $routeParams: routeParams,
                    mockservice: mockservice, appConstants: appconstants,
                    UtilityFactory: utilityfactory, RenewalUtilityFactory: renewalutilityfactory,
                    $window: windowobj
                });
        });

        it('should test navToBBL', function () {
            renewalsupportingdocscontroller.navToBBL();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navigateAnyWayrenewal', function () {
            renewalsupportingdocscontroller.navigationPath = "/testpath";
            renewalsupportingdocscontroller.DocumentList = documentlist.DocumentList;
            renewalsupportingdocscontroller.navigateAnyWay();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navToPrevious', function () {
            renewalsupportingdocscontroller.navToPrevious();
            //httpBackend.flush();
            expect(renewalsupportingdocscontroller.navigate).toBeFalsy();
        });

        it('should test navigateAnyWay', function () {
            renewalsupportingdocscontroller.DocumentList = documentlist.DocumentList;
            renewalsupportingdocscontroller.navigateAnyWay();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/renewalpayment/guid');
        });

        it('should test selectDropOption', function () {
            renewalsupportingdocscontroller.DocSubmType = "IN";
            renewalsupportingdocscontroller.selectDropOption();
            //httpBackend.flush();
        });

        it('should test removeUploadedFile', function () {
            renewalsupportingdocscontroller.DocumentList = documentlist.DocumentList;
            renewalsupportingdocscontroller.removeUploadedFile("1");
            httpBackend.flush();
        });

        it('should test navToRenewalPayment', function () {
            renewalsupportingdocscontroller.DocumentList = documentlist.DocumentList;
            renewalsupportingdocscontroller.navToRenewalPayment("1");
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/renewalpayment/guid');
        });       

    });


});