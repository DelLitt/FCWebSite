(function () {
    'use strict';

    angular
        .module('fc')
        .controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', 'rankingsSrv', 'publicationsSrv', 'gamesSrv', 'helper', 'configSrv'];

    function indexCtrl($scope, rankingsSrv, publicationsSrv, gamesSrv, helper, configSrv) {

        $scope.publications = {
            loading: true,
            count: 0,
            more: function () {
                publicationsSrv.loadPublicationsPack(configSrv.mainPublicationsMoreCount, this.count, morePublicationsLoaded);
            }
        };

        $scope.ranking = {
            loading: true
        };

        loadData();

        function loadData() {
            publicationsSrv.loadLatestPublications(configSrv.mainPublicationsCount, latestPublicationsLoaded);
            rankingsSrv.loadRankingTable(configSrv.mainTableTourneyId, rankingLoaded);
        }

        function latestPublicationsLoaded(response) {
            var publications = response.data;
            var hotCount = 1;

            if (angular.isArray(publications) && publications.length > 0) {
                hotCount = Math.min(configSrv.mainPublicationsHotCount, publications.length);
            }

            $scope.publications.hot = publications.length > 0 
                ? publications.slice(0, hotCount)
                : [];

            $scope.publications.rows = publications.length > hotCount
                ? helper.formRows(publications, configSrv.mainPublicationsRowCount, hotCount + 1)
                : [];

            $scope.publications.count += publications.length;
        }

        function morePublicationsLoaded(response) {
            var publications = response.data;

            if (!angular.isArray(publications) && publications.length == 0) {
                return;
            }
            
            var moreRows = helper.formRows(publications, configSrv.mainPublicationsRowCount)

            for (var i = 0; i < morePublicationsLoaded.length; i++) {
                $scope.publications.rows.push(moreRows[i]);
            }

            $scope.publications.count += publications.length;
        }

        function rankingLoaded(response) {
            var ranking = response.data;

            $scope.ranking.name = ranking.name;
            $scope.ranking.rows = ranking.rows;
        }
    }
})();
