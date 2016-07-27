describe('PreApp Questions Controller Spec', function () {

    var $scope, controller, httpBackend, mockservice, preAppQuesSpec, localStore, windowobj, timeout, rootscope, $location, utilityfac, basePath;
    var appconstants, errorfactory, popupfactory, authservice;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $window, $timeout, UtilityFactory, appConstants, appConstants, errorFactory, popupFactory, authService) {
        $scope = $rootScope.$new();
        rootscope = $rootScope.$new();
        $location = _$location_;
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
        timeout = $timeout;
        windowobj = $window;
        basePath = appConstants.apiServiceBaseUri;
        utilityfac = UtilityFactory;
        appConstants, errorFactory, popupFactory, authService


    }));
    afterEach(function () {
        httpBackend.verifyNoOutstandingExpectation();
        httpBackend.verifyNoOutstandingRequest();
    });

    var activitiesdata = [{ "ActivityID": "D96ABBB9-7437-4476-BBA6-72EF6A94F327", "ActivityName": "Automobiles, Parking and Towing", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "38E92001-449D-47C5-9FFA-3343A3AD09A0", "ActivityName": "Beauty Care", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "BE29F663-A3FA-4B64-8697-C9C4FF91B69F", "ActivityName": "Charity", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "27E3AC0D-21DB-4158-95C4-F550327FEB54", "ActivityName": "Employment Services", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "1A9D965E-BA85-4D44-A5D0-6CFC54D8FC11", "ActivityName": "Entertainment", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "FF0C5A9C-86B4-4378-BA8F-C1CE165B814E", "ActivityName": "Food & Beverage (Non-Alcoholic)", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "753B245D-92E1-474E-85F7-916AD2C95D97", "ActivityName": "Fuel, Environmental & Hazardous Materials", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "C49EACE2-2180-430E-BA2F-8B891D659421", "ActivityName": "Home Improvement and Security", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "1850DF4F-D97D-471C-B26F-F44E04BA288B", "ActivityName": "Pet Care & Retail", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A", "ActivityName": "Real Estate & Rentals", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "2AC509BD-CEE0-4F81-BEC0-308679346E5D", "ActivityName": "Repair & Sales", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "1ED33685-B54E-4D7A-BBCE-1C93BD762CB1", "ActivityName": "Retail Sales, Consulting, and Other Services", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "2E8CE67A-A867-4CAE-968A-903E7BA3F742", "ActivityName": "Sales & Services", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }, { "ActivityID": "d7fff4a3-a020-4649-99b0-fce0b5f10d15", "ActivityName": "test activity", "APP_Type": "1    ", "CreateDate": "2015-11-10T15:30:04.437", "UpdatedDate": "2015-11-10T15:30:04.437" }, { "ActivityID": "668F4B68-8437-431F-9052-60809E556028", "ActivityName": "Used Goods Dealing & Sales", "APP_Type": "1    ", "CreateDate": null, "UpdatedDate": null }],
        primarycategories = [{ "PrimaryID": "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54", "ActivityID": "BE29F663-A3FA-4B64-8697-C9C4FF91B69F", "Description": "Charitable Exempt\r\n", "Endorsement": "General Business", "CategoryCode": "4001", "UnitOne": "NA\r\n", "UnitTwo": "NA\r\n", "App_Type": "B", "IsSecondaryLicenseCategory": true, "IsSubCategory": false, "Status": true }, { "PrimaryID": "C1B0F3E8-7795-45CB-B763-593F235A8275", "ActivityID": "BE29F663-A3FA-4B64-8697-C9C4FF91B69F", "Description": "Charitable Solicitation\r\n", "Endorsement": "General Business", "CategoryCode": "4002", "UnitOne": "NA\r\n", "UnitTwo": "NA\r\n", "App_Type": "B", "IsSecondaryLicenseCategory": true, "IsSubCategory": false, "Status": true }],
        secondarycategories = [{ "SecondaryID": "1B69B449-ECAB-41AE-89A9-E2F76329A52B", "PrimaryID": "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54", "SecondaryLicenseCategory": "General Business Licenses\r\n", "UnitOne": null, "UnitTwo": null, "Endorsement": null, "IsSubCategory": true, "Status": true }]
    var checkDataAvailability, displayCatogeryList, setQuestionsInView;
    describe('test cases when location path is step1', function () {
        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('GET', basePath + 'api/BBLCategory/BusinessActivities').respond(activitiesdata);
            httpBackend.when('POST', basePath + 'api/BBLCategory/PrimaryCategoryList').respond(activitiesdata);
            //checkDataAvailability = jasmine.createSpy('checkDataAvailability');
            //displayCatogeryList = jasmine.createSpy('displayCatogeryList');
            //setQuestionsInView = jasmine.createSpy('setQuestionsInView');
            spyOn($location, 'path').and.returnValue('/preappquestions/step1');
            preAppQuesSpec = controller('PreAppQuestionsController',
                {
                    $scope: $scope, $rootScope: rootscope,
                    $location: $location, $window: windowobj,
                    $timeout: timeout, mockservice: mockservice,
                    utilityFac: utilityfac
                });
        });



        it('should test current page id', function () {
            httpBackend.flush();
            expect(preAppQuesSpec.currentPageid).toEqual('step1');
        });

        it('should test init', function () {
            httpBackend.flush();
            expect(JSON.stringify(rootscope.preAppQuestions)).toEqual(localStorage.getItem('preAppQuestionsData'));
            expect(preAppQuesSpec.categories).toBeDefined();
            expect(angular.equals(JSON.stringify(preAppQuesSpec.categories), JSON.stringify(activitiesdata))).toBeTruthy();
            timeout.flush();
        });

        it('should test navToPreAppQues_Step2 when nothing selected', function () {
            preAppQuesSpec.navToPreAppQues_Step2();
            expect(preAppQuesSpec.NothingSelected).toBeTruthy();
            timeout.flush();
            httpBackend.flush();
        });

        it('should test navToPreAppQues_Step2 when something is checked', function () {
            var html = '<span class="checked"><input type="checkbox" id="sBE29F663-A3FA-4B64-8697-C9C4FF91B69F"></span>';
            angular.element(document.body).append(html);
            preAppQuesSpec.navToPreAppQues_Step2();
            httpBackend.flush();
            timeout.flush();
            expect(rootscope.nextStepData).toBeDefined();
            expect($location.path).toHaveBeenCalledWith('/preappquestions/step2');
            angular.element(document.body).html('')
        });

        it('should test navToLogin', function () {
            preAppQuesSpec.navToLogin();
            expect($location.path).toHaveBeenCalledWith('/dashboard');
            httpBackend.flush();
        });

        it('should test navToLogin', function () {
            localStorage.loggedin = 0;
            preAppQuesSpec.navToLogin();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/login');
        });

        it('should test navToApply ', function () {
            preAppQuesSpec.navToApply();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/newbblwelcome');
        });

        it('should test navToPreAppQues_Step1', function () {
            preAppQuesSpec.navToPreAppQues_Step1();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/preappquestions/step1');
        });

        it('should test revisePreAppQue ', function () {
            preAppQuesSpec.revisePreAppQue();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/preappquestions/step1');
        });

        it('should test filterByStatus', function () {

            preAppQuesSpec.filterByStatus("test");
            expect(preAppQuesSpec.filterByStatusKeyword).toBe("test");
            httpBackend.flush();
        });

        it('should test continueToNavigate ', function () {
            preAppQuesSpec.nextNavigation = "test/#/test"
            preAppQuesSpec.continueToNavigate("test");
            expect($location.path).toHaveBeenCalledWith('/test');
            httpBackend.flush();
        });

        it('should test isNumber', function () {
            expect(preAppQuesSpec.isNumber(25)).toBe(true);
            expect(preAppQuesSpec.isNumber('as')).toBe(false);
            httpBackend.flush();
        });

        it('should test exitWithOutSaving ', function () {
            preAppQuesSpec.exitWithOutSaving();
            expect(preAppQuesSpec.nextNavigation).toBe("#/mybbl");
        });

        it('should test navToMyBBL', function () {
            httpBackend.when('POST', basePath + 'api/BBLAssociation/BblServiceList').respond({ status: true, result: [{"test":"test"}]});
            preAppQuesSpec.navToMyBBL();
            expect($location.path).toBe("/mybbl");
        });

        it('should test navigateToAppChecklist', function () {
            preAppQuesSpec.navigateToAppChecklist();
            expect(preAppQuesSpec.createChecklist).toBeTruthy();
        });

        it('should test toggleCheckbox', function () {
            preAppQuesSpec.currentPageid = "step1"
            preAppQuesSpec.toggleCheckbox("ctrlid");
            expect(preAppQuesSpec.createChecklist).toBeTruthy();
        });

    });

    describe('test case when location path is step2', function () {
        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('GET', basePath + 'api/BBLCategory/BusinessActivities').respond(activitiesdata);
            httpBackend.when('POST', basePath + 'api/BBLCategory/PrimaryCategoryList').respond(primarycategories);
            httpBackend.when('POST', basePath + 'api/BBLCategory/SecondaryCategoryList').respond(secondarycategories);
            //checkDataAvailability = jasmine.createSpy('checkDataAvailability');
            //displayCatogeryList = jasmine.createSpy('displayCatogeryList');
            //setQuestionsInView = jasmine.createSpy('setQuestionsInView');
            localStorage.setItem('preAppQuestionsData', '{"step0":[],"step1":"BE29F663-A3FA-4B64-8697-C9C4FF91B69F"}');
            spyOn($location, 'path').and.returnValue('/preappquestions/step2');
            preAppQuesSpec = controller('PreAppQuestionsController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, $window: windowobj,
                $timeout: timeout, mockservice: mockservice,
                utilityFac: utilityfac
            });
        });
        it('should test current page id', function () {
            httpBackend.flush();
            expect(preAppQuesSpec.currentPageid).toEqual('step2');
        });

        //it('should test init when localstorage value is null', function () {
        //    httpBackend.flush();
        //    timeout.flush();
        //    expect($location.path).toHaveBeenCalledWith('/newbblwelcome');
        //});

        it('should test init when localstorage value is not null', function () {

            httpBackend.flush();
            timeout.flush();
            expect(JSON.stringify(rootscope.preAppQuestions)).toEqual(localStorage.getItem('preAppQuestionsData'));
            expect(preAppQuesSpec.categories).toBeDefined();
            expect(angular.equals(JSON.stringify(preAppQuesSpec.categories), JSON.stringify(primarycategories))).toBeTruthy();
        });

        it('should test navToPreAppQues_Step2 when nothing selected', function () {
            //console.log("1234");
            preAppQuesSpec.navToPreAppQues_Step3();
            expect(preAppQuesSpec.NothingSelected).toBeTruthy();
            timeout.flush();
            httpBackend.flush();
        });

        it('should test navToPreAppQues_Step2 when something is checked', function () {
            var html = '<span class="checked"><input type="checkbox" id="sBE29F663-A3FA-4B64-8697-C9C4FF91B69F"></span>';
            angular.element(document.body).append(html);
            preAppQuesSpec.navToPreAppQues_Step3();
            httpBackend.flush();
            timeout.flush();
            expect(rootscope.nextStepData).toBeDefined();
            expect($location.path).toHaveBeenCalledWith('/preappquestions/step3');
        });

    });






    //it('should test current page id when it is step2', function () {
    //    console.log(localStorage.getItem('preAppQuestionsData'));
    //    //localStorage.setItem('preAppQuestionsData', '{"step0":[],"step1":"1850DF4F-D97D-471C-B26F-F44E04BA288B"}');
    //    //spyOn($location, 'path').and.returnValue('/preappquestions/step2');

    //});

});