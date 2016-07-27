(function(){

	'use strict';
	var controllerId = 'LookupController';
	angular.module('DCRA').controller(controllerId,
		['$scope', '$rootScope','$location', LookupController]);


	function LookupController($scope, $rootScope, $location) {

		init();

		/*
		 * Function: init
		 * init (initialize) method: first method to be executed on controller load.
		 */
		function init(){

		}
		$scope.navToHome = function(){
			$location.path('/home');
		};
		$scope.navToRegister = function(){
			$location.path('/register')
		};
		$scope.navToQuickSearch = function(){
			$location.path('/quicksearch');
		};
	}

})();

