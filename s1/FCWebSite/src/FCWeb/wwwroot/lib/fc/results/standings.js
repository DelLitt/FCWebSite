(function () {
    'use strict';

    angular
        .module('fc')
        .controller('standingsCtrl', standingsCtrl);

    standingsCtrl.$inject = ['$scope', '$translate', 'configSrv'];

    function standingsCtrl($scope, $translate, configSrv) {

        $translate('MAIN_TEAM_RESULTS').then(function (translation) {
            $scope.title = translation;
        });

        $scope.tourneyOptions = [
            { index: 0, team: "MAIN_TEAM", tourneyId: configSrv.Current.MainTeamTourneyIds[1] },
            { index: 1, team: "RESERVE_TEAM", tourneyId: configSrv.Current.ReserveTeamTourneyIds[0] }
        ]

        $scope.teamId = configSrv.Current.MainTeamId;
        $scope.hasData = false;


        // TODO: Change config to promise
        //scope.mainTeamId = 
        //configSrv.mainTeamTourneyIds
    }
})();
