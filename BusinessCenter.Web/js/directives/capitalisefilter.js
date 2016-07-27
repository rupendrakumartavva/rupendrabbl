(function () {

    'use strict';
    angular.module('DCRA').filter('capitalize', capitalize);
    function capitalize() {
        return function (input) {
            input = input || '';
            return input.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
        }
    }
})();

(function () {

    angular
		.module("DCRA")
		.filter("format", function () {
		    return function (input) {
		        var args = arguments;
		        return input.replace(/\{(\d+)\}/g, function (match, capture) {
		            return args[1 * capture + 1];
		        });
		    };
		});

})();