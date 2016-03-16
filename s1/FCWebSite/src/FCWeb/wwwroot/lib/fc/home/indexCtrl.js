(function () {
    'use strict';

    angular
        .module('fc')
        .controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', 'rankingsSrv', 'publicationsSrv', 'gamesSrv', 'helper'];

    function indexCtrl($scope, rankingsSrv, publicationsSrv, gamesSrv, helper) {

        $scope.publications = {
            loading : true
        };

        $scope.ranking = {
            loading: true
        };

        loadData();

        var roundResults = gamesSrv.getRoundGames(11, 22) || {};

        function roundLoaded(data) {
            if (angular.isObject(data)) {
                $scope.roundResults.roundName = data.roundName,
                $scope.roundResults.roundLogo = data.roundLogo,
                $scope.roundResults.roundGames = data.roundGames
            }

            console.log("roundLoaded();")
        }

        function previousRound() {
            console.log("previousRound();")
            gamesSrv.roundResultsManager.next(roundLoaded);            
        }

        function nextRound() {
            console.log("nextRound();")
            gamesSrv.roundResultsManager.next(roundLoaded);            
        }

        $scope.roundResults = {
            roundName: roundResults.roundName,
            roundLogo: roundResults.roundLogo,
            roundGames: roundResults.roundGames,
            previous: previousRound,
            next: nextRound
        };

        function loadData() {
            publicationsSrv.loadLatestPublications(helper.getRandom(0, 20), latestPublicationsLoaded);
            rankingsSrv.loadRankingTable(10, rankingLoaded);
        }

        function latestPublicationsLoaded(response) {
            var publications = response.data;

            $scope.publications.main = publications.length > 0 ? publications[0] : null;
            $scope.publications.rows = publications.length > 1 ? helper.formRows(publications, 3, 1) : [];
        }

        function rankingLoaded(response) {
            var ranking = response.data;

            $scope.ranking.name = ranking.name;
            $scope.ranking.rows = ranking.rows;
        }
    }
})();
