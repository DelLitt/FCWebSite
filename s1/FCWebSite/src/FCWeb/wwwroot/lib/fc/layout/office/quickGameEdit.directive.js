(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('quickGameEdit', quickGameEdit);

    quickGameEdit.$inject = ['$interval'];

    function quickGameEdit($interval) {
        return {
            restrict: 'E',
            replace: true,
            //scope: { model: '=' },
            link: function(scope, el, attrs){

                scope.$on('$destroy', function () {
                    console.log('Directive is destroyed!')
                })
            },
            templateUrl: '/lib/fc/layout/office/quickGameEdit.html'
        }
    }
})();