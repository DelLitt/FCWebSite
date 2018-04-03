(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('videoItem', videoItem);

    function videoItem() {
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
            templateUrl: '/lib/fc/layout/video/videoItem.html'
        }
    }
})();