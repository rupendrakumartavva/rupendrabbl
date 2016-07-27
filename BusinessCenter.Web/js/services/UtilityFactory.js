'use strict';

/**
 * @ngdoc service
 * @name DCRA.UtilityFactory
 * @description 
 * # CommonFactory
 * utilities Factory
 */
//utility factory or utility service

(function () {
    angular.module('DCRA').factory('UtilityFactory', [function () {

        var utilities = {};

        var masterIds = {};

        //Factory Methods

        //Guid
        utilities.getGuid = function () {
            return createGuid();
        };

        //create guid.
        function createGuid() {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                  .toString(16)
                  .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
              s4() + '-' + s4() + s4() + s4();
        }

        utilities.containsMasterId = function (masterid) {
            var MasterIdsobj = utilities.getAllMasterIds();
            var keys = Object.keys(MasterIdsobj)
            for (var i = 0; i < keys.length; i++) {
                if (MasterIdsobj[keys[i]] == masterid)
                    return true;
            }
            return false;
        };

        utilities.getAllMasterIds = function () {
            if (localStorage.getItem("masteridlist"))
                masterIds = JSON.parse(localStorage.getItem("masteridlist"));
            return masterIds;
        };

        utilities.addMasterId = function (guid, masterid) {
            utilities.getAllMasterIds()[guid] = masterid;
        }

        utilities.updateMasterIdsList = function () {
            localStorage.setItem("masteridlist", JSON.stringify(masterIds));
        }

        utilities.getMasterId = function (guid) {
            return utilities.getAllMasterIds()[guid];
        }

        utilities.getGuidByMasterId = function (masterid) {
            var jsonobj = utilities.getAllMasterIds();
            var keys = Object.keys(jsonobj);
            for (var i = 0; i < keys.length; i++) {
                if (jsonobj[keys[i]] == masterid)
                    return keys[i];
            }
            return undefined;
        }

        utilities.getSubmissionStatusObject = function (guid) {
            return {
                masterId: utilities.getMasterId(guid),
                userId: localStorage.getItem('userId')
            }
        }

        utilities.ifGuidAvailable = function (guid) {
            if (utilities.getMasterId(guid) == undefined)
                return false;
            return true;
        }

        utilities.removeGuid = function (guid) {
            delete utilities.getAllMasterIds()[guid];
            utilities.updateMasterIdsList();
        }

        return utilities;
    }]);
})();