(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('roundResults', roundResults);

    roundResults.$inject = ['gamesSrv', 'configSrv', 'helper'];

    function roundResults(gamesSrv, configSrv, helper) {
        return {
            restrict: 'E',
            replace: true, 
            scope: { model: '=' },
            link: function link(scope, element, attrs) {
                scope.roundResults = {
                    previous: previousRound,
                    next: nextRound
                };

                scope.loadingRR = true;
                scope.loadingImage = helper.getLoadingImg();
                scope.getTeamViewLink = helper.getTeamViewLink;
                scope.hasExtra = function (game) {
                    return angular.isString(game.extra) && game.extra.length > 0;
                }

                scope.getExtra = function (game) {
                    return game.extra === "1" ? "EX_TIME" : (game.extra === "2" ? "PENALTIES" : "");
                }

                scope.getAdvertImg = function (index) {
                    return "../../../images/skin/adv-result-" + (index + 1) + ".png";
                }

                gamesSrv.roundResultsManager.init(configSrv.Current.MainTeamId, configSrv.Current.MainTeamTourneyIds, roundLoaded);

                function roundLoaded(data) {
                    if (angular.isObject(data)) {
                        scope.roundResults.roundIndex = data.name;
                        scope.roundResults.roundName = data.nameFull;
                        scope.roundResults.tourney = data.tourney;
                        scope.roundResults.logo = data.logo;
                        scope.roundResults.dateGames = data.dateGames;

                        if (!angular.isDefined(scope.roundResults.dateGames.length)) {
                            scope.roundResults.dateGames = [];
                        }

                        while (scope.roundResults.dateGames.length < 4) {
                            scope.roundResults.dateGames.push({empty: true});
                        }

                        scope.loadingRR = false;
                    }
                }

                function previousRound() {
                    scope.loadingRR = true;
                    gamesSrv.roundResultsManager.previous(roundLoaded);
                }

                function nextRound() {
                    scope.loadingRR = true;
                    gamesSrv.roundResultsManager.next(roundLoaded);
                }

            },
            templateUrl: '/lib/fc/layout/roundResults.html'
        }
    }
})();