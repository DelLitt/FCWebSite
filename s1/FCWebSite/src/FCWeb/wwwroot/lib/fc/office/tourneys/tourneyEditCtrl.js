(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('tourneyEditCtrl', tourneyEditCtrl);

    tourneyEditCtrl.$inject = ['$scope', '$routeParams', '$compile'];

    function tourneyEditCtrl($scope, $routeParams, $compile) {

        $scope.rounds = [
            {
                round: {
                    id: 1,
                    name: "Round 1"
                },
                games: [
                    { 
                        date: "21.02.2016",
                        homeId: 3,
                        awayId: 12,
                        homeName: "Slutsk",
                        awayName: "Minsk",
                        scoreHome: 3,
                        scoreAway: 1,
                        scoreAddHome: null,
                        scoreAddAway: null,
                        scorePenaltyHome: null,
                        scorePenaltyAway: null,
                        stadiumId: 21,
                        played: true
                    },
                    {
                        date: "23.02.2016",
                        homeId: 14,
                        awayId: 22,
                        homeName: "BATE",
                        awayName: "Slaviya",
                        scoreHome: 2,
                        scoreAway: 2,
                        scoreAddHome: null,
                        scoreAddAway: null,
                        scorePenaltyHome: null,
                        scorePenaltyAway: null,
                        stadiumId: 14,
                        played: true
                    }
                ]
            }
        ];

        $scope.addRound = function () {
            $scope.rounds.push(
            {
                round: {
                    id: 0,
                    name: "Round " + $scope.rounds.length
                },
                games: []
            });
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
