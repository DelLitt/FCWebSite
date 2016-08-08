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

        $scope.editGame = function (game) {

            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'lib/fc/layout/office/games/gameEdit.html',
                controller: 'gameEditCtrl',
                //size: size,
                resolve: {
                    items: function () {
                        return {
                            game: game
                        };
                    }
                }
            });

            modalInstance.result.then(function (round) {
                //$scope.selected = selectedItem;
                notificationManager.displayInfo(round.name);
            }, function () {
                notificationManager.displayInfo('Modal dismissed at: ' + new Date());
            });
        };

        //$scope.editRound = function (round) {

        //    if (!angular.isObject(round)) {
        //        round = angular.copy(configSrv.Current.EmptyRound);
        //    }

        //    var roundScope = $scope.$new();
        //    roundScope.round = round;
        //    roundScope.onSave = function (roundScope) { console.log("Outer save click! " + roundScope.round.name); }
        //    round.roundScope = roundScope;

        //    var compiledDirective = $compile("<quick-round-edit></quick-round-edit>");
        //    var directiveElement = compiledDirective(round.roundScope);
        //    $('#new-round-place').append(directiveElement);
        //}

        function onSave(roundScope) {
            if (!angular.isObject(roundScope)) {
                return;
            }

            roundScope.$destroy();
            $('.my-directive-placeholder').empty();
        }

        $scope.stage = 'Add';
        var childScope;
        $scope.incq = 0;

        $scope.toggleStage = function () {
            $scope.incq++;
            if ($scope.stage === 'Add') {
                $scope.stage = 'Remove';

                childScope = $scope.$new();
                childScope.inc = $scope.incq;
                var compiledDirective = $compile("<quick-game-edit'></quick-game-edit>");
                var directiveElement = compiledDirective(childScope);
                $('.my-directive-placeholder').append(directiveElement);
            } else {
                childScope.$destroy();
                $('.my-directive-placeholder').empty();
                $scope.stage = 'Add';
            }
        }
    }
})();
