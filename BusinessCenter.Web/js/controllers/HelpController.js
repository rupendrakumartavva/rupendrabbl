(function(){

	'use strict';
	var controllerId = 'HelpController';
	angular.module('DCRA').controller(controllerId,
		['$scope', '$rootScope','$location', HelpController]);

	function HelpController($scope, $rootScope, $location) {

		init();

		/*
		 * Function: init
		 * init (initialize) method: first method to be executed on controller load.
		 */
		function init(){

			equalheight('.eheight');

			$(window).resize(function(){
				equalheight('.eheight');
			});
		}
		$scope.menuClick= function(){
			localStorage.loggedin=0;
			$location.path('/home');

			window.setTimeout(function(){

				$('#myModal .modal-body').html("<h3 class='error_message'>You have successfully logged out.</h3>");
				$('#myModal').modal('show');
			}, 100);
		};
		$scope.navToHome = function(){
			$location.path('/home');
		};
		$scope.navToLogin = function(){
			$location.path('/login');
		};
	}


})();
