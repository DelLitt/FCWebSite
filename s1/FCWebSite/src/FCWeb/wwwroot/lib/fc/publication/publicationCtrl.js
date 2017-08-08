(function () {
    'use strict';

    angular
        .module('fc')
        .controller('publicationCtrl', publicationCtrl);

    publicationCtrl.$inject = ['$scope', '$routeParams', '$location', '$sce', 'publicationsSrv', 'imageGallerySrv', 'videosSrv', 'helper'];

    function publicationCtrl($scope, $routeParams, $location, $sce, publicationsSrv, imageGallerySrv, videosSrv, helper) {

        $scope.loadingImage = helper.getLoadingImg();
        $scope.loadingPublication = true;
        $scope.loadingLatest = true;
        $scope.showGallery = false;
        $scope.showVideo = false;

        $scope.publication = {};
        $scope.lastPublications = {};

        $scope.toggleVideo = toggleVideo;
        $scope.toggleGallery = toggleGallery;

        $scope.publicationsFilter = ['Main', 'News', 'Reserve', 'Youth', 'Authorized'];

        var urlKey = $routeParams.id;
        
        loadData(urlKey);

        function loadData(urlKey) {

            publicationsSrv.loadPublicationByUrlKey(urlKey, publicationLoaded);
            publicationsSrv.loadNotFilteredPublications(5, 0, lastPublicationsLoaded);
        }

        function publicationLoaded(response) {
            var publication = response.data;

            $scope.publication = publication;

            $scope.publication.dateDisplayed = new Date(publication.dateDisplayed);
            $scope.publication.dateChanged = new Date(publication.dateChanged);
            $scope.publication.dateCreated = new Date(publication.dateCreated);

            $scope.showGallery = $scope.publication.imageGalleryId > 0;

            if ($scope.publication.videoId > 0) {
                videosSrv.loadVideo($scope.publication.videoId, videoLoaded);
            }

            $scope.loadingPublication = false;
            $scope.image = location.host + "/" + publication.image;

            //document.getElementById('vk_share_button').innerHTML =
            //    VK.Share.button({
            //        url: $location.absUrl(),
            //        title: publication.title,
            //        image: location.host + "/" + publication.image
            //    },
            //    { type: "custom", text: "<img src=\"https://vk.com/images/share_32.png\" width=\"32\" height=\"32\" />" });
        }

        function lastPublicationsLoaded(response) {
            var lastPublications = response.data;

            $scope.lastPublications = lastPublications;
            $scope.loadingLatest = false;
        }

        function videoLoaded(response) {
            var video = response.data;

            $scope.video = video;
            $scope.video.dateDisplayed = new Date(video.dateDisplayed);
            $scope.video.dateChanged = new Date(video.dateChanged);
            $scope.video.dateCreated = new Date(video.dateCreated);
            $scope.video.codeHTML = $sce.trustAsHtml(video.codeHTML);

            $scope.showVideo = true;
            toggleVideo();
        }

        function toggleVideo() {
            $scope.videoOpened = true;
            $scope.galleryOpened = false;
        }

        function toggleGallery() {
            $scope.galleryOpened = true;
            $scope.videoOpened = false;

            if ($scope.showGallery && $scope.galleryId !== $scope.publication.imageGalleryId) {
                $scope.galleryId = $scope.publication.imageGalleryId;
            }
        }
    }
})();
