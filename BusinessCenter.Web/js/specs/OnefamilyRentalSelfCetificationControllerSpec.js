describe('One family rental Controller Spec', function () {

    var $scope, controller, httpBackend, mockservice, onefamilyrentalcontroller, localStore, appconstants, routeParams;
    var basePath, utilityfactory, sessionfactory, bblsubmissionfactory, compile, errorfactory, authservice, localstorageservice, popupfactory;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $routeParams, UtilityFactory,
        errorFactory, SessionFactory, BBLSubmissionFactory, appConstants, $compile, popupFactory, authService, localStorageService) {
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
        compile = $compile;
        errorfactory = errorFactory;
        authservice = authService;
        popupfactory = popupFactory
        localstorageservice = localStorageService
    }));

    var submissionstatusdata = {
        "Status": "Draft", "MasterId": "17d151f0-09bf-4665-b481-5aa920bcad9f",
        "TradeName": "NA", "BusinessStructure": "GENERAL PARTNERSHIP", "IsCorporationDivision": false,
        "IsCoporateRegistration": true, "IsResidentAgent": true,
        "IsIndividual": false, "IsBusinessMustbeinDc": true, "AppType": "B",
        "IsFEIN": true, "DocSubmType": "", "PaymentId": null,
        "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "asd", "CurrentYear": "2016",
        "CreatedDate": "03/02/2016", "SelectedMailType": "", "PremisesAddress": "1111  11TH Street NW  102 Washington District of Columbia United States 20001",
        "BusinessName": "asd", "IsCategorySelfCertification": true
    },

    savedcertificationdata = [
        {
            "SelfCertificationId": "119e5df8-a6b8-4b82-89bb-2435917174f8", "MasterId": "17d151f0-09bf-4665-b481-5aa920bcad9f",
            "MasterCategoryId": null, "IsPropertyOccupied": true, "FullName": "asd", "SelfCertificationOn": "2016-03-02T00:00:00",
            "CreatedDate": "2016-03-02T12:07:25.327", "UpdatedDate": "2016-03-02T12:09:51.4", "IsAgree": true
        }
    ], save_certification_data = { "Status": true };

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

    describe('test cases when the accessed application is in guid is not available', function () {

        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            onefamilyrentalcontroller = controller('OnefamilyRentalSelfCetificationController',
                {
                    $scope: $scope, $rootScope: rootscope,
                    $location: $location, mockservice: mockservice,
                    appConstants: appconstants, $routeParams: routeParams,
                    UtilityFactory: utilityfactory, BBLSubmissionFactory: bblsubmissionfactory,
                    errorFactory: errorfactory,
                    SessionFactory: sessionfactory, popupFactory: popupfactory,
                    authService: authservice
                });
        });

        it('should test init with no guid available', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('when status is underreview', function () {

        submissionstatusdata.Status = "underreview";
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond(submissionstatusdata);
            httpBackend.when('GET', "partials/templates/popuptemplate.html").respond(200);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            onefamilyrentalcontroller = controller('OnefamilyRentalSelfCetificationController',
                 {
                     $scope: $scope, $rootScope: rootscope,
                     $location: $location, mockservice: mockservice,
                     appConstants: appconstants, $routeParams: routeParams,
                     UtilityFactory: utilityfactory, BBLSubmissionFactory: bblsubmissionfactory,
                     errorFactory: errorfactory,
                     SessionFactory: sessionfactory, popupFactory: popupfactory,
                     authService: authservice
                 });
        });

        it('should test', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('when status is not underreview and is fein with no initial data', function () {

        beforeEach(function () {
            submissionstatusdata.Status = "draft";
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond(submissionstatusdata);
            httpBackend.when('POST', basePath + 'api/BBLApplication/GetSelftCertification').respond(savedcertificationdata);
            httpBackend.when('POST', basePath + 'api/BBLApplication/SelftCertificationInsert').respond(save_certification_data);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            httpBackend.when('GET', "partials/templates/popuptemplate.html").respond(200);
            onefamilyrentalcontroller = controller('OnefamilyRentalSelfCetificationController',
                {
                    $scope: $scope, $rootScope: rootscope,
                    $location: $location, mockservice: mockservice,
                    appConstants: appconstants, $routeParams: routeParams,
                    UtilityFactory: utilityfactory, BBLSubmissionFactory: bblsubmissionfactory,
                    errorFactory: errorfactory,
                    SessionFactory: sessionfactory, popupFactory: popupfactory,
                    authService: authservice
                });
            $scope.vm = onefamilyrentalcontroller;
            var element = angular.element(
                   '<form name="vm.onefamilyrental">' +
                   '<input ng-model="vm.selfCertification" name="taxnumber" />' +
                   '<input ng-model="vm.selfCertification" name="signature" />' +
                   '</form>'
                   );
            compile(element)($scope);
            form = $scope.vm.onefamilyrental;

        });

        //it('should test init', function () {
        //    httpBackend.flush();
        //    expect(onefamilyrentalcontroller.isfein).toBeTruthy();
        //});

        //it('should test applyMask when given number is fein', function () {
        //    var e = {};
        //    e.keyCode = 3;
        //    onefamilyrentalcontroller.taxrevenue = {};
        //    onefamilyrentalcontroller.taxrevenue.number = "12";
        //    onefamilyrentalcontroller.applyMask(e, 'f');
        //    expect(onefamilyrentalcontroller.taxrevenue.number).toEqual("12-");
        //    httpBackend.flush();
        //});

        //it('should test applyMask when given number is fein', function () {
        //    var e = {};
        //    e.keyCode = 3;
        //    onefamilyrentalcontroller.taxrevenue = {};
        //    onefamilyrentalcontroller.taxrevenue.number = "123";
        //    onefamilyrentalcontroller.applyMask(e, 's');
        //    expect(onefamilyrentalcontroller.taxrevenue.number).toEqual("123-");
        //    httpBackend.flush();
        //});

        //it('should test watch of taxrevenue.number when invalid format is given', function () {
        //    onefamilyrentalcontroller.taxrevenue = {};
        //    onefamilyrentalcontroller.taxrevenue.number = "123@";
        //    $scope.$apply();
        //    expect(form.taxnumber.$valid).toBeFalsy();
        //});

        //it('should test watch of taxrevenue.number when hyphens are properly provided', function () {
        //    onefamilyrentalcontroller.isfein = true;
        //    onefamilyrentalcontroller.taxrevenue = {};
        //    onefamilyrentalcontroller.taxrevenue.number = "12-311";
        //    $scope.$apply();
        //    expect(form.taxnumber.$valid).toBeFalsy();

        //    onefamilyrentalcontroller.taxrevenue.number = "12-31111111";
        //    $scope.$apply();
        //    expect(form.taxnumber.$valid).toBeTruthy();
        //});

        //it('should test watch of taxrevenue.number when hyphens are properly provided', function () {
        //    onefamilyrentalcontroller.isfein = false;
        //    onefamilyrentalcontroller.taxrevenue = {};
        //    onefamilyrentalcontroller.taxrevenue.number = "123-11";
        //    $scope.$apply();
        //    expect(form.taxnumber.$valid).toBeFalsy();

        //    onefamilyrentalcontroller.taxrevenue.number = "12-31111111";
        //    $scope.$apply();
        //    expect(form.taxnumber.$valid).toBeTruthy();
        //});


        it('should test navToMailingAddress', function () {
            form.$invalid = true;
            onefamilyrentalcontroller.selfCertification = {};
            onefamilyrentalcontroller.selfCertification.signature = "test";
            onefamilyrentalcontroller.navToMailingAddress();
        });

        it('should test navToMailingAddress', function () {
            form.$invalid = false;
            onefamilyrentalcontroller.selfCertification = {};
            onefamilyrentalcontroller.selfCertification.signature = "test";
            onefamilyrentalcontroller.selfCertification.FullName = "test";
            onefamilyrentalcontroller.selfCertification.IsPropertyOccupied = "yes";
            onefamilyrentalcontroller.selfCertification.confirm = true;
            onefamilyrentalcontroller.navToMailingAddress();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/address/guid');
        });



        it('should test checkAndExit', function () {
            form.$invalid = true;
            onefamilyrentalcontroller.selfCertification = {};
            onefamilyrentalcontroller.selfCertification.signature = "test";
            onefamilyrentalcontroller.checkAndExit('appchecklist');
            httpBackend.flush();
            expect(onefamilyrentalcontroller.navigationPath).toBeTruthy();
        });



        it('should test checkAndExit', function () {
            form.$invalid = false;
            onefamilyrentalcontroller.selfCertification = {};
            onefamilyrentalcontroller.selfCertification.signature = "test";
            onefamilyrentalcontroller.checkAndExit('appchecklist');
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        });

        it('should test checkAndExit', function () {
            form.$invalid = false;
            onefamilyrentalcontroller.selfCertification = {};
            onefamilyrentalcontroller.selfCertification.signature = "test";
            onefamilyrentalcontroller.selfCertification.FullName = "test";
            onefamilyrentalcontroller.selfCertification.IsPropertyOccupied = "yes";
            onefamilyrentalcontroller.selfCertification.confirm = true;
            onefamilyrentalcontroller.checkAndExit('appchecklist');
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
            expect(onefamilyrentalcontroller.navigationPath).toBeTruthy();
        });

        //it('should test navigateAnyWay', function () {
        //    form.$invalid = false;
        //    onefamilyrentalcontroller.navigationPath = "mybbl";
        //    onefamilyrentalcontroller.navigateAnyway();
        //    httpBackend.flush();
        //    expect($location.path).toHaveBeenCalledWith('/mybbl');
        //});

        it('should test checkSignWithFullName when fullname and signature matches', function () {
            onefamilyrentalcontroller.selfCertification = {};
            onefamilyrentalcontroller.selfCertification.signature = "test";
            onefamilyrentalcontroller.selfCertification.FullName = "test";
            onefamilyrentalcontroller.checkSignWithFullName();
            expect(form.signature.$valid).toBeTruthy();
        });
        it('should test checkSignWithFullName when fullname and signature mismatches', function () {
            onefamilyrentalcontroller.selfCertification = {};
            onefamilyrentalcontroller.selfCertification.signature = "test";
            onefamilyrentalcontroller.selfCertification.FullName = "test1";
            onefamilyrentalcontroller.checkSignWithFullName();
            expect(form.signature.$valid).toBeFalsy();
        });

        it('should test checkSignWithFullName when fullname and signature mismatches', function () {

            onefamilyrentalcontroller.checkSignWithFullName();
            expect(onefamilyrentalcontroller.signatureMismatch).toBeFalsy();
        });
    });

});