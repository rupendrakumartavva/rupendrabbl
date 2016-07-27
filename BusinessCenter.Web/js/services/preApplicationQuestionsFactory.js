'use strict';

/**
 * @ngdoc service
 * @name DCRA.preApplicationQuestions
 * @description 
 * # preApplicationQuestions
 * preApplicationQuestions
 */
//preApplicationQuestions

angular.module('DCRA').factory('preApplicationQuestionsFactory', function (UtilityFactory, $routeParams, requestService, $http) {

    var preappquestions = {};

    var apicalls = {
        'step1': 'BusinessActivities',
        'step2': 'PrimaryCategoryList',
        'step3': 'SecondaryCategoryList',
        'step4': 'getsub',
        'step5': 'ScreeningQuestions',
        'step6': 'TotalFees'
    };

    var categoryids = {
        'BusinessActivities': 'ActivityID',
        'PrimaryCategoryList': 'PrimaryID',
        'SecondaryCategoryList': 'Secondary',
        'getsub': 'SubSubCategory',
        'ScreeningQuestions': ''
    };

    var selectedanswers = {};

    preappquestions.getGuid = function () {
        return UtilityFactory.getGuid();
    }

    preappquestions.containsGuid = function () {

    }

    preappquestions.saveSelected = function (id) {
        localStorage.setItem("preAppQuestionsData", JSON.stringify(selectedanswers));
    }

    preappquestions.getSavedData = function (pageid) {
        if (localStorage.getItem(selectedActivities))
            selectedanswers = JSON.parse(localStorage.getItem(selectedActivities));
        return selectedanswers[pageid];
    }

    preappquestions.ifanyanswerSelected = function () {

    }

    preappquestions.getQuestions_Step = function () {
        return requestService.getCurrentStepQeustions(apicalls['step1'], preappquestions.getSavedData('step1')).then(function (response) {
            return response.data;
        }, function () {
            console.log("Error");
        });
    }

    preappquestions.getPrimaryBusinessActivities = function () {
        return requestService.getCurrentStepQeustions('BusinessActivities').then(function (response) {
            return response.data;
        }, function () {
            console.log("Error");
        });
    }

    preappquestions.loadBusinessActivities = function () {
        return $http.get('js/businessactivities.json').then(function (response) {
            return response.data;
        });
    }

    //preappquestions.



    return preappquestions;

});