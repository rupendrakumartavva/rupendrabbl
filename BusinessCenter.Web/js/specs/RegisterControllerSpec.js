describe('Register Controller Test', function () {
    var $scope = {}, controller, httpBackend, mockservice, basePath, appconstants, vcrecaptchaservice, authservice;
    var questions = [
       { "id": 1, "question": "In what year was your father born?" },
       { "id": 2, "question": "In what city did your mother and father meet?" },
       { "id": 3, "question": "Who is your favorite American president?" },
       { "id": 4, "question": "Where were you when you first heard about 9/11?" },
       { "id": 5, "question": "What make of car did you drive for your driver’s license test?" },
    ];

    var registrationData = {

        registrationResponse: {
            "status": "success",
            "mailid": "test.balaji@gmail.com",
            "userId": "e6a290e3-65c8-4cbe-96f9-d1d746a2c6e0",
            "code": "3mjOT+Y1rWSPWzAu0oF65fDymTepUKc26lcEWotJsJivwtS7u378nT+Fn5vEECn3QRry9sxdXoD/VwP3V3JCQskzv7SDMDIu7duZEvB4j7zybdZtMlyePjxhDwD19yl1cub8CIg6P+SWvTwKXda3htWley7Kr0bvRjC10a8cQ3ghKdwGSEna/ERQBAkS4Q8K",
            "exception": "",
            "FullName": "asdf adsf"
        },
        registrationDetails: {
            Email: "test.balaji@gmail.com",
            FirstName: "asdf",
            LastName: "adsf",
            Password: "test@1234",
            SecurityAnswer1: "asdf",
            SecurityAnswer2: "sdf",
            SecurityAnswer3: "sadf",
            SecurityQuestion1: "In what city did your mother and father meet?",
            SecurityQuestion2: "In what city or town does your nearest sibling live?",
            Title: "MR.",
            UserName: "test12345",
            securityquestion3: "What is the name of your favorite book?"
        }
    }


    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, _$httpBackend_, requestService, appConstants, vcRecaptchaService, authService) {
        rootscope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
        mockservice = requestService;
        httpBackend = _$httpBackend_;
        controller = $controller;
        $scope.registration_form = {};
        basePath = appConstants.apiServiceBaseUri;
        authservice = authService;
        vcrecaptchaservice = vcRecaptchaService;
        appconstants = appConstants;
        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
        httpBackend.when('GET', basePath + 'api/UserAccounts/Questions').respond(questions);
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

        registercontroller = controller('RegisterController', {
            $scope: $scope,
            $rootScope: rootscope,
            $location: $location,
            requestService: mockservice,
            vcRecaptchaService: vcrecaptchaservice,
            appConstants: appconstants,
            authService: authservice
        });
    }));

    it('should test init', function () {
        httpBackend.flush();
        expect(angular.equals($scope.Questions, questions)).toBeTruthy();
    });

    it('should test setResponse', function () {
        $scope.setResponse("testresponse")
        httpBackend.flush();
        expect($scope.responseValue).toBe("testresponse");
    });

    it('should test setResponse', function () {
        $scope.setWidgetId("testwidget")
        httpBackend.flush();
        expect($scope.widgetId).toBe("testwidget");
    });

    it('should test navToVerifyEmail', function () {
        $scope.navToVerifyEmail();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/verifyemail');
    });

    it('should test navToTerms', function () {
        $scope.navToTerms();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/termsofservices');
    });

    it('should test createuser method when status is success', function () {
        $scope.register = registrationData.registrationDetails;
        httpBackend.when('POST', basePath + 'api/UserAccounts/UserReCaptcha').respond({ "status": "success" });
        httpBackend.when('POST', basePath + 'api/UserAccounts/create').respond(registrationData.registrationResponse);
        $scope.registration_form.$invalid = false;
        $scope.reg_form_validate();
        httpBackend.flush();
        expect($scope.status).toBe("success");
        expect($location.path).toHaveBeenCalledWith('/verifyemail');
    });

    it('should test setErrorMsg', function () {
        $scope.setErrorMsg();
        httpBackend.flush();
    });

    //it('should test reg_form_validate', function () {
    //    httpBackend.when('POST', basePath + 'api/UserAccounts/UserReCaptcha').respond({ "status": "success" });
    //    httpBackend.when('POST', basePath + 'api/UserAccounts/create').respond(registrationData.registrationResponse);
    //    $scope.registration_form.$invalid = true;
    //    $scope.reg_form_validate('register');
    //    httpBackend.flush();
    //});

});