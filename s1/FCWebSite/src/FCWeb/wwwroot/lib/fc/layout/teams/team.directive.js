(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('team', team);

    team.$inject = ['personsSrv', 'configSrv', 'helper', 'filterFilter', 'tourneysSrv'];

    function team(personsSrv, configSrv, helper, filterFilter, tourneysSrv) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                teamId: '=',
                tourneysIds: '=',
                teamTitle: '@'
            },
            link: function link(scope, element, attrs) {

                scope.loadingTeam = true;
                scope.loadingImage = helper.getLoadingImg();

                scope.persons = {};
                scope.goalkeepers = {};
                scope.defenders = {};
                scope.midfielders = {};
                scope.forwards = {};

                loadData();

                function loadData() {
                    personsSrv.loadTeamMainPlayers(scope.teamId, mainTeamLoaded);
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

                    scope.goalkeepers.rows = goalkeepers.length > 0 ? helper.formRows(goalkeepers, 1000, 0) : [];
                    scope.defenders.rows = defenders.length > 0 ? helper.formRows(defenders, 1000, 0) : [];
                    scope.midfielders.rows = midfielders.length > 0 ? helper.formRows(midfielders, 1000, 0) : [];
                    scope.forwards.rows = forwards.length > 0 ? helper.formRows(forwards, 1000, 0) : [];

                    scope.loadingTeam = false;
                }
            },
            templateUrl: '/lib/fc/layout/teams/team.html'
        }
    }
})();