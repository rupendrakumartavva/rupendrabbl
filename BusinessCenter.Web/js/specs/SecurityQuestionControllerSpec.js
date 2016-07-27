describe('it should test Security Questions Controller', function () {

    var scope, controller, httpBackend, mockservice, securityquestionsController, localStore, appConstants, basePath, rootscope, location;
    var questions = [
      { "id": 1, "question": "In what year was your father born?" },
      { "id": 2, "question": "In what city did your mother and father meet?" },
      { "id": 3, "question": "Who is your favorite American president?" },
      { "id": 4, "question": "Where were you when you first heard about 9/11?" },
      { "id": 5, "question": "What make of car did you drive for your driver’s license test?" },
    ];

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants) {
        basePath = appConstants.apiServiceBaseUri;
        rootscope = $rootScope.$new();
        location = _$location_;
        scope = $rootScope.$new();
        spyOn(location, 'path');
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
                },
                loggedin: '2'
            };
        })();

        Object.defineProperty(window, 'localStorage', { value: localStore });
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        appConstants = appConstants;        
    }));

    describe('it should test when user is not logged in', function () {
        beforeEach(function () {
            localStorage.loggedin = '0';
            securityquestionsController = controller('SecurityQuestionController', {
                $scope: scope, $rootScope: rootscope, $location: location,requestService:mockservice
            })
        });
        
        it('should test whether the user is loggedin', function () {
            expect(location.path).toHaveBeenCalledWith('/login');
        });
    });

    describe('it should test when user is logged in', function () {
        beforeEach(function () {
            localStorage.loggedin = '2';
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('GET', basePath + 'api/UserAccounts/Questions').respond(questions);
            securityquestionsController = controller('SecurityQuestionController', {
                $scope: scope, $rootScope: rootscope, $location: location, requestService: mockservice
            });
        });

        it('should test whether the user is loggedin', function () {
            httpBackend.flush();
            expect(scope.Questions).toEqual(questions);
        });

        it('should test navToQuickSearchResult', function () {
            scope.navToQuickSearchResult();
            expect(location.path).toHaveBeenCalledWith('/quicksearchresult');
        });

        it('should test navToRegister', function () {
            scope.navToRegister();
            expect(location.path).toHaveBeenCalledWith('/register');
        });

        it('should test navToLogin when loggedin value is 0 or undefined', function () {
            localStorage.loggedin = 0;
            scope.navToLogin();
            expect(location.path).toHaveBeenCalledWith('/login');
        });

        it('should test navToLogin when loggedin value is 2', function () {
            localStorage.loggedin = '2';
            scope.navToLogin();
            expect(location.path).toHaveBeenCalledWith('/dashboard');
        });

        it('should test navToforgotpassword', function () {
            scope.forgotpass = {};
            scope.navToforgotpassword();
            expect(location.path).toHaveBeenCalledWith('/forgotpassword');
        });

        it('should test navToValidateUser and when status is success', function () {
            scope.forgotpass = {};
            scope.forgotpass.username = "testuser";
            httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
                status: "success", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail:"test@test.com"
            });
            scope.navToValidateUser();
            httpBackend.flush();
            expect(location.path).toHaveBeenCalledWith('/securityquestion');
        });

        it('should test navToValidateUser and when status is Delete', function () {
            scope.forgotpass = {};
            scope.forgotpass.username = "testuser";
            httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
                status: "Delete", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com"
            });
            scope.navToValidateUser();
            httpBackend.flush();
           // expect(location.path).toHaveBeenCalledWith('/securityquestion');
        });

        it('should test navToValidateUser and when status is In-Activate', function () {
            scope.forgotpass = {};
            scope.forgotpass.username = "testuser";
            httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
                status: "In-Activate", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com"
            });
            scope.navToValidateUser();
            httpBackend.flush();
            // expect(location.path).toHaveBeenCalledWith('/securityquestion');
        });

        it('should test navToValidateUser and when status is Re-Register', function () {
            scope.forgotpass = {};
            scope.forgotpass.username = "testuser";
            httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
                status: "Re-Register", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com"
            });
            scope.navToValidateUser();
            httpBackend.flush();
            // expect(location.path).toHaveBeenCalledWith('/securityquestion');
        });

        it('should test navToValidateUser and when status is Lockout', function () {
            scope.forgotpass = {};
            scope.forgotpass.username = "testuser";
            httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
                status: "Lockout", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com"
            });
            scope.navToValidateUser();
            httpBackend.flush();
            // expect(location.path).toHaveBeenCalledWith('/securityquestion');
        });

        it('should test navToValidateUser and when status is NullInput', function () {
            scope.forgotpass = {};
            scope.forgotpass.username = "testuser";
            httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
                status: "NullInput", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com"
            });
            scope.navToValidateUser();
            httpBackend.flush();
            // expect(location.path).toHaveBeenCalledWith('/securityquestion');
        });

        it('should test navToValidateUser and when status is empty', function () {
            scope.forgotpass = {};
            scope.forgotpass.username = "testuser";
            httpBackend.when('POST', basePath + 'api/Account/NewForgotPassword').respond({
                status: "", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com"
            });
            scope.navToValidateUser();
            httpBackend.flush();
            // expect(location.path).toHaveBeenCalledWith('/securityquestion');
        });

        it('should test forgotPassword and when status is success', function () {
            scope.forgotpass = {
                securityanswer1:"ans1",
                securityanswer2:"ans2",
                securityanswer3:"ans3"
            };
            localStorage.question1 = questions[0].question;
            localStorage.question2 = questions[1].question;
            localStorage.question3 = questions[2].question;
            httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
                status: "success", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com"
            });
            scope.forgotPassword();
            httpBackend.flush();
            expect(location.path).toHaveBeenCalledWith('/forgotpasswordstatus');
        });

        it('should test forgotPassword and when status is success', function () {
            scope.forgotpass = {
                securityanswer1: "ans1",
                securityanswer2: "ans2",
                securityanswer3: "ans3"
            };
            localStorage.question1 = questions[0].question;
            localStorage.question2 = questions[1].question;
            localStorage.question3 = questions[2].question;
            //spyOn(location, 'search').and.returnValue({ 'userId': "111-111ef45se67tvv$12385", 'code': "a1g2h3j4k5l67b7g8h8k9l0kh5gb3j3hbej5l7m8bgh9g9" });
            httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
                status: "Delete", UserId: "111-111ef45se67tvv$12385",
                userMail: "test@test.com",code:"a1g2h3j4k5l67b7g8h8k9l0kh5gb3j3hbej5l7m8bgh9g9"
            });
            scope.forgotPassword();
            httpBackend.flush();
            //expect(location.path).toHaveBeenCalledWith('/getnewpassword');
        });

        it('should test forgotPassword and when status is success', function () {
            scope.forgotpass = {
                securityanswer1: "ans1",
                securityanswer2: "ans2",
                securityanswer3: "ans3"
            };
            localStorage.question1 = questions[0].question;
            localStorage.question2 = questions[1].question;
            localStorage.question3 = questions[2].question;
            httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
                status: "In-Active", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com"
            });
            scope.forgotPassword();
            httpBackend.flush();
        });

        it('should test forgotPassword and when status is success', function () {
            scope.forgotpass = {
                securityanswer1: "ans1",
                securityanswer2: "ans2",
                securityanswer3: "ans3"
            };
            localStorage.question1 = questions[0].question;
            localStorage.question2 = questions[1].question;
            localStorage.question3 = questions[2].question;
            httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
                status: "Re-Register", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com"
            });
            scope.forgotPassword();
            httpBackend.flush();
        });

        it('should test forgotPassword and when status is Lockout', function () {
            scope.forgotpass = {
                securityanswer1: "ans1",
                securityanswer2: "ans2",
                securityanswer3: "ans3"
            };
            localStorage.question1 = questions[0].question;
            localStorage.question2 = questions[1].question;
            localStorage.question3 = questions[2].question;
            httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
                status: "Lockout", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com"
            });
            scope.forgotPassword();
            httpBackend.flush();
            expect(location.path).toHaveBeenCalledWith("/lockout");
        });

        it('should test forgotPassword and when status is invalidans and failcount is 3', function () {
            scope.forgotpass = {
                securityanswer1: "ans1",
                securityanswer2: "ans2",
                securityanswer3: "ans3"
            };
            localStorage.question1 = questions[0].question;
            localStorage.question2 = questions[1].question;
            localStorage.question3 = questions[2].question;
            httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
                status: "invalidans", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com",failCount:3
            });
            scope.forgotPassword();
            httpBackend.flush();
            expect(scope.status).toBe("invalidans");
        });

        it('should test forgotPassword and when status is invalidans and failcount is 4', function () {
            scope.forgotpass = {
                securityanswer1: "ans1",
                securityanswer2: "ans2",
                securityanswer3: "ans3"
            };
            localStorage.question1 = questions[0].question;
            localStorage.question2 = questions[1].question;
            localStorage.question3 = questions[2].question;
            httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
                status: "invalidans", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com", failCount: 4
            });
            scope.forgotPassword();
            httpBackend.flush();
            expect(scope.status).toBe("invalidans");
        });

        it('should test forgotPassword and when status is invalidans and failcount is 0', function () {
            scope.forgotpass = {
                securityanswer1: "ans1",
                securityanswer2: "ans2",
                securityanswer3: "ans3"
            };
            localStorage.question1 = questions[0].question;
            localStorage.question2 = questions[1].question;
            localStorage.question3 = questions[2].question;
            httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
                status: "invalidans", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com", failCount: 0
            });
            scope.forgotPassword();
            httpBackend.flush();
            expect(location.path).toHaveBeenCalledWith("/lockout");
        });
       
        it('should test forgotPassword and when status is invalidans and failcount is 2', function () {
            scope.forgotpass = {
                securityanswer1: "ans1",
                securityanswer2: "ans2",
                securityanswer3: "ans3"
            };
            localStorage.question1 = questions[0].question;
            localStorage.question2 = questions[1].question;
            localStorage.question3 = questions[2].question;
            httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
                status: "invalidans", question1: questions[0].question,
                question2: questions[1].question, question3: questions[2].question,
                userMail: "test@test.com", failCount:2
            });
            scope.forgotPassword();
            httpBackend.flush();
            expect(scope.status).toBe("invalidans");
        });

        it('should test resendPassword', function () {
            httpBackend.when('POST', basePath + 'api/Account/ForgotValidation').respond({
                status: "success"
            });
            scope.resendPassword();
            httpBackend.flush();
        });

    });

});