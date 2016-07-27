describe('Account Recovery Controller Test', function () {

    var $scope = {}, controller, accountrecoverycontroller, localStore, authservice;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, authService) {
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
        localStorage.username = "testname";
        authservice = authService;
        accountrecoverycontroller = controller('AccountRecoveryController', {
            $scope: $scope, $rootScope: rootscope,
            $location: $location, authservice: authservice
        });
    }));

    it('should test init', function () {
        expect($scope.username).toBe('testname');
    });

    it('should test navToLogin when isAuth is false', function () {
        $scope.navToLogin();
        expect($location.path).toHaveBeenCalledWith('/login');
    });

    it('should test navToLogin when isAuth is true', function () {
        authservice.authentication = { isAuth: true };
        $scope.navToLogin();
        expect($location.path).toHaveBeenCalledWith('/dashboard');
    });

    it('should test navToforgotusername', function () {
        $scope.navToforgotusername();
        expect($location.path).toHaveBeenCalledWith('/forgotusername');
    });

    it('should test navTosecurityquestion', function () {
        $scope.navTosecurityquestion();
        expect($location.path).toHaveBeenCalledWith('/securityquestion');
    });
});