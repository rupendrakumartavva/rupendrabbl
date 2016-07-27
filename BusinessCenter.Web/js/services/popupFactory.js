/**
 * @ngdoc service
 * @name DCRA.popupFactory
 * @description 
 * # popupFactory
 * popupFactory
 */

(function () {
    'use strict';

    angular.module('DCRA').factory('popupFactory', ['errorFactory', '$modal', '$sce', popupfactory]);

    function popupfactory(errorFactory, $modal, $sce) {

        var popupservice = {}, popupInstance = null;

        popupservice.showpopup = function (message, path, configuration) {
            popupInstance = $modal.open({
                templateUrl: 'partials/templates/popuptemplate.html',
                size: undefined,
                controller: function ($scope, $modalInstance, $location, SessionFactory) {
                    $scope.isSingleButton = false;
                    $scope.noButtons = false;
                    $scope.htmlContent = '';

                    if (configuration != undefined) {
                        if(configuration.config.buttons == '1')
                            $scope.isSingleButton = true;
                        else if (configuration.config.buttons == '0')
                            $scope.noButtons = true;
                        if (configuration.config.isHtmlString != undefined && configuration.config.isHtmlString) {
                            $scope.htmlContent = $sce.trustAsHtml(message);
                            message = '';
                        }
                    }
                    $scope.message = message;
                    $scope.ok = function () {
                        $modalInstance.close();
                        SessionFactory.setSessionAsClear();
                        if (path != "") {
                            $location.path('/' + path);
                        }
                    };
                    $scope.cancel = function () {
                        $modalInstance.dismiss('cancel');
                    };
                }
            });
        }

        popupservice.getPopupInstance = function () {
            return popupInstance;
        }

        return popupservice;
    };
})();
