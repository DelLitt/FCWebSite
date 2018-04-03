(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('videoEditCtrl', videoEditCtrl);

    videoEditCtrl.$inject = ['$scope', '$routeParams', '$location', 'videosSrv', 'configSrv', 'helper'];

    function videoEditCtrl($scope, $routeParams, $location, videosSrv, configSrv, helper) {

        var formName = 'videoEdit';

        $scope.dateOptions = {
            showWeeks: false
        };
        $scope.loading = true;
        $scope.titleChanged = titleChanged;
        $scope.urlKeyRegexPattern = configSrv.Current.urlKeyRegexPattern;
        $scope.saveEdit = saveEdit;

        if (!angular.isDefined($scope.forms)) {
            $scope.forms = {};
        }

        $scope.videoId = parseInt($routeParams.id);

        loadData($scope.videoId || 0);

        function loadData(videoId) {
            if (!angular.isNumber(videoId) || videoId < 0) {
                $scope.video = {
                    id: null
                };
                $scope.expanded = false;

                return;
            }

            if (videoId == 0) {
                videosSrv.createVideo(videoLoaded);
                return;
            }

            videosSrv.loadVideo(videoId, videoLoaded);
        }

        function videoLoaded(response) {
            var video = response.data;

            $scope.video = video;
            $scope.video.dateDisplayed = new Date(video.dateDisplayed);
            $scope.video.dateChanged = new Date(video.dateChanged);
            $scope.video.dateCreated = new Date(video.dateCreated);
            $scope.currentVisibility = {
                main: (video.visibility & configSrv.Current.SettingsVisibility.Main) == configSrv.Current.SettingsVisibility.Main,
                news: (video.visibility & configSrv.Current.SettingsVisibility.News) == configSrv.Current.SettingsVisibility.News,
                reserve: (video.visibility & configSrv.Current.SettingsVisibility.Reserve) == configSrv.Current.SettingsVisibility.Reserve,
                youth: (video.visibility & configSrv.Current.SettingsVisibility.Youth) == configSrv.Current.SettingsVisibility.Youth,
                authorized: (video.visibility & configSrv.Current.SettingsVisibility.Authorized) == configSrv.Current.SettingsVisibility.Authorized
            };
            $scope.settingsVisibility = configSrv.Current.SettingsVisibility;
            $scope.expanded = angular.isNumber(video.id) && video.id >= 0;
        }

        function titleChanged(value) {
            $scope.video.urlKey = helper.createUrlKey(value);
        }

        function saveEdit(form) {

            $scope.submitted = true;

            if (!(angular.isNumber($scope.video.id) && $scope.video.id >= 0)) {
                return true;
            }

            if (!form.$valid) {
                return false;
            }

            var visibility = {
                main: $scope.currentVisibility.main ? configSrv.Current.SettingsVisibility.Main : 0,
                news: $scope.currentVisibility.news ? configSrv.Current.SettingsVisibility.News : 0,
                reserve: $scope.currentVisibility.reserve ? configSrv.Current.SettingsVisibility.Reserve : 0,
                youth: $scope.currentVisibility.youth ? configSrv.Current.SettingsVisibility.Youth : 0,
                authorized: $scope.currentVisibility.authorized ? configSrv.Current.SettingsVisibility.Authorized : 0,
            }

            $scope.video.visibility = visibility.main | visibility.news | visibility.reserve | visibility.youth | visibility.authorized;

            videosSrv.saveVideo($scope.video.id || 0, $scope.video, videoSaved);

            return true;
        }

        function videoSaved(response) {
            $location.path('/office/videos');
        }
    }
})();
