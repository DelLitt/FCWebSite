(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('quickGamesInfo', quickGamesInfo);

    quickGamesInfo.$inject = ['configSrv', 'gamesSrv', 'helper'];
    
    function quickGamesInfo(configSrv, gamesSrv, helper) {
        return {
            link: link,
            restrict: 'E',
            templateUrl: '/lib/fc/layout/games/quickGamesInfo.html'
        };

        function link(scope, element, attrs) {
            scope.index = 0;           
            scope.loadingQGI = true;
            scope.loadingImage = helper.getLoadingImg();
            scope.getTeamViewLink = helper.getTeamViewLink;

            scope.hasExtra = function (game) {
                return angular.isString(game.extra) && game.extra.length > 0;
            }

            scope.getExtra = function (game) {
                return game.extra === "1" ? "EX_TIME" : (game.extra === "2" ? "PENALTIES" : "");
            }

            loadData();

            scope.showNext = function () {
                scope.index++;
                loadData();
            }

            function loadData() {
                var teamId;
                scope.loadingQGI = true;

                if (scope.index == 1) {
                    teamId = configSrv.Current.ReserveTeamId;
                } else {
                    teamId = configSrv.Current.MainTeamId;
                    scope.index = 0;
                }

                gamesSrv.loadQuickGamesInfo(teamId, dataLoadedSuccessfully)                
            }

            function dataLoadedSuccessfully(response) {
                scope.loadingQGI = false;
                scope.group = response.data;
            }
        }
    }

})();