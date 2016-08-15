(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('quickTourneyEdit', quickTourneyEdit);

    quickTourneyEdit.$inject = [];

    function quickTourneyEdit() {
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
            controller: quickTourneyEditCtrl,
            templateUrl: '/lib/fc/layout/office/quickTourneyEdit.html'
        }
    }
})();