/**
 * @ngdoc service
 * @name DCRA.RenewalUtilityFactory
 * @description 
 * # RenewalUtilityFactory
 * RenewalUtility Factory
 */
(function () {
    'use strict';
    angular.module('DCRA').factory('RenewalUtilityFactory', ['UtilityFactory', '$location', renewalutilityfactory]);
    function renewalutilityfactory(UtilityFactory, $location) {

        var renewalApps = {}, renewalService = {};

        renewalService.getGuid = function () {
            return UtilityFactory.getGuid();
        };

        renewalService.getAllRenewals = function () {
            if (localStorage.getItem("renewalApps"))
                renewalApps = JSON.parse(localStorage.getItem("renewalApps"));
            return renewalApps;
        };

        renewalService.addRenewalApplication = function (guid, data) {
            renewalService.getAllRenewals()[guid] = data;
        };

        renewalService.updateRenewals = function () {
            localStorage.setItem("renewalApps", JSON.stringify(renewalApps));
        };

        renewalService.containsRenewalServiceId = function (entityid) {
            var renewals = renewalService.getAllRenewals();
            var keys = Object.keys(renewals);
            for (var i = 0; i < keys.length; i++) {
                if (renewals[keys[i]] == entityid)
                    return true;
            }
            return false;
        };

        renewalService.getGuidByRenewalServiceId = function (entityid) {
            var allRenewals = renewalService.getAllRenewals();
            var keys = Object.keys(allRenewals);
            for (var i = 0; i < keys.length; i++) {
                if (allRenewals[keys[i]] == entityid)
                    return keys[i];
            }
            return undefined;
        };

        renewalService.getRenewalServiceId = function (guid) {
            return renewalService.getAllRenewals()[guid];
        }

        renewalService.getRenewalObject = function (guid) {
            return {
                UserBblAssociateId: renewalService.getRenewalServiceId(guid),
                UserId: localStorage.getItem('userId')
            }
        }

        renewalService.ifGuidAvailable = function (guid) {
            if (renewalService.getRenewalServiceId(guid) == undefined)
                return false;
            return true;
        }

        renewalService.removeGuid = function (guid) {
            delete renewalService.getAllRenewals()[guid];
            renewalService.updateRenewals();
        }

        renewalService.noGuid = function () {
            $location.path('/mybbl');
        }

        return renewalService;

    };
})();
