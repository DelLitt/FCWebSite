(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('publicationEditCtrl', publicationEditCtrl);

    publicationEditCtrl.$inject = ['$scope', '$routeParams', 'publicationsSrv', 'fileBrowserSrv', 'configSrv'];

    function publicationEditCtrl($scope, $routeParams, publicationsSrv, fileBrowserSrv, configSrv) {

        if (!angular.isDefined($scope.forms)) {
            $scope.forms = {};
        }

        var saveCallbackName = "saveCallback";

        var videoId = -1;

        $scope.video = { 
            id: videoId,
            callbackData: {
                saveCallbackName: saveCallbackName
            }
        }

        $scope.loading = true;
        $scope.videoId = videoId;
        $scope.openFileBrowser = openFileBrowser;
        $scope.saveEdit = saveEdit;
        $scope.publication = {};
        $scope.dateOptions = {
            showWeeks: false
        };

        function openFileBrowser() {
            fileBrowserSrv.open(
                publicationsSrv.getImagesPath(),
                publicationsSrv.getImagesPath(),
                function (selectedFile) {
                    $scope.publication.image = selectedFile.path;
                });
        }

        // handle video autocomplete
        $scope.$watch(function (scope) {
            return scope.videoId;
        },
        function (newValue, oldValue) {
            $scope.video.id = newValue;
        });

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

            $scope.videoId = angular.isNumber(publication.videoId) ? publication.videoId : -1;
        }

        function saveEdit(form) {

            $scope.submitted = true;

            if (!form.$valid) {

                $scope.customValidity = true;

                // child forms aren't being checked, they will be checked in callback below
                if (angular.isDefined($scope.video.callbackData)
                    && angular.isDefined($scope.video.callbackData.formName)) {
                        angular.forEach(form.$error.required, function (field) {
                            if (field.$invalid 
                                && field.$name.endsWith('.' + this.video.callbackData.formName)) {
                                this.customValidity = this.customValidity && true;
                            } else {
                                this.customValidity = false;
                            }
                        }, $scope);
                }

                if (!$scope.customValidity) {
                    return;
                }                
            }

            if (angular.isDefined($scope.video.callbackData)
                && angular.isFunction($scope.video.callbackData[saveCallbackName])) {
                if (!$scope.video.callbackData[saveCallbackName]()) {
                    return;
                }
            }

            var visibility = {
                main: $scope.currentVisibility.main ? configSrv.settingsVisibility.main : 0,
                news: $scope.currentVisibility.news ? configSrv.settingsVisibility.news : 0,
                reserve: $scope.currentVisibility.reserve ? configSrv.settingsVisibility.reserve : 0,
                youth: $scope.currentVisibility.youth ? configSrv.settingsVisibility.youth : 0,
                authorized: $scope.currentVisibility.authorized ? configSrv.settingsVisibility.authorized : 0,
            }

            $scope.publication.visibility = visibility.main | visibility.news | visibility.reserve | visibility.youth | visibility.authorized;

            publicationsSrv.savePublication($scope.publication.id || 0, $scope.publication, publicationSaved);
        }

        function publicationSaved(response) {
            alert(response.data);
        }
    }
})();
