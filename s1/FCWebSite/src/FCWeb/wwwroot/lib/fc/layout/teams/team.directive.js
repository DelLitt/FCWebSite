(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('team', team);

    team.$inject = ['personsSrv', 'configSrv', 'helper', 'filterFilter', 'publicationsSrv', 'tourneysSrv'];

    function team(personsSrv, configSrv, helper, filterFilter, publicationsSrv, tourneysSrv) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                teamId: '=',
                tourneysIds: '=',
                publicationsCount: '=',
                teamTitle: '@'
            },
            link: function link(scope, element, attrs) {

                scope.loadingTeam = true;
                scope.loadingNews = true;
                scope.loadingImage = helper.getLoadingImg();
                scope.listview = false;
                scope.personsLoaded = false;
                scope.tourneysLoaded = false;
                scope.statsLoaded = false;

                scope.persons = {};
                scope.goalkeepers = {};
                scope.defenders = {};
                scope.midfielders = {};
                scope.forwards = {};

                scope.setView = setView;

                loadData();

                function loadData() {                    
                    tourneysSrv.loadTourneys(scope.tourneysIds, tourneysLoaded);
                    personsSrv.loadTeamMainPlayers(scope.teamId, mainTeamLoaded);
                    publicationsSrv.loadMainPublications(scope.publicationsCount, publicationsLoaded);
                }

                function tourneysLoaded(response) {
                    var tourneys = response.data;
                    scope.tourneys = tourneys;

                    scope.tourneysLoaded = true;
                    scope.statsLoaded = scope.personsLoaded && scope.tourneysLoaded;
                }

                function mainTeamLoaded(response) {
                    var persons = response.data;

                    scope.persons = persons;

                    angular.forEach(scope.persons, function (item) {
                        item.flagSrc = helper.getFlagSrc(item.city.countryId);
                    });

                    var goalkeepers = filterFilter(persons, { roleId: configSrv.Current.PersonRoleIds.PlayerGoalkeeper });
                    var defenders = filterFilter(persons, { roleId: configSrv.Current.PersonRoleIds.PlayerDefender });
                    var midfielders = filterFilter(persons, { roleId: configSrv.Current.PersonRoleIds.PlayerMidfielder });
                    var forwards = filterFilter(persons, { roleId: configSrv.Current.PersonRoleIds.PlayerForward });

                    scope.goalkeepers.rows = goalkeepers.length > 0 ? helper.formRows(goalkeepers, 4, 0) : [];
                    scope.defenders.rows = defenders.length > 0 ? helper.formRows(defenders, 4, 0) : [];
                    scope.midfielders.rows = midfielders.length > 0 ? helper.formRows(midfielders, 4, 0) : [];
                    scope.forwards.rows = forwards.length > 0 ? helper.formRows(forwards, 4, 0) : [];

                    scope.personsLoaded = true;
                    scope.statsLoaded = scope.personsLoaded && scope.tourneysLoaded;

                    scope.loadingTeam = false;
                }

                function publicationsLoaded(response) {
                    var publications = response.data;
                    scope.publications = publications;
                    scope.loadingNews = false;
                }

                function setView(index) {
                    scope.listview = index > 0 ? true : false;
                }
            },
            templateUrl: '/lib/fc/layout/teams/team.html'
        }
    }
})();