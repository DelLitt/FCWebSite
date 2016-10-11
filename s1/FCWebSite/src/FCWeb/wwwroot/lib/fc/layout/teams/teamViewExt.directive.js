(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('teamViewExt', teamViewExt);

    teamViewExt.$inject = ['configSrv', 'helper', 'tourneysSrv'];

    function teamViewExt(configSrv, helper, tourneysSrv) {

        return {
            restrict: 'E',
            replace: true,
            scope: {
                model: "="
            },
            link: function link(scope, element, attrs) {

                scope.loadingImage = helper.getLoadingImg();
                scope.tourneysLoaded = false;

                loadData();

                function loadData() {                    
                    //tourneysSrv.loadTourneys(scope.tourneysIds, tourneysLoaded);
                }

                function tourneysLoaded(response) {
                    scope.tourneys = response.data;
                    scope.tourneysLoaded = true;
                }
            },
            templateUrl: '/lib/fc/layout/teams/teamViewExt.html'
        }
    }
})();