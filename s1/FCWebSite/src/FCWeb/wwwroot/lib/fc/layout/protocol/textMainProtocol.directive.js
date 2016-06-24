(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('textMainProtocol', textMainProtocol);

    textMainProtocol.$inject = [];

    function textMainProtocol() {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                data: '='
            },
            link: function link(scope, element, attrs) {
                scope.wasSubstituted = function (item) {
                    return angular.isObject(item.extra) && item.extra.text.length > 0;
                }
            },
            templateUrl: '/lib/fc/layout/protocol/textMainProtocol.html'
        }
    }
})();