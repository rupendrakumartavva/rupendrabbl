describe('Delete Account Controller Test', function () {

    var $scope = {}, controller, httpBackend, mockservice, localStore;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_) {
        rootscope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
        controller = $controller;

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
    }));


    describe('when user is logged in', function () {

        beforeEach(function () {
            deleteaccountcontroller = controller('DeleteAccountController', { $scope: $scope, $rootScope: rootscope, $location: $location });
        });

        it('should test navToRegister', function () {
            $scope.navToRegister();
            expect($location.path).toHaveBeenCalledWith('/register');
        });
    });

});