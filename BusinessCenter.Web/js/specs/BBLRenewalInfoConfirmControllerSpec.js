describe('BBLRenewalInfoConfirmController Spec', function () {
    var $scope, controller, httpBackend, bblrenewalinfo, localStore, mockService, httpBackend, windowobj, deferred, basePath, routeparams, renewalutilityfac, sessionfac;
    var data = {
        "isValidated": true, "entityId": "10000002", "licenseNumber": "100112000001",
        "fName": "NA", "lName": "NA", "businessName": "BILOS MEGA CARE, LLC", "tradeName": "",
        "businessNameStructure": "Limited Liability Company", "expDate": "10/31/2015", "status": "Active"
    }, authservice, localstorageservice, popupfactory, errorfactory;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $routeParams, RenewalUtilityFactory, appConstants,
        SessionFactory, $window, authService, localStorageService, popupFactory, errorFactory) {

        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        httpBackend = $httpBackend;
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
        controller = $controller;
        basePath = appConstants.apiServiceBaseUri;
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        renewalutilityfac = RenewalUtilityFactory;
        windowobj = $window;
        sessionfac = SessionFactory;
        popupfactory = popupFactory;
        errorfactory = errorFactory;
        authservice = authService;
        localstorageservice = localStorageService;
    }));

    describe('when no guid is available', function () {
        beforeEach(function () {
            spyOn(renewalutilityfac, 'ifGuidAvailable').and.returnValue(false);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            bblrenewalinfo = controller('BBLRenewalInfoConfirmController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, mockService: mockService,
                $routeParams: routeparams, RenewalUtilityFactory: renewalutilityfac,
                window: windowobj, sessionfactory: sessionfac,
                popupFactory: popupfactory, errorFactory: errorfactory, authService: authservice
            });
        });
        it('should test navToConfirmAssociation', function () {
            bblrenewalinfo.navToConfirmAssociation();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });


    describe('when logged in and guid is available', function () {

        beforeEach(function () {
            spyOn(renewalutilityfac, 'ifGuidAvailable').and.returnValue(true);
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/Renew/GetRenewalData').respond({ "token": "success" });
            bblrenewalinfo = controller('BBLRenewalInfoConfirmController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, mockService: mockService,
                $routeParams: routeparams, RenewalUtilityFactory: renewalutilityfac,
                window: windowobj, sessionfactory: sessionfac,
                popupFactory: popupfactory, errorFactory: errorfactory, authService: authservice
            });
        });

        it('should test init', function () {
            httpBackend.flush();
        });

        it('should test navToConfirmAssociation', function () {
            bblrenewalinfo.navToConfirmAssociation();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test corpmodalchanged', function () {
            bblrenewalinfo.corpmodalchanged();
            expect(bblrenewalinfo.isActiveCorpNumber).toBeFalsy();
            expect(bblrenewalinfo.submitted).toBeFalsy();
        });

        it('should test setErrorMsg', function () {
            bblrenewalinfo.setErrorMsg();
            expect(bblrenewalinfo.searchcorponlinebuttonclicked).toBeFalsy();
            expect(bblrenewalinfo.submitted).toBeFalsy();
        });

        it('should test checkboxupdated', function () {
            bblrenewalinfo.corpnotregistered = true;
            bblrenewalinfo.checkboxupdated();
            expect(bblrenewalinfo.isActiveCorpNumber).toBeTruthy();
        });

        it('should test checkCorpRegistration', function () {
            bblrenewalinfo.IsCorpRegistration = true;
            bblrenewalinfo.checkCorpRegistration();
            expect(bblrenewalinfo.IsCorpRegistration).toBeFalsy();
        });

        it('should test checkCorpRegistration', function () {
            bblrenewalinfo.IsCorpRegistration = false;
            bblrenewalinfo.checkCorpRegistration();
            expect(bblrenewalinfo.IsCorpRegistration).toBeTruthy();
        });

        it('should test navigateAnyWay', function () {
            bblrenewalinfo.navigateAnyWay();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navToCancelBBLAssociation when corp number not defined', function () {
            bblrenewalinfo.navToCancelBBLAssociation();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navToCancelBBLAssociation when corp number is defined', function () {
            bblrenewalinfo.corp = {};
            bblrenewalinfo.corp.number = "12345";
            bblrenewalinfo.currentpage_errors = {};
            bblrenewalinfo.navToCancelBBLAssociation();
            //expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        //it('should test navToBblsHome', function () {
        //    bblrenewalinfo.currentpage_errors = {};
        //    bblrenewalinfo.navToBblsHome();
        //    //expect($location.path).toHaveBeenCalledWith('/mybbl');
        //});

        it('should test insertAndnavigate', function () {
            bblrenewalinfo.businessinfo = {
                "isValidated": true, "entityId": 10132243, "licenseNumber": "70100087",
                "fName": "MICHAEL", "lName": "FROHM", "businessName": "DAVIS MEMORIAL GOODWILL INDUSTRIES",
                "tradeName": "NA", "businessNameStructure": "Corporation (Non-Profit)", "expDate": "12/31/2015",
                "status": "Active", "subcategory": "Secondhand Dealers (A)", "activity": "Inspected Sales and Services",
                "category": "SECONDHAND DEALERS (A)", "applicationtype": "Business License", "corpNumber": "", "iscorp": false,
                "IsCorpRegistration": false, "licenseCategorystatus": "Category"
            }

            var renewdata = {
                "MasterId": "d1da7857-bac8-45c0-87ff-01a77042e0ad", "EntityId": "10132243",
                "LicenseNumber": "LREN16000272", "UserId": "B068CA9E-BF68-4E09-816E-C1135083880E",
                "GrandTotalAmount": 0.0, "CorpNumber": "", "IsCorp": true, "CorpStatus": "IsCorp", "TaxStatus": null, "TaxNumber": null,
                "SubCategoryName": "Secondhand Dealers (A)", "CategoryName": "Secondhand Dealers (A)", "ActivityName": "Inspected Sales and Services",
                "Endoresement": null, "CategoryId": "61950860-A32E-415D-AADA-BB438E449C6E", "ActivityId": null, "SubCategoryID": null,
                "LicenseAmount": 0.0, "EndorsementFee": 0.0, "ApplicationFee": 0.0, "TechFee": 0.0, "RAOFee": 0.0, "IsCorpRegistration": false,
                "IsCleanHands": false, "DocumentList": null, "ServiceCheckList": null, "BblAddress": null, "BblCity": null, "BblState": null,
                "BblZip": null, "LapsedAmount": 250.0000, "ExpiredAmount": 250.0000, "Extradays": "Expired", "RenewalLicenseCode": null,
                "LrenNumber": "LREN16000272", "UserBblAssociateId": "903", "LicenseDuration": 2, "RenewStatus": "Data", "InitalDocumet": null,
                "CurrentDate": null, "NameofLicense": null, "BusinessOwnerName": null, "SubmissionLicense": "LREN16000272", "ContactFirstName": null,
                "ContactMiddleName": null, "ContactLastName": null, "App_Type": "B"
            };
            bblrenewalinfo.insertAndnavigate();
            //expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test cancelNavigation', function () {
            bblrenewalinfo.cancelNavigation();
        });

        //it('should test navToPrevious', function () {
        //    bblrenewalinfo.navToPrevious();
        //    httpBackend.flush();
        //    expect(bblrenewalinfo.navigate).toBeUndefined();
        //});

        it('should test getCorpRegInfo', function () {
            bblrenewalinfo.getCorpRegInfo();
            expect(bblrenewalinfo.isActiveCorpNumber).toBeFalsy();
        });

    });


});