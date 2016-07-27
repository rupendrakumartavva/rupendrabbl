describe('Help Controller Test', function () {

    var $scope = {}, controller, httpBackend, mockservice, helpcontroller;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend) {
        rootscope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        helpcontroller = controller('HelpController', { $scope: $scope, $rootScope: rootscope, $location: $location, requestService: mockservice });
    }));
    it('should test navToHome', function () {
        $scope.navToHome();
        expect($location.path).toHaveBeenCalledWith('/home');
    });
    it('should test menuClick method when user is loggedout', function () {
        $scope.menuClick();
        expect($location.path).toHaveBeenCalledWith('/home');
    });
    it('should test navToLogin', function () {
        $scope.navToLogin();
        expect($location.path).toHaveBeenCalledWith('/login');
    });
});