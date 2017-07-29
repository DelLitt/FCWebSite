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

        $scope.teamOptions = [
            { index: 0, team: "MAIN_TEAM", tourneysIds: configSrv.Current.MainTeamTourneyIds },
            { index: 1, team: "RESERVE_TEAM", tourneysIds: configSrv.Current.ReserveTeamTourneyIds }
        ]

        $scope.teamId = configSrv.Current.MainTeamId;

        // TODO: Change config to promise
        //scope.mainTeamId = 
        //configSrv.mainTeamTourneyIds
    }
})();
