(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('entityLink', entityLink);

    entityLink.$inject = [];
    
    function entityLink() {

        return {
            restrict: 'E',
            scope: {
                path: '@',
                entityId: '@',
                text: '@',
                title: '@'
            },

            link: function link(scope, element, attrs) {
          
            },

            templateUrl: '/lib/fc/layout/entityLink.html'

        };
    }

})();