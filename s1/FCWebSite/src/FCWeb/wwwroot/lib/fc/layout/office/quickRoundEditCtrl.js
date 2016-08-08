(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('quickRoundEditCtrl', quickRoundEditCtrl);

    quickRoundEditCtrl.$inject = ['$scope', '$uibModalInstance', 'roundsSrv', 'configSrv', 'round', 'tourneyId'];

    function quickRoundEditCtrl($scope, $uibModalInstance, roundsSrv, configSrv, round, tourneyId) {

        $scope.round = angular.isObject(round)
            ? angular.copy(round)
            : angular.copy(configSrv.Current.EmptyRound);

        $scope.round.editMode = true;
        $scope.round.tourneyId = tourneyId;

        var selectedTeam = null;

        $scope.onTeamSelect = function (team) {
            selectedTeam = team;
        }

        $scope.addRoundTeam = function () {
            if (angular.isObject(selectedTeam)) {
                var teamView = angular.copy(configSrv.Current.EmptyEntityLink);

                teamView.id = selectedTeam.id;
                teamView.text = selectedTeam.name;
                teamView.tile = selectedTeam.name;

                if (!angular.isArray($scope.round.teams)) {
                    $scope.round.teams = [];
                }

                $scope.round.teams.push(teamView);
            }
        }

        $scope.removeRoundTeam = function (index) {
            $scope.round.teams.splice(index, 1);
        }

        $scope.ok = function () {
            if (angular.isArray($scope.round.teams)) {
                var teamList = '';
                for (var i = 0; i < $scope.round.teams.length; i++) {
                    teamList += $scope.round.teams[i].id.toString();

                    if(i < $scope.round.teams.length - 1) {
                        teamList += ',';
                    }
                }

                teamList = '[' + teamList + ']';
            } else {
                $scope.round.teamList = "[]";
            }

            roundsSrv.saveRound($scope.round.id, $scope.round, roundSaved);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        function roundSaved(response) {
            round = response.data;

            $uibModalInstance.close(round);
        }
    }
})();
