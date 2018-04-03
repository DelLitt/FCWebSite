(function () {
    'use strict';

    angular
        .module('fc')
        .controller('mainTeamCtrl', mainTeamCtrl);

    mainTeamCtrl.$inject = ['$scope', 'configSrv'];

    function mainTeamCtrl($scope, configSrv) {

        $scope.teamId = configSrv.Current.MainTeamId;
        $scope.tourneysIds = configSrv.Current.MainTeamTourneyIds;

        // TODO: Change config to promise
        //scope.mainTeamId = 
        //configSrv.mainTeamTourneyIds
    }
})();
