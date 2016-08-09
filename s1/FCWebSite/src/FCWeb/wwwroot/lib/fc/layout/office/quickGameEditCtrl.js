(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('quickGameEditCtrl', quickGameEditCtrl);

    quickGameEditCtrl.$inject = ['$scope', '$uibModalInstance', 'gamesSrv', 'configSrv', 'game', 'roundId'];

    function quickGameEditCtrl($scope, $uibModalInstance, gamesSrv, configSrv, game, roundId) {

        $scope.dateOptions = {
            showWeeks: false
        };

        $scope.game = angular.isObject(game)
            ? angular.copy(game)
            : angular.copy(configSrv.Current.EmptyGame);

        $scope.game.gameDate = new Date(game.gameDate);
        $scope.game.roundId = roundId;

        $scope.ok = function () {
            $uibModalInstance.close($scope.game);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        function gameSaved(response) {
            game = response.data;

            $uibModalInstance.close(game);
        }
    }
})();
