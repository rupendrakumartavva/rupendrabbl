describe('Profile Logout Controller Test', function () {

    var $scope = {}, controller, httpBackend, mockservice, profilelogoutcontroller;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend) {
        rootscope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        profilelogoutcontroller = controller('ProfileLogoutController', { $scope: $scope, $rootScope: rootscope, $location: $location, requestService: mockservice });
    }));
    //it('should test navToLogin', function () {
    //    $scope.navToLogin();
    //    expect($location.path).toHaveBeenCalledWith('/login');
    //});
});