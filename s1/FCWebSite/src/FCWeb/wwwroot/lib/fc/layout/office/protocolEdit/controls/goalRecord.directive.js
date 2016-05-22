(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('goalRecord', goalRecord);

    goalRecord.$inject = ['apiSrv', 'protocolSrv'];
    
    function goalRecord(apiSrv, protocolSrv) {

        return {
            restrict: 'E',
            scope: {
                record: '=',
                players: '='
            },

            link: function link(scope, element, attrs) {

                var assistIds = [1, 2];

                scope.eventInitUrl = '/api/events/' + scope.record.eventId;

                // watch initialId
                scope.$watch(function (scope) {
                    return scope.record.eventId;
                },
                function (newValue, oldValue) {
                    scope.showAssist = hasAssist(newValue);
                });

                function hasAssist(eventId) {
                    return assistIds.indexOf(eventId) > -1;
                }

                //loadData(scope.gameId);

                //function loadData(gameId) {
                //    if (gameId < 0) {
                //        return;
                //    }

                //    protocolSrv.loadProtocol(gameId, protocolLoaded);
                //}

                //function protocolLoaded(response) {
                //    var protocol = response.data;

                //    scope.protocol = protocol;
                //}
                
            },

            templateUrl: '/lib/fc/layout/office/protocolEdit/controls/goalRecord.html'

        };
    }

})();