(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('latestResults', latestResults);

    latestResults.$inject = ['gamesSrv', 'configSrv', 'helper'];

    function latestResults(gamesSrv, configSrv, helper) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                teamId: '=',
                tourneysIds: '=',
                hasData: '='
            },
            link: function link(scope, element, attrs) {

                scope.loadingResults = true;
                scope.loadingImage = helper.getLoadingImg();
                scope.quickMode = true;
                scope.showAll = showAll;
                scope.header = "NEAREST_GAMES";
                scope.isShowAllEnabled = true;

                scope.$watch(function (scope) {
                    return scope.tourneysIds;
                },
                function (newValue, oldValue) {
                    if (angular.isArray(newValue) || newValue.length > 0) {
                        loadData();
                    }
                });                

                function showAll() {
                    loadData();
                }

                function loadData() {
                    gamesSrv.loadTeamGames(scope.teamId, scope.tourneysIds, scope.quickMode, resultsLoaded);
                }

                function resultsLoaded(response) {
                    scope.games = response.data;
                    scope.loadingResults = false;
                    scope.header = scope.quickMode ? "NEAREST_GAMES" : "SCHEDULE";
                    scope.isShowAllEnabled = scope.quickMode;
                    scope.quickMode = false;
                    scope.hasData = angular.isArray(scope.games);
                }
            },
            templateUrl: '/lib/fc/layout/results/latestResults.html'
        }
    }
})();