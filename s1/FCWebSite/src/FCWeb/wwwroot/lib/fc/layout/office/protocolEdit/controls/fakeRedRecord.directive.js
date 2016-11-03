(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('fakeRedRecord', fakeRedRecord);

    fakeRedRecord.$inject = [];
    
    function fakeRedRecord() {

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