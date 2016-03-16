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

        var data = [
            { roundId: 2, roundData: null, current: true },
            { roundId: 3, roundData: null, current: false },
            { roundId: 5, roundData: null, current: false }
        ];

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

        function getRoundData(roundId, success) {
            var result = null;

            apiSrv.get('/api/round/' + roundId,
                null,
                function (response) {
                    result = response.data;
                },
                function (response) {
                    alert(response);
                });

            return result;
        }

        var selectedIndex = getSelectedIndex();

        this.next = function (success) {

            console.log("RoundResultsManager.next();")
            success();

            if (angular.isArray(data)) {
                selectedIndex++;
                if (selectedIndex > data.length - 1) {
                    selectedIndex = data.length - 1;
                }

                var nextData = data[selectedIndex];

                if (!angular.isObject(nextData.roundData)) {
                    nextData.roundData = getRoundData(nextData.roundId);
                }

                return nextData;
            }

            return null;
        }

        this.previous = function (success) {

            console.log("RoundResultsManager.previous();")
            success();

            if (angular.isArray(data)) {
                selectedIndex--;
                if (selectedIndex < 0) {
                    selectedIndex = 0;
                }

                var prevData = data[selectedIndex];

                if (!angular.isObject(prevData.roundData)) {
                    prevData.roundData = getRoundData(prevData.roundId);
                }

                return prevData;
            }

            return null;
        }

        this.reset = function () {
            selectedIndex = null;
            selectedIndex = getSelectedIndex();

            return this.getCurrent();
        }
    }
})();