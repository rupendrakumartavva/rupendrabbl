/**
 * @ngdoc service
 * @name DCRA.BBLSubmissionService
 * @description 
 * # BBLSubmissionService
 * BBLSubmissionService
 */

(function () {
    'use strict';

    angular.module('DCRA').service('BBLSubmissionFactory', ['UtilityFactory', '$location', bblsubmissionfactory]);

    function bblsubmissionfactory(UtilityFactory, $location) {


        var bblsubmission = {};

        bblsubmission.checkSubmissionStatus = function (data) {
            if (data.Status.toUpperCase() == 'UNDERREVIEW' || data.Status.toUpperCase() == 'ACTIVE')
                return true;
            return false;
        }

        bblsubmission.invalidSubmission = function () {
            $location.path('/mybbl');
        }

        bblsubmission.noGuidAvailable = function () {
            bblsubmission.invalidSubmission();
        }

        return bblsubmission


    };
})();

