(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('mainTable', mainTable);

    function mainTable() {
        return {
            restrict: 'E',
            replace: true,
            scope: { model: '=' },
            templateUrl: '/lib/fc/layout/mainTable.html'
        }
    }
})();