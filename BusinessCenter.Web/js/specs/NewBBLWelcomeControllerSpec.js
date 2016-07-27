describe('New BBL Welcome Controller Spec', function () {

    var $scope, controller, httpBackend, mockservice, newbblWelcome, localStore, basePath, sessionfactory, authservice, localstorageservice;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, SessionFactory, authService, localStorageService, appConstants) {
        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
        basePath = appConstants.apiServiceBaseUri;
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
        sessionfactory = SessionFactory;
        authservice = authService;
        localstorageservice = localStorageService;
        spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
        newbblWelcome = controller('NewBBLWelcomeController', {
            $scope: $scope, $rootScope: rootscope,
            $location: $location, SessionFactory: sessionfactory, authService: authservice
        });
    }));

    it('should test navigation to login', function () {
        authservice.authentication = { isAuth: true };
        newbblWelcome.navToLogin();
        expect($location.path).toHaveBeenCalledWith('/dashboard');
    });

    it('should test navigation to login', function () {
        authservice.authentication = { isAuth: false };
        newbblWelcome.navToLogin();
        expect($location.path).toHaveBeenCalledWith('/login');
    });

    it('should test navToMyBBL', function () {
        newbblWelcome.navToMyBBL();
        expect($location.path).toHaveBeenCalledWith('/mybbl');
    });

    it('should test navToPreAppQues', function () {
        newbblWelcome.navToPreAppQues();
        expect($location.path).toHaveBeenCalledWith('/preappquestions/step1');
    });

    it('should test navToExit', function () {
        newbblWelcome.navToExit();
        expect($location.path).toHaveBeenCalledWith('/mybbl');
    });
});