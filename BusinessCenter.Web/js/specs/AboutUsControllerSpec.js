describe('AboutUs Controller Test', function () {

    var $scope = {}, controller, aboutuscontroller;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_) {
        rootscope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
       
        controller = $controller;
        aboutuscontroller = controller('AboutUsController', { $scope: $scope, $rootScope: rootscope, $location: $location });
    }));

    it('should test navToHome', function () {
        $scope.navToHome();
        expect($location.path).toHaveBeenCalledWith('/home');
    });

});