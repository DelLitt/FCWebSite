(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('results', results);

    results.$inject = ['gamesSrv', 'configSrv', 'helper', 'filterFilter', 'publicationsSrv'];

    function results(gamesSrv, configSrv, helper, filterFilter) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                teamId: '=',
                title: '=',
                tourneysIds: '='
            },
            link: function link(scope, element, attrs) {

                scope.loadingGames = true;
                scope.loadingImage = helper.getLoadingImg();
                scope.getLogo = function (team) {
                    return helper.getTeamImage(team);
                }
                scope.personsLoaded = false;

                scope.hasExtra = function (game) {
                    return angular.isNumber(game.homeAddScore) || angular.isNumber(game.homePenalties);
                }

                loadData();

                function loadData() {
                    gamesSrv.loadSchedule(scope.tourneysIds, scheduleLoaded);
                }

                function scheduleLoaded(response) {
                    scope.schedule = response.data;
                    scope.loadingGames = false;
                }

            },
            templateUrl: '/lib/fc/layout/results/results.html'
        }
    }
})();