(function () {
    'use strict';

    angular.module('fc', ['fc.core', 'fc.ui', 'pascalprecht.translate', 'ngSanitize'])
        .config(config);

    config.$inject = ['$routeProvider', '$locationProvider', '$translateProvider'];

    function config($routeProvider, $locationProvider, $translateProvider) {

        $routeProvider
            .when("/", {
                templateUrl: "lib/fc/home/index.html",
                controller: "indexCtrl"
            })
            .when("/publication/:id", {
                templateUrl: "lib/fc/publication/publication.html",
                controller: "publicationCtrl"
            })
            .otherwise({ redirectTo: "/" });

        $locationProvider.html5Mode(true);

        $translateProvider.useStaticFilesLoader({
            prefix: '/static/lang/',
            suffix: '.json'
        });

        $translateProvider.preferredLanguage('ru');
        $translateProvider.useSanitizeValueStrategy(null);
        //$translateProvider.useLocalStorage();
    }

})();