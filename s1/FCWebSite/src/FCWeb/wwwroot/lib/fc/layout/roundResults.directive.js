(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('roundResults', roundResults);

    function roundResults() {
        return {
            restrict: 'E',
            replace: true, 
            scope: { model: '=' },
            templateUrl: '/lib/fc/layout/roundResults.html'
        }
    }
})();