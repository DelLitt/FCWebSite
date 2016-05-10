(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('gamesSrv', gamesSrv);

    gamesSrv.$inject = ['helper', 'apiSrv', 'notificationManager'];

    function gamesSrv(helper, apiSrv, notificationManager) {

        this.loadGame = function (id, success, failure) {
            apiSrv.get('/api/games/' + id, null,
                success,
                function (response) {
                    if (angular.isFunction(failure)) {
                        failure(response);
                    }

                    gamesLoadFail(response);
                });
        }

        function gamesLoadFail(response) {
            notificationManager.displayError(response.data);
        }

        this.saveGame = function (id, game, success, failure) {
            if (angular.isDefined(id) && parseInt(id) > 0) {
                apiSrv.put('/api/games/', id, game,
                                success,
                                function (response) {
                                    if (angular.isFunction(failure)) {
                                        failure(response);
                                    }

                                    gameSaveFailed(response);
                                });
            } else {
                apiSrv.post('/api/games/', game,
                                success,
                                function (response) {
                                    if (angular.isFunction(failure)) {
                                        failure(response);
                                    }

                                    gameSaveFailed(response);
                                });

            }
        }

        function gameSaveFailed(response) {
            notificationManager.displayError(response.data);
        }

        this.getRoundGames = function (tourneyId, roundId) {

            var rowsCount = helper.getRandom(0, 10);

            return {
                roundName: roundGames.roundName || '',
                roundLogo: roundGames.roundLogo,
                roundGames: roundGames.roundGames.slice(0, Math.min(roundGames.roundGames.length, rowsCount)) || []
            };
        };

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
            var url = "/api/games/round/team/" + team + "/slider?";

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
            apiSrv.get('/api/games/round/' + roundId + '/mode/1',
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