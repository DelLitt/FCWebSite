(function () {
    'use strict';

    angular
        .module('fc')
        .controller('resultsReserveTeamCtrl', resultsReserveTeamCtrl);

    resultsReserveTeamCtrl.$inject = ['$scope', '$translate', 'configSrv'];

    function resultsReserveTeamCtrl($scope, $translate, configSrv) {

        $translate('RESERVE_TEAM_RESULTS').then(function (translation) {
            $scope.title = translation;
        });

        $scope.teamId = configSrv.Current.MainTeamId;
        $scope.tourneysIds = configSrv.Current.ReserveTeamTourneyIds;
        //$scope.title = 'Результаты';

        // TODO: Change config to promise
        //scope.mainTeamId = 
        //configSrv.mainTeamTourneyIds
    }
})();
