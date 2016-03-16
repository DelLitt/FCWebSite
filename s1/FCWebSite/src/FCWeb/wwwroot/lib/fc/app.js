(function () {
    'use strict';

    angular.module('fc', ['fc.core', 'fc.ui', 'pascalprecht.translate'])
        .config(config);

    config.$inject = ['$routeProvider', '$translateProvider'];

    function config($routeProvider, $translateProvider) {

        $routeProvider
            .when("/", {
                templateUrl: "lib/fc/home/index.html",
                controller: "indexCtrl"
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
            .otherwise({ redirectTo: "/" });

        $translateProvider.useStaticFilesLoader({
            prefix: '/static/lang/',
            suffix: '.json'
        });

        $translateProvider.preferredLanguage('ru');
        $translateProvider.useSanitizeValueStrategy(null);
        //$translateProvider.useLocalStorage();
    }

})();