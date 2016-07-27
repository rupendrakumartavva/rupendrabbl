describe('Page Not Found Controller Spec', function () {

    var $scope, controller, httpBackend, mockservice, pagenotfoundController, localStore;
    var basePath = 'http://localhost:40001/'
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_ ) {
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
                },
                loggedin: '2'
            };
        })();

        Object.defineProperty(window, 'localStorage', { value: localStore });
        controller = $controller;
        VerifyBusinessBeforeAssociationController = controller('PageNotfoundController', { $scope: $scope, $rootScope: rootscope, $location: $location });
    }));

    it('should test PageNotfound', function () {
        console.log = jasmine.createSpy("log");
        $scope.PageNotfound();
        expect(console.log).toHaveBeenCalledWith("Page Not Found");
    });

});