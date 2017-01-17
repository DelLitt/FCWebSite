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
                    showHeader: "@"
                },

            link: function link(scope, element, attrs) {
                scope.showHeader = false;
                scope.addFileVariant = helper.addFileVariant;
                scope.model.img = helper.addFileVariant(scope.model.img, "v225x45");
            },
            templateUrl: '/lib/fc/layout/publications/publicationItem.html'
        }
    }
})();