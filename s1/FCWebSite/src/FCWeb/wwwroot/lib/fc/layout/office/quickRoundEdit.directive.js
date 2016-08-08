(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('quickRoundEdit', quickRoundEdit);

    quickRoundEdit.$inject = [];

    function quickRoundEdit() {
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
            controller: quickRoundEditCtrl,
            templateUrl: '/lib/fc/layout/office/quickRoundEdit.html'
        }
    }
})();