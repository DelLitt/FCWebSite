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
            .when("/office/publication/edit/:id", {
                templateUrl: "lib/fc/office/publication/publicationEdit.html",
                controller: "publicationEditCtrl"
            })
            .when("/office/person/edit/:id", {
                templateUrl: "lib/fc/office/persons/personEdit.html",
                controller: "personEditCtrl"
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
            //.when("/movies", {
            //    templateUrl: "scripts/spa/movies/movies.html",
            //    controller: "moviesCtrl"
            //})
            //.when("/movies/add", {
            //    templateUrl: "scripts/spa/movies/add.html",
            //    controller: "movieAddCtrl"
            //})
            //.when("/movies/:id", {
            //    templateUrl: "scripts/spa/movies/details.html",
            //    controller: "movieDetailsCtrl"
            //})
            //.when("/movies/edit/:id", {
            //    templateUrl: "scripts/spa/movies/edit.html",
            //    controller: "movieEditCtrl"
            //})
            .otherwise({ redirectTo: "/office" });

        $locationProvider.html5Mode(true);
    }

})();