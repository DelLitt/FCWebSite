﻿(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('fileBrowserPopupCtrl', fileBrowserPopupCtrl);

    fileBrowserPopupCtrl.$inject = ['$scope', '$uibModalInstance', 'fileBrowser'];

    function fileBrowserPopupCtrl($scope, $uibModalInstance, fileBrowser) {

        $scope.fileBrowser = {
            path: fileBrowser.path,
            root: fileBrowser.root,
            allowRemove: fileBrowser.allowRemove,
            multiple: fileBrowser.multiple,
            disableSubmit: fileBrowser.disableSubmit,
            onOk: function (selectedFile) {
                $uibModalInstance.close(selectedFile);
            },
            onCancel: function () {
                $uibModalInstance.dismiss('cancel');
            },
            options: fileBrowser.options
        };
    }
})();
