﻿(function () {
    'use strict';

    angular
        .module('fc')
        .controller('resultsMainTeamCtrl', resultsMainTeamCtrl);

    resultsMainTeamCtrl.$inject = ['$scope', 'configSrv'];

    function resultsMainTeamCtrl($scope, configSrv) {

        $scope.teamId = configSrv.getMainTeamId();
        $scope.tourneysIds = configSrv.mainTeamTourneyIds;
        $scope.publicationsCount = configSrv.teamPublicationsCount;
        $scope.title = 'Результаты игр главной команды';

        // TODO: Change config to promise
        //scope.mainTeamId = 
        //configSrv.mainTeamTourneyIds
    }
})();
