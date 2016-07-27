describe('Verify Email Controller Test', function () {

    var $scope, controller, httpBackend, mockservice, verifyemailcontroller, localStore, basePath, authservice;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, authService) {
        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        basePath = appConstants.apiServiceBaseUri;
        authservice = authService;
        spyOn($location, 'path');
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;

        verifyemailcontroller = controller('VerifyEmailController', { $scope: $scope, $rootScope: rootscope, $location: $location, requestService: mockservice });
    }));

    it('should test resendEmail method when user status is success', function () {
        localStorage.usermailinfo = '{"testinfo":"testinfo"}';
        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
        httpBackend.when('POST', basePath + 'api/UserAccounts/ResendMail').respond({ "status": "Success" });
        $scope.resendEmail();
        httpBackend.flush();
        expect($scope.status).toBe("Success");
    });

    it('should test navToQuickSearchResult', function () {
        $scope.navToQuickSearchResult();
        expect($location.path).toHaveBeenCalledWith('/quicksearchresult');
    });

    it('should test navToHelp', function () {
        $scope.navToHelp();
        expect($location.path).toHaveBeenCalledWith('/help');
    });

    it('should test navToRegister', function () {
        $scope.navToRegister();
        expect($location.path).toHaveBeenCalledWith('/register');
    });

    it('should test navToverifyModal', function () {
        httpBackend.when('POST', basePath + 'api/UserAccounts/ResendMail').respond({ "status": "Success" });
        $scope.navToverifyModal();
        httpBackend.flush();
    });

});

