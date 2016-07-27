'use strict';

/**
 * @ngdoc service
 * @name DCRA.CommonInjectorsFactory
 * @description 
 * # CommonInjectorsFactory
 * CommonInjectorsFactory
 */
angular.module('DCRA').factory('CommonInjectorsFactory', ['$rootScope', 'SessionFactory', 'requestService', '$routeParams', '$location', 'appConstants','UtilityFactory', commonInjectors]);

function commonInjectors($rootScope, SessionFactory, requestService, $routeParams, $location, appConstants, UtilityFactory) {

    var service = {
        $rootScope: $rootScope,
        SessionFactory: SessionFactory,
        requestService: requestService,
        $routeParams: $routeParams,
        $location: $location,
        appConstants: appConstants,
        UtilityFactory: UtilityFactory
    };

    return service;
};