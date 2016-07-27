describe('NoPoBox Controller Spec', function () {


    var scope, controller, httpBackend, mockservice, nopoboxController, localStore, appConstants, basePath, routeparams, utilityfactory, timeout, sessionfactory, filter, marservice, errorfactory, bblsubmissionfactory, popupfactory;
    var authservice, localstorageservice;
    beforeEach(module('DCRA'));

    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, $routeParams,
        UtilityFactory, $timeout, SessionFactory, $filter, MAR_validation_service, errorFactory,
        BBLSubmissionFactory, popupFactory, $compile, authService, localStorageService) {
        basePath = appConstants.apiServiceBaseUri;
        rootscope = $rootScope.$new();
        $location = _$location_;
        scope = $rootScope.$new();
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
        appConstants = appConstants;
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        utilityfactory = UtilityFactory;
        timeout = $timeout;
        sessionfactory = SessionFactory;
        filter = $filter;
        marservice = MAR_validation_service;
        errorfactory = errorFactory;
        bblsubmissionfactory = BBLSubmissionFactory;
        popupfactory = popupFactory;
        compile = $compile;
        authservice=authService;
        localstorageservice = localStorageService;
    }));

    var dropdownData = ["Alley", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive", "Driveway", "Expressway", "Freeway",
                    "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade", "Road", "Row", "Square", "Street", "Terrace", "Walk", "Way"];

    var initialData = {
        "WebserviceList": [], "Dropdownlist": ["", "Alley", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive"
        , "Driveway", "Expressway", "Freeway", "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade"
        , "Road", "Row", "Square", "Street", "Terrace", "Walk", "Way"], "STNAME": null
    };

    var statesdata = { "Status": [{ "StateCode": "AL", "StateName": "Alaska", "CountryCode": "US" }] };

    var submit_data = {
        "AddressID": null, "FullAddress": "1111 11TH ST NW UNIT 104", "AddressNumber": "1111", "AddressNumberSufix"
    : "", "StreetName": "11TH", "StreetType": "ST", "Quadrant": "NW", "City": "Washington", "State": "DC", "Xcoord": ""
, "Ycoord": "", "Anc": "2F", "Ward": "2", "Cluster": "", "ZipCode": "20001", "Latitude": "", "Longitude": "", "Vote_Prcnct"
: "", "UnitType": "", "UnitNumber": "104", "Phone": "", "Email": "", "Zone": "DD/R-5-E", "SMD": "2F06", "SSL": "0341 2015", "Street": "1111 11TH ST NW UNIT 104", "Zip": "20001", "Country": "US", "Unit": "104", "IsDataChange"
: true, "IsUploadSupportDoc": true, "OccupancyAddssValidate": "InCorrect", "IsValid": true, "MasterId": "20926ebb-c617-4fa7-b68c-48cf447b1c3d"
, "Number": "asdas", "DateofIssue": "01/01/1900", "Type": "NOPO"
    }

    var submissionststus_data = {
        "Status": "Draft", "MasterId": "61c6dd67-b3ec-4ac3-af25-a8830a5ff0c8", "TradeName": "NA", "BusinessStructure"
    : "Corporation (For Profit)", "IsCorporationDivision": true, "IsCoporateRegistration": false, "IsResidentAgent"
    : false, "IsIndividual": false, "IsBusinessMustbeinDc": true, "AppType": "B", "IsFEIN": true, "DocSubmType": ""
, "PaymentId": null, "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "", "CurrentYear"
: "2016", "CreatedDate": "03/21/2016", "SelectedMailType": "", "PremisesAddress": "", "BusinessName": "", "IsCategorySelfCertification"
: false
    }

    var webservice_data = {
        "WebserviceList": [
            {
                "STNAME": "44TH",
                "STREET_TYPE": "PLACE",
                "QUADRANT": "SE",
                "CITY": "WASHINGTON",
                "STATE": "DC",
                "ZIPCODE": "20019",
                "FullAddress": "1100 44TH PLACE SE"
            },
            {
                "STNAME": "45TH",
                "STREET_TYPE": "PLACE",
                "QUADRANT": "SE",
                "CITY": "WASHINGTON",
                "STATE": "DC",
                "ZIPCODE": "20019",
                "FullAddress": "1100 45TH PLACE SE"
            },
            {
                "STNAME": "46TH",
                "STREET_TYPE": "PLACE",
                "QUADRANT": "SE",
                "CITY": "WASHINGTON",
                "STATE": "DC",
                "ZIPCODE": "20019",
                "FullAddress": "1100 46TH PLACE SE"
            },
            {
                "STNAME": "46TH",
                "STREET_TYPE": "STREET",
                "QUADRANT": "SE",
                "CITY": "WASHINGTON",
                "STATE": "DC",
                "ZIPCODE": "20019",
                "FullAddress": "1100 46TH STREET SE"
            }
        ], "Dropdownlist": ["", "Alley", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive"
    , "Driveway", "Expressway", "Freeway", "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade"
    , "Road", "Row", "Square", "Street", "Terrace", "Walk", "Way"], "STNAME": null
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


    describe('it should test when no guid available', function () {
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            nopoboxController = controller('NoPoBoxController', {
                $scope: scope, $rootScope: rootscope,
                $location: $location, mockservice: mockservice,
                appConstants: appConstants,
                routeParams: routeparams, utilityFac: utilityfactory,
                $timeout: timeout, SessionFactory: sessionfactory, $filter: filter,
                MAR_validation_service: marservice, errorFactory: errorfactory,
                BBLSubmissionFactory: bblsubmissionfactory, popupFactory: popupfactory,
                authService:authservice
            });
        });

        it('should test when no guid is avalable', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

    });


    describe('it should test when guid is available and status is underreview', function () {
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            httpBackend.when('GET', "partials/templates/popuptemplate.html").respond(200);
            nopoboxController = controller('NoPoBoxController', {
                $scope: scope, $rootScope: rootscope,
                $location: $location, mockservice: mockservice,
                appConstants: appConstants,
                routeParams: routeparams, utilityFac: utilityfactory,
                $timeout: timeout, SessionFactory: sessionfactory, $filter: filter,
                MAR_validation_service: marservice, errorFactory: errorfactory,
                BBLSubmissionFactory: bblsubmissionfactory, popupFactory: popupfactory,
                authService: authservice
            });
        });

        it('should test when no guid is avalable', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

    });

    describe('it should test when guid is available and status is draft', function () {
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "Draft" });
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubCofoHopdetl').respond(initialData);
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/StateListBasedOnCode').respond(statesdata);
            httpBackend.when('POST', basePath + 'api/BBLApplication/LocationVerifier').respond(webservice_data);
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCofoHop').respond(true);
            httpBackend.when('POST', basePath + 'api/BBLApplication/DeleteEhopAddress').respond(true);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            httpBackend.when('GET', "partials/templates/popuptemplate.html").respond(200);
            nopoboxController = controller('NoPoBoxController', {
                $scope: scope, $rootScope: rootscope,
                $location: $location, mockservice: mockservice,
                appConstants: appConstants,
                routeParams: routeparams, utilityFac: utilityfactory,
                $timeout: timeout, SessionFactory: sessionfactory, $filter: filter,
                MAR_validation_service: marservice, errorFactory: errorfactory,
                BBLSubmissionFactory: bblsubmissionfactory, popupFactory: popupfactory,
                authService: authservice
            });

            scope.vm = nopoboxController;
            var element = angular.element(
                   '<form name="vm.nopoboxForm">' +
                   '<input ng-model="vm.zip" name="zip" />' +
                   '<input ng-model="vm.telephone" name="telephone" />' +
                   '</form>'
                   );
            compile(element)(scope);
            form = scope.vm.nopoboxForm;

        });

        it('should test when status is draft', function () {
            httpBackend.flush();
            expect(form.zip.$valid).toBeTruthy();
            expect(form.telephone.$valid).toBeTruthy();
        });

        it('should test getSuggestions when length is less than 4', function () {
            nopoboxController.getSuggestions("123");
            expect(nopoboxController.Address.length).toEqual(0);
        });

        it('should test getSuggestions when length is less than 4', function () {
            nopoboxController.getSuggestions("12345");
            expect(nopoboxController.Address.length).toEqual(0);
        });

        it('should test getSuggestions when length is equal to 4', function () {
            nopoboxController.getSuggestions("1234");
            httpBackend.flush();
            expect(nopoboxController.Address.length).toBeGreaterThan(0);
        });

        it('should test startsWith', function () {
            expect(nopoboxController.startsWith("1234 washington", "1234")).toBeTruthy();
        });

        it('should test getrelatedAddress when data not changed', function () {
            var item = {}
            nopoboxController.getrelatedAddress(item);
            expect(nopoboxController.nopobox.IsDataChange).toBeFalsy();
        });

        it('should test getrelatedAddress when data changed', function () {
            var item = {};
            item.IsDataChange = '';
            nopoboxController.prevObj = {};
            nopoboxController.prevObj.Street = "test1";
            item.FullAddress = "test";
            nopoboxController.getrelatedAddress(item);
            expect(nopoboxController.nopobox.IsDataChange).toBeTruthy();
        });

        it("should test check and exit when nopobox object is defined and delete data", function () {
            form.$invalid = true;
            spyOn(sessionfactory, 'isFormEmpty').and.returnValue(true);
            nopoboxController.CheckAndExit('testpath');
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/testpath');
        });

        it("should test check and exit when nopobox object is defined partially", function () {
            form.$invalid = true;
            spyOn(sessionfactory, 'isFormEmpty').and.returnValue(false);
            nopoboxController.CheckAndExit('appchecklist');
            httpBackend.flush();
            expect(sessionfactory.isSessionDirty()).toBeTruthy();
        });

        it("should test check and exit when nopobox object is defined completely", function () {
            form.$invalid = false;
            spyOn(sessionfactory, 'isFormEmpty').and.returnValue(false);
            nopoboxController.CheckAndExit('appchecklist');
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        });

        it("should test next when nopobox object is defined completely", function () {
            form.$invalid = false;
            nopoboxController.navToCorpRegistrationFromPO();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/corpreg/guid');
        });

        it("should test stateFieldvalidation", function () {
            nopoboxController.nopobox = {
                City: "WASHINGTON",
                DateofIssue: "01/01/1900",
                FileNumber: "NOPO",
                MasterId: "3e2a4923-4aea-4d7f-83bd-1d13fc1856ba",
                Number: "NOPO",
                Quadrant: "NE",
                State: "DC",
                Street: "1101 16TH STREET NE",
                StreetName: "16TH",
                StreetType: "Street",
                Telephone: "1345",
                Type: "NOPO",
                Unit: "123",
                UnitType: "BLDG",
                Zip: "20002"
            }


            nopoboxController.stateFieldvalidation();
            httpBackend.flush();
            expect(nopoboxController.stateValidation).toBeTruthy();
        });

        it("should test stateFieldvalidation when state is not DC", function () {
            nopoboxController.nopobox = {
                City: "WASHINGTON",
                DateofIssue: "01/01/1900",
                FileNumber: "NOPO",
                MasterId: "3e2a4923-4aea-4d7f-83bd-1d13fc1856ba",
                Number: "NOPO",
                Quadrant: "NE",
                State: "Alaska",
                Street: "1101 16TH STREET NE",
                StreetName: "16TH",
                StreetType: "Street",
                Telephone: "1345",
                Type: "NOPO",
                Unit: "123",
                UnitType: "BLDG",
                Zip: "20002"
            }
            nopoboxController.stateFieldvalidation();
            httpBackend.flush();
            timeout.flush();
            expect(nopoboxController.stateValidation).toBeFalsy();
        });

        it('should test checkTelephoneMaxLength when country is US', function () {
            nopoboxController.validations_wrt_contry = errorfactory.isCountryUS(true);
            nopoboxController.nopobox = {};
            nopoboxController.nopobox.Telephone = "11";
            nopoboxController.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeFalsy();

            //telephone length is correct

            nopoboxController.nopobox.Telephone = "1111111111";
            nopoboxController.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();
        });

        it('should test checkTelephoneMaxLength when country is not US', function () {
            nopoboxController.validations_wrt_contry = errorfactory.isCountryUS(false);
            nopoboxController.nopobox = {};
            nopoboxController.nopobox.Telephone = "11";
            nopoboxController.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();

            //telephone length is correct

            nopoboxController.nopobox.Telephone = "1111111111";
            nopoboxController.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();
        });

        it('should test checkTelephoneMaxLength when country is not US and telephone is valid', function () {
            nopoboxController.validations_wrt_contry = errorfactory.isCountryUS(false);
            nopoboxController.nopobox = {};
            nopoboxController.nopobox.Telephone = "1111111111";
            nopoboxController.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();
        });

        it('should test checkZipMaxLength when country is US', function () {
            nopoboxController.validations_wrt_contry = errorfactory.isCountryUS(true);
            nopoboxController.nopobox = {};
            nopoboxController.nopobox.Zip = "11";
            nopoboxController.checkZipMaxLength();
            expect(form.zip.$valid).toBeFalsy();

        });

        it('should test checkZipMaxLength when country is US and zip length is correct', function () {
            nopoboxController.validations_wrt_contry = errorfactory.isCountryUS(true);
            nopoboxController.nopobox = {};
            nopoboxController.nopobox.Zip = "11111";
            nopoboxController.checkZipMaxLength();
            expect(form.zip.$valid).toBeTruthy();
        });

        it('should test countryChanged when country is US', function () {
            nopoboxController.nopobox = {};
            nopoboxController.nopobox.Country = "US";
            nopoboxController.countryChanged();
            expect(nopoboxController.showStateAsDropdown).toBeFalsy();
        });

        it('should test countryChanged when country is not in US', function () {
            nopoboxController.nopobox = {};
            nopoboxController.nopobox.Country = "SA";
            nopoboxController.countryChanged(true);
            expect(nopoboxController.showStateAsDropdown).toBeTruthy();
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is true ", function () {
            nopoboxController.submissionStatusData = submissionststus_data;
            nopoboxController.submissionStatusData.IsCorporationDivision = true;
            nopoboxController.navToRegisteredAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/corpreg/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName not equal to null", function () {
            nopoboxController.submissionStatusData = submissionststus_data;
            nopoboxController.submissionStatusData.IsCorporationDivision = false;
            nopoboxController.submissionStatusData.TradeName = 'asdf';
            nopoboxController.navToRegisteredAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpreqregwithtradesecond/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is SOLE PROPRIETORSHIP", function () {
            nopoboxController.submissionStatusData = submissionststus_data;
            nopoboxController.submissionStatusData.IsCorporationDivision = false;
            nopoboxController.submissionStatusData.TradeName = 'NA';
            nopoboxController.submissionStatusData.BusinessStructure = 'SOLE PROPRIETORSHIP';
            nopoboxController.navToRegisteredAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is GENERAL PARTNERSHIP", function () {
            nopoboxController.submissionStatusData = submissionststus_data;
            nopoboxController.submissionStatusData.IsCorporationDivision = false;
            nopoboxController.submissionStatusData.TradeName = 'NA';
            nopoboxController.submissionStatusData.BusinessStructure = 'GENERAL PARTNERSHIP';
            nopoboxController.navToRegisteredAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is JOINT VENTURE", function () {
            nopoboxController.submissionStatusData = submissionststus_data;
            nopoboxController.submissionStatusData.IsCorporationDivision = false;
            nopoboxController.submissionStatusData.TradeName = 'NA';
            nopoboxController.submissionStatusData.BusinessStructure = 'JOINT VENTURE';
            nopoboxController.navToRegisteredAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is not equal to JOINT VENTURE", function () {
            nopoboxController.submissionStatusData = submissionststus_data;
            nopoboxController.submissionStatusData.IsCorporationDivision = false;
            nopoboxController.submissionStatusData.TradeName = 'NA';
            nopoboxController.submissionStatusData.BusinessStructure = 'Corporation (For Profit)';
            nopoboxController.navToRegisteredAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpreqregisterfirst/guid');
        });

        it('should test setErrorMsg ', function () {
            nopoboxController.setErrorMsg();
        });

    });

});