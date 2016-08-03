(function () {
    'use strict';

    angular.module('fc.admin', ['fc.core', 'fc.ui', 'ui.bootstrap', 'ui.bootstrap.datetimepicker', 'ui.numeric', 'ngFileUpload'])
        .config(config);

    config.$inject = ['$routeProvider', '$locationProvider'];

    function config($routeProvider, $locationProvider) {

        $routeProvider
            .when("/office", {
                templateUrl: "lib/fc/office/index.html",
                controller: "indexCtrl"
            })
            .when("/office/publications/", {
                templateUrl: "lib/fc/office/publications/publicationsList.html",
                controller: "publicationsListCtrl"
            })
            .when("/office/publications/edit/:id", {
                templateUrl: "lib/fc/office/publications/publicationEdit.html",
                controller: "publicationEditCtrl"
            })
            .when("/office/videos/", {
                templateUrl: "lib/fc/office/videos/videosList.html",
                controller: "videosListCtrl"
            })
            .when("/office/videos/edit/:id", {
                templateUrl: "lib/fc/office/videos/videoEdit.html",
                controller: "videoEditCtrl"
            })
            .when("/office/galleries/edit/:id", {
                templateUrl: "lib/fc/office/galleries/imageGalleryEdit.html",
                controller: "imageGalleryEditCtrl"
            })
            .when("/office/galleries/", {
                templateUrl: "lib/fc/office/galleries/imageGalleriesList.html",
                controller: "imageGalleriesListCtrl"
            })
            .when("/office/persons/edit/:id", {
                templateUrl: "lib/fc/office/persons/personEdit.html",
                controller: "personEditCtrl"
            })
            .when("/office/persons/", {
                templateUrl: "lib/fc/office/persons/personsList.html",
                controller: "personsListCtrl"
            })
            .when("/office/team/edit/:id", {
                templateUrl: "lib/fc/office/teams/teamEdit.html",
                controller: "teamEditCtrl"
            })
            .when("/office/game/edit/:id", {
                templateUrl: "lib/fc/office/games/gameEdit.html",
                controller: "gameEditCtrl"
            })
            .when("/office/protocol/edit/:id", {
                templateUrl: "lib/fc/office/games/protocolEdit.html",
                controller: "protocolEditCtrl"
            })
            .when("/office/filebrowser/", {
                templateUrl: "lib/fc/office/utils/fileBrowser.html",
                controller: "fileBrowserCtrl"
            })
            .when("/office/filebrowseradapter/", {
                templateUrl: "lib/fc/office/utils/fileBrowserAdapter.html",
                controller: "fileBrowserAdapterCtrl"
            })
            .when("/office/tournaments/edit/:id", {
                templateUrl: "lib/fc/office/tourneys/tourneyEdit.html",
                controller: "tourneyEditCtrl"
            })
            //.when("/login", {
            //    templateUrl: "scripts/spa/account/login.html",
            //    controller: "loginCtrl"
            //})
            //.when("/register", {
            //    templateUrl: "scripts/spa/account/register.html",
            //    controller: "registerCtrl"
            //})
            .otherwise({ redirectTo: "/office" });

        $locationProvider.html5Mode(true);
    }

})();