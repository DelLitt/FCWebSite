(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('textGoalProtocol', textGoalProtocol);

    textGoalProtocol.$inject = ['helper'];

    function textGoalProtocol(helper) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                data: '='
            },
            link: function link(scope, element, attrs) {
                var infoCache = {};

                scope.hasAssistant = helper.hasProtocolExtraLink;
                scope.hasData = helper.hasProtocolData;

                scope.hasExtMin = function (data) {
                    return angular.isNumber(data.extraMinute) && data.extraMinute > 0;
                }

                scope.filterGoalInfo = function (info) {
                    return getFilteredInfo(info);
                }

                scope.hasGoalInfo = function (info) {
                    return getFilteredInfo(info).length > 0;
                }

                function getFilteredInfo(info) {
                    if (angular.isString(info) && info.length > 0) {
                        if (angular.isString(infoCache[info])) {
                            return infoCache[info];
                        }

                        var filteredInfo = helper.filterGoalsInfo(info);
                        if (angular.isString(filteredInfo) && filteredInfo.length > 0) {
                            infoCache[info] = filteredInfo;
                            return infoCache[info];
                        }
                    }

                    return '';
                }
            },
            templateUrl: '/lib/fc/layout/protocol/textGoalProtocol.html'
        }
    }
})();