(function () {
    'use strict';

    angular
        .module('fc')
        .controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', 'unionSrv', 'publicationsSrv', 'imageGallerySrv', 'videosSrv', 'helper', 'configSrv', 'notificationManager'];

    function indexCtrl($scope, unionSrv, publicationsSrv, imageGallerySrv, videosSrv, helper, configSrv, notificationManager) {

        alert("Screen W: " + window.screen.availWidth + ", H: " + window.screen.availHeight);

        $scope.loadingImage = helper.getLoadingImg();

        $scope.publications = {
            loading: true,
            count: 0,
            more: function () {
                publicationsSrv.loadMainPublicationsPack(configSrv.Current.MainPublicationsMoreCount, this.count, morePublicationsLoaded);
            }
        };

        $scope.galleries = {
            loading: true,
            count: 0,
            more: function () {
                imageGallerySrv.loadMainGalleriesPack(configSrv.Current.MainGalleriesMoreCount, this.count, moreGalleriesLoaded);
            }
        };

        $scope.videos = {
            loading: true,
            count: 0,
            more: function () {
                videosSrv.loadMainVideosPack(configSrv.Current.MainVideosMoreCount, this.count, moreVideosLoaded);
            }
        };

        $scope.ranking = {
            loading: true,            
        };

        loadData();

        function loadData() {
            unionSrv.loadMainPagePublications(mainPagePublicationsLoaded);
        }

        function mainPagePublicationsLoaded(response) {
            var publicationItems = response.data;
            var publictionsContent = ['publications', 'imageGalleries', 'videos', 'rankingTable'];

            for (var i = 0; i < publictionsContent.length; i++) {
                var contentName = publictionsContent[i];

                if (!angular.isArray(publicationItems[contentName])) {
                    alertContentLoadError(contentName);
                    return;
                }

                if (publicationItems[contentName].length == 0) {
                    alertContentLoadWarning(contentName)
                }
            }

            var hotCount = Math.min(configSrv.Current.MainPublicationsHotCount, publicationItems.publications.length);

            $scope.publications.hot = publicationItems.publications.slice(0, hotCount);                
            $scope.publications.rows = helper.formRows(publicationItems.publications, configSrv.Current.MainPublicationsRowCount);
            $scope.publications.count += publicationItems.publications.length;
            $scope.publications.loading = false;

            $scope.galleries.rows = helper.formRows(publicationItems.imageGalleries, configSrv.Current.MainGalleriesRowCount);
            $scope.galleries.count += publicationItems.imageGalleries.length;
            $scope.galleries.loading = false;

            $scope.videos.rows = helper.formRows(publicationItems.videos, configSrv.Current.MainVideosRowCount);
            $scope.videos.count += publicationItems.videos.length;
            $scope.videos.loading = false;

            $scope.ranking.name = publicationItems.rankingTable[0].name;
            $scope.ranking.rows = publicationItems.rankingTable[0].rows;
            $scope.ranking.loading = false;
        }

        function morePublicationsLoaded(response) {
            var publications = response.data;

            if (!angular.isArray(publications) && publications.length == 0) {
                return;
            }
            
            var moreRows = helper.formRows(publications, configSrv.Current.MainPublicationsMoreCount)

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

            var moreRows = helper.formRows(videos, configSrv.Current.MainVideosMoreCount)

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

            var moreRows = helper.formRows(galleries, configSrv.Current.MainGalleriesMoreCount)

            for (var i = 0; i < moreRows.length; i++) {
                $scope.galleries.rows.push(moreRows[i]);
            }

            $scope.galleries.count += galleries.length;
        }

        function alertContentLoadWarning(contentName) {
            notificationManager.displayError("There are no '" + contentName + "' of the main page.");
        }

        function alertContentLoadError(contentName) {
            notificationManager.displayError("An error occured while loading of '" + contentName + "'");
        }
    }
})();
