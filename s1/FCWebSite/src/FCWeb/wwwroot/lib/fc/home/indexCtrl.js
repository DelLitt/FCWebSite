(function () {
    'use strict';

    angular
        .module('fc')
        .controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', 'rankingsSrv', 'publicationsSrv', 'gamesSrv', 'helper', 'configSrv', 'videosSrv', 'imageGallerySrv'];

    function indexCtrl($scope, rankingsSrv, publicationsSrv, gamesSrv, helper, configSrv, videosSrv, imageGallerySrv) {

        $scope.publications = {
            loading: true,
            count: 0,
            more: function () {
                publicationsSrv.loadPublicationsPack(configSrv.mainPublicationsMoreCount, this.count, morePublicationsLoaded);
            }
        };

        $scope.videos = {
            loading: true,
            count: 0,
            more: function () {
                videosSrv.loadVideosPack(configSrv.mainVideosMoreCount, this.count, moreVideosLoaded);
            }
        };

        $scope.galleries = {
            loading: true,
            count: 0,
            more: function () {
                imageGallerySrv.loadGalleriesPack(configSrv.mainGalleriesMoreCount, this.count, moreGalleriesLoaded);
            }
        };

        $scope.ranking = {
            loading: true
        };

        loadData();

        function loadData() {
            publicationsSrv.loadMainPublications(configSrv.mainPublicationsCount, mainPublicationsLoaded);
            videosSrv.loadMainVideos(configSrv.mainVideosCount, mainVideosLoaded);
            imageGallerySrv.loadMainGalleries(configSrv.mainGalleriesCount, mainGalleriesLoaded);
            rankingsSrv.loadRankingTable(configSrv.mainTableTourneyId, rankingLoaded);
        }

        function mainPublicationsLoaded(response) {
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

        function mainVideosLoaded(response) {
            var videos = response.data;

            $scope.videos.rows = videos.length > 0
                ? helper.formRows(videos, configSrv.mainVideosRowCount)
                : [];

            $scope.videos.count += videos.length;
        }

        function mainGalleriesLoaded(response) {
            var galleries = response.data;

            $scope.galleries.rows = galleries.length > 0
                ? helper.formRows(galleries, configSrv.mainGalleriesRowCount)
                : [];

            $scope.galleries.count += galleries.length;
        }

        function morePublicationsLoaded(response) {
            var publications = response.data;

            if (!angular.isArray(publications) && publications.length == 0) {
                return;
            }
            
            var moreRows = helper.formRows(publications, configSrv.mainPublicationsMoreCount)

            for (var i = 0; i < moreRows.length; i++) {
                $scope.publications.rows.push(moreRows[i]);
            }

            $scope.publications.count += publications.length;
        }

        function moreVideosLoaded(response) {
            var videos = response.data;

            if (!angular.isArray(videos) && videos.length == 0) {
                return;
            }

            var moreRows = helper.formRows(videos, configSrv.mainVideosMoreCount)

            for (var i = 0; i < moreRows.length; i++) {
                $scope.videos.rows.push(moreRows[i]);
            }

            $scope.videos.count += videos.length;
        }

        function moreGalleriesLoaded(response) {
            var galleries = response.data;

            if (!angular.isArray(galleries) && galleries.length == 0) {
                return;
            }

            var moreRows = helper.formRows(galleries, configSrv.mainGalleriesMoreCount)

            for (var i = 0; i < moreRows.length; i++) {
                $scope.galleries.rows.push(moreRows[i]);
            }

            $scope.galleries.count += galleries.length;
        }

        function rankingLoaded(response) {
            var ranking = response.data;

            $scope.ranking.name = ranking.name;
            $scope.ranking.rows = ranking.rows;
        }
    }
})();
