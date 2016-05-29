(function () {
    'use strict';

    angular
        .module('fc')
        .controller('publicationCtrl', publicationCtrl);

    publicationCtrl.$inject = ['$scope', '$routeParams', '$sce', 'publicationsSrv', 'videosSrv'];

    function publicationCtrl($scope, $routeParams, $sce, publicationsSrv, videosSrv, helper) {

        $scope.loading = true;
        $scope.showVideo = false;

        $scope.publication = {};

        var urlKey = $routeParams.id;
        
        loadData(urlKey);

        function loadData(urlKey) {

            publicationsSrv.loadPublicationByUrlKey(urlKey, publicationLoaded);
        }

        function publicationLoaded(response) {
            var publication = response.data;

            $scope.publication = publication;
            $scope.publication.dateDisplayed = new Date(publication.dateDisplayed);
            $scope.publication.dateChanged = new Date(publication.dateChanged);
            $scope.publication.dateCreated = new Date(publication.dateCreated);

            if ($scope.publication.videoId > 0) {
                videosSrv.loadVideo($scope.publication.videoId, videoLoaded);
            }
        }

        function videoLoaded(response) {
            var video = response.data;

            $scope.video = video;
            $scope.video.dateDisplayed = new Date(video.dateDisplayed);
            $scope.video.dateChanged = new Date(video.dateChanged);
            $scope.video.dateCreated = new Date(video.dateCreated);
            $scope.video.codeHTML = $sce.trustAsHtml(video.codeHTML);

            $scope.showVideo = true;
        }
    }
})();
