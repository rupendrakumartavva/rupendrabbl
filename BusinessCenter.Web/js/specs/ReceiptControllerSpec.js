describe('Receipt controller spec', function () {

    var scope, routeParams, controller, httpBackend, mockservice, receiptController, localStore, basePath,utilityfac;

    beforeEach(module('DCRA'));

    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $routeParams, appConstants, UtilityFactory) {
        rootscope = $rootScope.$new();
        $location = _$location_;
        scope = $rootScope.$new();
        basePath = appConstants.apiServiceBaseUri;
        spyOn($location, 'path').and.returnValue('test/ste/ast');
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
        utilityfac = UtilityFactory;
    }));


    describe('should test logged in users', function () {
        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Result": "DRAFT" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/GetReceiptDetails').respond(receiptData);
            receiptController = controller('ReceiptController', {
                $scope: scope, $rootScope: rootscope, $location: $location,
                routeParams: routeParams, mockservice: mockservice,
                utilityFac:utilityfac
            });
        });

        var receiptData = { "FullName": null, "TransactionSuccess": null, "InnerHtml": null, "EmailAddress": null, "MasterID": "357b2f1f-7342-46e1-b4ae-1f901175e88c", "PaymentID": "23c83a2e-39e9-4557-a407-33a992eeb4de", "SubNumber": "LAPP15089696", "AmountCharged": 962.5000, "ReceiptDate": "12/05/2015", "CardNumber": "************1111", "TransactionId": "Test-Transaction", "IsEhopAllowed": false, "DocType": "IN", "GrandTotal": 0.0, "TotalApplicationFee": 0.0, "ApplicationFee": 70.0000, "TotalLicenseFee": 0.0, "TotalEndosementFee": 0.0, "TotalTechFee": 0.0, "ExceptedFinalCheckingDate": "01/04/2016", "CategoryLicenseFee": 780.0000, "EndorsementFee": 25.0000, "SubTotal": 875.0000, "TechFee": 87.5000, "TotalFee": 962.5000, "Isehop": false, "IsSubmissionCofo": true, "IsSubmissionHop": false, "IsSubmissioneHop": false, "ExtraAmount": 0.0, "Extradays": "ACTIVE", "ServiceCheckList": [{ "Endorsement": "Motor Vehicle Sales, Service, & Repair", "CategoryId": "EC682485-900D-4425-BACF-53B256F7170B", "LicenseCategory": "Auto Rental", "Units": "NA ", "ApplicationFee": 70.0000, "CategoryLicenseFee": 780.0000, "EndorsementFee": 25.0000, "SubTotal": 875.0000, "TechFee": 87.5000, "TotalFee": 962.5000, "CategoryCode": "8001", "IsRaoFeeApplied": false, "LicenseDuration": "TWO (2) YEAR" }], "DocumentList": [{ "MasterId": "357b2f1f-7342-46e1-b4ae-1f901175e88c", "SubmissionId": "LAPP15089696", "SubmissionCategoryID": 999999, "ApprovedBy": "", "DocRequired": "Certificate of Occupancy (COfO) Document", "Agency": "DCRA", "Division": "Zoning", "Div": "Zoning", "FileName": "", "FileLocation": "", "DocStatus": "Open", "Description": "DCRA", "Endorsement": "Motor Vehicle Sales, Service, & Repair", "License": "Auto Rental", "CategoryID": "1306", "ShortName": "COFO", "IsUpload": true, "UploadFileName": "", "CheckListType": "Document", "CategoryCode": "8001", "LicenseName": "LAPP15089696" }], "GrandTotals": null, "TotalLicenseFees": null, "TotalTechFees": null, "ApplicationSubmit": null, "LicenseDuration": "2" }


        //test case partially completed
        it('should test init method', function () {
            localStorage.setItem("EmailConfirm", "");
            //httpBackend.flush();
            expect(receiptData.ServiceCheckList).toBeDefined();
        });



        it('should test navToMyBBLsList method', function () {
            receiptController.navToMyBBLsList();
            expect(receiptController.underreview).toBe(true);
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test filterByStatus method', function () {
            receiptController.filterByStatus('test');
            expect(receiptController.filterByStatusKeyword).toBe('test');
        });

        it('should test navTologin method', function () {
            receiptController.navTodashboard();
            expect($location.path).toHaveBeenCalledWith('/dashboard');
        });

        it('should test selectOption method', function () {
            scope.confirmed={num:10};
            receiptController.selectOption();
            expect(receiptController.itemPage).toEqual(10);
        });

        it('should test expandCollapse', function () {
            var event = { target: "" };
            receiptController.expandCollapse(event);
        });

    });

})