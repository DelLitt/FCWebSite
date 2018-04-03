(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('publicationItem', publicationItem);

    publicationItem.$inject = ['helper'];

    function publicationItem(helper) {
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
            templateUrl: '/lib/fc/layout/publications/publicationItem.html'
        }
    }
})();