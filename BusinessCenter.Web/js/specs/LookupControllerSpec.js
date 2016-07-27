describe('LookUp Controller Test', function () {

    var $scope = {}, controller, httpBackend, mockservice, lookupcontroller;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend) {
        rootscope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        lookupcontroller = controller('LookupController', { $scope: $scope, $rootScope: rootscope, $location: $location, requestService: mockservice });
    }));
    it('should test navToHome', function () {
        $scope.navToHome();
        expect($location.path).toHaveBeenCalledWith('/home');
    });
    it('should test navToRegister', function () {
        $scope.navToRegister();
        expect($location.path).toHaveBeenCalledWith('/register');
    });
    it('should test navToQuickSearch', function () {
        $scope.navToQuickSearch();
        expect($location.path).toHaveBeenCalledWith('/quicksearch');
    });
});