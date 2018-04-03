(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('textEventProtocol', textEventProtocol);

    textEventProtocol.$inject = ['helper'];

    function textEventProtocol(helper) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                data: '='
            },
            link: function link(scope, element, attrs) {
                var infoCache = {};

                scope.hasInfo = helper.hasProtocolInfo;

                scope.hasExtMin = function (data) {
                    return angular.isNumber(data.extraMinute) && data.extraMinute > 0;
                }

                scope.filterEventInfo = function (info) {
                    return getFilteredInfo(info);
                }

                scope.hasEventInfo = function (info) {
                    return getFilteredInfo(info).length > 0;
                }

                function getFilteredInfo(info) {
                    if (angular.isString(info) && info.length > 0) {
                        if (angular.isString(infoCache[info])) {
                            return infoCache[info];
                        }

                        var filteredInfo = helper.filterEventsInfo(info);
                        if (angular.isString(filteredInfo) && filteredInfo.length > 0) {
                            infoCache[info] = filteredInfo;
                            return infoCache[info];
                        }
                    }

                    return '';
                }
            },
            templateUrl: '/lib/fc/layout/protocol/textEventProtocol.html'
        }
    }
})();