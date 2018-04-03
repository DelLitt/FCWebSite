(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('contentUpload', contentUpload);

    contentUpload.$inject = ['Upload', 'notificationManager'];

    function contentUpload(Upload, notificationManager) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                    model: '=',
                    multiple: "@"
                },

            link: function link(scope, element, attrs) {
                scope.multiple = true;

                scope.upload = function (file) {

                    if (!angular.isString(scope.model.path)) {
                        notificationManager.displayError('Upload path is not defined!');
                        return;
                    }

                    Upload.upload({
                        url: 'api/filebrowser/upload',
                        file: file,
                        data: { path: scope.model.path },
                    }).then(function (resp) {
                        onUploadSuccess(resp.data);
                    }, function (resp) {
                        onUploadFail(resp.data);
                    }, function (resp) {
                        onUploadProgress(resp);
                        file.progress = Math.min(100, parseInt(100.0 *
                         resp.loaded / resp.total));
                    });
                };

                scope.submit = function () {
                    if (scope.form.file.$valid && scope.file) {
                        scope.upload(scope.file);
                    }

                    if (scope.files && scope.files.length) {
                        for (var i = 0; i < scope.files.length; i++) {
                            scope.upload(scope.files[i]);
                        }
                    }
                };

                function onUploadSuccess(response) {
                    if (angular.isFunction(scope.model.onUploadSuccess)) {
                        scope.model.onUploadSuccess(response);
                    }
                }

                function onUploadFail(response) {
                    if (angular.isFunction(scope.model.onUploadFail)) {
                        scope.model.onUploadFail(response);
                    }

                    notificationManager.displayError('Upload file fails! ' + response);
                }

                function onUploadProgress(event) {
                    var progressPercentage = parseInt(100.0 * event.loaded / event.total);

                    //console.log(progressPercentage);

                    if (angular.isFunction(scope.model.onUploadProgress)) {
                        scope.model.onUploadProgress(progressPercentage);
                    }
                }
            },

            templateUrl: '/lib/fc/layout/office/contentUpload/contentUpload.html'
        }
    }
})();