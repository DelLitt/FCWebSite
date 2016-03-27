(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('gamesSrv', gamesSrv);

    gamesSrv.$inject = ['helper', 'apiSrv', 'notificationManager'];

    function gamesSrv(helper, apiSrv, notificationManager) {

        this.getRoundGames = function (tourneyId, roundId) {

            var rowsCount = helper.getRandom(0, 10);
            var roundGames = gamesStore[0];

            return {
                roundName: roundGames.roundName || '',
                roundLogo: roundGames.roundLogo,
                roundGames: roundGames.roundGames.slice(0, Math.min(roundGames.roundGames.length, rowsCount)) || []
            };
        };

        var gamesStore = [
            {
                roundName: '23 тур. Беларусбанк - чемпионат Республики Беларусь',
                roundLogo: 'images/temp/logo_abff_vl_2015_80x120.png',
                roundGames: [
                    {
                        date: '29 сентября', day: 'Воскресенье', games:
                          [
                              { time: '18:00', homeTeam: 'Слуцк', awayTeam: 'Торпедо-БелАЗ', homeScore: 3, awayScore: 1 },
                              { time: '18:00', homeTeam: 'Белшина', awayTeam: 'Нафтан', homeScore: 1, awayScore: 1, extra: 'пен.' },
                              { time: '17:00', homeTeam: 'Неман', awayTeam: 'Ислочь', homeScore: 3, awayScore: 0 }
                          ]
                    },
                    {
                        date: '28 сентября', day: 'Суббота', games:
                          [
                              { time: '19:30', homeTeam: 'БАТЭ', awayTeam: 'Славия-Мозырь', homeScore: 2, awayScore: 0 },
                              { time: '19:00', homeTeam: 'Динамо-Брест', awayTeam: 'Динамо-Минск', homeScore: 1, awayScore: 4 },
                              { time: '17:00', homeTeam: 'Городея', awayTeam: 'Шахтер', homeScore: 3, awayScore: 3 }
                          ]
                    },
                    {
                        date: '27 сентября', day: 'Пятница', games:
                          [
                              { time: '17:30', homeTeam: 'Гранит', awayTeam: 'Минск', homeScore: 3, awayScore: 1, extra: 'доп.' },
                              { time: '17:30', homeTeam: 'Витебск', awayTeam: 'Крумкачы', homeScore: 2, awayScore: 0 }
                          ]
                    }],
            }];

        this.roundResultsManager = new RoundResultsManager(apiSrv);
    }

    function RoundResultsManager(apiSrv) {
        var current = null;
        var me = this;
        var loaded = false;
        var teamId = 0;

        var data = [];

        this.getCurrent = function () {
            if (!current && angular.isArray(data)) {
                data.forEach(function (element, index, array) {
                    if (element.current) {
                        current = element;
                    }
                });
            }

            return current;
        }

        this.init = function (team, tourneyIds, success) {
            teamId = team;

            if (!angular.isNumber(teamId) || !angular.isArray(tourneyIds)) { return; }

            var result = null;
            var url = "/api/games/" + team + "/slider?";

            tourneyIds.forEach(function (element, index, array) {
                url = url + "tourneyIds=" + element + (index < array.length - 1 ? "&" : "")
            });

            apiSrv.get(url,
                null,
                function (response) {
                    data = response.data;
                    loaded = true;
                    loadSelectedRoundGames(success);
                },
                function (response) {
                    alert(response);
                });
        }

        function getSelectedIndex() {
            if (angular.isNumber(selectedIndex)) {
                return selectedIndex;
            }

            var current = me.getCurrent();
            if (angular.isObject(current)) {
                selectedIndex = getRoundIndex(current.roundId);
            }

            return selectedIndex || 0;
        }

        function getRoundIndex(roundId) {
            if (angular.isArray(data)) {
                data.forEach(function (element, index, array) {
                    if (element.roundId == roundId) {
                        return index;
                    }
                });
            }
        }

        function getRoundGames(roundId, success) {
            apiSrv.get('/api/games/round/short/' + roundId,
                null,
                function (response) {
                    success(response.data);
                },
                function (response) {
                    alert(response);
                });
        }

        function loadSelectedRoundGames(success) {

            var result = data[selectedIndex];

            if (!angular.isObject(result.roundGames)) {
                    
                getRoundGames(result.roundId, function (roundGames) {
                    result.roundGames = roundGames;
                    success(result.roundGames);
                });
            }
            else {
                success(result.roundGames)
            }
        }

        var selectedIndex = getSelectedIndex();

        this.next = function (success) {
            if (!angular.isArray(data) || !loaded) { return; }

            selectedIndex++;
            if (selectedIndex >= data.length ) {
                selectedIndex = data.length - 1;
            }

            loadSelectedRoundGames(success);
        }

        this.previous = function (success) {
            if (!angular.isArray(data) || !loaded) { return; }

                selectedIndex--;
                if (selectedIndex < 0) {
                    selectedIndex = 0;
                }

            loadSelectedRoundGames(success);
        }

        this.reset = function () {
            selectedIndex = null;
            selectedIndex = getSelectedIndex();

            return this.getCurrent();
        }
    }
})();