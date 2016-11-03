(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('textSubsRecursiveInfo', textSubsRecursiveInfo);

    textSubsRecursiveInfo.$inject = ['helper'];
    
    function textSubsRecursiveInfo(helper) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                data: '='
            },
            link: function link(scope, element, attrs) {
                scope.hasSub = helper.hasProtocolExtraLink;
                scope.hasExtMin = angular.isNumber(scope.data.extraMinute) && scope.data.extraMinute > 0;
            },
            templateUrl: '/lib/fc/layout/protocol/textSubsRecursiveInfo.html'
        }
    }
})();