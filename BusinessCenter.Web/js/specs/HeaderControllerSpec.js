describe('HeaderController Spec', function () {


    var $scope, controller, httpBackend, mockservice, headerController, localStore, authservice, sessionfactory, basePath, appconstants,timeout;

    beforeEach(module('DCRA'));

    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, authService, SessionFactory,$timeout) {
        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        basePath = appConstants.apiServiceBaseUri;
        localStore = (function () {
            var store = {};
            return {
                getItem: function (key) {
                    return store[key];
                },
                setItem: function (key, value) {
                    store[key] = value;
                },
                removeItem: function (key) {
                    delete store[key];
                },
                clear: function () {
                    store = {};
                }
            };
        })();

        Object.defineProperty(window, 'localStorage', { value: localStore });
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        authservice = authService;
        sessionfactory = SessionFactory;
        appconstants = appConstants;
        timeout = $timeout;
    }));

    describe('when page path is home or lookup or aboutus', function () {
        beforeEach(function () {
            spyOn($location, 'path').and.returnValue('/home');
            headerController = controller('HeaderController', {
                $scope: $scope,
                $rootScope: rootscope,
                $location: $location,
                mockservice: mockservice,
                appConstants: appconstants,
                authService: authservice,
                SessionFactory: sessionfactory
            });
        });
        it('should test init', function () {
            expect($scope.mainmenu1).toBeTruthy();
            expect($scope.mainmenu2).toBeFalsy();
            expect($scope.mainmenu3).toBeFalsy();
        });
    });

    describe('when page path is login', function () {
        beforeEach(function () {
            spyOn($location, 'path').and.returnValue('/login');
            headerController = controller('HeaderController', {
                $scope: $scope,
                $rootScope: rootscope,
                $location: $location,
                mockservice: mockservice,
                appConstants: appconstants,
                authService: authservice,
                SessionFactory: sessionfactory,
                timeout: timeout
            });
        });
        it('should test init', function () {
            expect($scope.mainmenu1).toBeFalsy();
            expect($scope.mainmenu2).toBeFalsy();
            expect($scope.mainmenu3).toBeTruthy();
        });
    });

    describe('when page path is profile', function () {
        beforeEach(function () {
            spyOn($location, 'path').and.returnValue('/profile');
            headerController = controller('HeaderController', {
                $scope: $scope,
                $rootScope: rootscope,
                $location: $location,
                mockservice: mockservice,
                appConstants: appconstants,
                authService: authservice,
                SessionFactory: sessionfactory,
                timeout: timeout
            });
        });
        it('should test init', function () {
            expect($scope.mainmenu1).toBeFalsy();
            expect($scope.mainmenu2).toBeFalsy();
            expect($scope.mainmenu3).toBeTruthy();
        });
    });

    describe('when page path is profile', function () {
        beforeEach(function () {
            spyOn($location, 'path').and.returnValue('/mybbl');
            setFixtures('<ul class="smallMenu"><li><a></a></li><li><a></a></li><li><a></a></li><li><a></a></li></ul>');

            localStorage.loggedin = 2;
            headerController = controller('HeaderController', {
                $scope: $scope,
                $rootScope: rootscope,
                $location: $location,
                mockservice: mockservice,
                appConstants: appconstants,
                authService: authservice,
                SessionFactory: sessionfactory,
                timeout: timeout
            });
        });
        it('should test init', function () {
            expect($scope.mainmenu1).toBeFalsy();
            expect($scope.mainmenu2).toBeFalsy();
            expect($scope.mainmenu3).toBeTruthy();
        });

        it('should test navToSearchPage', function () {
            $scope.navToSearchPage();
            expect($location.path).toHaveBeenCalledWith('/searchresult');
        });

        it('should test navToProfile', function () {
            $scope.navToProfile();
            expect($location.path).toHaveBeenCalledWith('/profile');
        });

        it('should test navToHelp', function () {
            $scope.navToHelp();
            expect($location.path).toHaveBeenCalledWith('/help');
        });

        it('should test CancellogoutRedirect', function () {
            $scope.CancellogoutRedirect();
        });

        it('should test logoutRedirect', function () {
            localStorage.setItem('ls.authorizationData', JSON.stringify({ refreshToken: "testtoken" }));
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/Account/Logout').respond({ "status": "True" });
            $scope.logoutRedirect();
            //timeout.flush();
           // expect($location.path).toHaveBeenCalledWith('/login');
        });

    });

});