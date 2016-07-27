describe('PhysicalLocationCofo Controller Spec', function () {


    var scope, controller, httpBackend, mockservice, phylocCofoController, localStore, basePath, form, timeout, routeParams, utilityfac;
    var sessionfactory, popupfactory, bblsubmissionfactory, authservice, localstorageservice;
    var submissionststus_data = {
        "Status": "Draft", "MasterId": "61c6dd67-b3ec-4ac3-af25-a8830a5ff0c8", "TradeName": "NA", "BusinessStructure"
    : "Corporation (For Profit)", "IsCorporationDivision": true, "IsCoporateRegistration": false, "IsResidentAgent"
    : false, "IsIndividual": false, "IsBusinessMustbeinDc": true, "AppType": "B", "IsFEIN": true, "DocSubmType": ""
, "PaymentId": null, "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "", "CurrentYear"
: "2016", "CreatedDate": "03/21/2016", "SelectedMailType": "", "PremisesAddress": "", "BusinessName": "", "IsCategorySelfCertification"
: false
    }

    var initial_edit_data = {
        "WebserviceList": [{
            "MasterId": "61c6dd67-b3ec-4ac3-af25-a8830a5ff0c8", "CofoHopId": 0, "Number": "asdsad"
    , "Type": "cofo", "DateofIssue": "01/01/1900", "Street": "1111 11TH ST NW UNIT 104", "StreetName": "11TH", "StreetTypeId"
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

    var initial_edit_data_IsValid = {
        "WebserviceList": [{
            "MasterId": "61c6dd67-b3ec-4ac3-af25-a8830a5ff0c8", "CofoHopId": 0, "Number": "asdsad"
    , "Type": "cofo", "DateofIssue": "01/01/1900", "Street": "1111 11TH ST NW UNIT 104", "StreetName": "11TH", "StreetTypeId"
    : null, "StreetType": "ST", "Quadrant": "NW", "UnitType": "", "Unit": "104", "City": "Washington", "State": "DC", "Zip"
    : "20001", "Telephone": "", "DonothaveCof": false, "IsUploadSupportDoc": false, "IsValid": false, "IseHOPEligibility"
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

    var initialData = {
        "WebserviceList": [], "Dropdownlist": ["", "Alley", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive"
        , "Driveway", "Expressway", "Freeway", "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade"
        , "Road", "Row", "Square", "Street", "Terrace", "Walk", "Way"], "STNAME": null
    };
    var dropdownData = ["Alley", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive", "Driveway", "Expressway", "Freeway",
                    "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade", "Road", "Row", "Square", "Street", "Terrace", "Walk", "Way"];

    var nodata_result = [{
        "MasterId": "30c7aff5-5bb9-46a9-8c2e-5d9b48fe666f", "CofoHopId": 0, "Number": "sdafsa", "Type": "cofo", "DateofIssue": "11/05/2015", "Street": null, "StreetName": null, "StreetTypeId": null, "StreetType": null, "Quadrant": null, "UnitType"
        : null, "Unit": null, "City": null, "State": null, "Zip": null, "Telephone": null, "DonothaveCof": false, "IsUploadSupportDoc"
        : false, "IsValid": false, "IseHOPEligibility": false, "EHopEligibilityType": null, "ConfirmeHOPEligibilityType"
        : false, "Name": null, "Status": "NODATA", "Dropdownlist": null, "OccupancyAddssValidate": null, "Country": null
    }];

    var valid_search = [{
        "MasterId": "c1077064-4322-4292-8dbf-faaad896ec56", "CofoHopId": 1, "Number": "CO0800131", "Type": "cofo"
            , "DateofIssue": "09-25-2015", "Street": "", "StreetName": "13TH", "StreetTypeId": 22, "StreetType": "Street", "Quadrant"
            : "NW", "UnitType": "", "Unit": "", "City": "Washington", "State": "DC", "Zip": "", "Telephone": "", "DonothaveCof"
            : false, "IsUploadSupportDoc": false, "IsValid": false, "IseHOPEligibility": false, "EHopEligibilityType": null
            , "ConfirmeHOPEligibilityType": false, "Name": null, "Status": null, "Dropdownlist": null, "OccupancyAddssValidate"
            : "", "Country": null
    }];

    var submit_data = {
        "AddressID": null, "FullAddress": "1111 11TH ST NW UNIT 104", "AddressNumber": "1111", "AddressNumberSufix"
    : "", "StreetName": "11TH", "StreetType": "ST", "Quadrant": "NW", "City": "Washington", "State": "DC", "Xcoord": ""
, "Ycoord": "", "Anc": "2F", "Ward": "2", "Cluster": "", "ZipCode": "20001", "Latitude": "", "Longitude": "", "Vote_Prcnct"
: "", "UnitType": "", "UnitNumber": "104", "Phone": "", "Email": "", "Zone": "DD/R-5-E", "SMD": "2F06", "SSL": "0341 2015", "Street": "1111 11TH ST NW UNIT 104", "Zip": "20001", "Country": "US", "Unit": "104", "IsDataChange"
: true, "IsUploadSupportDoc": true, "OccupancyAddssValidate": "InCorrect", "IsValid": true, "MasterId": "20926ebb-c617-4fa7-b68c-48cf447b1c3d"
, "Number": "asdas", "DateofIssue": "01/01/1900", "Type": "cofo"
    }

    var submit_wrong_data = {
        "AddressID": null, "FullAddress": "", "AddressNumber": "", "AddressNumberSufix"
    : "", "StreetName": "", "StreetType": "", "Quadrant": "", "City": "", "State": "", "Xcoord": ""
, "Ycoord": "", "Anc": "", "Ward": "", "Cluster": "", "ZipCode": "", "Latitude": "", "Longitude": "", "Vote_Prcnct"
: "", "UnitType": "", "UnitNumber": "", "Phone": "", "Email": "", "Zone": "", "SMD": "", "SSL": "", "Street": "", "Zip": "", "Country": "", "Unit": "", "IsDataChange"
: false, "IsUploadSupportDoc": true, "OccupancyAddssValidate": "InCorrect", "IsValid": false, "MasterId": "20926ebb-c617-4fa7-b68c-48cf447b1c3d"
, "Number": "", "DateofIssue": "01/01/1900", "Type": "cofo"
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
    }, filter, marservice, appconstants;

    var removecofo_data = { "MasterId": "61c6dd67-b3ec-4ac3-af25-a8830a5ff0c8", "Type": "COFO", "DonothaveCof": true, "Number": "" }

    var empty_data = {
        "date": "01/01/1900", "MasterId": "10e195fc-6df0-4dcb-82f2-52aa0e2cb5a3", "number": "", "DateofIssue": "", "Type"
    : "cofo", "IsDataChange": true
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


    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, $timeout, $routeParams, UtilityFactory,
        $filter, MAR_validation_service, errorFactory, SessionFactory, popupFactory, BBLSubmissionFactory, authService, localStorageService) {
        rootscope = $rootScope.$new();
        scope = $rootScope.$new();
        $location = _$location_;
        basePath = appConstants.apiServiceBaseUri;
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
        timeout = $timeout;
        routeParams = $routeParams;
        routeParams.guid = 'guid';
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

    describe('test submission status is in underreview', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ Status: "underreview" });
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubCofoHopdetl').respond(initialData);
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('GET', "partials/templates/popuptemplate.html").respond(200);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            phylocCofoController = controller('PhysicalLocationCofoController',
            {
                $rootScope: rootscope, $scope: scope,
                $location: $location, mockservice: mockservice,
                appConstants: appconstants, timeOut: timeout,
                $routeParams: routeParams, utilityfactory: utilityfac,
                $filter: filter, MAR_validationService: marservice,
                errorfactory: errorfactory, SessionFactory: sessionfactory,
                popupFactory: popupfactory, BBLSubmissionFactory: bblsubmissionfactory, authService: authservice
            });
        });

        it('should test getCheckListData under review', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('should test init method', function () {

        beforeEach(function () {
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "draft" });
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubCofoHopdetl').respond(initial_edit_data);
            httpBackend.when('POST', basePath + 'api/BBLApplication/CofoHopDetails').respond(cofohopdetails);
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/LocationVerifier').respond(webservice_data);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/RemoveCofo').respond(removecofo_data);
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCofoHop').respond(true);
            httpBackend.when('GET', "partials/templates/popuptemplate.html").respond(200);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            phylocCofoController = controller('PhysicalLocationCofoController',
           {
               $rootScope: rootscope, $scope: scope,
               $location: $location, mockservice: mockservice,
               appConstants: appconstants, timeOut: timeout,
               $routeParams: routeParams, utilityfactory: utilityfac,
               $filter: filter, MAR_validationService: marservice,
               errorfactory: errorfactory, SessionFactory: sessionfactory,
               popupFactory: popupfactory, BBLSubmissionFactory: bblsubmissionfactory, authService: authservice
           });
        });

        it("should test init method when DonothaveCof is false", function () {
            phylocCofoController.getPreviousData = initial_edit_data;
            phylocCofoController.cofo.searchDetails = cofohopdetails;
            phylocCofoController.getPreviousData.WebserviceList[0].DonothaveCof = false;
            phylocCofoController.cofo.searchDetails.Status = 'NODATA';
            httpBackend.flush();
        });

    });

    describe('test submission status is in draft', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "draft" });
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubCofoHopdetl').respond(initialData);
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/LocationVerifier').respond(webservice_data);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/RemoveCofo').respond(removecofo_data);
            httpBackend.when('POST', basePath + 'api/BBLApplication/SubmitCofoHop').respond(true);
            httpBackend.when('GET', "partials/templates/popuptemplate.html").respond(200);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            phylocCofoController = controller('PhysicalLocationCofoController',
            {
                $rootScope: rootscope, $scope: scope,
                $location: $location, mockservice: mockservice,
                appConstants: appconstants, timeOut: timeout,
                $routeParams: routeParams, utilityfactory: utilityfac,
                $filter: filter, MAR_validationService: marservice,
                errorfactory: errorfactory, SessionFactory: sessionfactory,
                popupFactory: popupfactory, BBLSubmissionFactory: bblsubmissionfactory, authService: authservice
            });
        });

        //    it('should test GetSubmissionCofoHop', function () {
        //        httpBackend.flush();
        //        //console.log(phylocCofoController.StreetTypes);
        //        //expect(phylocCofoController.StreetTypes.length).toBeGreaterThan(0);
        //    });


        //    //it('should test form validation', function () {
        //    //    var data = { Number: 'adfasd', DateofIssue: "25/12/2015" };
        //    //    phylocCofoController.searchZoning()
        //    //    expect(phylocCofoController.cofo.number).toBe('adfasd');
        //    //});

        it("should test search Zoning validation when data Entered Wrong", function () {
            httpBackend.when('POST', basePath + 'api/BBLApplication/CofoHopDetails').respond(nodata_result);
            var data = { Number: 'adfasd', DateofIssue: "25/12/2015" };
            phylocCofoController.contact_us = {};
            phylocCofoController.contact_us.$invalid = false;
            phylocCofoController.searchZoning();
            httpBackend.flush();
            expect(phylocCofoController.cofo.searchDetails.Status).toBe("NODATA");
            expect(phylocCofoController.searchmatch).toBe(false);
        });

        it("should test when search is valid", function () {
            httpBackend.when('POST', basePath + 'api/BBLApplication/CofoHopDetails').respond(valid_search);
            phylocCofoController.contact_us = {};
            phylocCofoController.contact_us.$invalid = false;
            phylocCofoController.searchZoning();
            httpBackend.flush();
            expect(phylocCofoController.cofo.searchDetails.Status).toBe(null);
            expect(phylocCofoController.searchmatch).toBe(true);
        });

        it("should test toggle radio method", function () {
            phylocCofoController.toggleRadio('true');
            expect(phylocCofoController.correctSearch).toBe(true);
            phylocCofoController.toggleRadio('false');
            expect(phylocCofoController.correctSearch).toBe(false);
        });

        //it("should test toggleCheckbox method", function () {
        //    phylocCofoController.toggleCheckbox('testid');
        //    spyOn(angular, 'element').andCallFake(ngElementFake);
        //});


        ////    ////test case partially completed




        it('should test getrelatedAddress', function () {
            var item = {}
            phylocCofoController.getrelatedAddress(item);
            expect(phylocCofoController.fieldsDisable).toBeFalsy();
        });

        it('should test getSuggestions when length is less than 4', function () {
            phylocCofoController.getSuggestions("123");
            expect(phylocCofoController.Address.length).toEqual(0);
        });

        it('should test getSuggestions when length is less than 4', function () {
            phylocCofoController.cofoinfo = {};
            phylocCofoController.getSuggestions("12345");
            expect(phylocCofoController.Address.length).toEqual(0);
        });

        it('should test getSuggestions when length is equal to 4', function () {
            phylocCofoController.getSuggestions("1234");
            httpBackend.flush();
            expect(phylocCofoController.Address.length).toBeGreaterThan(0);
        });

        it('should test startsWith', function () {
            expect(phylocCofoController.startsWith("1234 washington", "1234")).toBeTruthy();
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is true ", function () {
            phylocCofoController.submissionStatusData = submissionststus_data;
            phylocCofoController.submissionStatusData.IsCorporationDivision = true;
            phylocCofoController.navToRegisteredAgent();
            expect($location.path).toHaveBeenCalledWith('/physicallocation/corpreg/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName not equal to null", function () {
            phylocCofoController.submissionStatusData = submissionststus_data;
            phylocCofoController.submissionStatusData.IsCorporationDivision = false;
            phylocCofoController.submissionStatusData.TradeName = 'asdf';
            phylocCofoController.navToRegisteredAgent();
            expect($location.path).toHaveBeenCalledWith('/corpreqregwithtradesecond/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is SOLE PROPRIETORSHIP", function () {
            phylocCofoController.submissionStatusData = submissionststus_data;
            phylocCofoController.submissionStatusData.IsCorporationDivision = false;
            phylocCofoController.submissionStatusData.TradeName = 'NA';
            phylocCofoController.submissionStatusData.BusinessStructure = 'SOLE PROPRIETORSHIP';
            phylocCofoController.navToRegisteredAgent();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is GENERAL PARTNERSHIP", function () {
            phylocCofoController.submissionStatusData = submissionststus_data;
            phylocCofoController.submissionStatusData.IsCorporationDivision = false;
            phylocCofoController.submissionStatusData.TradeName = 'NA';
            phylocCofoController.submissionStatusData.BusinessStructure = 'GENERAL PARTNERSHIP';
            phylocCofoController.navToRegisteredAgent();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is JOINT VENTURE", function () {
            phylocCofoController.submissionStatusData = submissionststus_data;
            phylocCofoController.submissionStatusData.IsCorporationDivision = false;
            phylocCofoController.submissionStatusData.TradeName = 'NA';
            phylocCofoController.submissionStatusData.BusinessStructure = 'JOINT VENTURE';
            phylocCofoController.navToRegisteredAgent();
            expect($location.path).toHaveBeenCalledWith('/corpnotregisteredaddress/guid');
        });

        it("should test navToRegisteredAgent with IsCorporationDivision is false and TradeName equal to null and BusinessStructure is not equal to JOINT VENTURE", function () {
            phylocCofoController.submissionStatusData = submissionststus_data;
            phylocCofoController.submissionStatusData.IsCorporationDivision = false;
            phylocCofoController.submissionStatusData.TradeName = 'NA';
            phylocCofoController.submissionStatusData.BusinessStructure = 'Corporation (For Profit)';
            phylocCofoController.navToRegisteredAgent();
            expect($location.path).toHaveBeenCalledWith('/corpreqregisterfirst/guid');
        });

        it("should test incorrectSearch ", function () {
            phylocCofoController.incorrectCount = 0;
            phylocCofoController.incorrectSearch();
            timeout.flush();
        });

        it("should test checkAndExit when not_valid_Address is true", function () {
            phylocCofoController.cofo = submit_data;
            phylocCofoController.not_valid_Address = true;
            phylocCofoController.checkAndExit('path');
            expect($location.path).toHaveBeenCalledWith('/path');
        });

        it("should test checkAndExit when not_valid_Address is false", function () {
            phylocCofoController.cofo = submit_wrong_data;
            phylocCofoController.searchZoningClicked = false;
            phylocCofoController.correctSearch = false;
            phylocCofoController.not_valid_Address = false;
            phylocCofoController.corpnorregadd_form = {};
            phylocCofoController.corpnorregadd_form.$invalid = true;
            phylocCofoController.currentpage_errors = {};
            phylocCofoController.checkAndExit('path');

        });

        it("should test navToNextStepfromcofo when form invalid", function () {
            phylocCofoController.cofo = submit_data;
            phylocCofoController.contact_us = {};
            phylocCofoController.contact_us.$invalid = true;
            phylocCofoController.currentpage_errors = {};
            phylocCofoController.navToNextStepfromcofo('path');
            
        });

        it("should test navToNextStepfromcofo when form valid", function () {
            phylocCofoController.cofoinfo = submit_data;
            phylocCofoController.contact_us = {};
            phylocCofoController.contact_us.$invalid = false;
            phylocCofoController.corpnorregadd_form = {};
            phylocCofoController.corpnorregadd_form.$invalid = false;
            phylocCofoController.correctSearch = false;
            phylocCofoController.navToNextStepfromcofo('path');
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/path');
        });

        //it("should test checkAndExit when not_valid_Address is false", function () {
        //    phylocCofoController.cofo = empty_data;
        //    phylocCofoController.nocofoSelected = false;
        //    phylocCofoController.checkAndExit('path');
        //    httpBackend.flush();
        //    expect($location.path).toBe('/path');
        //});

        it('should test cofoErrorMsg', function () {
            phylocCofoController.cofoErrorMsg();
        });

        it('should test setErrorMsg ', function () {
            phylocCofoController.setErrorMsg();
        });

        it("should test toggleCheckbox method when ctlId is nocofo", function () {

            phylocCofoController['noCofo'] = true;
            var ctlId = "noCofo"
            phylocCofoController.toggleCheckbox('noCofo');
            httpBackend.flush();
        });

        it("should test toggleCheckbox method when ctlId is not nocofo", function () {
            phylocCofoController.toggleCheckbox('ctlId');
            phylocCofoController['ctlId'] = true;
            phylocCofoController.ctlId = '';
        });

        //it("should test navigateAnyway when noCofo is true", function () {
        //    phylocCofoController.noCofo = true;
        //    phylocCofoController.nocofoSelected = true;
        //    phylocCofoController.navigateAnyway();
        //    httpBackend.flush();
        //});

        //it("should test navigateAnyway when noCofo is false", function () {
        //    phylocCofoController.noCofo = false;
        //    phylocCofoController.navigationPath = 'path'
        //    phylocCofoController.navigateAnyway();
        //    httpBackend.flush();
        //    expect($location.path).toHaveBeenCalledWith('/path');
        //});

        //it("should test dontNavigate", function () {
        //    phylocCofoController.noCofo = true;
        //    phylocCofoController.dontNavigate();
        //    httpBackend.flush();
        //});
    });
});