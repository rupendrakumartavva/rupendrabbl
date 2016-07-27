(function(){
	'use strict';
	var controllerId = 'SearchResultController';
	angular.module('DCRA').controller(controllerId,
		['$scope', '$rootScope','$location', SearchResultController]);

	function SearchResultController($scope, $rootScope, $location) {
		//$scope.slides = [];
		$scope.lists = [
			{
				id: 'list1',
				collection: [1, 2, 3, 4, 5,6,7,8,9,10]
			}
		];
		init();

		/*
		 * Function: init
		 * init (initialize) method: first method to be executed on controller load.
		 */
		function init() {
		    if (localStorage.loggedin == undefined || localStorage.loggedin == 0) {
		        $location.path('/login');
		    }
		}
		$scope.expandCollapse= function(event,type){
			if($(event.target).hasClass('glyphicon-menu-down')){
				$(event.target).addClass('glyphicon-menu-up').removeClass('glyphicon-menu-down');
				$(event.target).css('background','#444444');
				$(event.target).parent('h3').css('background','#444444');
				$(event.target).parent().parent().find('.accordian-components').show();
			}
			else{
				$(event.target).addClass('glyphicon-menu-down').removeClass('glyphicon-menu-up');
				$(event.target).parent().parent().find('.accordian-components').hide();
				$(event.target).css('background','#787878');
				$(event.target).parent('h3').css('background','#787878');
			}
		};
		$scope.Expandcollapse= function(event,type){
			if($(event.target).parent().find('span').hasClass('glyphicon-menu-down')){
				$(event.target).parent().find('span').addClass('glyphicon-menu-up').removeClass('glyphicon-menu-down');
				$(event.target).parent().css('background','#444444');
				$(event.target).parent().find('span').css('background','#444444');
				$(event.target).parent().parent().find('.accordian-components').show();
			}
			else{
				$(event.target).parent().find('span').addClass('glyphicon-menu-down').removeClass('glyphicon-menu-up');
				$(event.target).parent().parent().find('.accordian-components').hide();
				$(event.target).parent().find('span').css('background','#787878');
				$(event.target).parent().css('background','#787878');
			}
		};
	}

})();
