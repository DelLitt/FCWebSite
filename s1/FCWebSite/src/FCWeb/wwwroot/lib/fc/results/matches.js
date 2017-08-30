(function () {
    'use strict';

    angular
        .module('fc')
        .controller('matchesCtrl', matchesCtrl);

    matchesCtrl.$inject = ['$scope', '$translate', 'configSrv'];

    function matchesCtrl($scope, $translate, configSrv) {

        $translate('MAIN_TEAM_RESULTS').then(function (translation) {
            $scope.title = translation;
        });

        var teamOpts = [
            { index: 0, team: "MAIN_TEAM", tourneysIds: configSrv.Current.MainTeamTourneyIds },
            { index: 1, team: "RESERVE_TEAM", tourneysIds: configSrv.Current.ReserveTeamTourneyIds }
        ]

        $scope.seasonOptions = [
            { index: 0, season: "2017", dateStart: new Date(2017, 1, 1), dateEnd: new Date(2017, 11, 30), teamOptions: [ angular.copy(teamOpts[0]), angular.copy(teamOpts[1])] },
            { index: 1, season: "2016", dateStart: new Date(2016, 1, 1), dateEnd: new Date(2016, 11, 30), teamOptions: [ angular.copy(teamOpts[0])] }
        ]

        $scope.$watch(function (scope) {
            return $scope.seasonOpt;
        },
        function (newValue, oldValue) {
            if (angular.isObject(newValue)) {
                $scope.teamOptions = newValue.teamOptions;
                $scope.teamOpt = $scope.teamOptions[0];
            }
        });

        $scope.teamId = configSrv.Current.MainTeamId;

        // TODO: Change config to promise
        //scope.mainTeamId = 
        //configSrv.mainTeamTourneyIds
    }
})();
