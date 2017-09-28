(function () {
    'use strict';

    angular
        .module('fc.ui')
        .directive('tourneySchedule', tourneySchedule);

    tourneySchedule.$inject = ['gamesSrv', 'configSrv', 'helper'];

    function tourneySchedule(gamesSrv, configSrv, helper) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                teamIds: '=',
                tourneyId: '=',
                hasData: '='
            },
            link: function link(scope, element, attrs) {

                scope.loadingSchedule = true;
                scope.loadingImage = helper.getLoadingImg();
                scope.quickMode = true;
                scope.showAll = showAll;
                scope.header = "NEAREST_GAMES";
                scope.isShowAllEnabled = true;

                scope.$watch(function (scope) {
                    return scope.tourneyId;
                },
                function (newValue, oldValue) {
                    if (angular.isNumber(newValue)) {
                        loadData();
                    }
                });

                scope.hasExtra = function (game) {
                    return angular.isString(game.extra) && game.extra.length > 0;
                }

                scope.getExtra = function (game) {
                    return game.extra === "1" ? "EX_TIME" : (game.extra === "2" ? "PENALTIES" : "");
                }

                scope.hasScore = function (game) {
                    return angular.isNumber(game.homeScore) && angular.isNumber(game.awayScore);
                }

                function showAll() {
                    loadData();
                }

                function loadData() {
                    gamesSrv.loadTourneySchedule(scope.tourneyId, scope.teamIds, scope.quickMode, resultsLoaded);
                }

                function resultsLoaded(response) {
                    scope.games = response.data;
                    scope.loadingSchedule = false;
                    scope.header = scope.quickMode ? "NEAREST_GAMES" : "SCHEDULE";
                    scope.isShowAllEnabled = scope.quickMode;
                    scope.quickMode = false;
                    scope.hasData = angular.isArray(scope.games);
                }
            },
            templateUrl: '/lib/fc/layout/results/tourneySchedule.html'
        }
    }
})();