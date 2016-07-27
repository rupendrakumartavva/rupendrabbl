describe('eHOP HomeAddress Controller Spec', function () {

    var $scope, controller, httpBackend, mockservice, ehophomeaddressController, localStore, compile, basePath, appconstants, routeparams, utilityfactory, filter, marvalidationservice, bblsubmissionfactory;
    var errorfactory, sessionfactory, popupfactory, authservice, localstorageservice;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $compile, $routeParams, authService, localStorageService, appConstants, UtilityFactory, $filter, MAR_validation_service, BBLSubmissionFactory, errorFactory, SessionFactory, popupFactory) {
        basePath = appConstants.apiServiceBaseUri;
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
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        utilityfactory = UtilityFactory;
        appconstants = appConstants;
        filter = $filter;
        bblsubmissionfactory = BBLSubmissionFactory;
        marvalidationservice = MAR_validation_service;
        compile = $compile;
        errorfactory = errorFactory;
        sessionfactory = SessionFactory,
        popupfactory = popupFactory;
        authservice = authService;
        localstorageservice = localStorageService;
    }));

    var dropdownData = ["Alley", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive", "Driveway", "Expressway", "Freeway",
                    "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade", "Road", "Row", "Square", "Street", "Terrace", "Walk", "Way"];
    var homeAddressData = {
        "StreetNumber": null, "AddressID": "240920", "AddressNumber": "1234", "AddressNumberSufix": "asf", "Anc": "ANC 2F",
        "Cluster": "Cluster 8", "Latitude": "38.90419769", "Longitude": "-77.02931331", "Vote_Prcnct": "Precinct 17", "Ward": "Ward 2",
        "Xcoord": "397457.40", "Ycoord": "137371.65", "UserType": "Y-CORPREG", "FileNumber": "C880040", "MasterId": "39d78d76-8ab7-4a4b-9843-18aabfb91508",
        "CBusinessName": null, "TradeName": null, "BusinessStructure": null, "FirstName": "", "MiddleName": "", "LastName": "", "BusinessName": "",
        "BusinessAddressLine1": "MASSACHUSETTS", "BusinessAddressLine2": "Avenue", "BusinessAddressLine3": "1234 MASSACHUSETTS AVENUE NW",
        "BusinessAddressLine4": null, "BusinessCity": "WASHINGTON", "BusinessState": "DC", "BusinessCountry": "", "ZipCode": "20005", "Email": "",
        "EntityStatus": null, "SubCorporationRegId": 0, "UserSelectTpe": "Primses Address", "Quardrant": "NW", "UnitType": "", "Unit": "123",
        "Telphone": "12345", "IsValid": false, "Dropdownlist": ["", "Alley", "Avenue", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive",
            "Driveway", "Expressway", "Freeway", "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade", "Road", "Row", "Square",
            "Street", "Terrace", "Walk", "Way"], "OccupancyAddssValidate": "", "DonothaveCof": false, "CorpStatus": null, "HQStatus": null
    }, submissionststus_data = {
        "Status": "Draft", "MasterId": "61c6dd67-b3ec-4ac3-af25-a8830a5ff0c8", "TradeName": "NA", "BusinessStructure"
    : "Corporation (For Profit)", "IsCorporationDivision": true, "IsCoporateRegistration": false, "IsResidentAgent"
    : false, "IsIndividual": false, "IsBusinessMustbeinDc": true, "AppType": "B", "IsFEIN": true, "DocSubmType": ""
, "PaymentId": null, "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "", "CurrentYear"
: "2016", "CreatedDate": "03/21/2016", "SelectedMailType": "", "PremisesAddress": "", "BusinessName": "", "IsCategorySelfCertification"
: false
    }, webservice_data = {
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


    describe('test cases when the accessed application is in guid is not available', function () {

        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            ehophomeaddressController = controller('eHOPHomeAddressController', {
                $scope: $scope, $rootScope: rootscope, $location: $location,
                mockservice: mockservice, appconstants: appconstants,
                routeParams: routeparams,
                utilityFactory: utilityfactory,
                $filter: filter,
                MAR_validationService: marvalidationservice, BBLSubmissionFactory: bblsubmissionfactory,
                errorFactory: errorfactory,
                SessionFactory: sessionfactory, popupFactory: popupfactory, authService: authservice
            });
        });

        it('should test init with no guid available', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('test submission status is in underreview', function () {

        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            ehophomeaddressController = controller('eHOPHomeAddressController', {
                $scope: $scope, $rootScope: rootscope, $location: $location,
                mockservice: mockservice, appconstants: appconstants,
                routeParams: routeparams,
                utilityFactory: utilityfactory,
                $filter: filter,
                MAR_validationService: marvalidationservice, BBLSubmissionFactory: bblsubmissionfactory,
                errorFactory: errorfactory,
                SessionFactory: sessionfactory, popupFactory: popupfactory, authService: authservice
            });
        });

        it('should test getCheckListData under review', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    //describe('test cases when the accessed application is in guid is available but business addressline 3 is undefined or null', function () {

    //    beforeEach(function () {
    //        var homeaddress_nodata = homeAddressData;
    //        homeaddress_nodata.BusinessAddressLine3 = null;
    //        spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
    //        spyOn(marvalidationservice, 'invalid_DC_address').and.returnValue({ "test": "test" });
    //        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
    //        httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond(submissionststus_data);
    //        httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
    //        httpBackend.when('POST', basePath + 'api/BBLApplication/PrimisessAddress').respond(homeaddress_nodata);
    //        httpBackend.when('POST', basePath + 'api/BBLApplication/DeleteEhopAddress').respond(true);
    //        httpBackend.when('POST', basePath + 'api/BBLApplication/LocationVerifier').respond(webservice_data);
    //        httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCofoHop').respond(true);

    //        ehophomeaddressController = controller('eHOPHomeAddressController', {
    //            $scope: $scope, $rootScope: rootscope, $location: $location,
    //            mockservice: mockservice, appconstants: appconstants,
    //            routeParams: routeparams,
    //            utilityFactory: utilityfactory,
    //            $filter: filter,
    //            MAR_validationService: marvalidationservice, BBLSubmissionFactory: bblsubmissionfactory,
    //            errorFactory: errorfactory,
    //            SessionFactory: sessionfactory, popupFactory: popupfactory
    //        });
    //    });

    //    it('should test init with no guid available', function () {
    //        httpBackend.flush();
    //        expect(ehophomeaddressController.ehopaddress.test).toBe("test");
    //    });
    //});



    describe('test submission status is in draft', function () {
        var form;
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond(submissionststus_data);
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/PrimisessAddress').respond(homeAddressData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/DeleteEhopAddress').respond(true);
            httpBackend.when('POST', basePath + 'api/BBLApplication/LocationVerifier').respond(webservice_data);
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCofoHop').respond(true);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            ehophomeaddressController = controller('eHOPHomeAddressController', {
                $scope: $scope, $rootScope: rootscope, $location: $location,
                mockservice: mockservice, appconstants: appconstants,
                routeParams: routeparams,
                utilityFactory: utilityfactory,
                $filter: filter,
                MAR_validationService: marvalidationservice, BBLSubmissionFactory: bblsubmissionfactory,
                errorFactory: errorfactory,
                SessionFactory: sessionfactory, popupFactory: popupfactory, authService: authservice
            });

            $scope.vm = ehophomeaddressController;
            var element = angular.element(
                   '<form name="vm.ehophomeadd_form">' +
                   '<input ng-model="vm.zip" name="zip" />' +
                   '<input ng-model="vm.telephone" name="telephone" />' +
                   '</form>'
                   );
            compile(element)($scope);
            form = $scope.vm.ehophomeadd_form;

        });


        it('should test init', function () {
            httpBackend.flush();
            expect(ehophomeaddressController.address.type).toBe("2");
        });

        it('should test navToCorpRegistrationFromEhop', function () {
            httpBackend.flush();
            ehophomeaddressController.ehopaddress = {};
            ehophomeaddressController.ehopAddressStaticdata = {};
            ehophomeaddressController.ehopAddressStaticdata.City = "testcity";
            ehophomeaddressController.ehopAddressStaticdata.State = "teststate";
            form.$invalid = false;
            ehophomeaddressController.navToCorpRegistrationFromEhop();
        });

        it('should test navToCorpRegistrationFromEhop', function () {
            form.$invalid = true;
            ehophomeaddressController.navToCorpRegistrationFromEhop();
        });

        it('should test checkAndExit and form is empty', function () {
            ehophomeaddressController.ehopaddress = {};
            form.$invalid = false;
            ehophomeaddressController.checkAndExit('appchecklist');
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        });

        it('should test checkAndExit and not_valid_Address is false and form is invalid', function () {
            ehophomeaddressController.not_valid_Address = false;
            ehophomeaddressController.ehopaddress = { test: "test" };
            form.$invalid = true;
            httpBackend.when("partials/templates/popuptemplate.html").respond(200);
            ehophomeaddressController.checkAndExit('mybbl');
            expect(sessionfactory.isSessionDirty()).toBeTruthy();

        });

        it('should test checkAndExit and not_valid_Address is false and form is valid', function () {
            ehophomeaddressController.not_valid_Address = false;
            ehophomeaddressController.ehopaddress = { test: "test" };
            form.$invalid = false;
            ehophomeaddressController.checkAndExit('mybbl');
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test checkAndExit form is invalid and empty', function () {
            ehophomeaddressController.not_valid_Address = false;
            ehophomeaddressController.ehopaddress = { test: "test" };
            form.$invalid = true;
            spyOn(sessionfactory, 'isFormEmpty').and.returnValue(true);
            ehophomeaddressController.checkAndExit('mybbl');
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test getrelatedAddress', function () {
            var item = {}
            ehophomeaddressController.getrelatedAddress(item);
            expect(ehophomeaddressController.fieldsDisable).toBeTruthy();
        });

        it('should test getSuggestions when length is less than 4', function () {
            ehophomeaddressController.getSuggestions("123");
            expect(ehophomeaddressController.Address.length).toEqual(0);
        });

        it('should test getSuggestions when length is greater than 4', function () {
            ehophomeaddressController.ehopaddress = {};
            ehophomeaddressController.getSuggestions("12345");
            expect(ehophomeaddressController.Address.length).toEqual(0);
        });

        it('should test getSuggestions when length is equal to 4', function () {
            ehophomeaddressController.getSuggestions("1234");
            httpBackend.flush();
            expect(ehophomeaddressController.Address.length).toBeGreaterThan(0);
        });

        it('should test startsWith', function () {
            expect(ehophomeaddressController.startsWith("1234 washington", "1234")).toBeTruthy();
        });

        it('should test setErrorMsg', function () {
            ehophomeaddressController.setErrorMsg();
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is true ", function () {
            ehophomeaddressController.submissionStatusData = submissionststus_data;
            ehophomeaddressController.submissionStatusData.IsCorporationDivision = true;
            ehophomeaddressController.navToRegisteredAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/corpreg/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName not equal to null", function () {
            ehophomeaddressController.submissionStatusData = submissionststus_data;
            ehophomeaddressController.submissionStatusData.IsCorporationDivision = false;
            ehophomeaddressController.submissionStatusData.TradeName = 'asdf';
            ehophomeaddressController.navToRegisteredAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpreqregwithtradesecond/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is SOLE PROPRIETORSHIP", function () {
            ehophomeaddressController.submissionStatusData = submissionststus_data;
            ehophomeaddressController.submissionStatusData.IsCorporationDivision = false;
            ehophomeaddressController.submissionStatusData.TradeName = 'NA';
            ehophomeaddressController.submissionStatusData.BusinessStructure = 'SOLE PROPRIETORSHIP';
            ehophomeaddressController.navToRegisteredAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is GENERAL PARTNERSHIP", function () {
            ehophomeaddressController.submissionStatusData = submissionststus_data;
            ehophomeaddressController.submissionStatusData.IsCorporationDivision = false;
            ehophomeaddressController.submissionStatusData.TradeName = 'NA';
            ehophomeaddressController.submissionStatusData.BusinessStructure = 'GENERAL PARTNERSHIP';
            ehophomeaddressController.navToRegisteredAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is JOINT VENTURE", function () {
            ehophomeaddressController.submissionStatusData = submissionststus_data;
            ehophomeaddressController.submissionStatusData.IsCorporationDivision = false;
            ehophomeaddressController.submissionStatusData.TradeName = 'NA';
            ehophomeaddressController.submissionStatusData.BusinessStructure = 'JOINT VENTURE';
            ehophomeaddressController.navToRegisteredAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is not equal to JOINT VENTURE", function () {
            ehophomeaddressController.submissionStatusData = submissionststus_data;
            ehophomeaddressController.submissionStatusData.IsCorporationDivision = false;
            ehophomeaddressController.submissionStatusData.TradeName = 'NA';
            ehophomeaddressController.submissionStatusData.BusinessStructure = 'Corporation (For Profit)';
            ehophomeaddressController.navToRegisteredAgent();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpreqregisterfirst/guid');
        });

    });
});