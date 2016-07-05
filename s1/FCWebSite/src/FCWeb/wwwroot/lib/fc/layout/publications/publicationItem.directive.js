(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('publicationItem', publicationItem);

    function publicationItem() {
        return {
            restrict: 'E',
            replace: true,
            scope:
                {
                    model: '=',
                    showHeader: "@"
                },

            link: function link(scope, element, attrs) {
                scope.showHeader = false;
            },
            templateUrl: '/lib/fc/layout/publications/publicationItem.html'
        }
    }
})();