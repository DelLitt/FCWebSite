(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('galleryItem', galleryItem);

    function galleryItem() {
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
            templateUrl: '/lib/fc/layout/image/galleryItem.html'
        }
    }
})();