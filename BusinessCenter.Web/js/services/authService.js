(function () {

    'use strict';
    angular.module('DCRA').factory("authService", ['$http', '$q', 'localStorageService', 'appConstants', authService]);

    function authService($http, $q, localStorageService, appConstants) {

        var serviceBase = appConstants.apiServiceBaseUri;
        var authServiceFactory = {};

        var _authentication = {
            isAuth: false,
            userName: "",
            useRefreshTokens: false
        };

        var _externalAuthData = {
            provider: "",
            userName: "",
            externalAccessToken: ""
        };

        var _saveRegistration = function (registration) {

            _logOut();

            return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
                return response;
            });

        };

        var _login = function (loginData) {

            var data = "grant_type=password&username=" + loginData.UserName + "&password=" + loginData.Password;

            if (loginData.useRefreshTokens) {
                data = data + "&client_id=" + appConstants.clientId;
            }

            var deferred = $q.defer();

            $http.post(serviceBase + 'authtoken', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
                if (response.status == 'Success') {
                    localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.UserName, refreshToken: response.refresh_token, useRefreshTokens: true, expiresin: response.expires_in, expires_fulldate: response[".expires"], freeToken: false });
                    _authentication.isAuth = true;
                    _authentication.userName = loginData.UserName;
                    _authentication.useRefreshTokens = loginData.useRefreshTokens;
                }
                deferred.resolve(response);
            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        var _logOut = function () {

            var authData = localStorageService.get('authorizationData');
            //var data = { RefreshTokenId: authData.refreshToken }
            //if (authData.freeToken != null) {
            //    $http.post(serviceBase + 'api/RefreshToken/deleterefresh', data).success(function (response) {
            //    });
            //}

            ////localStorageService.remove('authorizationData');

            localStorageService.remove('authorizationData');
            localStorage.clear();
            _authentication.isAuth = false;
            _authentication.userName = "";
            _authentication.useRefreshTokens = false;
            localStorage.loggedin = 0;

        };

        var _fillAuthData = function () {

            var authData = localStorageService.get('authorizationData');
            if (!authData.freeToken || authData.freeToken == null) {                
                _authentication.isAuth = true;
                _authentication.userName = authData.userName;
                _authentication.useRefreshTokens = authData.useRefreshTokens;
            }

        };

        var _refreshToken = function () {
            var deferred = $q.defer();

            var authData = localStorageService.get('authorizationData');

            if (authData) {

                if (authData.useRefreshTokens) {
                    var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + appConstants.clientId;
                    localStorage.setItem("refreshtokenhit", "true");
                    localStorageService.remove('authorizationData');

                    $http.post(serviceBase + 'authtoken', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                        localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true, expiresin: response.expires_in, expires_fulldate: response[".expires"], freeToken: false, guiToken: response.GuiToken });

                        deferred.resolve(response);
                        localStorage.setItem("refreshtokenhit", "false");
                    }).error(function (err, status) {
                        _logOut();
                        localStorage.setItem("refreshtokenhit", "false");
                        deferred.reject(err);
                    });
                }
            }

            return deferred.promise;
        };

        var _freeToken = function () {
            localStorageService.remove('authorizationData');
            var data = "grant_type=password&username=codeit" + "&password=" + undefined;
            var deferred = $q.defer();
            $http.post(serviceBase + 'authtoken', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
                localStorageService.set('authorizationData', { token: response.access_token, refreshToken: "", useRefreshTokens: false, freeToken: true });
                _authentication.isAuth = false;
                _authentication.useRefreshTokens = false;
                deferred.resolve(response);
            }).error(function (err, status) {
                deferred.reject(err);
            });
            return deferred.promise;
        };

        var _obtainAccessToken = function (externalData) {

            var deferred = $q.defer();

            $http.get(serviceBase + 'api/account/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } }).success(function (response) {

                localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false });

                //_authentication.isAuth = true;
                _authentication.userName = response.userName;
                _authentication.useRefreshTokens = false;

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        var _registerExternal = function (registerExternalData) {

            var deferred = $q.defer();

            $http.post(serviceBase + 'api/account/registerexternal', registerExternalData).success(function (response) {

                localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false });

                _authentication.isAuth = true;
                _authentication.userName = response.userName;
                _authentication.useRefreshTokens = false;

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        authServiceFactory.saveRegistration = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.authentication = _authentication;
        authServiceFactory.refreshToken = _refreshToken;
        authServiceFactory.freeToken = _freeToken;
        authServiceFactory.obtainAccessToken = _obtainAccessToken;
        authServiceFactory.externalAuthData = _externalAuthData;
        authServiceFactory.registerExternal = _registerExternal;

        return authServiceFactory;
    }

})();