(function () {
    'use strict';

    angular
        .module('fc')
        .controller('resultsMainTeamCtrl', resultsMainTeamCtrl);

    resultsMainTeamCtrl.$inject = ['$scope', 'configSrv'];

    function resultsMainTeamCtrl($scope, configSrv) {

        $scope.teamId = configSrv.Current.MainTeamId;
        $scope.tourneysIds = configSrv.Current.MainTeamTourneyIds;
        $scope.publicationsCount = configSrv.Current.TeamPublicationsCount;
        $scope.title = 'Результаты игр главной команды';

        // TODO: Change config to promise
        //scope.mainTeamId = 
        //configSrv.mainTeamTourneyIds
    }
})();
