﻿(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('fileBrowserAdapterCtrl', fileBrowserAdapterCtrl);

    fileBrowserAdapterCtrl.$inject = ['$scope', 'configSrv'];

    function fileBrowserAdapterCtrl($scope, configSrv) {

        configSrv.loadConfigOffice(configLoaded);

        function configLoaded() {
            
        }

        $scope.fileBrowser = {
            path: 'images/store',
            root: 'images/store',
            allowRemove: true,
            multiple: true,
            onOk: function (selectedFile) {

                if (angular.isDefined(window)
                    && angular.isDefined(window.opener)
                    && angular.isDefined(window.opener.CKEDITOR)) {

                    var fnNum = $scope.getUrlParam('CKEditorFuncNum');
                    window.opener.CKEDITOR.tools.callFunction(fnNum, '/' + selectedFile.path);
                    window.close();
                }
            },
            onCancel: function () {

                if (angular.isDefined(window)) {
                    window.close();
                }
            }
        };

        // Helper function to get parameters from the query string.
        $scope.getUrlParam = function (paramName) {
            var reParam = new RegExp('(?:[\?&]|&)' + paramName + '=([^&]+)', 'i');
            var match = window.location.search.match(reParam);

            return (match && match.length > 1) ? match[1] : null;
        }
    }
})();
