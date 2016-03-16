(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('rankingsSrv', rankingsSrv);

    rankingsSrv.$inject = ['helper', 'apiSrv', 'notificationManager'];

    function rankingsSrv(helper, apiSrv, notificationManager) {

        this.loadRankingTable = function (tourneyId, success, failure) {
            apiSrv.get('/api/rankings/' + tourneyId, null, success, function (response) {
                if (failure != null) {
                    failure(response);
                }

                rankingTableLoadFailed(response);
            });
        }

        function rankingTableLoadFailed(response) {
            notificationManager.displayError(response.data);
        }


        //this.getTable = function (tourneyId) {

        //    var rowsCount = helper.getRandom(3, 40);
        //    var table = tableStore.slice(0, Math.min(tableStore.length, rowsCount));

        //    return {
        //        name: "Беларусбанк - чемпионат Республики Беларусь",
        //        rows: angular.isArray(table) && table.length > 0 ? table : []
        //    };
        //};

        //var tableStore = [
        //    { position: 1, team: 'БАТЭ', games: 23, wins: 19, draws: 2, loses: 1, scoredGoals: 47, missedGoals: 12, points: 59 },
        //    { position: 2, team: 'Слуцк', games: 23, wins: 17, draws: 3, loses: 3, scoredGoals: 43, missedGoals: 15, points: 54 },
        //    { position: 3, team: 'Динам-Минск', games: 23, wins: 15, draws: 3, loses: 5, scoredGoals: 40, missedGoals: 18, points: 48 },
        //    { position: 4, team: 'Торпедо-БелАЗ', games: 23, wins: 14, draws: 4, loses: 6, scoredGoals: 43, missedGoals: 22, points: 46 },
        //    { position: 5, team: 'Динам-Брест', games: 22, wins: 13, draws: 3, loses: 6, scoredGoals: 37, missedGoals: 27, points: 42 },
        //    { position: 6, team: 'Белшина', games: 23, wins: 13, draws: 3, loses: 7, scoredGoals: 33, missedGoals: 27, points: 42 },
        //    { position: 7, team: 'Городея', games: 23, wins: 11, draws: 4, loses: 5, scoredGoals: 34, missedGoals: 28, points: 37 },
        //    { position: 8, team: 'Неман', games: 22, wins: 10, draws: 5, loses: 7, scoredGoals: 31, missedGoals: 30, points: 35 },
        //    { position: 9, team: 'Неман1', games: 22, wins: 10, draws: 5, loses: 7, scoredGoals: 31, missedGoals: 30, points: 35 },
        //    { position: 10, team: 'Неман2', games: 22, wins: 10, draws: 5, loses: 7, scoredGoals: 31, missedGoals: 30, points: 35 },
        //    { position: 11, team: 'Неман3', games: 22, wins: 10, draws: 5, loses: 7, scoredGoals: 31, missedGoals: 30, points: 35 },
        //    { position: 12, team: 'Неман4', games: 22, wins: 10, draws: 5, loses: 7, scoredGoals: 31, missedGoals: 30, points: 35 },
        //    { position: 13, team: 'Неман5', games: 22, wins: 10, draws: 5, loses: 7, scoredGoals: 31, missedGoals: 30, points: 35 },
        //    { position: 14, team: 'Неман6', games: 22, wins: 10, draws: 5, loses: 7, scoredGoals: 31, missedGoals: 30, points: 35 },
        //    { position: 15, team: 'Неман7', games: 22, wins: 10, draws: 5, loses: 7, scoredGoals: 31, missedGoals: 30, points: 35 },
        //    { position: 16, team: 'Неман8', games: 22, wins: 10, draws: 5, loses: 7, scoredGoals: 31, missedGoals: 30, points: 35 },
        //    { position: 17, team: 'Неман9', games: 22, wins: 10, draws: 5, loses: 7, scoredGoals: 31, missedGoals: 30, points: 35 },
        //    { position: 18, team: 'Неман10', games: 22, wins: 10, draws: 5, loses: 7, scoredGoals: 31, missedGoals: 30, points: 35 },
        //];
    }
})();