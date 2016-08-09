(function () {
    'use strict';

    angular
        .module('fc.ui')
        .directive('quickGameEdit', quickGameEdit);

    quickGameEdit.$inject = [];

    function quickGameEdit() {
        return {
            restrict: 'E',
            replace: true,
            link: function (scope, el, attrs) {
                //scope.roundSave = angular.isFunction(scope.onSave)
                //    ? function () { scope.onSave(scope) }
                //    : function () { console.log("Default round '" + scope.round.name +"' save click!"); }

                //scope.$on('$destroy', function () {
                //    console.log('Directive is destroyed!')
                //})
            },
            controller: quickGameEditCtrl,
            templateUrl: '/lib/fc/layout/office/quickGameEdit.html'
        }
    }
})();