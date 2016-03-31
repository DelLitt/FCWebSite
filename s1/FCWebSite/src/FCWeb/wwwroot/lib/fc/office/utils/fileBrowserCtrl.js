(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('fileBrowserCtrl', fileBrowserCtrl);

    fileBrowserCtrl.$inject = ['$scope', '$routeParams', '$uibModalInstance', 'apiSrv', 'notificationManager', 'Upload'];

    function fileBrowserCtrl($scope, $routeParams, $uibModalInstance, apiSrv, notificationManager, Upload) {

        $scope.directoryView = {};
        $scope.uploadFile = {};
        $scope.selectedFile = $scope.initialItem;

        $scope.selectFile = function selectFile(file) {
            $scope.selectedFile = file;
        }

        $scope.openFolder = function openFolder(path) {
            loadData(path);
        }

        // upload later on form submit or something similar
        $scope.submit = function () {
            if ($scope.form.file.$valid && $scope.file) {
                $scope.upload($scope.file);
            }
        };

        // upload on file select or drop
        $scope.upload = function (file) {
            Upload.upload({
                url: 'api/filebrowser/upload',
                file: file,
                data: { path: $scope.directoryView.path },
            }).then(function (resp) {
                loadData($scope.directoryView.path);
                //console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data);
            }, function (resp) {
                console.log('Error status: ' + resp.status);
            }, function (evt) {
                var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                //console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
            });
        };

        // for multiple files:
        $scope.uploadFiles = function (files) {
            if (files && files.length) {
                for (var i = 0; i < files.length; i++) {
                    //Upload.upload({..., data: {file: files[i]}, ...})...;
                }
                // or send them all together for HTML5 browsers:
                //Upload.upload({..., data: {file: files}, ...})...;
            }
        };

        $scope.ok = function () {
            $uibModalInstance.close($scope.selectedFile.path);

            if (window && window.opener && window.opener.CKEDITOR) {

                var fnNum = $scope.getUrlParam('CKEditorFuncNum');
                var fileUrl = $scope.selectedFile.path;
                debugger;
                window.opener.CKEDITOR.tools.callFunction(fnNum, '/' + fileUrl);
                window.close();
            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');

            if (window) {
                window.close();
            }
        };

        // Helper function to get parameters from the query string.
        $scope.getUrlParam = function (paramName) {
            var reParam = new RegExp('(?:[\?&]|&)' + paramName + '=([^&]+)', 'i');
            var match = window.location.search.match(reParam);

            return (match && match.length > 1) ? match[1] : null;
        }

        function loadData(path) {
            apiSrv.get('/api/filebrowser?path=' + path, null, success, failure);
        }

        function success(response) {
            $scope.directoryView = response.data;
        }

        function failure(response) {
            notificationManager.displayError(response.data);
        }

        loadData("images/store");
    }
})();
