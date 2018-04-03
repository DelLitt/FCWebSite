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
                season: '=',
                tourneysIds: '=',
                imgVariant: '@'
            },
            link: function link(scope, element, attrs) {

                scope.loadingGames = true;
                scope.loadingImage = helper.getLoadingImg();

                scope.getLogo = function (team) {
                    var image = helper.getTeamImage(team);
                    return helper.addFileVariant(image, scope.imgVariant);
                }

                scope.hasExtra = function (game) {
                    return angular.isNumber(game.homeAddScore) || angular.isNumber(game.homePenalties);
                }

                scope.hasAddScore = function (game) {
                    return angular.isNumber(game.homeAddScore) && angular.isNumber(game.awayAddScore);
                }

                scope.hasPenalties = function (game) {
                    return angular.isNumber(game.homePenalties) && angular.isNumber(game.awayPenalties);
                }

                scope.$watch(function (scope) {
                    return scope.tourneysIds;
                },
                function (newValue, oldValue) {
                    if (angular.isArray(newValue)) {
                        loadData();
                    }
                });

                function loadData() {
                    scope.loadingGames = true;
                    gamesSrv.loadSchedule(scope.tourneysIds, scope.season.dateStart, scope.season.dateEnd, scheduleLoaded);
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