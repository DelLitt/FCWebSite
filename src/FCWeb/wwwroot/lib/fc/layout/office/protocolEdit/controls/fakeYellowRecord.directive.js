(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('fakeYellowRecord', fakeYellowRecord);

    fakeYellowRecord.$inject = [];
    
    function fakeYellowRecord() {

        return {
            restrict: 'E',
            scope: {
                record: '=',
                friendlyType: '@'
            },

            link: function link(scope, element, attrs) {
                scope.eventInitUrl = '/api/events/' + scope.record.eventId;
            },

            templateUrl: '/lib/fc/layout/office/protocolEdit/controls/fakeEventRecord.html'

        };
    }

})();