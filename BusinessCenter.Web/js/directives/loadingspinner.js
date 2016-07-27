angular.module('DCRA').factory('spinnerloading', function ($q, $window) {
    return function (promise) {
        return promise.then(function (response) {
            
            $("#spinner").hide();
            return response;
        }, function (response) {
            
            $("#spinner").hide();
            return $q.reject(response);
        });
    };
});