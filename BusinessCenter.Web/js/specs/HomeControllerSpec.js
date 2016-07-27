describe('Home Controller Test', function () {

    var $scope = {}, controller, httpBackend, mockservice, homecontroller;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend) {
        rootscope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        homecontroller = controller('HomeController', { $scope: $scope, $rootScope: rootscope, $location: $location, requestService: mockservice });
    }));
    it('should test navToLogin', function () {
        $scope.navToLogin();
        expect($location.path).toHaveBeenCalledWith('/login');
    });
    it('should test navToAbout', function () {
        $scope.navToAbout();
        expect($location.path).toHaveBeenCalledWith('/aboutus');
    });
    it('should test navToLookup', function () {
        $scope.navToLookup();
        expect($location.path).toHaveBeenCalledWith('/lookup');
    });
});