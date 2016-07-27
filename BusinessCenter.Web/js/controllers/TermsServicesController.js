(function(){
	'use strict';
	var controllerId = 'TermsServicesController';
	angular.module('DCRA').controller(controllerId,
		['$scope', '$rootScope','$location', TermsServicesController]);

	function TermsServicesController($scope, $rootScope, $location) {
		//$scope.slides = [];

		init();

		/*
		 * Function: init
		 * init (initialize) method: first method to be executed on controller load.
		 */
		function init() {


		}
		$scope.navToRegister = function(){
			$location.path('/register')
		};
		$scope.navToLogin = function(){
			$location.path('/login')
		};
		$scope.navToSecurityQuestion = function(){
			$location.path('/securityquestion')
		};
		$scope.navToHome = function(){
			$location.path('/home');
		};
	}


})();
