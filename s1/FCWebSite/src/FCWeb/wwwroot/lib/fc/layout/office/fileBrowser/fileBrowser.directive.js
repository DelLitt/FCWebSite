(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('fileBrowser', fileBrowser);

    function fileBrowser() {
        return {
            restrict: 'E',
            require: '?ngModel',
            replace: true,
            controller: "fileBrowserCtrl",
            templateUrl: '/lib/fc/layout/office/fileBrowser/fileBrowser.html'
        }
    }
})();