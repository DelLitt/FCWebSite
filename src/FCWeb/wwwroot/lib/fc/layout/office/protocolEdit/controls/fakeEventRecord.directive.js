(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('fakeEventRecord', fakeEventRecord);

    fakeEventRecord.$inject = [];
    
    function fakeEventRecord() {

        return {
            restrict: 'E',
            scope: {
                record: '=',
                friendlyType: '@'
            },

            link: function link(scope, element, attrs) {
                scope.eventInitUrl = '/api/events/' + scope.record.eventId;
                scope.eventSearchUrl = "/api/events/" + scope.friendlyType + "/search";
                scope.eventShowAllUrl = "xs";
                scope.eventShowAllUrl = "/api/events/" + scope.friendlyType;
            },

            templateUrl: '/lib/fc/layout/office/protocolEdit/controls/fakeEventRecord.html'

        };
    }

})();