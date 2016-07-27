describe('User Previous Email Confirmation Controller Test', function () {

    var $scope, controller, httpBackend, mockservice, userpreviousemailconfirmationcontroller, basePath, localStore;

    beforeEach(module('DCRA'));

    describe('api hit status is already exists and type is P', function () {
        beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants) {
            rootscope = $rootScope.$new();
            $scope = $rootScope.$new();
            $location = _$location_;
            mockservice = requestService;
            httpBackend = $httpBackend;
            controller = $controller;
            basePath = appConstants.apiServiceBaseUri;
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/UserAccounts/UserPreviousEmail').respond({ "status": "alreadyExists", "type": "P" });
            userpreviousemailconfirmationcontroller = controller('UserPreviousEmailConfirmationController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, requestService: mockservice
            });
        }));

        it('should test UserPreviousEmailConfirmation method when user status is alreadyExists', function () {
            httpBackend.flush();
            expect($scope.status).toEqual("alreadyExists");
        });
    });

    describe('api hit status is already exists and type is N', function () {
        beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants) {
            rootscope = $rootScope.$new();
            $scope = $rootScope.$new();
            $location = _$location_;
            mockservice = requestService;
            httpBackend = $httpBackend;
            controller = $controller;
            basePath = appConstants.apiServiceBaseUri;
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/UserAccounts/UserPreviousEmail').respond({ "status": "alreadyExists", "type": "N" });
            userpreviousemailconfirmationcontroller = controller('UserPreviousEmailConfirmationController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, requestService: mockservice
            });
        }));

        it('should test UserPreviousEmailConfirmation method when user status is alreadyExists', function () {
            httpBackend.flush();
            expect($scope.status).toEqual("alreadyExists");
        });
    });

    describe('api hit status is success and type is P', function () {
        beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants) {
            rootscope = $rootScope.$new();
            $scope = $rootScope.$new();
            $location = _$location_;
            mockservice = requestService;
            httpBackend = $httpBackend;
            controller = $controller;
            basePath = appConstants.apiServiceBaseUri;
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/UserAccounts/UserPreviousEmail').respond({ "status": "success", "type": "P" });
            userpreviousemailconfirmationcontroller = controller('UserPreviousEmailConfirmationController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, requestService: mockservice
            });
        }));

        it('should test UserPreviousEmailConfirmation method when user status is alreadyExists', function () {
            httpBackend.flush();
            expect($scope.status).toEqual("success");
        });
    });

    describe('api hit status is success and type is N', function () {
        beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants) {
            rootscope = $rootScope.$new();
            $scope = $rootScope.$new();
            $location = _$location_;
            mockservice = requestService;
            httpBackend = $httpBackend;
            controller = $controller;
            basePath = appConstants.apiServiceBaseUri;
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/UserAccounts/UserPreviousEmail').respond({ "status": "success", "type": "N" });
            userpreviousemailconfirmationcontroller = controller('UserPreviousEmailConfirmationController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, requestService: mockservice
            });
        }));

        it('should test UserPreviousEmailConfirmation method when user status is alreadyExists', function () {
            httpBackend.flush();
            expect($scope.status).toEqual("success");
        });
    });

    describe('api hit status is linkExpire and type is P', function () {
        beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants) {
            rootscope = $rootScope.$new();
            $scope = $rootScope.$new();
            $location = _$location_;
            mockservice = requestService;
            httpBackend = $httpBackend;
            controller = $controller;
            basePath = appConstants.apiServiceBaseUri;
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/UserAccounts/UserPreviousEmail').respond({ "status": "linkExpire", "type": "P" });
            userpreviousemailconfirmationcontroller = controller('UserPreviousEmailConfirmationController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, requestService: mockservice
            });
        }));

        it('should test UserPreviousEmailConfirmation method when user status is alreadyExists', function () {
            httpBackend.flush();
            expect($scope.status).toEqual("linkExpire");
        });
    });

    describe('api hit status is linkExpire and type is N', function () {
        beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants) {
            
            rootscope = $rootScope.$new();
            $scope = $rootScope.$new();
            $location = _$location_;
            spyOn($location, 'path');
            mockservice = requestService;
            httpBackend = $httpBackend;
            controller = $controller;
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
                    }
                };
            })();

            Object.defineProperty(window, 'localStorage', { value: localStore });
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/UserAccounts/UserPreviousEmail').respond({ "status": "linkExpire", "type": "N" });
            userpreviousemailconfirmationcontroller = controller('UserPreviousEmailConfirmationController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, requestService: mockservice
            });
        }));

        it('should test UserPreviousEmailConfirmation method when user status is alreadyExists', function () {
            httpBackend.flush();
            expect($scope.status).toEqual("linkExpire");
        });

        it('should test navToLogin', function () {
            localStorage.loggedin = 0;
            $scope.navToLogin();
            expect($location.path).toHaveBeenCalledWith("/login");
        });

        it('should test navToLogin', function () {
            localStorage.loggedin = 2;
            $scope.navToLogin();
            expect($location.path).toHaveBeenCalledWith("/dashboard");
        });

        it('should test navToVerifyEmail', function () {
            localStorage.loggedin = 0;
            $scope.navToVerifyEmail();
            expect($location.path).toHaveBeenCalledWith("/verifyemail");
        });
    });

});