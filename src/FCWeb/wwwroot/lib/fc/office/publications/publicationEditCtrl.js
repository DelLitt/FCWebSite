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
        $scope.urlKeyRegexPattern = configSrv.Current.urlKeyRegexPattern;
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

        loadData($routeParams.id || 0);

        function loadData(publicationId) {
            if (publicationId < 0) {
                return;
            }

            if (publicationId == 0) {
                publicationsSrv.createPublication(publicationLoaded);
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
            $scope.settingsVisibility = configSrv.Current.SettingsVisibility;
            $scope.currentVisibility = {
                main: (publication.visibility & configSrv.Current.SettingsVisibility.Main) == configSrv.Current.SettingsVisibility.Main,
                news: (publication.visibility & configSrv.Current.SettingsVisibility.News) == configSrv.Current.SettingsVisibility.News,
                reserve: (publication.visibility & configSrv.Current.SettingsVisibility.Reserve) == configSrv.Current.SettingsVisibility.Reserve,
                youth: (publication.visibility & configSrv.Current.SettingsVisibility.Youth) == configSrv.Current.SettingsVisibility.Youth,
                authorized: (publication.visibility & configSrv.Current.SettingsVisibility.Authorized) == configSrv.Current.SettingsVisibility.Authorized
            }

            $scope.galleryId = angular.isNumber(publication.imageGalleryId) ? publication.imageGalleryId : -1;
            $scope.galleryInitUrl = '/api/galleries/' + $scope.galleryId;

            $scope.videoId = angular.isNumber(publication.videoId) ? publication.videoId : -1;
            $scope.videoInitUrl = '/api/videos/' + $scope.videoId;
        }

        function saveEdit(form) {

            $scope.submitted = true;

            if (!form.$valid) {
                return;
            }

            var visibility = {
                main: $scope.currentVisibility.main ? configSrv.Current.SettingsVisibility.Main : 0,
                news: $scope.currentVisibility.news ? configSrv.Current.SettingsVisibility.News : 0,
                reserve: $scope.currentVisibility.reserve ? configSrv.Current.SettingsVisibility.Reserve : 0,
                youth: $scope.currentVisibility.youth ? configSrv.Current.SettingsVisibility.Youth : 0,
                authorized: $scope.currentVisibility.authorized ? configSrv.Current.SettingsVisibility.Authorized : 0,
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
