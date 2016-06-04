(function () {
    'use strict';

    angular
        .module('fc')
        .controller('mainTeamCtrl', mainTeamCtrl);

    mainTeamCtrl.$inject = ['$scope', 'personsSrv', 'configSrv', 'helper', 'filterFilter', 'publicationsSrv', 'tourneysSrv'];

    function mainTeamCtrl($scope, personsSrv, configSrv, helper, filterFilter, publicationsSrv, tourneysSrv) {

        // TODO: Change config to promise
        $scope.mainTeamId = configSrv.getMainTeamId();

        $scope.loading = true;
        $scope.listview = false;
        $scope.personsLoaded = false;
        $scope.tourneysLoaded = false;
        $scope.statsLoaded = false;        

        $scope.persons = {};
        $scope.goalkeepers = {};
        $scope.defenders = {};
        $scope.midfielders = {};
        $scope.forwards = {};

        loadData();

        function loadData() {
            tourneysSrv.loadTourneys(configSrv.mainTeamTourneyIds, tourneysLoaded);
            personsSrv.loadTeamMainPlayers($scope.mainTeamId, mainTeamLoaded);
            publicationsSrv.loadLatestPublications(7, publicationsLoaded);
        }

        function tourneysLoaded(response) {
            var tourneys = response.data;
            $scope.tourneys = tourneys;

            $scope.tourneysLoaded = true;
            $scope.statsLoaded = $scope.personsLoaded && $scope.tourneysLoaded;
        }

        function mainTeamLoaded(response) {
            var persons = response.data;

            $scope.persons = persons;

            angular.forEach($scope.persons, function (item) {
                item.flagSrc = helper.getFlagSrc(item.city.countryId);
            });

            var goalkeepers = filterFilter(persons, { roleId: configSrv.positions.rrGoalkeeper });
            var defenders = filterFilter(persons, { roleId: configSrv.positions.rrDefender });
            var midfielders = filterFilter(persons, { roleId: configSrv.positions.rrMidfielder });
            var forwards = filterFilter(persons, { roleId: configSrv.positions.rrForward });

            $scope.goalkeepers.rows = goalkeepers.length > 0 ? helper.formRows(goalkeepers, 3, 0) : [];
            $scope.defenders.rows = defenders.length > 0 ? helper.formRows(defenders, 3, 0) : [];
            $scope.midfielders.rows = midfielders.length > 0 ? helper.formRows(midfielders, 3, 0) : [];
            $scope.forwards.rows = forwards.length > 0 ? helper.formRows(forwards, 3, 0) : [];

            $scope.personsLoaded = true;
            $scope.statsLoaded = $scope.personsLoaded && $scope.tourneysLoaded;
            //$scope.person.BirthDate = new Date(person.BirthDate);
        }

        function publicationsLoaded(response) {
            var publications = response.data;
            $scope.publications = publications;
        }
    }
})();
