describe('Forgot UserName Controller Test', function () {

    var $scope = {}, controller, httpBackend, mockservice, forgotusernamecontroller, localStore, basePath, popupfactory, authservice, localstorageservice;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, authService, popupFactory, localStorageService) {
        rootscope = $rootScope.$new();
        basePath = appConstants.apiServiceBaseUri;
        $location = _$location_;
        spyOn($location, 'path');
        mockservice = requestService;
        httpBackend = $httpBackend;
        authservice = authService;
        popupfactory = popupFactory;
        controller = $controller;
        localstorageservice = localStorageService;
        $scope.contact_us = {};
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
        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
        
        spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
        forgotusernamecontroller = controller('ForgotUsernameController', {
            $scope: $scope, $rootScope: rootscope,
            $location: $location, requestService: mockservice,
            authService: authservice, popupFactory: popupfactory
        });
        
    }));

    var forgotusername_response = {
        "status": "Delete", "userID": 0, "question1": "", "question2": "",
        "question3": "", "FullName": "test test", "UserName": "test1234", "Rcount": "3"
    }

    it('should test CheckUserEmail method with email validation', function () {
        $scope.forgotusername = {};
        $scope.forgotusername.email = "testgmail.com";
        $scope.CheckUserEmail();
    });

    it('should test CheckUserEmail method with email is not defined', function () {
        $scope.forgotusername = {};
        $scope.CheckUserEmail();
    });

    it('should test CheckUserEmail method when user status is success', function () {
        spyOn(popupfactory, 'showpopup').and.returnValue(true);
        $scope.forgotusername = {};
        forgotusername_response.status = "success";
        $scope.forgotusername.email = "test@gmail.com";
        httpBackend.when('POST', basePath + 'api/Account/ForgotUserName').respond(forgotusername_response);
        $scope.CheckUserEmail();
        
        httpBackend.flush();
        
        
        expect(rootscope.UserName1).toBe('test1234');
    });

    it('should test CheckUserEmail method when user status is Delete', function () {
        $scope.forgotusername = {};
        $scope.forgotusername.email = "test@gmail.com";
        forgotusername_response.status = "Delete";
        httpBackend.when('POST', basePath + 'api/Account/ForgotUserName').respond(forgotusername_response);
        $scope.CheckUserEmail();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/deletestatus');
    });

    it('should test CheckUserEmail method when user status is In-Active', function () {
        forgotusername_response.status = "In-Activate";
        httpBackend.when('POST', basePath + 'api/Account/ForgotUserName').respond(forgotusername_response);
        $scope.CheckUserEmail();
        httpBackend.flush();
    });

    it('should test CheckUserEmail method when user status is Re-Register', function () {
        forgotusername_response.status = "Re-Register";
        httpBackend.when('POST', basePath + 'api/Account/ForgotUserName').respond(forgotusername_response);
        $scope.CheckUserEmail();
        httpBackend.flush();
    });

    it('should test CheckUserEmail method when user status is Lockout', function () {
        forgotusername_response.status = "Lockout";
        httpBackend.when('POST', basePath + 'api/Account/ForgotUserName').respond(forgotusername_response);
        $scope.CheckUserEmail();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/lockoutfrgtuser');
    });

    it('should test CheckUserEmail method when user status is empty', function () {
        forgotusername_response.status = "";
        httpBackend.when('POST', basePath + 'api/Account/ForgotUserName').respond(forgotusername_response);
        $scope.CheckUserEmail();
        httpBackend.flush();
    });

    it('should test CheckUserEmail method when Rcount is 1', function () {
        forgotusername_response.Rcount = 1;
        httpBackend.when('POST', basePath + 'api/Account/ForgotUserName').respond(forgotusername_response);
        $scope.CheckUserEmail();
        httpBackend.flush();
    });

    it('should test CheckUserEmail method when Rcount is 4', function () {
        forgotusername_response.Rcount = 4;
        httpBackend.when('POST', basePath + 'api/Account/ForgotUserName').respond(forgotusername_response);
        $scope.CheckUserEmail();
        httpBackend.flush();
    });

    it('should test navToQuickSearchResult', function () {
        $scope.navToQuickSearchResult();
        expect($location.path).toHaveBeenCalledWith('/quicksearchresult');
    });

    it('should test navToRegister', function () {
        $scope.navToRegister();
        expect($location.path).toHaveBeenCalledWith('/register');
    });

    it('should test setErrorMsg', function () {
        $scope.setErrorMsg();
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