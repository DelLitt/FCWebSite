(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('fakeGoalRecord', fakeGoalRecord);

    fakeGoalRecord.$inject = [];
    
    function fakeGoalRecord() {

        return {
            restrict: 'E',
            scope: {
                record: '='
            },

            link: function link(scope, element, attrs) {
                scope.eventInitUrl = '/api/events/' + scope.record.eventId;
            },

            templateUrl: '/lib/fc/layout/office/protocolEdit/controls/fakeGoalRecord.html'

        };
    }

})();