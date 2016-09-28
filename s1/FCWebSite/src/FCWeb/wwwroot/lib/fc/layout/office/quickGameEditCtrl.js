(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('quickGameEditCtrl', quickGameEditCtrl);

    quickGameEditCtrl.$inject = ['$scope', '$uibModalInstance', 'gamesSrv', 'configSrv', 'helper', 'notificationManager', 'game', 'round'];

    function quickGameEditCtrl($scope, $uibModalInstance, gamesSrv, configSrv, helper, notificationManager, game, round) {

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        if (!angular.isObject(round)) {
            notificationManager.displayError("Unable to edit game dialog: round is not defined!");
            $scope.cancel();
        }

        //if (!angular.isArray(round.teams)) {
        //    notificationManager.displayError("Unable to edit game dialog: teams available of the round is not defined!");
        //    $scope.cancel();
        //}

        $scope.round = round;

        $scope.dateOptions = {
            showWeeks: false
        };        

        if(angular.isObject(game)) {
            $scope.game = angular.copy(game);
            $scope.game.gameDate = angular.isDate(game.gameDate)
                ? game.gameDate
                : new Date(game.gameDate);
        } else {
            $scope.game = angular.copy(configSrv.Current.EmptyGame);
            $scope.game.roundId = round.id;
            $scope.game.gameDate = new Date();
        }        

        $scope.stadiumInitUrl = angular.isNumber($scope.game.stadiumId)
            ? '/api/stadiums/' + $scope.game.stadiumId
            : null;

        $scope.ok = function () {
            gamesSrv.saveGame($scope.game.id, $scope.game, gameSaved);
        };

        function gameSaved(response) {
            var savedGame = response.data;
            notificationManager.displayInfo('Game (ID: ' + savedGame.id + ') was saved successfully!');

            $uibModalInstance.close(savedGame);
        }
    }
})();
