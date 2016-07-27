describe('Physical location Mailing Controller Spec', function () {

    var scope, rootscope, controller, httpBackend, mockservice, phylocMailing, localStore, basePath, form, appconstants, compile, routeparams, utilityfactory;
    var timeout, sessionfactory, errorfactory, bblsubmissionfactory, popupfactory, authservice, localstorageservice;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, $routeParams, UtilityFactory, $timeout,
        $compile, SessionFactory, errorFactory, BBLSubmissionFactory, popupFactory, authService, localStorageService) {
        basePath = appConstants.apiServiceBaseUri;
        rootscope = $rootScope.$new();
        scope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path').and.returnValue('test/ste/ast');

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
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        utilityfactory = UtilityFactory;
        sessionfactory = SessionFactory;
        timeout = $timeout;
        errorfactory = errorFactory;
        bblsubmissionfactory = BBLSubmissionFactory;
        compile = $compile;
        authservice = authService;
        localstorageservice = localStorageService;
        popupfactory = popupFactory;
    }));

    var initialData = {
        "UserType": "", "FileNumber": null, "MasterId": "3e2a4923-4aea-4d7f-83bd-1d13fc1856ba", "CBusinessName": null,
        "TradeName": null, "BusinessStructure": null, "FirstName": null, "MiddleName": null, "LastName": null, "BusinessName": null,
        "BusinessAddressLine1": null, "BusinessAddressLine2": null, "BusinessAddressLine3": null, "BusinessAddressLine4": null,
        "BusinessCity": null, "BusinessState": null, "BusinessCountry": null, "ZipCode": null, "Email": null, "EntityStatus": null,
        "SubCorporationRegId": 0, "UserSelectTpe": null, "Quardrant": null, "UnitType": null, "Unit": null, "Telphone": null,
        "IsValid": false, "Dropdownlist": null, "OccupancyAddssValidate": null, "DonothaveCof": false, "CorpStatus": null, "HQStatus": null
    }

    var initialfull_data = {
        "StreetNumber": null, "AddressID": null, "AddressNumber": "", "AddressNumberSufix": null, "Anc": null, "Cluster"
    : null, "Latitude": null, "Longitude": null, "Vote_Prcnct": null, "Ward": null, "Xcoord": null, "Ycoord": null, "UserType"
    : "NEWMAIL", "FileNumber": "900180", "MasterId": "3bf1795b-64ff-4c97-921d-cc8b0f136da9", "CBusinessName": null
, "TradeName": null, "BusinessStructure": null, "FirstName": "dsad", "MiddleName": "dsa", "LastName": "dasd", "BusinessName"
: "asdas", "BusinessAddressLine1": "asda", "BusinessAddressLine2": "dasd", "BusinessAddressLine3": "dsad", "BusinessAddressLine4"
: null, "BusinessCity": "dsad", "BusinessState": "sad", "BusinessCountry": "TT", "ZipCode": "13123", "Email": "asd@g.in", "EntityStatus": null, "SubCorporationRegId": 0, "UserSelectTpe": "NEWMAIL", "Quardrant": null, "UnitType"
: null, "Unit": null, "Telphone": "321321321312", "IsValid": false, "Dropdownlist": null, "OccupancyAddssValidate"
    : null, "DonothaveCof": false, "CorpStatus": null, "HQStatus": null, "Zone": null, "Smd": null, "SSL": null, "BusinessStructureStatus"
    : false, "IsDataChange": false
    }

    var dropdownData = ["Alley", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive", "Driveway", "Expressway", "Freeway",
                    "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade", "Road", "Row", "Square", "Street", "Terrace", "Walk", "Way"];

    var statesdata = { "Status": [{ "StateCode": "AL", "StateName": "Alaska", "CountryCode": "US" }] };

    var submit_data = {
        "StreetNumber": null, "AddressID": null, "AddressNumber": "", "AddressNumberSufix": null, "Anc": null, "Cluster"
    : null, "Latitude": null, "Longitude": null, "Vote_Prcnct": null, "Ward": null, "Xcoord": null, "Ycoord": null, "UserType"
    : "NEWMAIL", "FileNumber": "900180", "MasterId": "3bf1795b-64ff-4c97-921d-cc8b0f136da9", "CBusinessName": "asdas"
, "TradeName": null, "BusinessStructure": null, "FirstName": "dsad", "MiddleName": "dsa", "LastName": "dasd", "BusinessName"
: "asdas", "BusinessAddressLine1": "asda", "BusinessAddressLine2": "dasd", "BusinessAddressLine3": "dsad", "BusinessAddressLine4"
: null, "BusinessCity": "dsad", "BusinessState": "asdasdsa", "BusinessCountry": "TT", "ZipCode": "13123", "Email"
: "asd@g.in", "EntityStatus": null, "SubCorporationRegId": 0, "UserSelectTpe": "NEWMAIL", "Quardrant": null, "UnitType"
: null, "Unit": null, "Telphone": "321321321312", "IsValid": false, "Dropdownlist": null, "OccupancyAddssValidate"
: null, "DonothaveCof": false, "CorpStatus": null, "HQStatus": null, "Zone": null, "Smd": null, "SSL": null, "BusinessStructureStatus"
: false, "IsDataChange": false
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

    describe('test cases for init with no guid available', function () {
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            phylocMailing = controller('PhysicallocMailingController', {
                $scope: scope, $rootScope: rootscope, $location: $location,
                mockservice: mockservice, appconstants: appconstants,
                $routeParams: routeparams, UtilityFactory: utilityfactory, $timeout: timeout,
                SessionFactory: sessionfactory, errorFactory: errorfactory,
                BBLSubmissionFactory: bblsubmissionfactory,
                popupFactory: popupfactory, authService: authservice
            });
        });

        it('should test init with underreivew status', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('test cases for init with underreivew status', function () {
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/HeadQuarterAddress').respond(initialData);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            phylocMailing = controller('PhysicallocMailingController', {
                $scope: scope, $rootScope: rootscope, $location: $location,
                mockservice: mockservice, appconstants: appconstants,
                $routeParams: routeparams, UtilityFactory: utilityfactory, $timeout: timeout,
                SessionFactory: sessionfactory, errorFactory: errorfactory,
                BBLSubmissionFactory: bblsubmissionfactory,
                popupFactory: popupfactory, authService: authservice
            });
        });

        it('should test init with underreivew status', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

    });

    describe('test cases for init with draft status', function () {
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "DRAFT" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/StateListBasedOnCode').respond(statesdata);
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCorpAgent').respond(true);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/GetMailType').respond(initialData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/HeadQuarterAddress').respond(initialData);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            phylocMailing = controller('PhysicallocMailingController',
                {
                    $scope: scope, $rootScope: rootscope, $location: $location,
                    mockservice: mockservice, appconstants: appconstants,
                    $routeParams: routeparams, UtilityFactory: utilityfactory, $timeout: timeout,
                    SessionFactory: sessionfactory, errorFactory: errorfactory,
                    BBLSubmissionFactory: bblsubmissionfactory,
                    popupFactory: popupfactory, authService: authservice
                });
            scope.vm = phylocMailing;
            var element = angular.element(
                   '<form name="vm.prefer_mail">' +
                   '<input ng-model="vm.zip" name="Zipcode" />' +
                   '<input ng-model="vm.telephone" name="telephone" />' +
                   '</form>'
                   );
            compile(element)(scope);
            form = scope.vm.prefer_mail;
        });

        it('should test init with draft status', function () {
            httpBackend.flush();
            expect(phylocMailing.diffmail.BusinessCountry).toBe('US');
        });

        it('it should test selectOption method when type is one', function () {

            phylocMailing.address = {};
            phylocMailing.address.type = "1";
            phylocMailing.diffmail = {};
            phylocMailing.selectOption();
            httpBackend.flush();
            expect(phylocMailing.diffmail.BusinessCountry).toBe('US');
            expect(phylocMailing.validateCountry).toBeTruthy();
        });

        it('should test checkAndExit', function () {
            phylocMailing.diffmail = {};
            form.$invalid = false;
            phylocMailing.prefer_mail = {};
            phylocMailing.prefer_mail.$invalid = false;
            phylocMailing.checkAndExit("mybbl");
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test checkAndExit when both objects are same', function () {
            phylocMailing.diffmail = submit_data;
            phylocMailing.prevObj = {};
            form.$invalid = false;
            phylocMailing.checkAndExit("appchecklist");
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        });

        //it('should test stayOnThisPage', function () {
        //    phylocMailing.stayOnThisPage();
        //    //expect(phylocMailing.navigate).toBeTruthy();
        //});

        //it('should test navigateAnyWay', function () {
        //    phylocMailing.navigationPath = "testpath";
        //    phylocMailing.navigateAnyWay();
        //    expect(phylocMailing.navigate).toBeTruthy();
        //    expect($location.path).toHaveBeenCalledWith('/testpath');
        //});

        it('should test countryChanged when selected country is US', function () {
            phylocMailing.diffmail = {};
            phylocMailing.diffmail.BusinessCountry = "US"
            phylocMailing.navigationPath = "testpath";
            phylocMailing.countryChanged();
            expect(phylocMailing.validateCountry).toBeTruthy();
        });

        it('should test countryChanged when selected country is not US', function () {
            phylocMailing.diffmail = {};
            phylocMailing.diffmail.BusinessCountry = "Brazil"
            phylocMailing.navigationPath = "testpath";
            phylocMailing.countryChanged();
            expect(phylocMailing.validateCountry).toBeFalsy();
        });

        it('should test checkTelephoneMaxLength when country is US', function () {
            phylocMailing.validations_wrt_contry = errorfactory.isCountryUS(true);
            phylocMailing.diffmail = {};
            phylocMailing.diffmail.Telphone = "11";
            phylocMailing.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeFalsy();
        });

        it('should test checkTelephoneMaxLength when country is US and telephone length is correct', function () {
            phylocMailing.validations_wrt_contry = errorfactory.isCountryUS(true);
            phylocMailing.diffmail = {};
            phylocMailing.diffmail.Telphone = "1111111111";
            phylocMailing.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();
        });

        it('should test checkTelephoneMaxLength when country is not US', function () {
            phylocMailing.validations_wrt_contry = errorfactory.isCountryUS(false);
            phylocMailing.diffmail = {};
            phylocMailing.diffmail.Telphone = "11";
            phylocMailing.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();
        });

        it('should test checkTelephoneMaxLength when country is not US and telephone length is correct', function () {
            phylocMailing.validations_wrt_contry = errorfactory.isCountryUS(false);
            phylocMailing.diffmail = {};
            phylocMailing.diffmail.Telphone = "1111111111";
            phylocMailing.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();
        });

        it('should test checkZipMaxLength when country is US', function () {
            phylocMailing.validations_wrt_contry = errorfactory.isCountryUS(true);
            phylocMailing.diffmail = {};
            phylocMailing.diffmail.ZipCode = "11";
            phylocMailing.checkZipMaxLength();
            expect(form.Zipcode.$valid).toBeFalsy();
        });

        it('should test checkZipMaxLength when country is US zip length is correct', function () {
            phylocMailing.validations_wrt_contry = errorfactory.isCountryUS(true);
            phylocMailing.diffmail = {};
            phylocMailing.diffmail.ZipCode = "11111";
            phylocMailing.checkZipMaxLength();
            expect(form.Zipcode.$valid).toBeTruthy();
        });

        it('should test setErrorMsg', function () {
            phylocMailing.setErrorMsg();
        });
        //    it('should set $scope.success to true', function () {
        //        var newUrl = 'http://#/test/foourl.com';
        //        var oldUrl = 'http://#/test/barurl.com'

        //        scope.$apply(function () {
        //            rootscope.$broadcast('$locationChangeStart', newUrl, oldUrl);
        //        });
        //        //scope.$broadcast('$locationChangeSuccess');
        //        expect(phylocMailing.navigationPath).toBe(true);
        //});
    });
});