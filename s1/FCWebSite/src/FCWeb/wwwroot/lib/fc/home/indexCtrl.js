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

        function loadData() {
            publicationsSrv.loadLatestPublications(7, latestPublicationsLoaded);
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
