'use strict';

/**
 * @ngdoc service
 * @name DCRA.validations_errorsFactory
 * @description validations_errorsFactory
 * # validations_errorsFactory
 * validations_errorsFactory
 */
(function () {
    angular.module('DCRA').factory('errorFactory', ['appConstants', '$http', '$interval', validation_errorfactory]);

    function validation_errorfactory(appConstants, $http, $interval) {

        var errorservice = {}, validationmessages = {}, errormessages = {};

        errorservice.isCountryUS = function (bool) {
            if (bool) {
                return appConstants.patternvalidations.country_US;
            }
            return appConstants.patternvalidations.country_not_US;
        }

        errorservice.setAllFormControlsEmpty = function (form) {
            angular.forEach(form, function (value, key) {
                if (typeof value === 'object' && value.$name != "countrydd" && value.$viewValue != undefined) {
                    form[value.$name].$setViewValue("");
                    $("#" + value.$name).val("");
                }
            });
        }


        errorservice.loadvalidationmessages = function () {
            $http.get(appConstants.apiServiceBaseUri + 'api/BBLApplication/GetAllMessages').then(function (resp) {

                angular.forEach(resp.data, function (value, key) {
                    validationmessages[value.ShortName] = value.ErrrorMessage;
                });

            }, function (resp) {
                console.log(resp);
            });
        }

        errorservice.getErrorMessagesByPage = function () {
            return $http.get('js/errormessages.json').then(function (resp) {
                return resp.data;
            }, function (resp) {
                console.log(resp);
            });
        }

        errorservice.getBBLErrorMessages = function () {
            return validationmessages;
        }

        return errorservice;

    }
})();
