(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('textEventProtocol', textEventProtocol);

    textEventProtocol.$inject = [];

    function textEventProtocol() {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                data: '='
            },
            link: function link(scope, element, attrs) {
                scope.hasInfo = function (item) {
                    return item.info.length > 0;
                }
            },
            templateUrl: '/lib/fc/layout/protocol/textEventProtocol.html'
        }
    }
})();