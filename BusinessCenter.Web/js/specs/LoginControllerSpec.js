describe('Login Controller Test', function () {

    var $scope = {}, controller, httpBackend, userCredentials = { "UserName": "test1234", "Password": "Test@1234" }, mockservice, logincontroller, basePath, localStore, authservice, timerfactory, errorfactory;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, authService, TimerFactory, errorFactory) {
        basePath = appConstants.apiServiceBaseUri;
        rootscope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'search');
        spyOn($location, 'path');
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        authservice = authService;
        timerfactory = TimerFactory;
        errorfactory = errorFactory;
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
        $scope.userCredentials = userCredentials;
        logincontroller = controller('LoginController', {
            $scope: $scope, $rootScope: rootscope,
            $location: $location, requestService: mockservice,
            authservice: authservice, timerfactory: timerfactory, errorfactory: errorfactory
        });
        $scope.login_form = {};

    }));

    var loginDetails = {
        responsewithSuccess: {
            "access_token": "NKuInQ0n90vR7W610QpvECGkGqZvB4VirS_cVUX7TKnGLKn9l6DJbV6Tmdshfk0vSgSt56YY3sz9gsLnIjsJlB_WpdBbB9FlEspaKL7Ip6QZiD2PIxc9IeVo2_foY5v85YVFun0_-CihlvDv2unk87GiGi-nABADZ0D2VgW2U7_1mrz-oGVBVUxsn0u_qWdmkPzlSUHpntTlTXCriBL3aWIIXPFInxftl9Ktb8Tp_GPOZakGAQHJXHVBh6G4vZcRqvORORS7XKHH_bC5nzM8Kxp5Iw263iNzMTHT6JFDByUaDZJ4s9u4RYQqdqgqYy9HGnYJIxvMdc-t2f2IT0nktNDj62VtWB4JTyFisUQJuuqahVdJhk4bX9_Hx2mXwoa3n6_7OcPtiBCpML-X4U0zVg",
            "token_type": "bearer", "expires_in": 1799, "refresh_token": "d92ed7113a20459dbeb29377b63979a1", "as:client_id": "352549070fb44ce793a5343a5f846dcc", "userName": "sanjayiyer242", "status": "Success", "userID": "C4C26C13-5881-4060-8A87-CBC6B1DB9CA9", "userFullName": "sanjay iyer", "RoleCount": "3", "FirstName": "sanjay", "LastName": "iyer", "failCount": "0",
            ".issued": "Thu, 24 Dec 2015 10:14:56 GMT", ".expires": "Thu, 24 Dec 2015 10:44:56 GMT"
        },
        responseWithInactive: {
            "access_token": "NKuInQ0n90vR7W610QpvECGkGqZvB4VirS_cVUX7TKnGLKn9l6DJbV6Tmdshfk0vSgSt56YY3sz9gsLnIjsJlB_WpdBbB9FlEspaKL7Ip6QZiD2PIxc9IeVo2_foY5v85YVFun0_-CihlvDv2unk87GiGi-nABADZ0D2VgW2U7_1mrz-oGVBVUxsn0u_qWdmkPzlSUHpntTlTXCriBL3aWIIXPFInxftl9Ktb8Tp_GPOZakGAQHJXHVBh6G4vZcRqvORORS7XKHH_bC5nzM8Kxp5Iw263iNzMTHT6JFDByUaDZJ4s9u4RYQqdqgqYy9HGnYJIxvMdc-t2f2IT0nktNDj62VtWB4JTyFisUQJuuqahVdJhk4bX9_Hx2mXwoa3n6_7OcPtiBCpML-X4U0zVg",
            "token_type": "bearer", "expires_in": 1799, "refresh_token": "d92ed7113a20459dbeb29377b63979a1", "as:client_id": "352549070fb44ce793a5343a5f846dcc", "userName": "sanjayiyer242", "status": "In-Activate", "userID": "C4C26C13-5881-4060-8A87-CBC6B1DB9CA9", "userFullName": "sanjay iyer", "RoleCount": "3", "FirstName": "sanjay", "LastName": "iyer", "failCount": "0",
            ".issued": "Thu, 24 Dec 2015 10:14:56 GMT", ".expires": "Thu, 24 Dec 2015 10:44:56 GMT"
        },
        responseWithExpireUser: {
            "access_token": "NKuInQ0n90vR7W610QpvECGkGqZvB4VirS_cVUX7TKnGLKn9l6DJbV6Tmdshfk0vSgSt56YY3sz9gsLnIjsJlB_WpdBbB9FlEspaKL7Ip6QZiD2PIxc9IeVo2_foY5v85YVFun0_-CihlvDv2unk87GiGi-nABADZ0D2VgW2U7_1mrz-oGVBVUxsn0u_qWdmkPzlSUHpntTlTXCriBL3aWIIXPFInxftl9Ktb8Tp_GPOZakGAQHJXHVBh6G4vZcRqvORORS7XKHH_bC5nzM8Kxp5Iw263iNzMTHT6JFDByUaDZJ4s9u4RYQqdqgqYy9HGnYJIxvMdc-t2f2IT0nktNDj62VtWB4JTyFisUQJuuqahVdJhk4bX9_Hx2mXwoa3n6_7OcPtiBCpML-X4U0zVg",
            "token_type": "bearer", "expires_in": 1799, "refresh_token": "d92ed7113a20459dbeb29377b63979a1", "as:client_id": "352549070fb44ce793a5343a5f846dcc", "userName": "sanjayiyer242", "status": "expireuser", "userID": "C4C26C13-5881-4060-8A87-CBC6B1DB9CA9", "userFullName": "sanjay iyer", "RoleCount": "3", "FirstName": "sanjay", "LastName": "iyer", "failCount": "0",
            ".issued": "Thu, 24 Dec 2015 10:14:56 GMT", ".expires": "Thu, 24 Dec 2015 10:44:56 GMT"
        },
        responseWithLockout: {
            "access_token": "NKuInQ0n90vR7W610QpvECGkGqZvB4VirS_cVUX7TKnGLKn9l6DJbV6Tmdshfk0vSgSt56YY3sz9gsLnIjsJlB_WpdBbB9FlEspaKL7Ip6QZiD2PIxc9IeVo2_foY5v85YVFun0_-CihlvDv2unk87GiGi-nABADZ0D2VgW2U7_1mrz-oGVBVUxsn0u_qWdmkPzlSUHpntTlTXCriBL3aWIIXPFInxftl9Ktb8Tp_GPOZakGAQHJXHVBh6G4vZcRqvORORS7XKHH_bC5nzM8Kxp5Iw263iNzMTHT6JFDByUaDZJ4s9u4RYQqdqgqYy9HGnYJIxvMdc-t2f2IT0nktNDj62VtWB4JTyFisUQJuuqahVdJhk4bX9_Hx2mXwoa3n6_7OcPtiBCpML-X4U0zVg",
            "token_type": "bearer", "expires_in": 1799, "refresh_token": "d92ed7113a20459dbeb29377b63979a1", "as:client_id": "352549070fb44ce793a5343a5f846dcc", "userName": "sanjayiyer242", "status": "LockedOut", "userID": "C4C26C13-5881-4060-8A87-CBC6B1DB9CA9", "userFullName": "sanjay iyer", "RoleCount": "3", "FirstName": "sanjay", "LastName": "iyer", "failCount": "0",
            ".issued": "Thu, 24 Dec 2015 10:14:56 GMT", ".expires": "Thu, 24 Dec 2015 10:44:56 GMT"
        },
        responseWithDelete: {

            "access_token": "NKuInQ0n90vR7W610QpvECGkGqZvB4VirS_cVUX7TKnGLKn9l6DJbV6Tmdshfk0vSgSt56YY3sz9gsLnIjsJlB_WpdBbB9FlEspaKL7Ip6QZiD2PIxc9IeVo2_foY5v85YVFun0_-CihlvDv2unk87GiGi-nABADZ0D2VgW2U7_1mrz-oGVBVUxsn0u_qWdmkPzlSUHpntTlTXCriBL3aWIIXPFInxftl9Ktb8Tp_GPOZakGAQHJXHVBh6G4vZcRqvORORS7XKHH_bC5nzM8Kxp5Iw263iNzMTHT6JFDByUaDZJ4s9u4RYQqdqgqYy9HGnYJIxvMdc-t2f2IT0nktNDj62VtWB4JTyFisUQJuuqahVdJhk4bX9_Hx2mXwoa3n6_7OcPtiBCpML-X4U0zVg",
            "token_type": "bearer", "expires_in": 1799, "refresh_token": "d92ed7113a20459dbeb29377b63979a1", "as:client_id": "352549070fb44ce793a5343a5f846dcc", "userName": "sanjayiyer242", "status": "Delete", "userID": "C4C26C13-5881-4060-8A87-CBC6B1DB9CA9", "userFullName": "sanjay iyer", "RoleCount": "3", "FirstName": "sanjay", "LastName": "iyer", "failCount": "0",
            ".issued": "Thu, 24 Dec 2015 10:14:56 GMT", ".expires": "Thu, 24 Dec 2015 10:44:56 GMT"
        }
    };


    it('should test form_validate method when user status is success', function () {
        httpBackend.when('POST', basePath + 'authtoken').respond(loginDetails.responsewithSuccess);
        httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond({});
        $scope.login_form.$invalid = false;
        $scope.form_validate();
        httpBackend.flush();
        expect($scope.status).toBe("Success");
        expect($location.path).toHaveBeenCalledWith('/dashboard');
    });

    it('should test form_validate method when user status is success', function () {
        httpBackend.when('POST', basePath + 'authtoken').respond(loginDetails.responseWithDelete);
        httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond({});
        $scope.login_form.$invalid = false;
        $scope.form_validate();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/deletestatus');
    });

    it('should test form_validate method when user status is lockout', function () {
        httpBackend.when('POST', basePath + 'authtoken').respond(loginDetails.responseWithLockout);
        $scope.login_form.$invalid = false;
        $scope.form_validate();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/lockout');
    });

    it('should test form_validate method when user status is In-Activate', function () {
        httpBackend.when('POST', basePath + 'authtoken').respond(loginDetails.responseWithInactive);
        $scope.login_form.$invalid = false;
        $scope.form_validate();
        httpBackend.flush();
        expect($scope.status).toEqual("In-Activate");
    });

    it('should test form_validate method when user status is expireuser', function () {
        httpBackend.when('POST', basePath + 'authtoken').respond(loginDetails.responseWithExpireUser);
        $scope.login_form.$invalid = false;
        $scope.form_validate();
        httpBackend.flush();
        expect($scope.status).toEqual("expireuser");
    });

    it('should test form_validate method when user status is linkExpire', function () {
        loginDetails.responseWithExpireUser.status = "linkExpire";
        httpBackend.when('POST', basePath + 'authtoken').respond(loginDetails.responseWithExpireUser);
        $scope.login_form.$invalid = false;
        $scope.form_validate();
        httpBackend.flush();
        expect($scope.status).toEqual("linkExpire");
    });

    it('should test form_validate method when user status is Failure', function () {
        loginDetails.responseWithExpireUser.status = "Failure";
        loginDetails.responseWithExpireUser.failCount = "3";
        httpBackend.when('POST', basePath + 'authtoken').respond(loginDetails.responseWithExpireUser);
        $scope.login_form.$invalid = false;
        $scope.form_validate();
        httpBackend.flush();
        expect($scope.status).toEqual("Failure");
    });

    it('should test form_validate method when user status is Failure and failcount is 4', function () {
        loginDetails.responseWithExpireUser.status = "Failure";
        loginDetails.responseWithExpireUser.failCount = "4";
        httpBackend.when('POST', basePath + 'authtoken').respond(loginDetails.responseWithExpireUser);
        $scope.login_form.$invalid = false;
        $scope.form_validate();
        httpBackend.flush();
        expect($scope.status).toEqual("Failure");
    });

    it('should test form_validate method when user status is Failure and failcount is 2', function () {
        loginDetails.responseWithExpireUser.status = "Failure";
        loginDetails.responseWithExpireUser.failCount = "2";
        httpBackend.when('POST', basePath + 'authtoken').respond(loginDetails.responseWithExpireUser);
        $scope.login_form.$invalid = false;
        $scope.form_validate();
        httpBackend.flush();
        expect($scope.status).toEqual("Failure");
    });

    it('should test form_validate method when user status is No User', function () {
        loginDetails.responseWithExpireUser.status = "No User";
        httpBackend.when('POST', basePath + 'authtoken').respond(loginDetails.responseWithExpireUser);
        $scope.login_form.$invalid = false;
        $scope.form_validate();
        httpBackend.flush();
        expect($scope.status).toEqual("No User");
    });

    //it('should test form_validate method when rolecount is 4', function () {
    //    var logindata = loginDetails.responsewithSuccess;
    //    logindata.RoleCount = "4";
    //    httpBackend.when('POST', basePath + 'authtoken').respond(logindata);
    //    $scope.login_form.$invalid = false;
    //    $scope.form_validate();
    //    httpBackend.flush();
    //});

    it('should test form_validate method when rolecount is 1', function () {
        var logindata_withrolecount = loginDetails.responsewithSuccess;
        logindata_withrolecount.RoleCount = "1";
        httpBackend.when('POST', basePath + 'authtoken').respond(logindata_withrolecount);
        $scope.login_form.$invalid = false;
        $scope.form_validate();
        httpBackend.flush();
    });

    it('should test navToRegister', function () {
        $scope.navToRegister();
        expect($location.path).toHaveBeenCalledWith('/register');
    });

    it('should test setErrorMsg', function () {
        $scope.setErrorMsg("id");
    });

    it('should test navToForgotPassword', function () {
        $scope.navToForgotPassword();
        expect($location.path).toHaveBeenCalledWith('/forgotpassword');
    });

    it('should test navToForgotUsername', function () {
        $scope.navToForgotUsername();
        expect($location.path).toHaveBeenCalledWith('/forgotusername');
    });

});