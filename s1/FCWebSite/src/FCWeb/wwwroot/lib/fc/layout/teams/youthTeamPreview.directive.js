(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('youthTeamPreview', youthTeamPreview);

    youthTeamPreview.$inject = ['configSrv', 'helper'];

    function youthTeamPreview(configSrv, helper) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                model: '='
            },
            link: function link(scope, element, attrs) {

                scope.getImage = function () {
                    return helper.getTeamFakeInfoImage(scope.model);
                }
            },
            templateUrl: '/lib/fc/layout/teams/youthTeamPreview.html'
        }
    }
})();