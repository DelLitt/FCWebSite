(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('textGoalProtocol', textGoalProtocol);

    textGoalProtocol.$inject = [];

    function textGoalProtocol() {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                data: '='
            },
            link: function link(scope, element, attrs) {
                scope.hasAssistant = function (item) {
                    return angular.isObject(item.extra) && item.extra.text.length > 0;
                }
            },
            templateUrl: '/lib/fc/layout/protocol/textGoalProtocol.html'
        }
    }
})();