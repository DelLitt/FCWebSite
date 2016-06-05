(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('staff', staff);

    staff.$inject = ['personsSrv', 'configSrv', 'helper', 'filterFilter', 'publicationsSrv', 'tourneysSrv'];

    function staff(personsSrv, configSrv, helper, filterFilter, publicationsSrv, tourneysSrv) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                teamId: '=',
                tourneysIds: '=',
                publicationsCount: '=',
                teamTitle: '='
            },
            link: function link(scope, element, attrs) {

                scope.loadingTeam = true;
                scope.loadingNews = true;
                scope.loadingImage = helper.getLoadingImg();
                scope.personsLoaded = false;

                scope.persons = {};
                scope.goalkeepers = {};

                loadData();

                function loadData() {                    
                    personsSrv.loadCoachesStaff(scope.teamId, staffLoaded);
                    publicationsSrv.loadLatestPublications(scope.publicationsCount, publicationsLoaded);
                }

                function mainTeamLoaded(response) {
                    var persons = response.data;

                    scope.persons = persons;

                    angular.forEach(scope.persons, function (item) {
                        item.flagSrc = helper.getFlagSrc(item.city.countryId);
                    });

                    var goalkeepers = filterFilter(persons, { roleId: configSrv.positions.rrGoalkeeper });
                    var defenders = filterFilter(persons, { roleId: configSrv.positions.rrDefender });
                    var midfielders = filterFilter(persons, { roleId: configSrv.positions.rrMidfielder });
                    var forwards = filterFilter(persons, { roleId: configSrv.positions.rrForward });

                    scope.goalkeepers.rows = goalkeepers.length > 0 ? helper.formRows(goalkeepers, 3, 0) : [];
                    scope.defenders.rows = defenders.length > 0 ? helper.formRows(defenders, 3, 0) : [];
                    scope.midfielders.rows = midfielders.length > 0 ? helper.formRows(midfielders, 3, 0) : [];
                    scope.forwards.rows = forwards.length > 0 ? helper.formRows(forwards, 3, 0) : [];

                    scope.personsLoaded = true;
                    scope.statsLoaded = scope.personsLoaded && scope.tourneysLoaded;

                    scope.loadingTeam = false;
                }

                function publicationsLoaded(response) {
                    var publications = response.data;
                    scope.publications = publications;
                    scope.loadingNews = false;
                }
            },
            templateUrl: '/lib/fc/layout/teams/team.html'
        }
    }
})();