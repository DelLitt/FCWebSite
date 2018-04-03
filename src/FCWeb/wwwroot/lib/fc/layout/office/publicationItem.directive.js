(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('publicationItem', publicationItem);

    function publicationItem() {
        return {
            restrict: 'E',
            replace: true,
            scope: { model: '=' },
            templateUrl: '/lib/fc/layout/office/publicationItem.html'
        }
    }
})();