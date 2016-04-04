(function () {
    'use strict';

    angular
        .module('fc.ui')
        .service('fileBrowserSrv', fileBrowserSrv);

    fileBrowserSrv.$inject = ['$rootScope', '$uibModal'];

    function fileBrowserSrv($rootScope, $uibModal) {

        this.open = function (path, onSelect, onCancel) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'lib/fc/office/utils/fileBrowserPopup.html',
                controller: 'fileBrowserPopupCtrl',
                resolve: {
                    fileBrowser : {
                        path: path
                    }
                }
            });

            modalInstance.result.then(function (selectedFile) {
                if (angular.isFunction(onSelect)) {
                    onSelect(selectedFile);
                }
            },
            function () {
                if (angular.isFunction(onCancel)) {
                    onCancel();
                }
            });
        }

    }
})();