(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('gameEditCtrl', gameEditCtrl);

    gameEditCtrl.$inject = ['$scope', '$routeParams', '$location', 'gamesSrv', 'fileBrowserSrv', 'notificationManager'];

    function gameEditCtrl($scope, $routeParams, $location, gamesSrv, fileBrowserSrv, notificationManager) {

        if (!angular.isDefined($scope.forms)) {
            $scope.forms = {};
        }

        $scope.loading = true;
        $scope.saveEdit = saveEdit;
        $scope.roundSearchUrl = '';
        $scope.roundAllUrl = 'willbeloaded';
        $scope.homeAllUrl = 'willbeloaded';
        $scope.awayAllUrl = 'willbeloaded';
        $scope.game = {};
        $scope.tourneyId = -1;
        $scope.roundId = -1;
        $scope.dateOptions = {
            showWeeks: false
        };

        $scope.onHomeSelect = function (team) {
            if (!$scope.isStadiumInit) {
                $scope.stadiumInitUrl = angular.isNumber(team.stadiumId)
                    ? '/api/stadiums/' + team.stadiumId
                    : null;
            }
            else {
                $scope.isStadiumInit = false;
            }
        }

        $scope.fileBrowser = {
            path: '',
            root: ''
        };

        var curDate = new Date();
        var currentYear = curDate.getFullYear();

        $scope.$watch(function (scope) {
            return scope.tourneyId;
        },
        function (newValue, oldValue) {
            notificationManager.displayInfo("TourneyId: " + newValue);
            if (newValue !== oldValue) {                
                if (!$scope.isRoundInit) {
                    $scope.roundInitUrl = $scope.tourneyId;
                    setRoundUrls();
                } else {
                    $scope.isRoundInit = false;
                }
            }
        });

        $scope.$watch(function (scope) {
            return scope.roundId;
        },
        function (newValue, oldValue) {
            notificationManager.displayInfo("RoundId: " + newValue);
            if (newValue !== oldValue) {
                if (!$scope.isTeamsInit) {
                    $scope.homeInitUrl = $scope.roundId;
                    $scope.awayInitUrl = $scope.roundId;
                    setTeamsUrls();
                } else {
                    $scope.isTeamsInit = false;
                }
            }
        });

        loadData($routeParams.id);

        function loadData(gameId) {
            if (gameId < 0) {
                return;
            }

            gamesSrv.loadGame(gameId, gameLoaded);
        }

        function gameLoaded(response) {
            var game = response.data;

            $scope.game = game;

            if(angular.isObject(game.round) && angular.isNumber(game.round.tourneyId)) {
                $scope.tourneyId = game.round.tourneyId;
            }

            $scope.isRoundInit = game.roundId > 0 ? true : false;
            $scope.roundId = game.roundId;
            $scope.isTeamsInit = game.homeId > 0 && game.awayId > 0 ? true : false;
            $scope.homeId = game.homeId;
            $scope.isStadiumInit = game.stadiumId > 0 ? true : false;

            $scope.game.gameDate = new Date(game.gameDate);

            $scope.tourneyInitUrl = $scope.tourneyId > 0
                ? '/api/tourneys/' + $scope.tourneyId
                : null;

            $scope.roundInitUrl = angular.isNumber(game.roundId)
                ? '/api/rounds/' + game.roundId
                : null;

            $scope.homeInitUrl = angular.isNumber(game.homeId)
                ? '/api/teams/' + game.homeId
                : null;

            $scope.awayInitUrl = angular.isNumber(game.awayId)
                ? '/api/teams/' + game.awayId
                : null;

            $scope.stadiumInitUrl = angular.isNumber(game.stadiumId)
                ? '/api/stadiums/' + game.stadiumId
                : null;

            setRoundUrls();
            setTeamsUrls();
        }

        function setRoundUrls() {
            $scope.roundSearchUrl = '/api/tourneys/' + $scope.tourneyId + '/rounds/search';
            $scope.roundAllUrl = '/api/tourneys/' + $scope.tourneyId + '/rounds';
        }

        function setTeamsUrls() {
            $scope.homeSearchUrl = '/api/round/' + $scope.roundId + '/teams/search';
            $scope.homeAllUrl = '/api/round/' + $scope.roundId + '/teams';
            $scope.awaySearchUrl = '/api/round/' + $scope.roundId + '/teams/search';
            $scope.awayAllUrl = '/api/round/' + $scope.roundId + '/teams';
        }

        function saveEdit(form) {

            $scope.submitted = true;

            if (!form.$valid) {
                return;
            }

            $scope.game.roundId = $scope.roundId;
            $scope.game.homeId = $scope.homeId;

            gamesSrv.saveGame($scope.game.id || 0, $scope.game, gameSaved);
        }

        function gameSaved(response) {
            notificationManager.displayInfo("Saved: " + response);
            $location.path('/office')
        }
    }
})();
