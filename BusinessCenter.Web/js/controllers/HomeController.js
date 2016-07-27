(function(){
	'use strict';

	var controllerId = 'HomeController';
	angular.module('DCRA').controller(controllerId,
		['$scope', '$rootScope','$location', HomeController]);

	function HomeController($scope, $rootScope, $location) {
		init();

		/*
		 * Function: init
		 * init (initialize) method: first method to be executed on controller load.
		 */
		function init() {

			customSlider();
			$scope.navToLogin = function(){
				$location.path('/login');
			};
			$scope.navToAbout = function(){
				$location.path('/aboutus');
			};
			$scope.navToLookup = function(){
				$location.path('/lookup')
			};
		}
		function customSlider(){
			$scope.slides = [
				{ id:1, name : 'John Smith' , designation: 'Restaurant Owner', url: 'images/dc_ss_john_tomson.png'},
				{ id:2, name : 'Steven Tray' , designation: 'Restaurant Owner', url: 'images/dc_ss_manuel_jobs.png'},
				{ id:3, name : 'Laura Pol' , designation: 'Restaurant Owner', url: 'images/dc_ss_monoca_anderson.png'},
				{ id:4, name : 'Ray Donovon' , designation: 'Restaurant Owner', url: 'images/dc_ss_andrew_lopez.png'},
				{ id:5, name : 'Ray ' , designation: 'Restaurant Owner', url: 'images/dc_success_img_dummy.jpg'},
			];
		}
	}
})();




