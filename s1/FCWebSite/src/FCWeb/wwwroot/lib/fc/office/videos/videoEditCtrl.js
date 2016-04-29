(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('videoEditCtrl', videoEditCtrl);

    videoEditCtrl.$inject = ['$scope', '$routeParams', 'videosSrv', 'configSrv', 'helper'];

    function videoEditCtrl($scope, $routeParams, videosSrv, configSrv, helper) {

        var formName = 'videoEdit';

        $scope.dateOptions = {
            showWeeks: false
        };
        $scope.loading = true;
        $scope.titleChanged = titleChanged;
        $scope.urlKeyRegexPattern = configSrv.urlKeyRegexPattern;
        $scope.single = !isDirective();
        $scope.saveEdit = saveEdit;

        if (isDirective()) {
            $scope.videoId = $scope.videoid;
            $scope.callbackdata[$scope.callbackdata.saveCallbackName] = saveCallback;
            $scope.callbackdata.formName = formName;

            // handle related videoId changes
            $scope.$watch(function (scope) {
                return $scope.videoid;
            },
            function (newValue, oldValue) {
                loadData(newValue);
            });
        } else {
            if (!angular.isDefined($scope.forms)) {
                $scope.forms = {};
            }

            $scope.videoId = parseInt($routeParams.id);
        }

        function isDirective() {
            return angular.isDefined($scope.videoid)
                && angular.isDefined($scope.forms)
                && angular.isDefined($scope.callbackdata);
        }

        loadData($scope.videoId);

        function loadData(videoId) {
            if (!angular.isNumber(videoId) || videoId < 0) {
                $scope.video = {
                    id: null
                };
                $scope.expanded = false;

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
                main: (video.visibility & configSrv.settingsVisibility.main) == configSrv.settingsVisibility.main,
                news: (video.visibility & configSrv.settingsVisibility.news) == configSrv.settingsVisibility.news,
                reserve: (video.visibility & configSrv.settingsVisibility.reserve) == configSrv.settingsVisibility.reserve,
                youth: (video.visibility & configSrv.settingsVisibility.youth) == configSrv.settingsVisibility.youth,
                authorized: (video.visibility & configSrv.settingsVisibility.authorized) == configSrv.settingsVisibility.authorized
            };
            $scope.settingsVisibility = configSrv.settingsVisibility;
            $scope.expanded = angular.isNumber(video.id) && video.id >= 0;
        }

        function titleChanged(value) {
            $scope.video.urlKey = helper.createUrlKey(value);
        }

        function saveCallback() {
            return saveEdit($scope.forms[formName]);
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
                main: $scope.currentVisibility.main ? configSrv.settingsVisibility.main : 0,
                news: $scope.currentVisibility.news ? configSrv.settingsVisibility.news : 0,
                reserve: $scope.currentVisibility.reserve ? configSrv.settingsVisibility.reserve : 0,
                youth: $scope.currentVisibility.youth ? configSrv.settingsVisibility.youth : 0,
                authorized: $scope.currentVisibility.authorized ? configSrv.settingsVisibility.authorized : 0,
            }

            $scope.video.visibility = visibility.main | visibility.news | visibility.reserve | visibility.youth | visibility.authorized;

            videosSrv.saveVideo($scope.video.id || 0, $scope.video, videoSaved);

            return true;
        }

        function videoSaved(response) {
            alert(response.data);
        }
    }
})();
