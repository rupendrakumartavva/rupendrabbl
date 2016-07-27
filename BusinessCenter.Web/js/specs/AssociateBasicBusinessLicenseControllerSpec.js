describe('AssociateBBLController Spec', function () {
    var $scope, controller, authservice, localstorageservice, httpBackend, AssociateBBLController, localStore, mockService, httpBackend, $q, deferred, basePath, routeparams, renewalutilityfac, compile, form;
    var data = {
        "isValidated": true, "entityId": "10000002", "licenseNumber": "100112000001",
        "fName": "NA", "lName": "NA", "businessName": "BILOS MEGA CARE, LLC", "tradeName": "",
        "businessNameStructure": "Limited Liability Company", "expDate": "10/31/2015", "status": "Active"
    };

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $routeParams, RenewalUtilityFactory, appConstants, $compile, authService, localStorageService) {

        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        httpBackend = $httpBackend;
        compile = $compile;
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
        renewalutilityfac = RenewalUtilityFactory;
        authservice = authService;
        localstorageservice = localStorageService;
    }));

    describe('when logged in', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/CheckAssociate').respond(data);
            httpBackend.when('POST', basePath + 'api/BBLAssociation/AssociateBblService').respond({ "Result": "1" });
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });

            AssociateBBLController = controller('AssociateBBLController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, mockService: mockService,
                $routeParams: routeparams, RenewalUtilityFactory: renewalutilityfac,
                authService: authservice, localStorageService: localstorageservice
            });
            $scope.vm = AssociateBBLController;
            var element = angular.element(
                   '<form name="vm.associatebblform">' +
                   '<input ng-model="vm.taxnumber" name="taxnumber" />' +
                   '</form>'
                   );
            compile(element)($scope);
            form = $scope.vm.associatebblform;
            AssociateBBLController.taxnumber = "111-11";
        });

        it('should test toggle pin message', function () {
            httpBackend.flush();
            expect(AssociateBBLController.togglepinvalmsg).toBe(false);
        });

        it('should test navToBBL', function () {
            AssociateBBLController.navToBBL();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navToAssociateBBL', function () {
            spyOn(renewalutilityfac, 'getGuidByRenewalServiceId').and.returnValue('guid');
            AssociateBBLController.pinnumber = 123;
            AssociateBBLController.licnumber = "adfs"
            AssociateBBLController.taxnumber = "Asfd"
            AssociateBBLController.navToAssociateBBL();
            httpBackend.flush();
            expect(AssociateBBLController.pinnumber).toEqual(123);
            expect($location.path).toHaveBeenCalledWith('/verifybusinessbeforeassociation/' + 'guid');
        });

        it('should test cancel', function () {
            AssociateBBLController.cancel();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test cancel', function () {
            AssociateBBLController.hideError();
            expect(AssociateBBLController.togglepinvalmsg).toBeFalsy();
        });

        it('should test scope.watch', function () {
            $scope.$digest();
            expect(form.taxnumber.$valid).toBeFalsy();
        });

        it('should test watch when correct fein number is given', function () {
            AssociateBBLController.taxnumber = "11-1111111";
            $scope.$digest();
            expect(form.taxnumber.$valid).toBeTruthy();
        });

        it('should test watch when correct ssn number is given', function () {
            AssociateBBLController.taxnumber = "111-11-1111";
            $scope.$digest();
            expect(form.taxnumber.$valid).toBeTruthy();
        });

        it('should test watch when correct ssn number is given wrong format', function () {
            AssociateBBLController.taxnumber = "11111-1111";
            $scope.$digest();
            expect(form.taxnumber.$valid).toBeFalsy();
        });

        it('should test watch when correct ssn number is given correct format and restrict overloadedvalues', function () {
            AssociateBBLController.taxnumber = "111-11-11111";
            $scope.$digest();
            expect(form.taxnumber.$valid).toBeTruthy();
            expect(AssociateBBLController.taxnumber).toEqual("111-11-1111");
        });

        it('should test watch when correct fein number is given correct format and restrict overloadedvalues', function () {
            AssociateBBLController.taxnumber = "11-1111111";
            $scope.$digest();
            expect(form.taxnumber.$valid).toBeTruthy();
            expect(AssociateBBLController.taxnumber).toEqual("11-1111111");
        });
    });
});