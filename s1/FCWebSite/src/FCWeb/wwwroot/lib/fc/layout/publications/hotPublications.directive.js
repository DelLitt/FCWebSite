(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('hotPublications', hotPublications);

    function hotPublications() {
        return {
            restrict: 'E',
            replace: true,
            scope:
                {
                    model: '='
                },
            link: function link(scope, element, attrs) {
                
                scope.load = load;

                // watch initialId
                scope.$watch(function (scope) {
                    return scope.model;
                },
                function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        load(0);
                    }
                });                

                function load(index) {
                    scope.selectedIndex = index;
                    scope.main = scope.model[index];
                }
            },
            templateUrl: '/lib/fc/layout/publications/hotPublications.html'
        }
    }
})();