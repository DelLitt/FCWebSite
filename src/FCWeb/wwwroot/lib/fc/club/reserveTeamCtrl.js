﻿(function () {
    'use strict';

    angular
        .module('fc')
        .controller('reserveTeamCtrl', reserveTeamCtrl);

    reserveTeamCtrl.$inject = ['$scope', 'configSrv'];

    function reserveTeamCtrl($scope, configSrv) {

        $scope.teamId = configSrv.Current.ReserveTeamId;
        $scope.tourneysIds = configSrv.Current.ReserveTeamTourneyIds;

        // TODO: Change config to promise
        //scope.mainTeamId = 
        //configSrv.mainTeamTourneyIds
    }
})();
