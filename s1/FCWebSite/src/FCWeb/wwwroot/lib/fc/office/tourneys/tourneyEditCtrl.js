/// <reference path="../../layout/office/quickroundedit.html" />
(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('tourneyEditCtrl', tourneyEditCtrl);

    tourneyEditCtrl.$inject = ['$scope', '$routeParams', 'configSrv', 'helper', 'tourneysSrv', '$uibModal', 'notificationManager', 'gamesSrv', 'roundsSrv'];

    function tourneyEditCtrl($scope, $routeParams, configSrv, helper, tourneysSrv, $uibModal, notificationManager, gamesSrv, roundsSrv) {

        $scope.loading = true;
        $scope.change = false;
        $scope.tourneyId = $routeParams.id;

        loadData($scope.tourneyId);

        function loadData(tourneyId) {
            if (tourneyId < 0) {
                return;
            }

            tourneysSrv.loadTourney(tourneyId, tourneyLoaded);
        }

        function tourneyLoaded(response) {
            $scope.tourney = response.data;
        }

        $scope.animationsEnabled = true;

        $scope.editRound = function (round) {
            var lScope = $scope;

            var editRound = null;

            if(angular.isObject(round)) {
                editRound = round;
                if (!angular.isArray(editRound.teams)) {
                    editRound.teams = [];
                }
            } else {
                editRound = angular.copy(configSrv.Current.EmptyRound);
                editRound.teams =
                    angular.isArray($scope.tourney.rounds) && $scope.tourney.rounds.length > 0
                    ? $scope.tourney.rounds[$scope.tourney.rounds.length - 1].teams || []
                    : [];
            }

            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'lib/fc/layout/office/quickRoundEdit.html',
                controller: 'quickRoundEditCtrl',
                //size: size,
                resolve: {
                    round: editRound,
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

        $scope.showRanking = function () {
            var lScope = $scope;

            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'lib/fc/layout/office/rankingTablePopup.html',
                controller: 'rankingTablePopupCtrl',
                //size: size,
                resolve: {
                    rankingModel: {
                        tourneyId: $scope.tourney.id,
                        changed: $scope.change
                    }
                }
            });

            modalInstance.result.then(function (response) {

            }, function () {

            });
        }

        $scope.editGame = function (game, round) {
            var lRound = round;

            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'lib/fc/layout/office/quickGameEdit.html',
                controller: 'quickGameEditCtrl',
                //size: size,
                resolve: {
                    game: game,
                    round:  angular.copy(lRound)
                }
            });

            modalInstance.result.then(function (savedGame) {

                if (angular.isObject(game) && game.id > 0) {
                    for (var i = 0; i < lRound.games.length; i++) {
                        if (lRound.games[i].id == savedGame.id) {
                            lRound.games[i] = savedGame;
                        }
                    }
                } else {
                    lRound.games.push(savedGame);
                }

                $scope.change = true;

                notificationManager.displayInfo(savedGame.gameDate);
            }, function () {
                notificationManager.displayInfo('Modal dismissed at: ' + new Date());
            });
        }

        $scope.removeRound = function (round) {
            if (!angular.isObject(round)) {
                notificationManager.displayError('Unable to remove empty round object!');
            }

            var confirmed = confirm("Есть уверенность?");

            if (confirmed) {
                roundsSrv.removeRound(round.id, removeRoundSuccess);
            }
        }

        function removeRoundSuccess(response) {
            var remRoundId = response.data;

            for (var i = 0; i < $scope.tourney.rounds.length; i++) {
                if ($scope.tourney.rounds[i].id == remRoundId) {
                    $scope.tourney.rounds.splice(i, 1);
                    notificationManager.displayInfo('Round (ID: ' + remRoundId + ') was removed successfully!');
                    return;
                }
            }

            $scope.change = true;
        }

        $scope.removeGame = function (game) {
            if (!angular.isObject(game)) {
                notificationManager.displayError('Unable to remove empty game object!');
            }

            var confirmed = confirm("Есть уверенность?");

            if (confirmed) {
                gamesSrv.removeGame(game.id, removeGameSuccess);
            }            
        }

        function removeGameSuccess(response) {
            var remGameId = response.data;

            for (var i = 0; i < $scope.tourney.rounds.length; i++) {
                for (var j = 0; j < $scope.tourney.rounds[i].games.length; j++) {
                    if ($scope.tourney.rounds[i].games[j].id == remGameId) {
                        $scope.tourney.rounds[i].games.splice(j, 1);
                        notificationManager.displayInfo('Game (ID: ' + remGameId + ') was removed successfully!');
                        return;
                    }
                }
            }

            $scope.change = true;
        }
    }
})();
