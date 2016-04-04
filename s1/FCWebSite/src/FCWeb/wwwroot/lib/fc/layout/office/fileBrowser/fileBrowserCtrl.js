(function () {
    'use strict';

    angular
        .module('fc.ui')
        .controller('fileBrowserCtrl', fileBrowserCtrl);

    fileBrowserCtrl.$inject = ['$scope', '$routeParams', 'apiSrv', 'notificationManager'];

    function fileBrowserCtrl($scope, $routeParams, apiSrv, notificationManager) {

        if (!angular.isDefined($scope.fileBrowser)) {
            notificationManager.displayError('File browser model is not defined! ("fileBrowser")');
        }

        $scope.directoryView = {};

        $scope.contentUpload = {
            onUploadSuccess: function (uploadData) {
                //if (angular.isArray(uploadData)) {
                //    setPersonImage(uploadData[0].name);
                //}

                loadData($scope.fileBrowser.path);
            }
        };

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

        $scope.ok = function () {
            if(angular.isFunction($scope.fileBrowser.onOk)) {
                $scope.fileBrowser.onOk($scope.selectedFile);
            }
        };

        $scope.cancel = function () {
            if (angular.isFunction($scope.fileBrowser.onCancel)) {
                $scope.fileBrowser.onCancel();
            }
        };

        function loadData(path) {
            apiSrv.get('/api/filebrowser?path=' + path, null, success, failure);            
        }

        function success(response) {
            $scope.directoryView = response.data;
            $scope.contentUpload.path = response.data.path;
        }

        function failure(response) {
            notificationManager.displayError(response.data);
        }

        loadData($scope.fileBrowser.path);
    }
})();
