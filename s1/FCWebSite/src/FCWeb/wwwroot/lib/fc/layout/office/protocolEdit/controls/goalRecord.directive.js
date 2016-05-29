(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('goalRecord', goalRecord);

    goalRecord.$inject = [];
    
    function goalRecord() {

        return {
            restrict: 'E',
            scope: {
                record: '=',
                players: '='
            },

            link: function link(scope, element, attrs) {

                var assistIds = [1, 2];

                scope.eventInitUrl = '/api/events/' + scope.record.eventId;

                scope.$watch(function (scope) {
                    return scope.record.eventId;
                },
                function (newValue, oldValue) {
                    scope.showAssist = hasAssist(newValue);
                });

                function hasAssist(eventId) {
                    return assistIds.indexOf(eventId) > -1;
                }
                
            },

            templateUrl: '/lib/fc/layout/office/protocolEdit/controls/goalRecord.html'
        };
    }

})();