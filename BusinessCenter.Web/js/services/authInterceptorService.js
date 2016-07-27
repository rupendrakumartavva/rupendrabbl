(function () {
    'use strict';
    var factoryId = 'authInterceptorService';
    angular.module('DCRA').factory(factoryId, ['$q', '$injector', 'localStorageService', '$location', authInterceptorService]);

    function authInterceptorService($q, $injector, localStorageService, $location) {

        var authInterceptorServiceFactory = {};

        var _request = function (config) {

            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            //if (authData == null)
            //{ var authData = localStorageService.get('authorizationData1'); }
            //console.log("Main:"+JSON.stringify(authData));
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
                config.headers.refreshTokenId = authData.refreshToken;
                // config.headers["Access-Control-Expose-Headers"] = 'expiretime';
                //console.log("Accessed");
            }
            return config;
        }


        var _responseError = function (rejection) {
            var deferred = $q.defer();
            if (rejection.status === 401 || rejection.status === 400) {
                var authService = $injector.get('authService');
                //if (localStorageService.get('authorizationData') != null && localStorageService.get('authorizationData').expires_fulldate != null) {
                //    var authExpiration = localStorageService.get('authorizationData').expires_fulldate;
                //    //if (moment().unix() - moment(authExpiration).unix() < 2 * 30 * 1000) {
                //    //    authService.refreshToken().then(function () {
                //    //        _retryHttpRequest(rejection.config, deferred);
                //    //    }, function () {
                //    //        authService.logOut();
                //    //        $location.path('/login');
                //    //        deferred.reject(rejection);
                //    //    });
                //    //}
                //} else {
                authService.logOut();
                $location.path('/login');
            }
            //else if (rejection.status === 500) {
            //    $location.path('/inconvenience');
            //}
            //} else {
            //    deferred.reject(rejection);
            //}
            return deferred.promise;
        }

        //var _retryHttpRequest = function (config, deferred) {
        //    var $http = $http || $injector.get('$http');
        //    $http(config).then(function (response) {
        //        deferred.resolve(response);
        //    }, function (response) {
        //        deferred.reject(response);
        //    });
        //}

        authInterceptorServiceFactory.request = _request;
        //authInterceptorServiceFactory.response = _response;
        authInterceptorServiceFactory.responseError = _responseError;

        return authInterceptorServiceFactory;
    }
})();