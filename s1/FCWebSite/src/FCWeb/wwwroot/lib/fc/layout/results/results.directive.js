(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('results', results);

    results.$inject = ['gamesSrv', 'configSrv', 'helper', 'filterFilter', 'publicationsSrv'];

    function results(gamesSrv, configSrv, helper, filterFilter, publicationsSrv) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                teamId: '=',
                publicationsCount: '=',
                title: '=',
                tourneysIds: '='
            },
            link: function link(scope, element, attrs) {

                scope.loadingGames = true;
                scope.loadingNews = true;
                scope.loadingImage = helper.getLoadingImg();
                scope.personsLoaded = false;

                loadData();

                function loadData() {
                    gamesSrv.loadSchedule(scope.tourneysIds, scheduleLoaded);
                    publicationsSrv.loadLatestPublications(scope.publicationsCount, publicationsLoaded);
                }

                function scheduleLoaded(response) {
                    scope.schedule = response.data;
                    scope.loadingGames = false;
                }

                function publicationsLoaded(response) {
                    var publications = response.data;
                    scope.publications = publications;
                    scope.loadingNews = false;
                }
            },
            templateUrl: '/lib/fc/layout/results/results.html'
        }
    }
})();