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

        $scope.roundResults = {
            previous: previousRound,
            next: nextRound
        };

        loadData();

        function loadData() {
            publicationsSrv.loadLatestPublications(helper.getRandom(0, 20), latestPublicationsLoaded);
            rankingsSrv.loadRankingTable(10, rankingLoaded);
            gamesSrv.roundResultsManager.init(3, [10, 11], roundLoaded);
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

        function roundLoaded(data) {
            if (angular.isObject(data)) {
                $scope.roundResults.name = data.name,
                $scope.roundResults.logo = data.logo,
                $scope.roundResults.dateGames = data.dateGames
            }
        }

        function previousRound() {
            gamesSrv.roundResultsManager.previous(roundLoaded);
        }

        function nextRound() {
            gamesSrv.roundResultsManager.next(roundLoaded);
        }
    }
})();
