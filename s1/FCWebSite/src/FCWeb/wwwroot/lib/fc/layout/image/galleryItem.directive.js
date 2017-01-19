(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('galleryItem', galleryItem);

    galleryItem.$inject = ['helper'];

    function galleryItem(helper) {
        return {
            restrict: 'E',
            replace: true,
            scope:
                {
                    model: '=',
                    imgVariant: '@',
                    showHeader: "@"
                },

            link: function link(scope, element, attrs) {
                scope.showHeader = false;
                scope.img = helper.addFileVariant(scope.model.img, scope.imgVariant);
            },
            templateUrl: '/lib/fc/layout/image/galleryItem.html'
        }
    }
})();