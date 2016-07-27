describe('MyBBL Controller Spec', function () {


    var $scope, controller, httpBackend, mockservice, mybblController, localStore, timeout, http, basePath, utilityfactory, renewalutility, authservice, localstorageservice;
    var appconstants, filter, sessionfactory;

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

    beforeEach(module('DCRA'));

    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $timeout, $http,
        appConstants, UtilityFactory, RenewalUtilityFactory, $filter, SessionFactory, authService, localStorageService) {
        var data = {
            "result": [{
                "MasterId": "31163aec-7ead-4dae-94be-81aefcc0ead1",
                "BblServiceDoc": [], "IsIndividual": false, "IsFEIN": true, "DocSubType": "", "IsHop": false, "IsCof": true, "AppType": "B", "BusinessStructure": "For-ProfitCorporation", "TradeName": "", "CategoryName": "Secondhand Dealers (C)", "IsCorporationDivision": true, "ISFEINSSN": false, "IsCleanHandsVerify": false, "IsCorporateRegistration": true, "IsBHAddress": true, "IsBPAddress": true, "IsMailAddress": false, "IsResidentAgent": true, "IsDocforCleanHands": false, "IsDocforCofo": false, "IsDocforHop": false, "IsDocforEhop": false, "IsSubmissionCofo": true, "IsSubmissionHop": false, "IsSubmissioneHop": false, "CheckedStatus": false, "IsSubmissionCorpReg": false, "IsSubmissionAgent": false, "IsHomeBased": false, "IsBusinessMustbeinDC": true, "IsMcofo": true, "CategoryCode": "6020", "IsIndividualValid": false, "IsValidateTextRevenue": false
            }], "masterTextRevenue": false, "validateCorpFileStatus": "ACTIVE"
        };
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
        timeout = $timeout;
        http = $http;
        utilityfactory = UtilityFactory;
        renewalutility = RenewalUtilityFactory;
        authservice = authService;
        appconstants=appConstants;
        sessionfactory=SessionFactory;
        filter=$filter;
        localstorageservice = localStorageService;
        basePath = appConstants.apiServiceBaseUri;
        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
        httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Result": "true" });
        httpBackend.when('POST', basePath + 'api/BBLAssociation/BblServiceList').respond({ "Result": "test" });
        httpBackend.when('POST', basePath + 'api/BBLAssociation/BblRemoveServiceList').respond({ "Result": "true" });
        httpBackend.when('POST', basePath + 'api/Renew/RemoveRenewalData').respond("true");
        httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
        spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
        spyOn(localStorage, 'getItem').and.returnValue(JSON.stringify({ cId: 'c01', num: 10 }));
        mybblController = controller('MyBBLController', {
            $scope: $scope, $rootScope: rootscope,
            $location: $location, mockservice: mockservice,
            timeout: timeout, http: http,
            utilityFactory: utilityfactory, renewalFactory: renewalutility,
            appConstants: appconstants, $filter: filter, SessionFactory: sessionfactory,
            authService: authservice
        });
    }));

    it('should test getBblServiceList not in review', function () {
        httpBackend.flush();
        expect(mybblController.itemPage).toEqual(10);
    });

    it('should test navigation to Application Checklist', function () {
        spyOn(utilityfactory, 'getGuidByMasterId').and.returnValue('guid');
        mybblController.navToAppChecklist();
        expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
    });


    it('should test  ShowMessage', function () {
        mybblController.filterByStatusKeyword = "test";
        mybblController.ShowMessage();
        expect(mybblController.currentPage).toEqual(1);
        expect(localStorage.pagenumber).toEqual("1");
        expect(mybblController.currentPage).toEqual(1);
        expect(localStorage.filterByStatus).toBeUndefined();
        expect(mybblController.filterByStatusKeyword).toBe("test");
    });

    it('should test delete_record', function () {
        mybblController.delete_record();
        httpBackend.flush();
        expect(mybblController.businessList).toBe("test")
    });

    it('should test cancel_delete', function () {
        mybblController.cancel_delete();
        expect(localStorage.showMessagePopup).toBe('false');
    });

    it('should test selectOption', function () {
        mybblController.selectOption();
        expect(mybblController.currentPage).toEqual(1);
    });

    it('should test mybblfilter when item status is active', function () {
        var item = {};
        item.Status = 'Active'
        mybblController.filterByStatusKeyword = 'Active';
        mybblController.mybblfilter(item);
        expect(mybblController.mybblfilter(item)).toBeTruthy();
    });

    it('should test mybblfilter when item status is Eligible for Renewal', function () {
        var item = {};
        item.Status = 'Expired'
        mybblController.filterByStatusKeyword = 'Eligible for Renewal';
        mybblController.mybblfilter(item);
        expect(mybblController.mybblfilter(item)).toBeTruthy();
    });

    it('should test mybblfilter when item status is Under Review', function () {
        var item = {};
        item.Status = 'Under Review'
        mybblController.filterByStatusKeyword = 'Under Review';
        mybblController.mybblfilter(item);
        expect(mybblController.mybblfilter(item)).toBeTruthy();
    });

    it('should test mybblfilter when item status is eHOP', function () {
        var item = {};
        item.IsEhop = true;
        item.EhopNumber='testnumber'
        mybblController.filterByStatusKeyword = 'eHOP';
        expect(mybblController.mybblfilter(item)).toBe('testnumber');
    });

    it('should test mybblfilter when no filtering is made', function () {
        var item = {};
        item.IsEhop = true;
        mybblController.filterByStatusKeyword = '';
        mybblController.mybblfilter(item);
        expect(mybblController.mybblfilter(item)).toBeFalsy();
    });

    it('should test filterByStatus', function () {
        mybblController.filterByStatus("test", "testid");
        expect(localStorage.pagenumber).toEqual('1');
        expect(mybblController.currentPage).toEqual(1);
        expect(localStorage.filterByStatusKeyword).toBe("test");
        expect(mybblController.filterByStatusKeyword).toBe("test");
        expect(mybblController.previousElement).not.toBeUndefined();
    });

    it('should test setDisabled', function () {
        var boolval = mybblController.setDisabled(0, "testid");
        expect(boolval).toBe(true);
    });
    it('should test setDisabled', function () {
        var boolval = mybblController.setDisabled(1, "testid");
        expect(boolval).toBeFalsy();
    });


    it('should test navToApplyNewLicense', function () {
        mybblController.navToApplyNewLicense();
        expect($location.path).toHaveBeenCalledWith('/newbblwelcome');
    });

    it('should test navToAssociateBusinessLicense', function () {
        mybblController.navToAssociateBusinessLicense();
        expect($location.path).toHaveBeenCalledWith('/associatebbl');
    });

    it('should test navToRenewalConfirm', function () {
        spyOn(renewalutility, 'getGuidByRenewalServiceId').and.returnValue('guid');
        mybblController.navToRenewalConfirm('testentity', 'testlicense', 'testserviceid');
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/bblrenewalconfirm/guid');
    });

    it('should test navToRemove', function () {
        localStorage.showMessagePopup = false;
        mybblController.navToRemove("testid", "testlicense");
        expect(mybblController.SubmissionLicense).toBe('testid');
        expect(mybblController.licensenumber).toBe('testlicense');
    });

});