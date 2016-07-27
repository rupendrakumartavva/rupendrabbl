describe('EmailConfirmationController Spec', function () {


    var $scope, controller, httpBackend, mockservice, emailconfirmationController, localStore, basePath, routeparams, utilityfactory;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, $routeParams, UtilityFactory) {
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
                }
            };
        })();

        Object.defineProperty(window, 'localStorage', { value: localStore });
        mockservice = requestService;
        httpBackend = $httpBackend;
        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
        controller = $controller;
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        utilityfactory = UtilityFactory;
        emailconfirmationController = controller('EmailConfirmationController', {
            $scope: $scope, $rootScope: rootscope,
            $location: $location, mockservice: mockservice,
            routeParams: routeparams, UtilityFac: utilityfactory
        });
    }));

    it('should test navToReceipt', function () {
        emailconfirmationController.emailconfirmForm = {};
        emailconfirmationController.emailconfirmForm.$invalid = false;
        httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmitPayment').respond({ "trasactionresult": { "Success": true }, "paymentId": "testid" });
        emailconfirmationController.navToReceipt()
        httpBackend.flush();
        expect(emailconfirmationController.diffbillingaddress).toBeTruthy();
        expect($location.path).toHaveBeenCalledWith('/receipt/guid');
    });

    it('should test navToReceipt when payment failed', function () {
        emailconfirmationController.emailconfirmForm = {};
        emailconfirmationController.emailconfirmForm.$invalid = false;
        httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmitPayment').respond({ "trasactionresult": { "Success": false }, "paymentId": "testid" });
        emailconfirmationController.navToReceipt()
        httpBackend.flush();
        expect(emailconfirmationController.diffbillingaddress).toBeTruthy();
        expect($location.path).toHaveBeenCalledWith('/paymentfailure/guid');
    });

    it('should test toggleCheckbox', function () {
        emailconfirmationController.toggleCheckbox("id");
    });

    it('should test setErrorMsg', function () {
        $scope.setErrorMsg("id");
    });

    it('should test navToChecklist', function () {
        emailconfirmationController.navToChecklist()
        //httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
    });
});