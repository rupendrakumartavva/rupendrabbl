describe('corp not registerd address Controller Spec', function () {

    var scope, controller, httpBackend,localstorageservice, mockservice, authservice, corpnotRegAddress, localStore, basePath, form, utilityfactory, compile, routeparams, appconstants, sessionfactory, errorfactory, bblsubmissionfactory, popupfactory;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope,
        _$location_, requestService, $httpBackend, appConstants, UtilityFactory, authService, $routeParams, SessionFactory, errorFactory, BBLSubmissionFactory, $compile, popupFactory, localStorageService) {
        rootscope = $rootScope.$new();
        scope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path').and.returnValue('test/test1/test2');

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
        appconstants = appConstants;
        basePath = appConstants.apiServiceBaseUri;
        utilityfactory = UtilityFactory;
        routeparams = $routeParams;
        sessionfactory = SessionFactory;
        errorfactory = errorFactory;
        bblsubmissionfactory = BBLSubmissionFactory;
        routeparams.guid = 'guid';
        compile = $compile;
        popupfactory = popupFactory;
        authservice = authService;
        localstorageservice = localStorageService;
    }));


    var initialData = { "UserType": "N-CORPREG", "FileNumber": "NA", "MasterId": "3e2a4923-4aea-4d7f-83bd-1d13fc1856ba", "CBusinessName": null, "TradeName": null, "BusinessStructure": "", "FirstName": null, "MiddleName": null, "LastName": null, "BusinessName": null, "BusinessAddressLine1": null, "BusinessAddressLine2": null, "BusinessAddressLine3": null, "BusinessAddressLine4": null, "BusinessCity": null, "BusinessState": null, "BusinessCountry": null, "ZipCode": null, "Email": null, "EntityStatus": null, "SubCorporationRegId": 0, "UserSelectTpe": null, "Quardrant": null, "UnitType": null, "Unit": null, "Telphone": null, "IsValid": false, "Dropdownlist": null, "OccupancyAddssValidate": null, "DonothaveCof": false, "CorpStatus": null, "HQStatus": null }
    var dropdownData = ["Alley", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive", "Driveway", "Expressway", "Freeway",
                    "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade", "Road", "Row", "Square", "Street", "Terrace", "Walk", "Way"];

    var statesdata = { "Status": [{ "StateCode": "AL", "StateName": "Alaska", "CountryCode": "US" }] };

    var errormessages = [{ "ShortName": "incompleteData", "ErrrorMessage": "All requested information is required in order to save the data you entered.  Please select [OK] to exit without saving or [CANCEL] to stay on the page." }, { "ShortName": "ehop_inEligible", "ErrrorMessage": "Given your response, you are ineligible for an Expedited Home Occupancy Permit.  Please select [Return to Checklist] and visit the DC Office of Zoning at 1100 4th St., SW or call 202-442-4400 to obtain an HOP." }, { "ShortName": "verifyandcontinuemessage", "ErrrorMessage": "To revise any of your responses, select [Cancel] and then select the [Return to Checklist] button. To proceed, select [Confirm]." }, { "ShortName": "corpnodata", "ErrrorMessage": "The File Number you entered does not match the Corporations records. Please check your entry and try again. If your entry does not match, please select [Return to Checklist] and visit the DCRA Corporations Division at 1100 4th St., SW or call  202-442-4400." }, { "ShortName": "feinssnNonCompliance", "ErrrorMessage": "According to Office of Tax and Revenue (OTR) records, the FEIN you entered is not in compliance with the District of Columbia's Clean Hands requirements. Please click on Tax and Revenue link below to know how to proceed further." }, { "ShortName": "corpSearchNotClicked", "ErrrorMessage": "You must select [Search Corp Online].  Please click [OK] to proceed without saving the entered data or click [CANCEL] to stay on the page." }, { "ShortName": "allfieldsNotFilled", "ErrrorMessage": "Please complete all the required fields." }, { "ShortName": "renewalNavigation", "ErrrorMessage": "To save any updates/changes, please select [CANCEL] to stay on the page or [OK] to leave without saving." }, { "ShortName": "navigateaway", "ErrrorMessage": "To save any updates/changes, please select [CANCEL] to stay on the page or [OK] to leave without saving." }, { "ShortName": "hopallfieldsNotFilled", "ErrrorMessage": "Please complete all fields." }, { "ShortName": "corpFileNumberError", "ErrrorMessage": "Please enter your Corporate Registration File Number." }, { "ShortName": "createChecklistnavigation", "ErrrorMessage": "The data you have selected/entered so far will be lost. You must complete all of the pre-application questions and create a checklist to save your data. Do you want to exit without saving?" }, { "ShortName": "NextButtonIncompleteData", "ErrrorMessage": "Please provide all requested data and select [Next]." }, { "ShortName": "ehopSelectionErrorMsg", "ErrrorMessage": "You must select [Confirm eHOP Eligibility].  Please click [OK] to proceed without saving the entered data or click [CANCEL] to stay on the page." }, { "ShortName": "corp_number_failedstatus", "ErrrorMessage": "According to the Corporations Division's files, the Status of your Corporate Registration is {0}. You must resolve any  issues prior to submitting your Business License application. Please select Return to Checklist and visit the DCRA Corporations Division at 1100 4th St., SW, or call 202-442-4400." }, { "ShortName": "createCheckList", "ErrrorMessage": "Making changes after your Checklist is created will require you to discard your Application and start a new Application from the beginning. To proceed and create your Checklist select [Confirm]. To review and revise your responses select [Cancel] and select the Revise button on the bottom of the page." }, { "ShortName": "corp_businessstructure_mismatch", "ErrrorMessage": "The Business Structure you selected in the Pre-Application Questions does not match the Corporations records. Please check your entry and try again. If your entry does not match, please select [Return to Checklist] and visit the DCRA Corporations Division at 1100 4th St., SW or call 202-442-4400." }, { "ShortName": "donothaveCofo", "ErrrorMessage": "The data selected/entered for this CofO will not be retained. Select [OK] to proceed with this option or [Cancel] to retain the data as presented." }, { "ShortName": "searchNotClicked", "ErrrorMessage": "Please select [Search Zoning], or click [OK] to proceed without saving, or select [CANCEL] to stay on the page." }, { "ShortName": "renewalpaymentallfieldsNotFilled", "ErrrorMessage": "Please provide all requested data." }];
    describe('test cases when the accessed application is in guid is not available', function () {

        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            corpnotRegAddress = controller('CorpNotRegisteredAddressController',
                {
                    $scope: scope,
                    $rootScope: rootscope, $location: $location,
                    mockservice: mockservice, appConstants: appconstants,
                    routeParams: routeparams, UtilityFactory: utilityfactory,
                    sessionFactory: sessionfactory,
                    errorFactory: errorfactory, BBLSubmissionFactory: bblsubmissionfactory,
                    popupFactory: popupfactory, authService: authservice
                });
        });

        it('should test init with no guid available', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });


    describe('test cases when the accessed application is in underreview status', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            corpnotRegAddress = controller('CorpNotRegisteredAddressController',
                {
                    $scope: scope,
                    $rootScope: rootscope, $location: $location,
                    mockservice: mockservice, appConstants: appconstants,
                    routeParams: routeparams, UtilityFactory: utilityfactory,
                    sessionFactory: sessionfactory,
                    errorFactory: errorfactory, BBLSubmissionFactory: bblsubmissionfactory,
                    popupFactory: popupfactory, authService: authservice
                });
        });

        it('should test init with underreivew status', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });


    describe('test cases when the accessed application is in draft status', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "DRAFT" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/StateListBasedOnCode').respond(statesdata);
            httpBackend.when('POST', basePath + 'api/BBLApplication/EmptyHeadQuarterAddress').respond(true);
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCorpAgent').respond(true);
            httpBackend.when('POST', basePath + 'api/BBLApplication/HeadQuarterAddress').respond(initialData);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            httpBackend.when("partials/templates/popuptemplate.html").respond(200);
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);

            corpnotRegAddress = controller('CorpNotRegisteredAddressController',
                {
                    $scope: scope,
                    $rootScope: rootscope, $location: $location,
                    mockservice: mockservice, appConstants: appconstants,
                    routeParams: routeparams, UtilityFactory: utilityfactory,
                    sessionFactory: sessionfactory,
                    errorFactory: errorfactory, BBLSubmissionFactory: bblsubmissionfactory,
                    popupFactory: popupfactory, authService: authservice
                });
            scope.vm = corpnotRegAddress;
            var element = angular.element(
                   '<form name="vm.corpnorregadd_form">' +
                   '<input ng-model="vm.zip" name="ZipCode" />' +
                   '<input ng-model="vm.telephone" name="Telephone" />' +
                   '</form>'
                   );
            compile(element)(scope);
            form = scope.vm.corpnorregadd_form;
        });

        it('should test init with draft status', function () {
            httpBackend.flush();
            expect(corpnotRegAddress.previousObj).toEqual(corpnotRegAddress.ehopaddress);
        });

        it('should test navToAgent form is invalid', function () {
            form.$invalid = true;
            corpnotRegAddress.navToAgent();
            expect(sessionfactory.isSessionDirty()).toBeTruthy();
        });

        it('should test navToAgent when form is valid', function () {
            corpnotRegAddress.navToAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredagent/guid');
        });

        it('should test setErrorMsg', function () {
            corpnotRegAddress.setErrorMsg();
        });

        it('should test checkAndExit when form is valid', function () {
            corpnotRegAddress.ehopaddress = { 'BusinessName': 'test' };
            corpnotRegAddress.checkAndExit('testpath');
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/testpath');
        });

        it('should test checkAndExit when form is invalid and empty', function () {
            corpnotRegAddress.ehopaddress = { 'BusinessName': 'test' };
            form.$invalid = true;
            spyOn(sessionfactory, 'isFormEmpty').and.returnValue(true);
            corpnotRegAddress.checkAndExit('appchecklist');
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        });

        it('should test checkAndExit when form is invalid and not empty', function () {
            corpnotRegAddress.ehopaddress = { 'BusinessName': 'test' };
            form.$invalid = true;
            spyOn(sessionfactory, 'isFormEmpty').and.returnValue(false);
            corpnotRegAddress.checkAndExit('appchecklist');
            expect(sessionfactory.isSessionDirty()).toBeTruthy();
        });

        it('should test checkTelephoneMaxLength when country is US', function () {
            corpnotRegAddress.validations_wrt_contry = errorfactory.isCountryUS(true);
            corpnotRegAddress.ehopaddress = {};
            corpnotRegAddress.ehopaddress.Telphone = "11";
            corpnotRegAddress.checkTelephoneMaxLength();
            expect(form.Telephone.$valid).toBeFalsy();

            //telephone length is correct

            corpnotRegAddress.ehopaddress.Telphone = "1111111111";
            corpnotRegAddress.checkTelephoneMaxLength();
            expect(form.Telephone.$valid).toBeTruthy();
        });

        it('should test checkTelephoneMaxLength when country is not US', function () {
            corpnotRegAddress.validations_wrt_contry = errorfactory.isCountryUS(false);
            corpnotRegAddress.ehopaddress = {};
            corpnotRegAddress.ehopaddress.Telphone = "11";
            corpnotRegAddress.checkTelephoneMaxLength();
            expect(form.Telephone.$valid).toBeTruthy();

            //telephone length is correct

            corpnotRegAddress.ehopaddress.Telphone = "1111111111";
            corpnotRegAddress.checkTelephoneMaxLength();
            expect(form.Telephone.$valid).toBeTruthy();
        });

        it('should test checkZipMaxLength when country is US', function () {
            corpnotRegAddress.validations_wrt_contry = errorfactory.isCountryUS(true);
            corpnotRegAddress.ehopaddress = {};
            corpnotRegAddress.ehopaddress.ZipCode = "11";
            corpnotRegAddress.checkZipMaxLength();
            expect(form.ZipCode.$valid).toBeFalsy();

            //zip length is correct
            corpnotRegAddress.ehopaddress.ZipCode = "11111";
            corpnotRegAddress.checkZipMaxLength();
            expect(form.ZipCode.$valid).toBeTruthy();
        });

        it('should test countryChanged when selected country is US', function () {
            corpnotRegAddress.ehopaddressStaticData = {};
            corpnotRegAddress.ehopaddressStaticData.BusinessCountry = "US";
            corpnotRegAddress.ehopaddress = {
                BusinessCountry: "US"
            }
            corpnotRegAddress.countryChanged();
            expect(corpnotRegAddress.validations_wrt_contry).toEqual(errorfactory.isCountryUS(true));
            expect(corpnotRegAddress.showStateAsDropdown).toBeFalsy();
        });

        it('should test countryChanged', function () {
            corpnotRegAddress.ehopaddressStaticData = {};
            corpnotRegAddress.ehopaddressStaticData.BusinessCountry = "NUS";
            corpnotRegAddress.countryChanged(true);
            expect(corpnotRegAddress.validations_wrt_contry).toEqual(errorfactory.isCountryUS(false));
            expect(corpnotRegAddress.showStateAsDropdown).toBeTruthy();
        });

    });
});