describe('PhysicalLocationHop Controller Spec', function () {


    var scope, controller, httpBackend, mockservice, phylocHopController, localStore, basePath, form, timeout, routeparams, utilityfac, appconstants, filter, marservice, errorfactory;
    var sessionfactory, popupfactory, bblsubmissionfactory, authservice, localstorageservice;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $routeParams, appConstants, $compile, $timeout,
        $routeParams, UtilityFactory, $filter, MAR_validation_service, errorFactory, SessionFactory, popupFactory, BBLSubmissionFactory, authService, localStorageService) {
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
        basePath = appConstants.apiServiceBaseUri;
        timeout = $timeout;
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        utilityfac = UtilityFactory;
        filter = $filter;
        marservice = MAR_validation_service;
        errorfactory = errorFactory;
        authservice = authService;
        localstorageservice = localStorageService;
        bblsubmissionfactory = BBLSubmissionFactory;
        sessionfactory = SessionFactory;
        popupfactory = popupFactory;
    }));

    var submit_data = {
        "AddressID": null, "FullAddress": "1111 11TH ST NW UNIT 104", "AddressNumber": "1111", "AddressNumberSufix"
   : "", "StreetName": "11TH", "StreetType": "ST", "Quadrant": "NW", "City": "Washington", "State": "DC", "Xcoord": ""
, "Ycoord": "", "Anc": "2F", "Ward": "2", "Cluster": "", "ZipCode": "20001", "Latitude": "", "Longitude": "", "Vote_Prcnct"
: "", "UnitType": "", "UnitNumber": "104", "Phone": "", "Email": "", "Zone": "DD/R-5-E", "SMD": "2F06", "SSL": "0341 2015", "Street": "1111 11TH ST NW UNIT 104", "Zip": "20001", "Country": "US", "Unit": "104", "IsDataChange"
: true, "IsUploadSupportDoc": true, "OccupancyAddssValidate": "InCorrect", "IsValid": true, "MasterId": "20926ebb-c617-4fa7-b68c-48cf447b1c3d"
, "Number": "asdas", "DateofIssue": "01/01/1900", "Type": "hop"
    }

    var submit_wrong_data = {
        "AddressID": null, "FullAddress": "", "AddressNumber": "", "AddressNumberSufix"
    : "", "StreetName": "", "StreetType": "", "Quadrant": "", "City": "", "State": "", "Xcoord": ""
, "Ycoord": "", "Anc": "", "Ward": "", "Cluster": "", "ZipCode": "", "Latitude": "", "Longitude": "", "Vote_Prcnct"
: "", "UnitType": "", "UnitNumber": "", "Phone": "", "Email": "", "Zone": "", "SMD": "", "SSL": "", "Street": "", "Zip": "", "Country": "", "Unit": "", "IsDataChange"
: false, "IsUploadSupportDoc": true, "OccupancyAddssValidate": "InCorrect", "IsValid": false, "MasterId": "20926ebb-c617-4fa7-b68c-48cf447b1c3d"
, "Number": "", "DateofIssue": "01/01/1900", "Type": "hop"
    }

    var submissionststus_data = {
        "Status": "Draft", "MasterId": "61c6dd67-b3ec-4ac3-af25-a8830a5ff0c8", "TradeName": "NA", "BusinessStructure"
    : "Corporation (For Profit)", "IsCorporationDivision": true, "IsCoporateRegistration": false, "IsResidentAgent"
    : false, "IsIndividual": false, "IsBusinessMustbeinDc": true, "AppType": "B", "IsFEIN": true, "DocSubmType": ""
, "PaymentId": null, "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "", "CurrentYear"
: "2016", "CreatedDate": "03/21/2016", "SelectedMailType": "", "PremisesAddress": "", "BusinessName": "", "IsCategorySelfCertification"
: false
    }

    var initialData = {
        "WebserviceList": [], "Dropdownlist": ["", "Alley", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive"
        , "Driveway", "Expressway", "Freeway", "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade"
        , "Road", "Row", "Square", "Street", "Terrace", "Walk", "Way"], "STNAME": null
    };
    var dropdownData = ["Alley", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive", "Driveway", "Expressway", "Freeway",
                    "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade", "Road", "Row", "Square", "Street", "Terrace", "Walk", "Way"];

    var nodata_result = [{
        "MasterId": "30c7aff5-5bb9-46a9-8c2e-5d9b48fe666f", "CofoHopId": 0, "Number": "sdafsa", "Type": "hop", "DateofIssue": "11/05/2015", "Street": null, "StreetName": null, "StreetTypeId": null, "StreetType": null, "Quadrant": null, "UnitType"
        : null, "Unit": null, "City": null, "State": null, "Zip": null, "Telephone": null, "DonothaveCof": false, "IsUploadSupportDoc"
        : false, "IsValid": false, "IseHOPEligibility": false, "EHopEligibilityType": null, "ConfirmeHOPEligibilityType"
        : false, "Name": null, "Status": "NODATA", "Dropdownlist": null, "OccupancyAddssValidate": null, "Country": null
    }];

    var valid_search = [{
        "MasterId": "c1077064-4322-4292-8dbf-faaad896ec56", "CofoHopId": 1, "Number": "CO0800131", "Type": "hop"
            , "DateofIssue": "09-25-2015", "Street": "", "StreetName": "13TH", "StreetTypeId": 22, "StreetType": "Street", "Quadrant"
            : "NW", "UnitType": "", "Unit": "", "City": "Washington", "State": "DC", "Zip": "", "Telephone": "", "DonothaveCof"
            : false, "IsUploadSupportDoc": false, "IsValid": false, "IseHOPEligibility": false, "EHopEligibilityType": null
            , "ConfirmeHOPEligibilityType": false, "Name": null, "Status": null, "Dropdownlist": null, "OccupancyAddssValidate"
            : "", "Country": null
    }];

    var initial_edit_data = {
        "WebserviceList": [{
            "MasterId": "61c6dd67-b3ec-4ac3-af25-a8830a5ff0c8", "CofoHopId": 0, "Number": "asdsad"
    , "Type": "HOP", "DateofIssue": "01/01/1900", "Street": "1111 11TH ST NW UNIT 104", "StreetName": "11TH", "StreetTypeId"
    : null, "StreetType": "ST", "Quadrant": "NW", "UnitType": "", "Unit": "104", "City": "Washington", "State": "DC", "Zip"
    : "20001", "Telephone": "", "DonothaveCof": false, "IsUploadSupportDoc": false, "IsValid": true, "IseHOPEligibility"
    : false, "EHopEligibilityType": null, "ConfirmeHOPEligibilityType": false, "Name": null, "Status": null, "Dropdownlist"
    : null, "OccupancyAddssValidate": "InCorrect", "Country": "US", "AddressId": null, "AddressNumber": "1111", "AddressNumberSufix"
    : "", "Xcoord": "", "Ycoord": "", "Anc": "2F", "Ward": "2", "Cluster": "", "Latitude": "", "Longitude": "", "Vote_Prcnct"
    : "", "TradeName": "NA", "BusinessStructure": "Corporation (For Profit)", "Zone": "DD/R-5-E", "SMD": "2F06", "SSL"
    : "0341    2015", "UnitNumber": null, "IsQuadrant": false, "IsStreetName": false, "IsStreetType": false, "IsStreetNumber"
    : false, "IsCofoHop": false, "IsDataChange": false, "IsSelfCertificationChange": false
        }], "Dropdownlist": [{
            "StreetType"
            : "Alley", "StreetCode": "AL"
        }, { "StreetType": "Avenue", "StreetCode": "AVE" }, {
            "StreetType": "Boulevard", "StreetCode"
            : "BLVD"
        }, { "StreetType": "Bridge", "StreetCode": "BRG" }, { "StreetType": "Circle", "StreetCode": "CIR" }, {
            "StreetType"
            : "Court", "StreetCode": "CT"
        }, { "StreetType": "Crescent", "StreetCode": "CRES" }, {
            "StreetType": "Drive", "StreetCode"
            : "DR"
        }, { "StreetType": "Driveway", "StreetCode": "DRWY" }, { "StreetType": "Expressway", "StreetCode": "EXPY" }
        , { "StreetType": "Freeway", "StreetCode": "FWY" }, { "StreetType": "Gardens", "StreetCode": "GDNS" }, {
            "StreetType"
            : "Green", "StreetCode": "GRN"
        }, { "StreetType": "Kys", "StreetCode": "KYS" }, {
            "StreetType": "Lane", "StreetCode"
            : "LN"
        }, { "StreetType": "Mews", "StreetCode": "MEWS" }, { "StreetType": "Parkway", "StreetCode": "PKWY" }, {
            "StreetType"
            : "Place", "StreetCode": "PL"
        }, { "StreetType": "Plaza", "StreetCode": "PLZ" }, {
            "StreetType": "Promenade", "StreetCode"
            : "PROM"
        }, { "StreetType": "Road", "StreetCode": "RD" }, { "StreetType": "Row", "StreetCode": "ROW" }, {
            "StreetType"
            : "Square", "StreetCode": "SQ"
        }, { "StreetType": "Street", "StreetCode": "ST" }, {
            "StreetType": "Terrace", "StreetCode"
            : "TER"
        }, { "StreetType": "Walk", "StreetCode": "WALK" }, { "StreetType": "Way", "StreetCode": "WAY" }], "STNAME": null
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
    }

    var cofohopdetails = [{
        "MasterId": "20926ebb-c617-4fa7-b68c-48cf447b1c3d", "CofoHopId": 0, "Number": "asdas", "Type": "cofo", "DateofIssue"
    : "01/01/1900", "Street": null, "StreetName": null, "StreetTypeId": null, "StreetType": null, "Quadrant": null, "UnitType"
    : null, "Unit": null, "City": null, "State": null, "Zip": null, "Telephone": null, "DonothaveCof": false, "IsUploadSupportDoc"
    : false, "IsValid": false, "IseHOPEligibility": false, "EHopEligibilityType": null, "ConfirmeHOPEligibilityType"
    : false, "Name": null, "Status": "NODATA", "Dropdownlist": null, "OccupancyAddssValidate": null, "Country": null
, "AddressId": null, "AddressNumber": null, "AddressNumberSufix": null, "Xcoord": null, "Ycoord": null, "Anc": null
, "Ward": null, "Cluster": null, "Latitude": null, "Longitude": null, "Vote_Prcnct": null, "TradeName": null, "BusinessStructure"
: null, "Zone": null, "SMD": null, "SSL": null, "UnitNumber": null, "IsQuadrant": false, "IsStreetName": false, "IsStreetType"
: false, "IsStreetNumber": false, "IsCofoHop": false, "IsDataChange": false, "IsSelfCertificationChange": false
    }];
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


    describe('should test when application status is underreview', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            phylocHopController = controller('PhysicalLocationHopController', {
                $rootScope: rootscope, $scope: scope,
                $location: $location, mockservice: mockservice,
                appConstants: appconstants, timeOut: timeout,
                routeParams: routeparams, utilityfactory: utilityfac,
                $filter: filter, MAR_validationService: marservice,
                errorFactory: errorfactory, SessionFactory: sessionfactory,
                popupFactory: popupfactory, BBLSubmissionFactory: bblsubmissionfactory,
                authService: authservice
            });
        });
        it('should test init', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('should test init method', function () {

        beforeEach(function () {

            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "draft" });
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubCofoHopdetl').respond(initial_edit_data);
            httpBackend.when('POST', basePath + 'api/BBLApplication/CofoHopDetails').respond(cofohopdetails);
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/LocationVerifier').respond(webservice_data);
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCofoHop').respond(true);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            phylocHopController = controller('PhysicalLocationHopController', {
                $rootScope: rootscope, $scope: scope,
                $location: $location, mockservice: mockservice,
                appConstants: appconstants, timeOut: timeout,
                routeParams: routeparams, utilityfactory: utilityfac,
                $filter: filter, MAR_validationService: marservice,
                errorFactory: errorfactory, SessionFactory: sessionfactory,
                popupFactory: popupfactory, BBLSubmissionFactory: bblsubmissionfactory,
                authService: authservice
            });
        });

        it("should test init method when DonothaveCof is false", function () {
            //httpBackend.flush();
            phylocHopController.getPreviousData = initial_edit_data;
            phylocHopController.hop.searchDetails = cofohopdetails;
            phylocHopController.getPreviousData.WebserviceList[0].DonothaveCof = false;
            phylocHopController.hop.searchDetails.Status = 'NODATA';
        });


    });

    describe('should test when application status is draft', function () {

        beforeEach(function () {

            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "DRAFT" });
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubCofoHopdetl').respond(initialData);
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/LocationVerifier').respond(webservice_data);
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCofoHop').respond(true);
            phylocHopController = controller('PhysicalLocationHopController', {
                $rootScope: rootscope, $scope: scope,
                $location: $location, mockservice: mockservice,
                appConstants: appconstants, timeOut: timeout,
                routeParams: routeparams, utilityfactory: utilityfac,
                $filter: filter, MAR_validationService: marservice,
                errorFactory: errorfactory, SessionFactory: sessionfactory,
                popupFactory: popupfactory, BBLSubmissionFactory: bblsubmissionfactory,
                authService: authservice
            });
        });
        it("should test check and exit when hop object is defined", function () {
            phylocHopController.hop = {}
            phylocHopController.hop.number = "1234567";
            phylocHopController.hop.date = "12/34/1567";
            phylocHopController.hop.searchDetails = valid_search;
            phylocHopController.correctSearch = "sdaf"
            phylocHopController.corpnorregadd_form = {};
            phylocHopController.corpnorregadd_form.$invalid = false;
            spyOn(phylocHopController, 'navToRegisteredAgent');
            phylocHopController.checkAndExit('mypath');
            httpBackend.when('POST', basePath + 'api/BBLApplication/CofoHopDetails').respond(nodata_result);
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCofoHop').respond(true);
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mypath');

        });

        it("should test search Zoning validation when data Entered Wrong", function () {
            var data = { Number: 'adfasd', DateofIssue: "25/12/2015" };
            phylocHopController.contact_us = {};
            phylocHopController.contact_us.$invalid = false;
            phylocHopController.searchZoning();
            httpBackend.when('POST', basePath + 'api/BBLApplication/CofoHopDetails').respond(nodata_result);
            httpBackend.flush();
            expect(phylocHopController.hop.searchDetails.Status).toBe("NODATA");
            expect(phylocHopController.searchmatch).toBe(false);
        });

        it("should test when search is valid", function () {

            phylocHopController.contact_us = {};
            phylocHopController.contact_us.$invalid = false;
            phylocHopController.searchZoning();
            httpBackend.when('POST', basePath + 'api/BBLApplication/CofoHopDetails').respond(valid_search);
            httpBackend.flush();
            expect(phylocHopController.hop.searchDetails.Status).toBe(null);
            expect(phylocHopController.searchmatch).toBe(true);
        });

        it("should test toggle radio method", function () {
            phylocHopController.toggleRadio('true');
            expect(phylocHopController.correctSearch).toBe(true);
            phylocHopController.toggleRadio('false');
            expect(phylocHopController.correctSearch).toBe(false);
        });

        it("should test toggleCheckbox method", function () {
            phylocHopController.toggleCheckbox('testid');
            //spyOn(angular, 'element').andCallFake(ngElementFake);
        });


        it("should test nav to determine ehop eligibility", function () {
            phylocHopController.navToDetermineeHOPEligibility();
            expect($location.path).toHaveBeenCalledWith('/ehopeligibility/guid');
        });

        it('should test getrelatedAddress', function () {
            var item = {}
            phylocHopController.getrelatedAddress(item);
            expect(phylocHopController.fieldsDisable).toBeFalsy();
        });

        it('should test getSuggestions when length is less than 4', function () {
            phylocHopController.getSuggestions("123");
            expect(phylocHopController.Address.length).toEqual(0);
        });

        it('should test getSuggestions when length is less than 4', function () {
            phylocHopController.hopinfo = {};
            phylocHopController.getSuggestions("12345");
            expect(phylocHopController.Address.length).toEqual(0);
        });

        it('should test getSuggestions when length is equal to 4', function () {
            phylocHopController.getSuggestions("1234");
            httpBackend.flush();
            expect(phylocHopController.Address.length).toBeGreaterThan(0);
        });

        it('should test startsWith', function () {
            expect(phylocHopController.startsWith("1234 washington", "1234")).toBeTruthy();
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is true ", function () {
            phylocHopController.submissionStatusData = submissionststus_data;
            phylocHopController.submissionStatusData.IsCorporationDivision = true;
            //httpBackend.flush();
            phylocHopController.navToRegisteredAgent();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/corpreg/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName not equal to null", function () {
            phylocHopController.submissionStatusData = submissionststus_data;
            phylocHopController.submissionStatusData.IsCorporationDivision = false;
            phylocHopController.submissionStatusData.TradeName = 'asdf';
            phylocHopController.navToRegisteredAgent();
            //httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpreqregwithtradesecond/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is SOLE PROPRIETORSHIP", function () {
            phylocHopController.submissionStatusData = submissionststus_data;
            phylocHopController.submissionStatusData.IsCorporationDivision = false;
            phylocHopController.submissionStatusData.TradeName = 'NA';
            phylocHopController.submissionStatusData.BusinessStructure = 'SOLE PROPRIETORSHIP';
            phylocHopController.navToRegisteredAgent();
            //httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is GENERAL PARTNERSHIP", function () {
            phylocHopController.submissionStatusData = submissionststus_data;
            phylocHopController.submissionStatusData.IsCorporationDivision = false;
            phylocHopController.submissionStatusData.TradeName = 'NA';
            phylocHopController.submissionStatusData.BusinessStructure = 'GENERAL PARTNERSHIP';
            phylocHopController.navToRegisteredAgent();
            //httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is JOINT VENTURE", function () {
            phylocHopController.submissionStatusData = submissionststus_data;
            phylocHopController.submissionStatusData.IsCorporationDivision = false;
            phylocHopController.submissionStatusData.TradeName = 'NA';
            phylocHopController.submissionStatusData.BusinessStructure = 'JOINT VENTURE';
            phylocHopController.navToRegisteredAgent();
            //httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is not equal to JOINT VENTURE", function () {
            phylocHopController.submissionStatusData = submissionststus_data;
            phylocHopController.submissionStatusData.IsCorporationDivision = false;
            phylocHopController.submissionStatusData.TradeName = 'NA';
            phylocHopController.submissionStatusData.BusinessStructure = 'Corporation (For Profit)';
            phylocHopController.navToRegisteredAgent();
            //httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/corpreqregisterfirst/guid');
        });

        it("should test checkAndExit when not_valid_Address is true", function () {
            phylocHopController.hop = submit_data;
            phylocHopController.not_valid_Address = true;
            phylocHopController.checkAndExit('path');
            //httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/path');
        });

        it("should test checkAndExit when not_valid_Address is false", function () {
            phylocHopController.hop = submit_wrong_data;
            phylocHopController.searchZoningClicked = false;
            phylocHopController.correctSearch = false;
            phylocHopController.not_valid_Address = false;
            phylocHopController.corpnorregadd_form = {};
            phylocHopController.corpnorregadd_form.$invalid = true;
            phylocHopController.checkAndExit('path');
            //httpBackend.flush();
        });

        it("should test navToNextStepfromcofo when form invalid", function () {
            phylocHopController.hop = submit_data;
            phylocHopController.contact_us = {};
            phylocHopController.contact_us.$invalid = true;
            phylocHopController.navToNextStepfromhop('path');
            //httpBackend.flush();
        });

        it("should test navToNextStepfromcofo when form valid", function () {
            phylocHopController.hopinfo = submit_data;
            phylocHopController.contact_us = {};
            phylocHopController.contact_us.$invalid = false;
            phylocHopController.corpnorregadd_form = {};
            phylocHopController.corpnorregadd_form.$invalid = false;
            phylocHopController.correctSearch = false;
            phylocHopController.navToNextStepfromhop('path');
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/path');
        });

        it("should test incorrectSearch ", function () {
            phylocHopController.incorrectCount = 0;
            phylocHopController.incorrectSearch();
            timeout.flush();
        });

        it('should test hopErrorMsg', function () {
            phylocHopController.hopErrorMsg();
        });

        it("should test norelatedMatch method", function () {
            phylocHopController.norelatedMatch();
        });

        //it("should test navigateAnyway when noCofo is false", function () {
        //    phylocHopController.navigationPath = 'path'
        //    phylocHopController.navigateAnyway();
        //    httpBackend.flush();
        //    expect($location.path).toHaveBeenCalledWith('/path');
        //});

        //it("should test dontNavigate", function () {
        //    phylocHopController.dontNavigate();
        //    httpBackend.flush();
        //});

    });
});