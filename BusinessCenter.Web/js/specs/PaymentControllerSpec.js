describe('payment controller spec', function () {

    var scope, routeParams, sessionfactory, popupfactory, controller, httpBackend, mockservice, paymentController, localStore, appconstants, basePath, utilityfactory, errorfactory, compile, bblsubmissionfactory, form;
    var authservice, localstorageservice;
    var paymentData = { "StreetNumber": null, "MasterID": "54648e65-66cc-4a5b-b1ba-fd0349396c6d", "OccupanyNumber": null, "DateofIssue": null, "CorpFileNo": null, "OrgType": null, "BusinessOwner": null, "FienNumber": null, "TradeName": null, "DocType": null, "GrandTotal": 0.0, "IsCofo": false, "IsHomeBased": false, "IsSubmissionCofo": false, "IsSubmissionHop": false, "IsSubmissioneHop": false, "FEIN": null, "IsFEIN": false, "HeadQName": null, "HeadQBName": null, "HeadQAddress": null, "HeadQAddressNumber": null, "HeadQStreetName": null, "HeadQStreetType": null, "HeadQAQuadrant": null, "HeadQCity": null, "HeadQState": null, "HeadQCountry": null, "HeadQZip": null, "HeadQEmail": null, "HeadQTelePhone": null, "PremiseBName": null, "PremiseName": null, "PremiseAddress": null, "PremiseAddressNumberSufix": null, "PremiseCity": null, "PremiseState": null, "PremiseCountry": null, "PremiseZip": null, "PremiseEmail": null, "PremiseTelePhone": null, "PremiseQuadrant": null, "PremiseUnitType": null, "PremiseUnit": null, "PremiseAddressNumber": null, "PremiseStreetType": null, "PremiseStreetName": null, "MailingBName": "INCORPORATING SERVICES", "MailingAddressNumberSufix": null, "MailingName": "  ", "MailingAddress": "1100 H STREET, N.W., SUITE 840  ", "MailingCity": "Washington", "MailingState": "DC", "MailingCountry": "", "MailingZip": "20005", "MailingEmail": "", "MailingTelePhone": "", "MailingQuadrant": "", "MailingUnitType": null, "MailingUnit": "", "MailingStreetName": "1100 H STREET, N.W., SUITE 840", "MailingStreetNumber": "", "MailingStreetType": "", "MailingFirstName": "", "MailingMiddleName": "", "MailingLastName": "", "AddressNumber": null, "AgentBName": null, "AgentName": null, "AgentUnit": null, "AgentAddress": null, "AgentCity": null, "AgentState": null, "AgentCountry": null, "AgentZip": null, "AgentEmail": null, "AgentAddressNumber": null, "AgentStreetName": null, "AgentStreetType": null, "AgentQuadrant": null, "AgentTelePhone": null, "IsRaoFeeApplied": false, "ApplicationFee": 0.0, "CategoryLicenseFee": 0.0000, "EndorsementFee": 0.0000, "SubTotal": 0.0000, "TechFee": 0.0000, "TotalFee": 0.0000, "Isehop": false, "ServiceCheckList": [{ "Endorsement": "General Business", "CategoryId": "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54", "LicenseCategory": "Charitable Exempt", "Units": "NA ", "ApplicationFee": 0.0, "CategoryLicenseFee": 0.0000, "EndorsementFee": 0.0000, "SubTotal": 0.0000, "TechFee": 0.0000, "TotalFee": 0.0000, "CategoryCode": "4001", "IsRaoFeeApplied": false, "LicenseDuration": "TWO (2) YEAR" }, { "Endorsement": "General Business", "CategoryId": "1B69B449-ECAB-41AE-89A9-E2F76329A52B", "LicenseCategory": "General Business Licenses", "Units": "NA ", "ApplicationFee": 0.0, "CategoryLicenseFee": 0.0000, "EndorsementFee": 0.0000, "SubTotal": 0.0000, "TechFee": 0.0000, "TotalFee": 0.0000, "CategoryCode": "4003", "IsRaoFeeApplied": false, "LicenseDuration": "TWO (2) YEAR" }, { "Endorsement": "General Business", "CategoryId": "47ACA831-5093-4225-8324-4DEBFED93E43", "LicenseCategory": "Yoga Studies", "Units": "NA", "ApplicationFee": 0.0, "CategoryLicenseFee": 0.0, "EndorsementFee": 0.0, "SubTotal": 0.0, "TechFee": 0.0, "TotalFee": 0.0, "CategoryCode": "4003", "IsRaoFeeApplied": false, "LicenseDuration": "TWO (2) YEAR" }], "DocumentList": null, "LicenseNumber": null }
    beforeEach(module('DCRA'));

    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, $routeParams, UtilityFactory,
        errorFactory, BBLSubmissionFactory, $compile, SessionFactory, popupFactory, authService, localStorageService) {
        rootscope = $rootScope.$new();
        $location = _$location_;
        scope = $rootScope.$new();
        basePath = appConstants.apiServiceBaseUri;
        spyOn($location, 'path').and.returnValue('test/test1/test2');
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
        appconstants = appConstants;
        utilityfactory = UtilityFactory;
        routeParams = $routeParams;
        routeParams.guid = 'guid';
        errorfactory = errorFactory;
        bblsubmissionfactory = BBLSubmissionFactory;
        compile = $compile;
        sessionfactory = SessionFactory;
        popupfactory = popupFactory;
        authservice = authService;
        localstorageservice = localStorageService;

    }));

    var doneWithPaymentDetails = { "paymentId": "22abcf0f-4626-4ede-b36f-5fd02fa2ce3f", "trasactionresult": { "Success": true, "Result": 0, "Message": "Test Approval", "Id": "Test-Transaction", "Time": "2015-12-25T15:37:52.8420345+05:30" }, "finalsuccess": "YES", "masterTextRevenue": false, "validateCorpFileStatus": "Active" };

    var dropdownData = ["Alley", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive", "Driveway", "Expressway", "Freeway",
                    "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade", "Road", "Row", "Square", "Street", "Terrace", "Walk", "Way"];

    var statesdata = { "Status": [{ "StateCode": "AL", "StateName": "Alaska", "CountryCode": "US" }] };

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

    describe('when guid is not available', function () {

        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            paymentController = controller('PaymentController', {
                $scope: scope, $rootScope: rootscope,
                $location: $location, routeParams: routeParams,
                mockservice: mockservice, appConstants: appconstants,
                utilityFac: utilityfactory,
                errorFactory: errorfactory, BBLSubmissionFactory: bblsubmissionfactory,
                sessionFactory: sessionfactory, popupFactory: popupfactory, authService: authservice
            });
        });

        it('should test payment init method', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('when status is under review', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/StateListBasedOnCode').respond(statesdata);
            httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            paymentController = controller('PaymentController', {
                $scope: scope, $rootScope: rootscope,
                $location: $location, routeParams: routeParams,
                mockservice: mockservice, appConstants: appconstants,
                utilityFac: utilityfactory,
                errorFactory: errorfactory, BBLSubmissionFactory: bblsubmissionfactory,
                sessionFactory: sessionfactory, popupFactory: popupfactory, authService: authservice
            });
        });

        it('should test payment init method', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('when status is in draft', function () {
        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            paymentData.CurrentDate = new Date().getMonth() + 1 + "/" + new Date().getDate() + "/" + new Date().getFullYear();
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "Draft" });
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/StateListBasedOnCode').respond(statesdata);
            httpBackend.when('POST', basePath + 'api/BBLApplication/VerficationPayDetails').respond(paymentData);

            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            paymentController = controller('PaymentController', {
                $scope: scope, $rootScope: rootscope,
                $location: $location, routeParams: routeParams,
                mockservice: mockservice, appConstants: appconstants,
                utilityFac: utilityfactory,
                errorFactory: errorfactory, BBLSubmissionFactory: bblsubmissionfactory,
                sessionFactory: sessionfactory, popupFactory: popupfactory, authService: authservice
            });
            scope.vm = paymentController;
            var element = angular.element(
                   '<form name="vm.paymentform">' +
                   '<input ng-model="vm.cardexpdate" name="cardexpdate" />' +
                   '<input ng-model="vm.emailconfirm" name="emailconfirm" />' +
                   '<input ng-model="vm.zip" name="zip" />' +
                   '<input ng-model="vm.telephone" name="telephone" />' +
                   '</form>'
                   );
            compile(element)(scope);
            form = scope.vm.paymentform;
        });

        it('should test navToChecklist method', function () {
            paymentController.navToChecklist();
            expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
        });

        it('should test navToMybbl method', function () {
            paymentController.navToMybbl();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test ValidateDate when year is undefined', function () {
            paymentController.currentMonth = new Date().getMonth() + 1;
            paymentController.cardexpdate = 22;
            paymentController.ValidateDate();
            expect(form.cardexpdate.$valid).toBeFalsy();
        });

        it('should test ValidateDate when year is undefined and card expiration month is valid', function () {
            paymentController.currentMonth = new Date().getMonth() + 1;
            paymentController.cardexpdate = 11;
            paymentController.ValidateDate();
            expect(form.cardexpdate.$valid).toBeTruthy();
        });

        it('should test ValidateDate when year is defined and month is valid', function () {
            paymentController.currentMonth = new Date().getMonth() + 1;
            paymentController.currentyear = new Date().getFullYear();
            paymentController.cardexpdate = 11;
            paymentController.year = 2015;
            paymentController.ValidateDate();
            expect(form.cardexpdate.$valid).toBeFalsy();
        });

        it('should test ValidateDate when year is valid and month is valid', function () {
            paymentController.currentMonth = new Date().getMonth() + 1;
            paymentController.currentyear = new Date().getFullYear();
            paymentController.cardexpdate = 11;
            paymentController.year = 2017;
            paymentController.ValidateDate();
            expect(form.cardexpdate.$valid).toBeTruthy();
        });

        it('should test ValidateDate when year is equal to current year and month is valid', function () {
            paymentController.currentMonth = new Date().getMonth() + 1;
            paymentController.currentyear = new Date().getFullYear();
            paymentController.cardexpdate = 11;
            paymentController.year = 2016;
            paymentController.ValidateDate();
            expect(form.cardexpdate.$valid).toBeTruthy();
        });

        it('should test ValidateYear when creditcardclear is defined', function () {
            paymentController.creditcardclear();
            expect(paymentController.cardnumber).toBe('');
        });

        it('should test toggleRadio method', function () {
            paymentController.toggleRadio();
            expect(paymentController.country).toBe("US");
        });

        it('should test selectcountryoption method when country is US', function () {
            paymentController.country = "US";
            paymentController.selectcountryoption();
            expect(form.zip.$valid).toBeTruthy();
            expect(form.telephone.$valid).toBeTruthy();
            expect(paymentController.selectStateDropdown).toBeFalsy();
        });

        it('should test selectcountryoption method when country is not in US', function () {
            paymentController.country = "otherthanus";
            paymentController.selectcountryoption();
            expect(form.zip.$valid).toBeTruthy();
            expect(form.telephone.$valid).toBeTruthy();
            expect(paymentController.selectStateDropdown).toBeTruthy();
        });

        it('should test navToReceipt method when form is invalid', function () {
            form.$invalid = true;
            paymentController.navToReceipt();
        });

        it('should test checkTelephoneMaxLength when country is US', function () {
            paymentController.validations_wrt_contry = errorfactory.isCountryUS(true);
            paymentController.diffPayment = {};
            paymentController.diffPayment.Telephone = "11";
            paymentController.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeFalsy();

            //telephone length is correct

            paymentController.diffPayment.Telephone = "1111111111";
            paymentController.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();
        });

        it('should test checkTelephoneMaxLength when country is not US', function () {
            paymentController.validations_wrt_contry = errorfactory.isCountryUS(false);
            paymentController.diffPayment = {};
            paymentController.diffPayment.Telephone = "11";
            paymentController.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();

            //telephone length is correct

            paymentController.diffPayment.Telephone = "1111111111";
            paymentController.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();
        });

        it('should test checkZipMaxLength when country is US', function () {
            paymentController.validations_wrt_contry = errorfactory.isCountryUS(true);
            paymentController.diffPayment = {};
            paymentController.diffPayment.zip = "11";
            paymentController.checkZipMaxLength();
            expect(form.zip.$valid).toBeFalsy();

            //zip length is correct
            paymentController.diffPayment.zip = "11111";
            paymentController.checkZipMaxLength();
            expect(form.zip.$valid).toBeTruthy();
        });

        it('should test emailconfirmcase is not undefined', function () {
            paymentController.emailconfirm = "a@b.co";
            paymentController.email = "a@b.co";
            paymentController.emailconfirmcase();
            expect(form.emailconfirm.$valid).toBeTruthy();
        });

        it('should test emailconfirmcase is not undefined and both are not equal', function () {
            paymentController.emailconfirm = "a@ber.co";
            paymentController.email = "a@b.co";
            paymentController.emailconfirmcase();
            expect(form.emailconfirm.$valid).toBeFalsy();
        });

        it('should test emailconfirmcase is not defined', function () {
            paymentController.emailconfirmcase();
            expect(form.emailconfirm.$valid).toBeTruthy();
        });

        it('should test navToReceipt method when form is invalid', function () {
            form.$invalid = true;
            paymentController.navToReceipt();
        });

        it('should test navToReceipt method when form is valid', function () {
            form.$invalid = false;            
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmitPayment').respond(doneWithPaymentDetails);
            paymentController.navToReceipt();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/receipt/guid');
        });

        it('should test navToReceipt method when form is valid and transaction fails', function () {
            form.$invalid = false;
            doneWithPaymentDetails.trasactionresult.Success = false;
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmitPayment').respond(doneWithPaymentDetails);
            paymentController.navToReceipt();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/paymentfailure/apply/guid');
        });

        it('should test navToReceipt method when form is valid and finalsuccess is not yes', function () {
            form.$invalid = false;
            doneWithPaymentDetails.finalsuccess = "No";
            paymentController.paymentdetails = {
                TotalFee: 0
            };
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmitPayment').respond(doneWithPaymentDetails);
            paymentController.navToReceipt();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/paymentfailure/apply/guid');
        });

    });
});