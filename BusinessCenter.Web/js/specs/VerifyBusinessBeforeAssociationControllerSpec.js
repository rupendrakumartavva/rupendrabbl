describe('Verify Business Before Association Controller Spec', function () {

    var $scope, controller, httpBackend, mockservice, VerifyBusinessBeforeAssociationController, localStore, basePath, routeparams, renewalutility;
    var data = [
        {
            "isValidated": true,
            "entityId": 10000002,
            "licenseNumber": "100112000001",
            "fName": "NA",
            "lName": "NA",
            "businessName": "BILOS MEGA CARE, LLC",
            "tradeName": "",
            "businessNameStructure": "Limited Liability Company",
            "expDate": "10/31/2015",
            "status": "Active"
        }
    ];
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $routeParams, RenewalUtilityFactory,appConstants) {
        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
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
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        renewalutility = RenewalUtilityFactory;
        Object.defineProperty(window, 'localStorage', { value: localStore });
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
        httpBackend.when('POST', basePath + 'api/BBLAssociation/UserServiceDetailsOnId').respond(data);
        VerifyBusinessBeforeAssociationController = controller('VerifyBusinessBeforeAssociationController',
            {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, mockservice: mockservice,
                routeParams: routeparams, RenewalutilityFactory: renewalutility
            });
    }));

   
    it('should test init method', function () {
        httpBackend.flush();
        expect(VerifyBusinessBeforeAssociationController.business.toString()).toEqual(data.toString());
    });

    it('should test navToBBL', function () {
        VerifyBusinessBeforeAssociationController.navToBBL();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/mybbl');
    });

    it('should test navToConfirmAssociation when result is not true', function () {
        console.log = jasmine.createSpy("log");
        httpBackend.when('POST', basePath + 'api/BBLAssociation/AssociateBblService').respond(data);
        VerifyBusinessBeforeAssociationController.business = data;
        VerifyBusinessBeforeAssociationController.navToConfirmAssociation();
        httpBackend.flush();
        expect(console.log).toHaveBeenCalledWith("Error");
    });

    it('should test cancel', function () {
        VerifyBusinessBeforeAssociationController.cancel();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/associatebbl');
    });

    it('should test navToConfirmAssociation when result is true', function () {
        httpBackend.when('POST', basePath + 'api/BBLAssociation/AssociateBblService').respond({ Result: true });
        VerifyBusinessBeforeAssociationController.business = data;
        VerifyBusinessBeforeAssociationController.navToConfirmAssociation();        
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/mybbl');
    });
});