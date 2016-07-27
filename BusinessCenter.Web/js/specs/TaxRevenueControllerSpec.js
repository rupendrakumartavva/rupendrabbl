describe('TaxRevenue Controller Spec', function () {

    var $scope, controller, httpBackend, mockservice, taxrevenueController, localStore, appconstants, routeParams;
    var basePath, utilityfactory, sessionfactory, bblsubmissionfactory, compile;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $routeParams, UtilityFactory, SessionFactory, BBLSubmissionFactory, appConstants,$compile) {
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
        routeParams = $routeParams;
        routeParams.guid = 'guid';
        utilityfactory = UtilityFactory;
        sessionfactory = SessionFactory;
        bblsubmissionfactory = BBLSubmissionFactory;
        appconstants = appConstants;
        basePath = appconstants.apiServiceBaseUri;
        compile = $compile;
    }));

    var submissionstatusdata = { "Status": "Draft", "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "TradeName": "", "BusinessStructure": "", "IsCorporationDivision": true, "IsCoporateRegistration": true, "IsResidentAgent": true, "IsIndividual": false, "IsBusinessMustbeinDc": true, "AppType": "B", "IsFEIN": true, "DocSubmType": "", "PaymentId": null, "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "AGENT FOR SERVICE", "CurrentYear": "2016", "CreatedDate": "02/08/2016", "SelectedMailType": "NEWMAIL", "PremisesAddress": "1523  3RD Street NW  2 Washington District of Columbia United States 20001", "BusinessName": "AGENT FOR SERVICE", "IsCategorySelfCertification": false },
        taxrevenuedata = { "taxrevenue": [{ "SubmissionTaxRevenueId": 3, "MasterId": "ed37c075-8a53-4761-a79b-4db10add5999", "TaxRevenueNumber": "11-1111111", "TaxRevenueType": "FEIN", "CreatdedDate": "2016-02-08T15:42:48.18", "UpdatedDate": null, "FullName": "asdf", "BusinessOwnerRoles": "Owner" }], "primisessAddress": "1523  3RD Street NW", "tradeName": "asdf", "bOwnerName": "" },
        submittaxformdata = { "status": true };



    describe('test cases when the accessed application is in guid is not available', function () {

        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
            taxrevenueController = controller('TaxRevenueController',
                {
                    $scope: $scope, $rootScope: rootscope,
                    $location: $location, mockservice: mockservice,
                    appConstants: appconstants, $routeParams: routeParams,
                    UtilityFactory: utilityfactory, SessionFactory: sessionfactory,
                    BBLSubmissionFactory:bblsubmissionfactory
                });
        });

        it('should test init with no guid available', function () {
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('when status is underreview', function () {

        submissionstatusdata.Status = "underreview";
        beforeEach(function () {
            spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond(submissionstatusdata);
            taxrevenueController = controller('TaxRevenueController',
                {
                    $scope: $scope, $rootScope: rootscope,
                    $location: $location, mockservice: mockservice,
                    appConstants: appconstants, $routeParams: routeParams,
                    UtilityFactory: utilityfactory, SessionFactory: sessionfactory,
                    BBLSubmissionFactory: bblsubmissionfactory
                });
        });

        it('should test', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    });

    describe('when status is not underreview and is fein with no initial data', function () {

        describe('fein with no initial data', function () {
            beforeEach(function () {
                submissionstatusdata.Status = "draft";
                spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
                localStorage.setItem('supportingDocsData', '[{"IsFEIN":true}]');
                httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
                httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond(submissionstatusdata);
                httpBackend.when('POST', basePath + 'api/BBLApplication/TaxRevenueNumber').respond(taxrevenuedata);
                httpBackend.when('POST', basePath + 'api/BBLApplication/TaxValidation').respond(submittaxformdata);
                taxrevenueController = controller('TaxRevenueController',
                {
                    $scope: $scope, $rootScope: rootscope,
                    $location: $location, mockservice: mockservice,
                    appConstants: appconstants, $routeParams: routeParams,
                    UtilityFactory: utilityfactory, SessionFactory: sessionfactory,
                    BBLSubmissionFactory: bblsubmissionfactory
                });
                $scope.vm = taxrevenueController;
                var element = angular.element(
                       '<form name="vm.taxrevenueform">' +
                       '<input ng-model="vm.taxnumber" name="taxnumber" />' +
                       '<input ng-model="vm.taxnumber" name="signature" />' +
                       '</form>'
                       );
                compile(element)($scope);
                form = $scope.vm.taxrevenueform;

            });

            it('should test init', function () {
                httpBackend.flush();
                expect(taxrevenueController.isfein).toBeTruthy();
            });

            it('should test applyMask when given number is fein', function () {
                var e = {};
                e.keyCode = 3;
                taxrevenueController.taxrevenue = {};
                taxrevenueController.taxrevenue.number = "12";
                taxrevenueController.applyMask(e, 'f');
                expect(taxrevenueController.taxrevenue.number).toEqual("12-");
                httpBackend.flush();
            });

            it('should test applyMask when given number is fein', function () {
                var e = {};
                e.keyCode = 3;
                taxrevenueController.taxrevenue = {};
                taxrevenueController.taxrevenue.number = "123";
                taxrevenueController.applyMask(e, 's');
                expect(taxrevenueController.taxrevenue.number).toEqual("123-");
                httpBackend.flush();
            });

            it('should test watch of taxrevenue.number when invalid format is given', function () {
                taxrevenueController.taxrevenue = {};
                taxrevenueController.taxrevenue.number = "123@";
                $scope.$apply();
                expect(form.taxnumber.$valid).toBeFalsy();
            });

            it('should test watch of taxrevenue.number when hyphens are properly provided', function () {
                taxrevenueController.isfein = true;
                taxrevenueController.taxrevenue = {};
                taxrevenueController.taxrevenue.number = "12-311";
                $scope.$apply();
                expect(form.taxnumber.$valid).toBeFalsy();

                taxrevenueController.taxrevenue.number = "12-31111111";
                $scope.$apply();
                expect(form.taxnumber.$valid).toBeTruthy();
            });

            it('should test watch of taxrevenue.number when hyphens are properly provided', function () {
                taxrevenueController.isfein = false;
                taxrevenueController.taxrevenue = {};
                taxrevenueController.taxrevenue.number = "123-11";
                $scope.$apply();
                expect(form.taxnumber.$valid).toBeFalsy();

                taxrevenueController.taxrevenue.number = "12-31111111";
                $scope.$apply();
                expect(form.taxnumber.$valid).toBeTruthy();
            });

            it('should test checkAndExit', function () {
                form.$invalid = true;
                taxrevenueController.taxrevenue = taxrevenueController.prevData = {};
                taxrevenueController.checkAndExit('mybbl');
                expect($location.path).toHaveBeenCalledWith('/mybbl');
            });

            it('should test checkAndExit', function () {
                form.$invalid = true;
                taxrevenueController.taxrevenue = {};
                taxrevenueController.prevData = { "abc": "def" };
                taxrevenueController.currentpage_errors = {};
                taxrevenueController.checkAndExit('path');
            });

            it('should test checkAndExit', function () {
                form.$invalid = false;
                taxrevenueController.taxrevenue = taxrevenueController.prevData = {};
                taxrevenueController.checkAndExit('appchecklist');
                httpBackend.flush();
                expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
            });

            it('should test checkSignWithFullName when fullname and signature matches', function () {
                taxrevenueController.taxrevenue = {};
                taxrevenueController.taxrevenue.signature = "test";
                taxrevenueController.taxrevenue.FullName = "test";
                taxrevenueController.checkSignWithFullName();
                expect(form.signature.$valid).toBeTruthy();
            });
            it('should test checkSignWithFullName when fullname and signature mismatches', function () {
                taxrevenueController.taxrevenue = {};
                taxrevenueController.taxrevenue.signature = "test";
                taxrevenueController.taxrevenue.FullName = "test1";
                taxrevenueController.checkSignWithFullName();
                expect(form.signature.$valid).toBeFalsy();
            });

            it('should test checkSignWithFullName when fullname and signature mismatches', function () {               
                taxrevenueController.checkSignWithFullName();
                expect(taxrevenueController.signatureMismatch).toBeFalsy();
            });

            it('should test setErrorMsg ', function () {
                taxrevenueController.setErrorMsg();
            });

            it('should test stayOnThisPage ', function () {
                taxrevenueController.stayOnThisPage();
            });

        });
    });

});