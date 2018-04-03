(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('textMainProtocol', textMainProtocol);

    textMainProtocol.$inject = ['helper'];

    function textMainProtocol(helper) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                data: '='
            },
            link: function link(scope, element, attrs) {
                scope.hasSub = function (item) {

                    if (angular.isString(item.main.id) && item.main.id.length > 0) {
                        return helper.hasProtocolExtraLink(item);
                    }

                    return angular.isString(item.info) && item.info.length > 0;
                }
            },
            templateUrl: '/lib/fc/layout/protocol/textMainProtocol.html'
        }
    }
})();