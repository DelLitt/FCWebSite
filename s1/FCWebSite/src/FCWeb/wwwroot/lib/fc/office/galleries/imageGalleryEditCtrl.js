(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('imageGalleryEditCtrl', imageGalleryEditCtrl);

    imageGalleryEditCtrl.$inject = ['$scope', '$routeParams', 'imageGallerySrv', 'configSrv', 'helper'];

    function imageGalleryEditCtrl($scope, $routeParams, imageGallerySrv, configSrv, helper) {

        $scope.fileBrowser = {
            path: 'content/gallery',
            root: 'content/gallery',
            allowRemove: true,
            multiple: true,
            disableSubmit: true,
            options: {
                createNew: false
            },
            onOk: function (selectedFile) {
                alert("Ok!");
            },
            onCancel: function () {
                alert("Cancel!");
            }
        };

        var formName = 'galleryEdit';

        $scope.dateOptions = {
            showWeeks: false
        };
        $scope.loading = true;
        $scope.gallery = {};
        $scope.titleChanged = titleChanged;
        $scope.urlKeyRegexPattern = configSrv.urlKeyRegexPattern;
        $scope.saveEdit = saveEdit;

        $scope.galleryId = parseInt($routeParams.id);

        loadData($scope.galleryId);

        function loadData(galleryId) {
            imageGallerySrv.loadGallery(galleryId, galleryLoaded);
        }

        function galleryLoaded(response) {
            var gallery = response.data;

            $scope.gallery = gallery;
            $scope.gallery.dateDisplayed = new Date(gallery.dateDisplayed);
            $scope.gallery.dateChanged = new Date(gallery.dateChanged);
            $scope.gallery.dateCreated = new Date(gallery.dateCreated);
            $scope.currentVisibility = {
                main: (gallery.visibility & configSrv.settingsVisibility.main) == configSrv.settingsVisibility.main,
                news: (gallery.visibility & configSrv.settingsVisibility.news) == configSrv.settingsVisibility.news,
                reserve: (gallery.visibility & configSrv.settingsVisibility.reserve) == configSrv.settingsVisibility.reserve,
                youth: (gallery.visibility & configSrv.settingsVisibility.youth) == configSrv.settingsVisibility.youth,
                authorized: (gallery.visibility & configSrv.settingsVisibility.authorized) == configSrv.settingsVisibility.authorized
            };
            $scope.settingsVisibility = configSrv.settingsVisibility;

            $scope.fileBrowser.path = gallery.path;
            $scope.fileBrowser.options.createNew = gallery.createNew;
            $scope.fileBrowser.root = gallery.path;
        }

        function titleChanged(value) {
            $scope.gallery.urlKey = helper.createUrlKey(value);
        }

        function saveEdit(form) {

            $scope.submitted = true;

            if (!(angular.isNumber($scope.gallery.id) && $scope.gallery.id >= 0)) {
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

            $scope.gallery.visibility = visibility.main | visibility.news | visibility.reserve | visibility.youth | visibility.authorized;

            imageGallerySrv.saveGallery($scope.gallery.id || 0, $scope.gallery, gallerySaved);

            return true;
        }

        function gallerySaved(response) {
            alert(response.data);
        }
    }
})();
