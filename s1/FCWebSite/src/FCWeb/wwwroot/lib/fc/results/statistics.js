(function () {
    'use strict';

    angular
        .module('fc')
        .controller('statisticsCtrl', statisticsCtrl);

    statisticsCtrl.$inject = ['$scope', '$translate', 'configSrv', 'personsSrv', 'tourneysSrv'];

    function statisticsCtrl($scope, $translate, configSrv, personsSrv, tourneysSrv) {
        $scope.teamId = configSrv.Current.MainTeamId;
        $scope.tourneysIds = configSrv.Current.MainTeamTourneyIds;

        $scope.statsLoaded = false;
        $scope.personsLoaded = false;
        $scope.tourneysLoaded = false;
        
        //$translate('MAIN_TEAM_RESULTS').then(function (translation) {
        //    $scope.title = translation;
        //});

        //$scope.tourneyOptions = [
        //    { index: 0, team: "MAIN_TEAM", tourneyId: configSrv.Current.MainTeamTourneyIds[2] },
        //    { index: 1, team: "RESERVE_TEAM", tourneyId: configSrv.Current.ReserveTeamTourneyIds[0] }
        //]

        loadData();

        function loadData() {
            tourneysSrv.loadTourneys($scope.tourneysIds, tourneysLoaded);
            personsSrv.loadTeamMainPlayers($scope.teamId, mainTeamLoaded);
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

            //angular.forEach($scope.persons, function (item) {
            //    item.flagSrc = helper.getFlagSrc(item.city.countryId);
            //});

            $scope.personsLoaded = true;
            $scope.statsLoaded = $scope.personsLoaded && $scope.tourneysLoaded;

            $scope.loadingTeam = false;
        }
    }
})();
