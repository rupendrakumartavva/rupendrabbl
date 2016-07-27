/**
 * @ngdoc service
 * @name DCRA.MAR_validation_Service
 * @description 
 * # MAR_validation_Service
 * MAR_validation_Service
 */


(function () {
    'use strict';
    angular.module('DCRA').factory('MAR_validation_service', ['requestService', 'appConstants', marvalidationservice]);

    function marvalidationservice(requestService, appConstants) {
        var marservice = {};

        marservice.invalid_DC_address = function (street) {
            return {
                Street: street,
                State: 'DC',
                City: 'Washington',
                Country: "US"
            }
        }

        marservice.getTypeAheadData = function (street, spinnerid) {
            return requestService.StreetTypeAhead({ STNAME: street }).then(function (response) {
                return response.data.WebserviceList;
            });
        }

        marservice.getTotalAddress = function (address) {
            address.Street = address.FullAddress;
            if (angular.isDefined(address.ZipCode))
                address.Zip = address.ZipCode;
            address.Country = "US";
            address.State = 'DC';
            address.City = 'Washington';
            address.Unit = address.UnitNumber;
            return address;
        };

        marservice.manageStreet = function (length) {
            if (parseInt(appConstants.marstringlength) == length) {
                return "fillmarAddress";
            }
            else if (parseInt(appConstants.marstringlength) > length) {
                return "smallerstring";
            } else {
                return "morethenrequired"
            }
        }

        marservice.initialDisablingFields = function () {
            return true;
        }

        return marservice;
    }
})();