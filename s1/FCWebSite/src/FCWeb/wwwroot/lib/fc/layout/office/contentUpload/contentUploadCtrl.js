(function () {
    'use strict';

    angular
        .module('fc.ui')
        .controller('contentUploadCtrl', contentUploadCtrl);

    contentUploadCtrl.$inject = ['$scope', 'Upload', 'notificationManager'];

    function contentUploadCtrl($scope, Upload, notificationManager) {

        //$scope.upload = function (file) {

        //    if (!angular.isString($scope.contentUpload.path)) {
        //        notificationManager.displayError('Upload path is not defined!');
        //        return;
        //    }

        //    Upload.upload({
        //        url: 'api/filebrowser/upload',
        //        file: file,
        //        data: { path: $scope.contentUpload.path },
        //    }).then(function (resp) {
        //        onUploadSuccess(resp.data)
        //    }, function (resp) {
        //        onUploadFail(resp.data)
        //    }, function (resp) {
        //        onUploadProgress(resp)
        //    });
        //};

        //$scope.submit = function () {
        //    if ($scope.form.file.$valid && $scope.file) {
        //        $scope.upload($scope.file);
        //    }
        //};

        //function onUploadSuccess(response) {
        //    if (angular.isFunction($scope.contentUpload.onUploadSuccess)) {
        //        $scope.contentUpload.onUploadSuccess(response);
        //    }
        //}

        //function onUploadFail(response) {
        //    if (angular.isFunction($scope.contentUpload.onUploadFail)) {
        //        $scope.contentUpload.onUploadFail(response);
        //    }

        //    notificationManager.displayError('Upload file fails! ' + response);
        //}

        //function onUploadProgress(event) {
        //    var progressPercentage = parseInt(100.0 * event.loaded / event.total);

        //    if (angular.isFunction($scope.contentUpload.onUploadProgress)) {
        //        $scope.contentUpload.onUploadProgress(progressPercentage);
        //    }            
        //}
    }
})();
