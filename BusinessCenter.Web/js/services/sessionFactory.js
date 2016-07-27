/**
 * @ngdoc service
 * @name DCRA.SessionFactory
 * @description 
 * # SessionFactory
 * Session Factory
 */

(function () {

    'use strict';
    angular.module('DCRA').factory('SessionFactory', [function () {

        var service = {};

        var session = {
            isDirty: false,
            isformSubmitted: false
        };

        //Factory Methods

        //Session
        service.getSession = function () {
            return session;
        };

        service.setSessionAsDirty = function () {
            service.getSession().isDirty = true;
        };

        service.setSessionAsClear = function () {
            service.getSession().isDirty = false;
            service.getSession().isformSubmitted = true;
        };

        service.isSessionDirty = function () {
            return service.getSession().isDirty;
        }

        service.isFormSubmitted = function () {
            return service.getSession().isformSubmitted;
        }

        service.setFormAsSubmitted = function () {
            service.getSession().isformSubmitted = true;
        }

        service.setFormAsDirty = function () {
            service.getSession().isformSubmitted = false;
        }

        service.removeUndefinedProperties = function (obj) {
            var keys = Object.keys(obj);
            for (var i = 0; i < keys.length; i++) {
                if (obj[keys[i]] === undefined) {
                    delete obj[keys[i]];
                }
            }
            return obj;
        }

        service.isFormEmpty = function (form) {
            var deleted_data_controls = 0;
            var form_controls = 0;
            angular.forEach(form, function (value, key) {
                if (typeof value === 'object') {
                    form_controls++;
                    if (value.$modelValue == undefined ||
                         value.$modelValue == "US" ||
                         value.$modelValue == "" ||
                         $("#" + value.$name).prop('disabled') ||
                         $("#" + value.$name).is(':hidden')) {
                        deleted_data_controls++;
                    }
                }
            });
            if (deleted_data_controls == form_controls)
                return true;
            return false;
        }

        service.compareObjectsInCurrentSession = function (formObject, objectFromDb) {
            if (service.isSessionDirty()) {
                //console.log(objectFromDb)
                //console.log(formObject)
                if (!angular.equals(service.removeUndefinedProperties(formObject), objectFromDb)) {
                    service.setSessionAsDirty();
                } else {
                    service.setSessionAsClear();
                }
            }
        }

        service.compareObjects = function (formObject, objectFromDb) {
            if (service.isSessionDirty()) {

                if (!angular.equals(service.nullUndefinedProperties(formObject), objectFromDb)) {
                    service.setSessionAsDirty();
                } else {
                    service.setSessionAsClear();
                }
            }
        }

        service.nullUndefinedProperties = function (obj) {
            var keys = Object.keys(obj);
            for (var i = 0; i < keys.length; i++) {
                if (obj[keys[i]] === undefined) {
                    obj[keys[i]] = "";
                }
            }
            return obj;
        }

        return service;
    }]);
})();