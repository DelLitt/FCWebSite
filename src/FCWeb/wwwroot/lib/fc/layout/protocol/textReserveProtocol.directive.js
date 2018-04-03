(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('textReserveProtocol', textReserveProtocol);

    textReserveProtocol.$inject = [];

    function textReserveProtocol() {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                data: '='
            },
            link: function link(scope, element, attrs) {

            },
            templateUrl: '/lib/fc/layout/protocol/textReserveProtocol.html'
        }
    }
})();