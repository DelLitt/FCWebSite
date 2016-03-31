(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('fileBrowserAdapterCtrl', fileBrowserAdapterCtrl);

    fileBrowserAdapterCtrl.$inject = ['$scope', '$routeParams', '$uibModal', 'apiSrv', 'notificationManager'];

    function fileBrowserAdapterCtrl($scope, $routeParams, $uibModal, $uibModalInstance, apiSrv, notificationManager) {      

        $scope.openFileBrowser = function () {

            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'lib/fc/office/utils/filebrowser.html',
                controller: 'fileBrowserCtrl',
                resolve: {
                    selFile: function () {
                        return "dsdsd";
                    }
                }
            });

            modalInstance.result.then(function (selectedImage) {
                $scope.selectedImage = selectedImage;

                //var fnNum = getUrlParam('CKEditorFuncNum');
                //var fileUrl = selectedImage.path;
                //window.opener.CKEDITOR.tools.callFunction(fnNum, fileUrl);
                //window.close();

            }, function () {
                // $log.info('Modal dismissed at: ' + new Date());
            });
        }

        $scope.openFileBrowser();
    }
})();
