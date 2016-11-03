(function () {
    'use strict';

    angular
        .module('fc')
        .controller('gameCtrl', gameCtrl);

    gameCtrl.$inject = ['$scope', '$routeParams', 'apiSrv', 'gamesSrv', 'protocolSrv', 'helper'];

    function gameCtrl($scope, $routeParams, apiSrv, gamesSrv, protocolSrv, helper) {

        var gameId = $routeParams.id;

        $scope.loadingGame = true;
        $scope.loadingProtocol = true;
        $scope.showAddScore = false;
        $scope.showPenalties = false;
        $scope.showToteResult = false;
        $scope.protocol = {
            home: {},
            away: {},
            allGoals: []
        }

        loadData();

        function loadData() {
            gamesSrv.loadGame(gameId, gameLoaded);
            protocolSrv.loadProtocolText(gameId, protocolLoaded);
        };

        function gameLoaded(response) {
            $scope.game = response.data;

            $scope.showAddScore = angular.isNumber($scope.game.homeAddScore);
            $scope.showPenalties = angular.isNumber($scope.game.homePenalties);
            $scope.homeLink = helper.getTeamViewLink($scope.game.home);
            $scope.awayLink = helper.getTeamViewLink($scope.game.away);
            $scope.logoHome = helper.getTeamImage($scope.game.home);
            $scope.logoAway = helper.getTeamImage($scope.game.away);

            $scope.loadingGame = false;
        }

        function protocolLoaded(response) {
            $scope.protocol = response.data;
            $scope.protocol.allGoals = $scope.protocol.home.goals.concat($scope.protocol.away.goals);
            $scope.protocol.allYellows = $scope.protocol.home.yellows.concat($scope.protocol.away.yellows);
            $scope.protocol.allReds = $scope.protocol.home.reds.concat($scope.protocol.away.reds);

            $scope.loadingProtocol = false;
        }

        //function loadResults() {
        //    apiSrv.get('/api/game/' + gameId + '/tote', null,
        //        function (response) {
        //            $scope.toteResult = response.data;
        //            setResults($scope.toteResult);
        //        },
        //        voteFailed);
        //}

        //$scope.toteResult = {};

        //$scope.vote = function (value) {
        //    if (value < 0 || value > 2) {
        //        return;
        //    }

        //    apiSrv.post('/api/game/' + gameId + '/tote', value, null,
        //        function (response) {
        //            $scope.toteResult = response.data;
        //            setResults($scope.toteResult);
        //            $scope.showToteResult = true;
        //        },
        //        voteFailed);
        //}

        //checkUserVoted();
        //loadResults();

        //function checkUserVoted() {
        //    apiSrv.get('/api/game/' + gameId + '/tote/true', null,
        //        function (response) {
        //            $scope.showToteResult = response.data.success === true;
        //        });
        //}

        //function setResults(data) {
        //    var totalVotes = data.WinsCount + data.DrawsCount + data.LosesCount;

        //    $scope.homeWin = (data.WinsCount / totalVotes * 100).toFixed(2);
        //    $scope.draw = (data.DrawsCount / totalVotes * 100).toFixed(2);
        //    $scope.awayWin = (data.LosesCount / totalVotes * 100).toFixed(2);
        //}
    }
})();
