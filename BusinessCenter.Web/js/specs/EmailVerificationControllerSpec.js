﻿describe('Email verification Controller Test', function () {

    var $scope, controller, httpBackend, mockservice, emailverificationcontroller, basePath, $location;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, authService, localStorageService) {
        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        mockservice = requestService;
        httpBackend = $httpBackend;
        spyOn($location, 'path');
        controller = $controller;
        basePath = appConstants.apiServiceBaseUri;
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
        authservice = authService;
        localstorageservice = localStorageService;
        Object.defineProperty(window, 'localStorage', { value: localStore });
        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });

        emailverificationcontroller = controller('EmailVerification', { $scope: $scope, $rootScope: rootscope, $location: $location, requestService: mockservice, authService: authservice });

    }));

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

    it('should test EmailVerification method when user status is UserActive', function () {
        httpBackend.when('POST', basePath + 'api/UserAccounts/ConfirmEmail').respond({ "status": "UserActive" });
        httpBackend.flush();
        expect($scope.status).toBe("UserActive");
    });
    it('should test EmailVerification method when user status is Success', function () {
        httpBackend.when('POST', basePath + 'api/UserAccounts/ConfirmEmail').respond({ "status": "success" });
        httpBackend.flush();
        expect($scope.status).toBe("success");
    });

    it('should test EmailVerification method when user status is AccountExpired', function () {
        httpBackend.when('POST', basePath + 'api/UserAccounts/ConfirmEmail').respond({ "status": "AccountExpired" });
        httpBackend.flush();
        expect($scope.status).toBe("AccountExpired");
    });

    it('should test EmailVerification method when user status is empty', function () {
        httpBackend.when('POST', basePath + 'api/UserAccounts/ConfirmEmail').respond({ "status": "" });
        httpBackend.flush();
        expect($scope.status).toBe("");
    });

    it('should test navToRegister', function () {
        $scope.navToRegister();
        expect($location.path).toHaveBeenCalledWith('/register');
    });
    it('should test navToLogin', function () {
        authservice.authentication = { isAuth: false };
        $scope.navToLogin();
        expect($location.path).toHaveBeenCalledWith('/login');
    });
    it('should test navToLogin', function () {
        authservice.authentication = { isAuth: true };
        $scope.navToLogin();
        expect($location.path).toHaveBeenCalledWith('/dashboard');
    });
});