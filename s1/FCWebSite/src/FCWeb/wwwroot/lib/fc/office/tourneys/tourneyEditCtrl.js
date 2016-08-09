/// <reference path="../../layout/office/quickroundedit.html" />
(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('tourneyEditCtrl', tourneyEditCtrl);

    tourneyEditCtrl.$inject = ['$scope', '$routeParams', '$compile', 'configSrv', 'tourneysSrv', '$uibModal', 'notificationManager'];

    function tourneyEditCtrl($scope, $routeParams, $compile, configSrv, tourneysSrv, $uibModal, notificationManager) {

        $scope.loading = true;

        loadData($routeParams.id);

        function loadData(tourneyId) {
            if (tourneyId < 0) {
                return;
            }

            tourneysSrv.loadTourney(tourneyId, tourneyLoaded);
        }

        function tourneyLoaded(response) {
            var tourney = response.data;

            $scope.tourney = tourney;
        }

        $scope.animationsEnabled = true;

        $scope.editRound = function (round) {
            var lScope = $scope;            

            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'lib/fc/layout/office/quickRoundEdit.html',
                controller: 'quickRoundEditCtrl',
                //size: size,
                resolve: {
                    round: round,
                    tourneyId: $scope.tourney.id
                }
            });

            modalInstance.result.then(function (savedRound) {
                if (angular.isObject(round) && round.id > 0) {
                    for (var i = 0; i < lScope.tourney.rounds.length; i++) {
                        if (lScope.tourney.rounds[i].id == savedRound.id) {
                            lScope.tourney.rounds[i] = savedRound;
                        }
                    }
                } else {
                    lScope.tourney.rounds.push(savedRound);
                }

                notificationManager.displayInfo(savedRound.name);
            }, function () {
                notificationManager.displayInfo('Modal dismissed at: ' + new Date());
            });
        }

        $scope.editGame = function (game, round) {

            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'lib/fc/layout/office/quickGameEdit.html',
                controller: 'quickGameEditCtrl',
                //size: size,
                resolve: {
                    game: game,
                    roundId: round.id
                }
            });

            modalInstance.result.then(function (game) {
                //$scope.selected = selectedItem;
                notificationManager.displayInfo(game.gameDate);
            }, function () {
                notificationManager.displayInfo('Modal dismissed at: ' + new Date());
            });
        };
    }
})();
