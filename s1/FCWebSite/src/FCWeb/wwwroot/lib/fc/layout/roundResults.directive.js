(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('roundResults', roundResults);

    roundResults.$inject = ['gamesSrv', 'configSrv'];

    function roundResults(gamesSrv, configSrv) {
        return {
            restrict: 'E',
            replace: true, 
            scope: { model: '=' },
            link: function link(scope, element, attrs) {
                scope.roundResults = {
                    previous: previousRound,
                    next: nextRound
                };

                scope.getAdvertImg = function (index) {
                    return "../../../images/skin/adv-result-" + (index + 1) + ".png";
                }

                gamesSrv.roundResultsManager.init(configSrv.Current.MainTeamId, configSrv.mainTeamTourneyIds, roundLoaded);

                function roundLoaded(data) {
                    if (angular.isObject(data)) {
                        scope.roundResults.name = data.name;
                        scope.roundResults.tourney = data.tourney;
                        scope.roundResults.logo = data.logo;
                        scope.roundResults.dateGames = data.dateGames;

                        if (!angular.isDefined(scope.roundResults.dateGames.length)) {
                            scope.roundResults.dateGames = [];
                        }

                        while (scope.roundResults.dateGames.length < 4) {
                            scope.roundResults.dateGames.push({empty: true});
                        }
                    }
                }

                function previousRound() {
                    gamesSrv.roundResultsManager.previous(roundLoaded);
                }

                function nextRound() {
                    gamesSrv.roundResultsManager.next(roundLoaded);
                }

            },
            templateUrl: '/lib/fc/layout/roundResults.html'
        }
    }
})();