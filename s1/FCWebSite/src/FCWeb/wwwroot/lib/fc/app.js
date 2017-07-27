(function () {
    'use strict';

    angular.module('fc', ['fc.core', 'fc.ui', 'pascalprecht.translate', 'ngSanitize', 'thatisuday.ng-image-gallery'])
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
            .when("/club/mainteam", {
                templateUrl: "lib/fc/club/mainteam.html",
                controller: "mainTeamCtrl"
            })
            .when("/club/reserveteam", {
                templateUrl: "lib/fc/club/reserveteam.html",
                controller: "reserveTeamCtrl"
            })
            .when("/club/youthteams", {
                templateUrl: "lib/fc/club/youth/youthteams.html",
                controller: "youthTeamsCtrl"
            })
            .when("/club/direction", {
                templateUrl: "lib/fc/club/seniorstaff.html",
                controller: "seniorStaffCtrl"
            })
           .when("/club/coaches", {
               templateUrl: "lib/fc/club/coachesstaff.html",
               controller: "coachesStaffCtrl"
           })
           .when("/club/medics", {
               templateUrl: "lib/fc/club/medicalstaff.html",
               controller: "medicalStaffCtrl"
           })
           .when("/club/specialists", {
               templateUrl: "lib/fc/club/specialistsstaff.html",
               controller: "specialistsStaffCtrl"
           })
           .when("/results/matches", {
               templateUrl: "lib/fc/results/matches.html",
               controller: "matchesCtrl"
           })
           .when("/results/reserveteam", {
               templateUrl: "lib/fc/results/resultsreserveteam.html",
               controller: "resultsReserveTeamCtrl"
           })
            .when("/person/:id", {
                templateUrl: "lib/fc/person/person.html",
                controller: "personCtrl"
            })
            .when("/game/:id", {
                templateUrl: "lib/fc/game/game.html",
                controller: "gameCtrl"
            })
            .when("/team/:id", {
                templateUrl: "lib/fc/team/team.html",
                controller: "teamCtrl"
            })
           .when("/info/tickets", {
               templateUrl: "lib/fc/info/tickets/tickets.html",
               controller: "ticketsCtrl"
           })
           .when("/info/stadium-rules", {
               templateUrl: "lib/fc/info/stadiumrules/stadiumrules.html",
               controller: "stadiumRulesCtrl"
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