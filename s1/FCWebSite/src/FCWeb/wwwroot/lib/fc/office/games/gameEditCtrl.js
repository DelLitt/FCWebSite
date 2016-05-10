(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('gameEditCtrl', gameEditCtrl);

    gameEditCtrl.$inject = ['$scope', '$routeParams', 'gamesSrv', 'fileBrowserSrv', 'notificationManager'];

    function gameEditCtrl($scope, $routeParams, gamesSrv, fileBrowserSrv, notificationManager) {

        if (!angular.isDefined($scope.forms)) {
            $scope.forms = {};
        }

        $scope.loading = true;
        $scope.saveEdit = saveEdit;
        $scope.game = {};
        $scope.tourneyId = -1;
        $scope.dateOptions = {
            showWeeks: false
        };

        $scope.fileBrowser = {
            path: '',
            root: ''
        };

        var curDate = new Date();
        var currentYear = curDate.getFullYear();

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

            $scope.tourneyInitUrl = angular.isNumber($scope.game.roundId)
                ? "/api/tourneys/round/" + $scope.game.roundId
                : null;
        }

        function saveEdit(form) {

            $scope.submitted = true;

            if (!form.$valid) {
                    return;
            }

            teamsSrv.saveTeam($scope.team.id || 0, $scope.team, teamSaved);
        }

        function teamSaved(response) {
            alert(response.data);
        }
    }
})();
