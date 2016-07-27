describe('Forgot Password Controller Test', function () {

    var $scope, controller, httpBackend, mockservice, forgotpasswordcontroller, basePath, form, compile;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, $compile) {
        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        basePath = appConstants.apiServiceBaseUri;
        compile = $compile;
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
        httpBackend.when('GET', basePath + 'api/UserAccounts/Questions').respond(questions);
        forgotpasswordcontroller = controller('ForgotPasswordController', { $scope: $scope, $rootScope: rootscope, $location: $location, requestService: mockservice });

        var element = angular.element(
               '<form name="forgotpassword_form">' +
               '</form>'
               );
        compile(element)($scope);
        form = $scope.forgotpassword_form;

    }));

    var questions = [
        { "id": 1, "question": "In what year was your father born?" },
        { "id": 2, "question": "In what city did your mother and father meet?" },
        { "id": 3, "question": "Who is your favorite American president?" },
        { "id": 4, "question": "Where were you when you first heard about 9/11?" },
        { "id": 5, "question": "What make of car did you drive for your driver’s license test?" },
    ];


    it('should test init', function () {
        httpBackend.flush();
        expect($scope.ValidateUser).toBe("Email");
        expect(angular.equals($scope.Questions, questions)).toBeTruthy();
    });

    it('should test navToQuickSearchResult', function () {
        $scope.navToQuickSearchResult();
        expect($location.path).toHaveBeenCalledWith('/quicksearchresult');
    });

    it('should test navToRegister', function () {
        $scope.navToRegister();
        expect($location.path).toHaveBeenCalledWith('/register');
    });

    it('should test navToforgotusername', function () {
        $scope.navToforgotusername();
        expect($location.path).toHaveBeenCalledWith('/forgotusername');
    });

    it('should test navToforgotpassword', function () {
        $scope.forgotpass = {};
        $scope.navToforgotpassword();
        expect($location.path).toHaveBeenCalledWith('/forgotpassword');
    });

    it('should test checkrightClick', function () {
        var e = { which: 3, target: { href: '' } };
        e.preventDefault = function () {

        };
        $scope.checkrightClick("/testurl", e);
        expect(e.target.href).toBe("/testurl")
    });

    it('should test setPwdErrorMsg', function () {
        $scope.setPwdErrorMsg();
    });

    it('should test forgotpassword_form_validation', function () {
        form.$invalid = false;
        $scope.forgotpass = {
            username: "testname"
        };
        httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
            status: "success", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", Rcount: "3"
        });
        $scope.forgotpassword_form_validation();
    });

    it('should test navToValidateUser when status is success and Rcount is 3', function () {
        $scope.forgotpass = {
            username: "testname"
        };
        httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
            status: "success", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", Rcount: "3"
        });
        $scope.navToValidateUser();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/forgotpassword');
    });

    it('should test navToValidateUser when status is Delete and Rcount is 3', function () {
        $scope.forgotpass = {
            username: "testname"
        };
        httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
            status: "Delete", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", Rcount: "3"
        });
        $scope.navToValidateUser();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/deletestatus');
    });

    it('should test navToValidateUser when status is Delete and Rcount is 3', function () {
        $scope.forgotpass = {
            username: "testname"
        };
        httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
            status: "In-Activate", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", Rcount: "3"
        });
        $scope.navToValidateUser();
        httpBackend.flush();

    });

    it('should test navToValidateUser when status is Re-Register and Rcount is 3', function () {
        $scope.forgotpass = {
            username: "testname"
        };
        httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
            status: "Re-Register", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", Rcount: "3"
        });
        $scope.navToValidateUser();
        httpBackend.flush();
    });

    it('should test navToValidateUser when status is Lockout,Rcount is 3 and ValidateUser is Email', function () {
        $scope.forgotpass = {
            username: "testname"
        };
        $scope.ValidateUser = "Email"
        httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
            status: "Lockout", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", Rcount: "3"
        });
        $scope.navToValidateUser();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/lockoutfrgtpwd');
    });

    it('should test navToValidateUser when status is Lockout,Rcount is 3 and ValidateUser is not Email', function () {
        $scope.forgotpass = {
            username: "testname"
        };
        httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
            status: "Lockout", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", Rcount: "3"
        });
        $scope.navToValidateUser();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/lockoutfrgtpwd');
    });

    it('should test navToValidateUser when status is Lockout,Rcount is 3 and ValidateUser is not Email', function () {
        $scope.forgotpass = {
            username: "testname"
        };
        httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
            status: "NullInput", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", Rcount: "3"
        });
        $scope.navToValidateUser();
        httpBackend.flush();
    });

    it('should test navToValidateUser when status is Lockout,Rcount is 3 and ValidateUser is not Email', function () {
        $scope.forgotpass = {
            username: "testname"
        };
        httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
            status: "", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", Rcount: "3"
        });
        $scope.navToValidateUser();
        httpBackend.flush();
    });

    it('should test navToValidateUser when status is Lockout,Rcount is 4', function () {
        $scope.forgotpass = {
            username: "testname"
        };
        httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
            status: "NullInput", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", Rcount: "4"
        });
        $scope.navToValidateUser();
        httpBackend.flush();
    });

    it('should test navToValidateUser when status is Lockout,Rcount is 0', function () {
        $scope.forgotpass = {
            username: "testname"
        };
        httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
            status: "NullInput", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", Rcount: "0"
        });
        $scope.navToValidateUser();
        httpBackend.flush();
    });

    it('should test setErrorMsg', function () {
        $scope.setErrorMsg("testid");
    });

    it('should test licenseQSpanClick', function () {
        $scope.licenseQSpanClick("checkedid","radioid","testid","testradioid","radio_id_unselect_testid");
    });

    it('should test security_form_validation when form is invalid', function () {
        $scope.security_form = { $invalid: true };
        $scope.security_form_validation();
    });

    it('should test security_form_validation when form is valid', function () {
        $scope.security_form = { $invalid: false };
        $scope.forgotpass = {};
        localStorage.question1 = "In what year was your father born?";
        httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({ "status": "Delete" });
        $scope.security_form_validation();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/deletestatus');
    });

    it('should test forgot Password method when status is Delete', function () {
        $scope.forgotpass = {};
        localStorage.question1 = "In what year was your father born?";
        httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({ "status": "Delete" });
        $scope.forgotPassword();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/deletestatus');
    });

    it('should test forgot Password method when status is success', function () {
        $scope.forgotpass = {};
        localStorage.question1 = "In what year was your father born?";
        httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({ "status": "success" });
        $scope.forgotPassword();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/forgotpasswordstatus');
    });

    it('should test forgot Password method when status is In-Active', function () {
        $scope.forgotpass = {};
        localStorage.question1 = "In what year was your father born?";
        httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({ "status": "In-Active" });
        $scope.forgotPassword();
        httpBackend.flush();
        //expect($location.path).toHaveBeenCalledWith('/forgotpasswordstatus');
    });

    //it('should test forgot Password method when status is success', function () {
    //    $scope.forgotpass = {};
    //    localStorage.question1 = "In what year was your father born?";
    //    spyOn($location, 'search').and.returnValue({ 'userId':"123aske34hb34ui-3nk2q3345bw4", 'code': "asd15ser2esd5f1da1rdsf52asd1", 'type': 'S' });
    //    httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({ "status": "movenewpassword" });
    //    $scope.forgotPassword();
    //    httpBackend.flush();
    //    expect($location.path).toHaveBeenCalledWith('/getnewpassword');
    //    expect($location.search).toEqual({ 'userId': "123aske34hb34ui-3nk2q3345bw4", 'code': "asd15ser2esd5f1da1rdsf52asd1", 'type': 'S' });
    //});

    it('should test forgot Password method when status is Re-Register', function () {
        $scope.forgotpass = {};
        localStorage.question1 = "In what year was your father born?";
        httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({ "status": "Re-Register" });
        $scope.forgotPassword();
        httpBackend.flush();
    });

    it('should test forgot Password method when status is Lockout', function () {
        $scope.forgotpass = {};
        localStorage.question1 = "In what year was your father born?";
        httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({ "status": "Lockout" });
        $scope.forgotPassword();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/lockoutfrgtpwd');
    });

    it('should test forgot Password method when status is invalidans and fail count is 3', function () {
        $scope.forgotpass = {};
        localStorage.question1 = "In what year was your father born?";
        httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
            status: "invalidans", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", failCount: 3
        });
        $scope.forgotPassword();
        httpBackend.flush();
        expect($scope.status).toEqual("invalidans");
    });

    it('should test forgot Password method when status is invalidans and fail count is 4', function () {
        $scope.forgotpass = {};
        localStorage.question1 = "In what year was your father born?";
        httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
            status: "invalidans", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", failCount: 4
        });
        $scope.forgotPassword();
        httpBackend.flush();
        expect($scope.status).toEqual("invalidans");
    });

    it('should test forgot Password method when status is invalidans and fail count is 0', function () {
        $scope.forgotpass = {};
        localStorage.question1 = "In what year was your father born?";
        httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
            status: "invalidans", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", failCount: 0
        });
        $scope.forgotPassword();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/lockoutSQ');
    });

    it('should test forgot Password method when status is invalidans and fail count is 0', function () {
        $scope.forgotpass = {};
        localStorage.question1 = "In what year was your father born?";
        httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
            status: "invalidans", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", failCount: 2
        });
        $scope.forgotPassword();
        httpBackend.flush();
        expect($scope.status).toEqual("invalidans");
    });

    it('should test resendPassword', function () {
        $scope.forgotpass = {};
        localStorage.question1 = "In what year was your father born?";
        httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
            status: "success", question1: questions[0].question,
            question2: questions[1].question, question3: questions[2].question,
            userMail: "test@test.com", failCount: 2
        });
        $scope.resendPassword();
        httpBackend.flush();
    });
});