angular.module('DCRA').directive('fileuploadctl', function (appConstants, $compile, $sce, $location) {

    return {
        restrict: 'A',
        link: function (scope, element, attrs) {

            element.fileinput({
                maxFileCount: 1, allowedFileExtensions: ['pdf'], maxFileSize: 5120, showPreview: false, uploadUrl: appConstants.apiServiceBaseUri + "api/BBLAssociation/BblServiceDocument",
                uploadExtraData: {
                    "uploadData": attrs.inputdata,
                    "key": attrs.id
                }
            });

            element.on('filebrowse', function (event) {
                // scope.notuploaded[attrs.id] = attrs.id;
            });

            element.on('filecleared', function (event) {
                angular.forEach(scope.notuploaded, function (val, key) {
                    if (attrs.id == val) {
                        scope.notuploaded.splice(key, 1);
                    }
                });
            });

            element.on('fileloaded', function (event, file, previewId, index, reader) {
                scope.notuploaded.push(attrs.id);
            });

            element.on('filebatchuploadsuccess', function (event, data, previewId, index) {
                if (data.response.Status) {
                    $('#r' + attrs.id).show();
                    angular.element(document.getElementById('r' + attrs.id)).html($compile('<span> File Name : </span>' + data.response.FileName + '<img src="../images/remove_doc.png" style="cursor:pointer;" id="remove' + attrs.id + '" ng-click="vm.removeUploadedFile(' + attrs.id + ')" />')(scope));
                    $("#icon" + attrs.id).addClass('glyphicon-ok').removeClass('glyphicon-unchecked');
                    $('#f' + attrs.id).hide();
                } else {
                    console.log("Error");
                }
            });

            element.on('filebatchuploaderror', function (event, data, previewId, index) {
                //if (data.jqXHR.status === 500)
                //    $location.path('/inconvenience');
            });
            element.fileinput('clear');
        }
    }
});