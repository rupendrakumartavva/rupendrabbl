describe('Profile Controller Test', function () {

    var $scope = {}, controller, httpBackend, mockservice, profilecontroller, UserDetails = { "UserId": "localStorage.userId" }, localStore, basepath;

    var userDetails_data={"userDetails":{"Result":{"Claims":[],"Logins":[],"Roles":[{"UserId":"2bfe7e43-d343-4fd5-9997-5bda0b7ce25e","RoleId":"3"}],"Title":"MR.","Address":"","City":"","FirstName":"sanjay","LastName":"iyer","MobileNumber":"","PostalCode":"","State":"","LastLoginDateandTime":"2016-03-17T16:11:09.157","IsActive":true,
        "SecurityQuestion1":"What make of car did you drive for your driver’s license test?","SecurityQuestion2":"In what city did your mother and father meet?","SecurityQuestion3":"Who is your favorite American president?","SecurityAnswer1":"test",
        "SecurityAnswer2":"test","SecurityAnswer3":"test","ActivationCode":"o8Fy+qs7lroEVVrRnpn5vJHpXGMGlEyjFUjTap1xxSLPB3HWbL2fRmgsAZ5aJgYer8h8GzXK079f5bmyakfg4p5l0D2ATr1VdH5ZYq2uFy3cEv0ra66UaP/kLYSqlG6HCyx5MIKA/hc8UX5CkRWKr0FJZ8HpRgVHutBtAQvt5xUlVTtUC7yK63XGrh8eaVn8",
        "ActivationDate":"2016-02-11T10:34:32.253","SecondaryEmail":"","ChangeEmailValidate":"2016-03-17T16:11:09.15","ChangeEmailConfirmed":false,"PreviousEmailValidate":"2016-03-17T16:11:09.15","PreviousEmailConfirmed":true,"IsDelete":false,
        "CreatedDate":"2016-02-10T10:34:30.91","IsForgot":false,"DeleteComment":null,"UpdatedDate":"2016-03-17T16:10:43.087","IsLoggedIn":false,"Email":"sanjayiyer242@gmail.com","EmailConfirmed":true,"PasswordHash"
    :"ALrQfKB/AoGLn6mhcp44KHrX3/aXUBhQZoAhIOLLudtmG+hX0Yb3Z2U8D0XibT1hzQ==","SecurityStamp":"e5ed0c42-04d2-423e-b963-6dcecd214e20"
    ,"PhoneNumber":null,"PhoneNumberConfirmed":true,"TwoFactorEnabled":false,"LockoutEndDateUtc":null,"LockoutEnabled"
    :false,"AccessFailedCount":0,"Id":"2bfe7e43-d343-4fd5-9997-5bda0b7ce25e","UserName":"sanjayiyer242"}
,"Id":2,"Exception":null,"Status":5,"IsCanceled":false,"IsCompleted":true,"CreationOptions":0,"AsyncState"
:null,"IsFaulted":false}}

    beforeEach(module('DCRA'));
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
                },
                loggedin: '2'
            };
        })();
        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
        Object.defineProperty(window, 'localStorage', { value: localStore });
        profilecontroller = controller('ProfileController', { $scope: $scope, $rootScope: rootscope, $location: $location, requestService: mockservice});
    }));
    //it('should test update user method when user status is success', function () {
    //    $scope.UserId = {};
    //    $scope.UserDetails = UserDetails;
    //    $scope.profile = {};
    //    $scope.update_form = {};
    //    $scope.profile.Password = undefined;
    //    httpBackend.when('GET', 'http://localhost:40001/api/UserAccounts/Questions').respond({ "status": "Success" });
    //    httpBackend.when('POST', 'http://localhost:40001/api/UserAccounts/UserDetails').respond(userDetails_data);
    //    httpBackend.when('POST', 'http://localhost:40001/api/UserAccounts/CheckUserEmailProfile').respond({ "Status": "Success" });
    //    httpBackend.when('POST', 'http://localhost:40001/api/UserAccounts/ProfileUpdate').respond({ "status": "Success" });
    //    $scope.update_form.$invalid = false;
    //    $scope.updateProfile();
    //    httpBackend.flush();
    //    expect($scope.status).toBe(undefined);
    //    //expect($location.path).toHaveBeenCalledWith('/profilelogout');
    //});
    it('should test init method', function () {
        httpBackend.when('GET', 'http://localhost:40001/api/UserAccounts/Questions').respond({ "status": "Success" });
        httpBackend.when('POST', 'http://localhost:40001/api/UserAccounts/UserDetails').respond(userDetails_data);
        httpBackend.flush();
    });
    it('should test menuClick method when user status is true', function () {
        httpBackend.when('POST', 'http://localhost:40001/api/Account/Logout').respond({ "status": "True" });
        $scope.menuClick();
        //httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/login');
    });
    it('should test navToRegister', function () {
        $scope.navToRegister();
        expect($location.path).toHaveBeenCalledWith('/register');
    });
    
    it('should test navToLogin', function () {
        localStorage.loggedin = 0;
        $scope.navToLogin();        
        expect($location.path).toHaveBeenCalledWith('/login');
    });
    it('should test navToLogin', function () {
        localStorage.loggedin = 2;
        $scope.navToLogin();
        expect($location.path).toHaveBeenCalledWith('/dashboard');
    });

    it('should test navToDashboard', function () {
        $scope.navToDashboard();        
        expect($location.path).toHaveBeenCalledWith('/dashboard');
    });

    it('should test replaceemail', function () {
        $scope.profile = {};
        $scope.profile.Email = "abc@gmail.com";
        $scope.replaceemail();
        expect($scope.profile.Email).toBe("");
    });

    //it('should test deleteUser', function () {
    //    httpBackend.when('POST', 'http://localhost:40001/api/UserAccounts/delete').respond({ "status": "success" });
    //    $scope.deleteUser();
    //    expect($location.path).toHaveBeenCalledWith('/deleteaccount');
    //});

    it('should test cancel_validation', function () {
        $scope.profile = {};
        $scope.profile.Email = "abc@gmail.com";
        $scope.cancel_validation();
        expect($scope.profile.Email).toBe("");
    });

    //it('should test checkExisting', function (key) {
    //    $scope.profile = {};
    //    $scope.profile.Password = undefined;
    //    $scope.checkExisting(key);
    //    expect($scope.profile.NewPassword).toBe(undefined);
    //});
    
});