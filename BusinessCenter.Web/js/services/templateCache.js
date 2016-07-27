angular.module('DCRA').run(function ($rootScope, $templateCache) {
    $rootScope.$on('$routeChangeStart', function (event, next, current) {
        if (typeof (current) !== 'undefined') {
           
            $templateCache.remove(current.templateUrl);
        }
    });
});