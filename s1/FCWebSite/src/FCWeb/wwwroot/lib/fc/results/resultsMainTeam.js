(function () {
    'use strict';

    angular
        .module('fc')
        .controller('resultsMainTeamCtrl', resultsMainTeamCtrl);

    resultsMainTeamCtrl.$inject = ['$scope', '$translate', 'configSrv'];

    function resultsMainTeamCtrl($scope, $translate, configSrv) {

        $translate('MAIN_TEAM_RESULTS').then(function (translation) {
            $scope.title = translation;
        });

        $scope.teamId = configSrv.Current.MainTeamId;
        $scope.tourneysIds = configSrv.Current.MainTeamTourneyIds;
        //$scope.title = 'Результаты';

        // TODO: Change config to promise
        //scope.mainTeamId = 
        //configSrv.mainTeamTourneyIds
    }
})();
