describe('Terms Service Controller Test', function () {

    var $scope = {}, controller, httpBackend, mockservice, termsservicecontroller;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend) {
        rootscope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        termsservicecontroller = controller('TermsServicesController', { $scope: $scope, $rootScope: rootscope, $location: $location, requestService: mockservice });
    }));
    it('should test navToRegister', function () {
        $scope.navToRegister();
        expect($location.path).toHaveBeenCalledWith('/register');
    });
    it('should test navToLogin', function () {
        $scope.navToLogin();
        expect($location.path).toHaveBeenCalledWith('/login');
    });
    it('should test navToSecurityQuestion', function () {
        $scope.navToSecurityQuestion();
        expect($location.path).toHaveBeenCalledWith('/securityquestion');
    });
    it('should test navToHome', function () {
        $scope.navToHome();
        expect($location.path).toHaveBeenCalledWith('/home');
    });
});