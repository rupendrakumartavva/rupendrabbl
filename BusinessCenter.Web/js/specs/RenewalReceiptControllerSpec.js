describe('Renewal Receipt controller spec', function () {

    var scope, routeParams, controller, httpBackend, mockservice, renewalreceiptController, localStore, basePath, renewalutilityfac, appconstants;
    var data = { "Result": [{ "MasterId": "353233c1-6d6d-4f4d-a275-cf5a70cc0bc8", "SubmissionLicense": "10049456", "UserID": "C4C26C13-5881-4060-8A87-CBC6B1DB9CA9", "ActivityID": "", "ApplicationFee": 70.0000, "RAOFee": 0.0000, "IseHOP": false, "eHOP": 0.0000, "Status": "Active", "ExpirationDate": "2015-12-24T17:17:32.39", "ApprovedBy": "", "Description": "E1593984", "App_Type": "", "DocSubmType": "ON", "FEIN": "", "IsFEIN": false, "IsBusinessMustbeinDC": false, "IsHomeBased": false, "IsCofo": false, "IsPhysicalLocationVerify": false, "GrandTotal": 690.3000, "isCorporationDivision": false, "BusinessStructure": "", "TradeName": "", "CreatedDate": "2015-12-24T17:17:32.39", "Updatedate": "2015-12-25T11:54:07.497", "UserSelectMailAddressType": "", "LicenseDuration": 2, "UserBblAssociateId": "1" }] };
    var receiptDetails = { "FullName": null, "TransactionSuccess": null, "InnerHtml": null, "EmailAddress": null, "MasterID": "353233c1-6d6d-4f4d-a275-cf5a70cc0bc8", "PaymentID": "99c588c8-f060-48e8-a549-5d7df975044a", "SubNumber": "LREN15018910", "AmountCharged": 690.3000, "ReceiptDate": "12/25/2015", "CardNumber": "************1111", "TransactionId": "Test-Transaction", "IsEhopAllowed": false, "DocType": "ON", "GrandTotal": 0.0, "TotalApplicationFee": 0.0, "ApplicationFee": 70.00, "TotalLicenseFee": 0.0, "TotalEndosementFee": 0.0, "TotalTechFee": 0.0, "ExceptedFinalCheckingDate": "01/24/2016", "CategoryLicenseFee": 35.00, "EndorsementFee": 25.00, "SubTotal": 0.0, "TechFee": 17.30, "TotalFee": 690.30, "Isehop": false, "IsSubmissionCofo": false, "IsSubmissionHop": false, "IsSubmissioneHop": false, "ExtraAmount": 500.0, "Extradays": null, "ServiceCheckList": [{ "Endorsement": "Housing: Residential", "CategoryId": "3E5B807D-2ADB-4417-B4EF-5368DA34F817", "LicenseCategory": "One Family Rental", "Units": "1 ", "ApplicationFee": 70.00, "CategoryLicenseFee": 35.00, "EndorsementFee": 25.00, "SubTotal": 130.00, "TechFee": 13.00, "TotalFee": 143.00, "CategoryCode": "5005", "IsRaoFeeApplied": true, "LicenseDuration": "" }], "DocumentList": [], "GrandTotals": null, "TotalLicenseFees": null, "TotalTechFees": null, "ApplicationSubmit": null, "LicenseDuration": "2" }
    beforeEach(module('DCRA'));

    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, RenewalUtilityFactory, appConstants) {
        basePath = appConstants.apiServiceBaseUri;
        rootscope = $rootScope.$new();
        $location = _$location_;
        scope = $rootScope.$new();
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
        renewalutilityfac = RenewalUtilityFactory;
        appconstants = appConstants;
    }));


    describe('when no guid is available', function () {
        beforeEach(function () {
            spyOn(renewalutilityfac, 'ifGuidAvailable').and.returnValue(false);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/GetMasterBasedonUserAssociateId').respond(data);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/GetReceiptDetails').respond(receiptDetails);
            renewalreceiptController = controller('RenewalReceiptController', {
                $scope: scope, $rootScope: rootscope, $location: $location,
                routeParams: routeParams, mockservice: mockservice,
                renewalUtilityFactory: renewalutilityfac, appConstants: appconstants
            });
        });

        it('should test init', function () {
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });


    describe('should test  logged in users', function () {
        beforeEach(function () {
            spyOn(renewalutilityfac, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/GetMasterBasedonUserAssociateId').respond(data);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/GetReceiptDetails').respond(receiptDetails);
            renewalreceiptController = controller('RenewalReceiptController', {
                $scope: scope, $rootScope: rootscope,
                $location: $location, routeParams: routeParams, mockservice: mockservice,
                renewalUtilityFactory: renewalutilityfac, appConstants: appconstants
            });
        });

        //test case partially completed
        it('should test init method', function () {
            localStorage.setItem("EmailConfirm", "");
            httpBackend.flush();
            expect(renewalreceiptController.documenttype).toBe('ON');
        });

        it('should test navToMyBBLsList method', function () {
            renewalreceiptController.navToMyBBLsList();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navTologin method', function () {
            renewalreceiptController.navTodashboard();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/dashboard');
        });

        it('should test selectOption method', function () {
            scope.confirmed = { num: 10 };
            renewalreceiptController.selectOption();
            expect(renewalreceiptController.itemPage).toEqual(10);
        });

        it('should test expandCollapse', function () {
            var event = { target: "" };
            renewalreceiptController.expandCollapse(event);
        });

    });

});