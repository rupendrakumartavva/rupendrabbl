angular.module('DCRA').directive('toggleIcon', function () {

    return {
        restrict: 'A',
        replace: 'true',
        link: function (scope, element, attrs) {
            element.on('click', function () {
                if ($(element).hasClass("collapseIcon")) {
                    $(element).removeClass("collapseIcon hide-details").addClass("expandIcon see-details");
                }
                else {
                    $(element).removeClass("expandIcon see-details").addClass("collapseIcon hide-details");
                }
            });
        }
    }
});