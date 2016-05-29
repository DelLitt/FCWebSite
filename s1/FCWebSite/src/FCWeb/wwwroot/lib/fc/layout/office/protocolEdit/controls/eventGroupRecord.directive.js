(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('eventGroupRecord', eventGroupRecord);

    eventGroupRecord.$inject = ['configSrv', 'notificationManager'];
    
    function eventGroupRecord(configSrv, notificationManager) {

        return {
            restrict: 'E',
            scope: {
                eventgrouptitle: '@',
                eventtitle: '@',
                persontitle: '@',
                eventgroupsfilter: '@',
                record: '=',
                players: '='
            },

            link: function link(scope, element, attrs) {
                var friendlyNames = configSrv.getEventGroupFriendlyNames();

                scope.eventGroupId = angular.isObject(scope.record.eventModel) ? scope.record.eventModel.eventGroupId : 0;
                scope.eventGroupUrls.urlinit = '/api/eventgroups/' + scope.eventGroupId;
                scope.eventUrls.urlinit = '/api/events/' + scope.record.eventId;
                scope.isEventInit = true;

                var url = "/api/eventgroups/filter?";
                scope.eventgroupsfilter.split(',').forEach(function (element, index, array) {
                    url = url + "ids=" + element + (index < array.length - 1 ? "&" : "")
                });

                scope.eventGroupUrls.urlshowall = url;

                scope.$watch(function (scope) {
                    return scope.eventGroupId;
                },
                function (newValue, oldValue) {
                    notificationManager.displayInfo("EventGroupId: " + newValue);
                    if (newValue !== oldValue) {
                            var friendlyName = friendlyNames[newValue.toString()];

                            scope.record.eventId = scope.record.eventId <= 0 ? scope.record.eventId - 1 : 0;

                            scope.eventUrls.urlsearch = "/api/events/" + friendlyName + "/search";
                            scope.eventUrls.urlinit = scope.eventUrls.urlinit === "" ? null : "";
                            scope.eventUrls.urlshowall = "/api/events/" + friendlyName;
                    }
                });

                //function hasAssist(eventId) {
                //    return assistIds.indexOf(eventId) > -1;
                //}
                
            },

            controller: ['$scope', function($scope) {
                $scope.eventUrls = {
                    urlsearch: "",
                    urlinit: "",
                    urlshowall: "wilbeloaded"
                }

                $scope.eventGroupUrls = {
                    urlsearch: "",
                    urlinit: "",
                    urlshowall: "wilbeloaded"
                }
            }],

            templateUrl: '/lib/fc/layout/office/protocolEdit/controls/eventGroupRecord.html'
        };
    }

})();