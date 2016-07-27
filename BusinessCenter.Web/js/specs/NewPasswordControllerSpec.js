describe('New Password Controller Test', function () {

    var $scope, controller, httpBackend, mockservice, newpasswordcontroller, basePath, authservice, compile, form, popupfactory;

    beforeEach(module('DCRA'));

    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, authService, $compile, popupFactory, localStorageService) {
        rootscope = $rootScope.$new();
        basePath = appConstants.apiServiceBaseUri;
        $scope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
        spyOn($location, 'search').and.returnValue({ "code": "asd15ser2esd5f1da1rdsf52asd1" });
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        authservice = authService;
        compile = $compile;
        popupfactory = popupFactory;
        localstorageservice = localStorageService;
    }));

    //httpBackend.when('POST', basePath + 'api/Account/ForgetPasswordCheck').respond({ "status": "nrfp" });
    describe('it should test api/Account/ForgetPasswordCheck hit with nrfp', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/Account/ForgetPasswordCheck').respond({ "status": "nrfp" });
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            httpBackend.when('GET', "partials/templates/popuptemplate.html").respond(200);
            newpasswordcontroller = controller('NewPasswordController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, requestService: mockservice,
                authService: authservice, popupFactory: popupfactory
            });
        });

        it('should test nrfp status', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/passwordrecheck');
        });
    });

    describe('it should test api/Account/ForgetPasswordCheck hit with success', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/Account/ForgetPasswordCheck').respond({ "status": "success" });
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            httpBackend.when('GET', "partials/templates/popuptemplate.html").respond(200);
            newpasswordcontroller = controller('NewPasswordController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, requestService: mockservice,
                authService: authservice, popupFactory: popupfactory
            });
        });

        it('should test nrfp status', function () {
            httpBackend.flush();
        });
    });

    describe('it should test api/Account/ForgetPasswordCheck hit with Lockout status', function () {

        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/Account/ForgetPasswordCheck').respond({ "status": "Lockout" });
            spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
            httpBackend.when('GET', "partials/templates/popuptemplate.html").respond(200);
            newpasswordcontroller = controller('NewPasswordController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, requestService: mockservice,
                authService: authservice, popupFactory: popupfactory
            });
        });

        it('should test lockout status', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/lockout');
        });
    });

    describe('it should test api/Account/ForgetPasswordCheck hit with linkExpire status', function () {
        beforeEach(function () {
            httpBackend.when('POST', basePath + 'api/Account/ForgetPasswordCheck').respond({ "status": "linkExpire" });
            httpBackend.when('GET', "partials/templates/popuptemplate.html").respond(200);
            newpasswordcontroller = controller('NewPasswordController', { $scope: $scope, $rootScope: rootscope, $location: $location, requestService: mockservice, authservice: authservice });
        });

        it('should test linkExpire status', function () {
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/passwordexpiry');
        });
    });

    describe('it should test api/Account/ForgetPasswordCheck hit with different status', function () {
        beforeEach(function () {
            httpBackend.when('POST', basePath + 'api/Account/ForgetPasswordCheck').respond({ "status": "linkExpire" });
            $scope.register = { Password: "Test@123", ConfirmPassword: "Test@123" };
            httpBackend.when('GET', "partials/templates/popuptemplate.html").respond(200);
            newpasswordcontroller = controller('NewPasswordController', {
                $scope: $scope, $rootScope: rootscope, $location: $location,
                requestService: mockservice, authservice: authservice
            });

            var element = angular.element(
                   '<form name="contact_us">' +
                   '</form>'
                   );
            compile(element)($scope);
            form = $scope.contact_us;

        });

        it('should test newpassword method when form is invalid', function () {
            form.$invalid = true;
            $scope.newpassword();
        });

        it('should test newpassword method when form is valid', function () {
            form.$invalid = false;
            httpBackend.when('POST', basePath + 'api/Account/ValidatePassword').respond("true");
            $scope.newpassword();
            httpBackend.flush();
        });

        it('should test newpassword method when form is valid and response is success', function () {
            form.$invalid = false;
            httpBackend.when('POST', basePath + 'api/Account/ValidatePassword').respond("false");
            httpBackend.when('POST', basePath + 'api/Account/ConfirmForgotPassword').respond({ "status": "success" });
            $scope.newpassword();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/resetsuccessful');
        });

        it('should test newpassword method when form is valid and response is nrfp', function () {
            form.$invalid = false;
            httpBackend.when('POST', basePath + 'api/Account/ValidatePassword').respond("false");
            httpBackend.when('POST', basePath + 'api/Account/ConfirmForgotPassword').respond({ "status": "nrfp" });
            $scope.newpassword();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/passwordrecheck');
        });

        it('should test newpassword method when form is valid and response is other than success and failure', function () {
            form.$invalid = false;
            httpBackend.when('POST', basePath + 'api/Account/ValidatePassword').respond("false");
            httpBackend.when('POST', basePath + 'api/Account/ConfirmForgotPassword').respond({ "status": "falsestatus" });
            $scope.newpassword();
            httpBackend.flush();
            //expect($location.path).toHaveBeenCalledWith('/passwordrecheck');
        });

        it('should test navToRegister', function () {
            $scope.navToRegister();
            expect($location.path).toHaveBeenCalledWith('/register');
        });

        it('should test navToForgotPassword', function () {
            $scope.navToForgotPassword();
            expect($location.path).toHaveBeenCalledWith('/forgotpassword');
        });
    });

});