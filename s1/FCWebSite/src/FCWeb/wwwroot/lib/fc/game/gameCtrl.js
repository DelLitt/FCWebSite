﻿(function () {
    'use strict';

    angular
        .module('fc')
        .controller('gameCtrl', gameCtrl);

    gameCtrl.$inject = ['$scope', '$routeParams', 'apiSrv', 'gamesSrv', 'notificationManager'];

    function gameCtrl($scope, $routeParams, apiSrv, gamesSrv, notificationManager) {

        var gameId = $routeParams.id;

        $scope.loadingGame = true;
        $scope.showAddScore = false;
        $scope.showPenalties = false;
        $scope.showToteResult = false;
        $scope.toteResult = {};

        $scope.vote = function (value) {
            if(value < 0 || value > 2) {
                return;
            }

            apiSrv.post('/api/game/' + gameId + '/tote', value,
                function (response) {
                    $scope.toteResult = response.data;
                    setResults($scope.toteResult);
                    $scope.showToteResult = true;
                },
                voteFailed);
        }

        gamesSrv.loadGame(gameId, gameLoaded);

        checkUserVoted();
        loadResults();

        function gameLoaded(response) {
            $scope.game = response.data;

            $scope.loadingGame = false;
            $scope.showAddScore = angular.isNumber($scope.game.homeAddScore);
            $scope.showPenalties = angular.isNumber($scope.game.homePenalties);
        }

        function checkUserVoted() {
            apiSrv.get('/api/game/' + gameId + '/tote/true', null,
                function (response) {
                    $scope.showToteResult = response.data.success === true;
                },
                voteFailed);
        }

        function loadResults() {
            apiSrv.get('/api/game/' + gameId + '/tote', null,
                function (response) {
                    $scope.toteResult = response.data;
                    setResults($scope.toteResult);
                },
                voteFailed);
        }

        function setResults(data) {
            var totalVotes = data.WinsCount + data.DrawsCount + data.LosesCount;

            $scope.homeWin = (data.WinsCount / totalVotes * 100).toFixed(2);
            $scope.draw = (data.DrawsCount / totalVotes * 100).toFixed(2);
            $scope.awayWin = (data.LosesCount / totalVotes * 100).toFixed(2);
        }

        function voteFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();
