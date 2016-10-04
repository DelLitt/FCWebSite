(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('hotPublications', hotPublications);

    hotPublications.$inject = ['helper'];

    function hotPublications(helper) {
        return {
            restrict: 'E',
            replace: true,
            scope:
                {
                    model: '='
                },
            link: function link(scope, element, attrs) {
                
                scope.load = load;
                scope.loadingHP = true;
                scope.loadingImage = helper.getLoadingImg();

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
                    scope.loadingHP = false;
                }
            },
            templateUrl: '/lib/fc/layout/publications/hotPublications.html'
        }
    }
})();