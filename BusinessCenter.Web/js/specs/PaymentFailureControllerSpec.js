describe('Payment Failure Controller Spec', function () {
    var $scope, controller, httpBackend, mockservice, paymentFailureController, localStore, appConstants, windowObj, routeParams, basePath, utilityfac, sessionfactory;
    var errorfactory, authservice, localstorageservice;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $window, $routeParams, appConstants,
        UtilityFactory, SessionFactory, errorFactory, authService, localStorageService) {
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
        appConstants = appConstants;
        windowObj = $window;
        routeParams = $routeParams;
        routeParams.guid = 'guid';
        basePath = appConstants.apiServiceBaseUri;
        utilityfac = UtilityFactory;
        sessionfactory = SessionFactory;
        authservice = authService;
        localstorageservice = localStorageService;
        errorfactory = errorFactory;
    }));

    var submissionststus_data = {
        "Status": "Draft", "MasterId": "61c6dd67-b3ec-4ac3-af25-a8830a5ff0c8", "TradeName": "NA", "BusinessStructure"
    : "Corporation (For Profit)", "IsCorporationDivision": true, "IsCoporateRegistration": false, "IsResidentAgent"
    : false, "IsIndividual": false, "IsBusinessMustbeinDc": true, "AppType": "B", "IsFEIN": true, "DocSubmType": ""
, "PaymentId": null, "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "", "CurrentYear"
: "2016", "CreatedDate": "03/21/2016", "SelectedMailType": "", "PremisesAddress": "", "BusinessName": "", "IsCategorySelfCertification"
: false
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

    describe('it should test when routeparams.paymenttype is renewal', function () {
        beforeEach(function () {
            routeParams.paymenttype = "renewal";
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond(submissionststus_data);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            paymentFailureController = controller('PaymentFailureController', {
                $scope: $scope, $rootScope: rootscope,
                windowObj: windowObj, $location: $location,
                $routeParams: routeParams, mockservice: mockservice,
                appConstants: appConstants, utilityfactory: utilityfac,
                SessionFactory: sessionfactory,
                errorFactory: errorfactory, authService: authservice
            });
        });

        it('should test payment type is renewal', function () {
            httpBackend.flush();
            expect(paymentFailureController.renewalBreadcrum).toBeTruthy();
        });
    });

    describe('it should test when routeparams.paymenttype is renewal and payment status is TRUE and corp status is not active', function () {
        beforeEach(function () {
            submissionststus_data.PaymentStatus = "TRUE";
            submissionststus_data.CorporationStatus = "revoked";
            routeParams.paymenttype = "renewal";
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond(submissionststus_data);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            paymentFailureController = controller('PaymentFailureController', {
                $scope: $scope, $rootScope: rootscope,
                windowObj: windowObj, $location: $location,
                $routeParams: routeParams, mockservice: mockservice,
                appConstants: appConstants, utilityfactory: utilityfac,
                SessionFactory: sessionfactory,
                errorFactory: errorfactory, authService: authservice
            });
        });

        it('should test payment type is renewal', function () {
            httpBackend.flush();
            expect(paymentFailureController.renewalBreadcrum).toBeTruthy();
            expect(paymentFailureController.paymentFailure).toEqual('false');
            expect(paymentFailureController.corpFailure).toEqual('true');
        });
    });

    describe('it should test when routeparams.paymenttype is not renewal ', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond(submissionststus_data);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            paymentFailureController = controller('PaymentFailureController', {
                $scope: $scope, $rootScope: rootscope,
                windowObj: windowObj, $location: $location,
                $routeParams: routeParams, mockservice: mockservice,
                appConstants: appConstants, utilityfactory: utilityfac,
                SessionFactory: sessionfactory,
                errorFactory: errorfactory, authService: authservice
            });
        });

        it('should test payment type is renewal', function () {
            httpBackend.flush();
            expect(paymentFailureController.renewalBreadcrum).toBeFalsy();
        });

        it('should test dontNavigate method', function () {
            paymentFailureController.dontNavigate()
            expect(paymentFailureController.navigate).toBeFalsy();
        });

        it('should test navigateAnyway method', function () {
            paymentFailureController.next = 'test#/test';
            paymentFailureController.navigateAnyway();
            expect(paymentFailureController.navigate).toBeTruthy();
            expect($location.path).toHaveBeenCalledWith('/test/guid');
        });

        it('should test navToChecklist method', function () {
            paymentFailureController.navToChecklist();
            expect(paymentFailureController.navigate).toBeTruthy();
            //expect(localStorage.getItem("vm.PaymentStatus")).toBeUndefined();
            //expect(localStorage.getItem("vm.taxRevenueValidation")).toBeUndefined();
            //expect(localStorage.getItem("vm.validateCorp")).toBeUndefined();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        });

        it('should test navToMyBBL method', function () {
            paymentFailureController.navToMyBBL();
            expect(paymentFailureController.navigate).toBeTruthy();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navToPayment method', function () {
            paymentFailureController.navToPayment();
            expect(paymentFailureController.navigate).toBeTruthy();
            //expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

    });
});