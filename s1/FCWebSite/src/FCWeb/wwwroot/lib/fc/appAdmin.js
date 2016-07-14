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
            .when("/office/video/edit/:id", {
                templateUrl: "lib/fc/office/videos/videoEdit.html",
                controller: "videoEditCtrl"
            })
            .when("/office/gallery/edit/:id", {
                templateUrl: "lib/fc/office/images/imageGalleryEdit.html",
                controller: "imageGalleryEditCtrl"
            })
            .when("/office/person/edit/:id", {
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