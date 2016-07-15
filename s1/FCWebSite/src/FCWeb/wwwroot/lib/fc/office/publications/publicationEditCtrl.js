(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('publicationEditCtrl', publicationEditCtrl);

    publicationEditCtrl.$inject = ['$scope', '$routeParams', '$location', 'publicationsSrv', 'fileBrowserSrv', 'configSrv', 'helper'];

    function publicationEditCtrl($scope, $routeParams, $location, publicationsSrv, fileBrowserSrv, configSrv, helper) {

        if (!angular.isDefined($scope.forms)) {
            $scope.forms = {};
        }

        $scope.loading = true;
        $scope.galleryId = -1;
        $scope.videoId = -1;
        $scope.openFileBrowser = openFileBrowser;
        $scope.saveEdit = saveEdit;
        $scope.titleChanged = titleChanged;
        $scope.urlKeyRegexPattern = configSrv.urlKeyRegexPattern;
        $scope.publication = {};
        $scope.dateOptions = {
            showWeeks: false
        };

        function titleChanged(value) {
            $scope.publication.urlKey = helper.createUrlKey(value);
        }

        function openFileBrowser() {
            fileBrowserSrv.open(
                publicationsSrv.getImagesPath(),
                publicationsSrv.getImagesPath(),
                true,
                true,
                false,
                function (selectedFile) {
                    $scope.publication.image = selectedFile.path;
                });
        }

        loadData($routeParams.id);

        function loadData(publicationId) {
            if (publicationId < 0) {
                return;
            }

            publicationsSrv.loadPublication(publicationId, publicationLoaded);
        }

        function publicationLoaded(response) {
            var publication = response.data;

            $scope.publication = publication;
            $scope.publication.dateDisplayed = new Date(publication.dateDisplayed);
            $scope.publication.dateChanged = new Date(publication.dateChanged);
            $scope.publication.dateCreated = new Date(publication.dateCreated);
            $scope.settingsVisibility = configSrv.settingsVisibility;
            $scope.currentVisibility = {
                main: (publication.visibility & configSrv.settingsVisibility.main) == configSrv.settingsVisibility.main,
                news: (publication.visibility & configSrv.settingsVisibility.news) == configSrv.settingsVisibility.news,
                reserve: (publication.visibility & configSrv.settingsVisibility.reserve) == configSrv.settingsVisibility.reserve,
                youth: (publication.visibility & configSrv.settingsVisibility.youth) == configSrv.settingsVisibility.youth,
                authorized: (publication.visibility & configSrv.settingsVisibility.authorized) == configSrv.settingsVisibility.authorized
            }

            $scope.galleryId = angular.isNumber(publication.imageGalleryId) ? publication.imageGalleryId : -1;
            $scope.galleryInitUrl = '/api/galleries/' + $scope.imageGalleryId;

            $scope.videoId = angular.isNumber(publication.videoId) ? publication.videoId : -1;
            $scope.videoInitUrl = '/api/videos/' + $scope.videoId;
        }

        function saveEdit(form) {

            $scope.submitted = true;

            if (!form.$valid) {
                return;
            }

            var visibility = {
                main: $scope.currentVisibility.main ? configSrv.settingsVisibility.main : 0,
                news: $scope.currentVisibility.news ? configSrv.settingsVisibility.news : 0,
                reserve: $scope.currentVisibility.reserve ? configSrv.settingsVisibility.reserve : 0,
                youth: $scope.currentVisibility.youth ? configSrv.settingsVisibility.youth : 0,
                authorized: $scope.currentVisibility.authorized ? configSrv.settingsVisibility.authorized : 0,
            }

            $scope.publication.visibility = visibility.main | visibility.news | visibility.reserve | visibility.youth | visibility.authorized;

            $scope.publication.imageGalleryId = $scope.galleryId;
            $scope.publication.videoId = $scope.videoId;

            publicationsSrv.savePublication($scope.publication.id || 0, $scope.publication, publicationSaved);
        }

        function publicationSaved(response) {
            $location.path('/office/publications');
        }
    }
})();
