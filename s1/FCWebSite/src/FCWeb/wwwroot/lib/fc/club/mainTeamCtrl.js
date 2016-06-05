(function () {
    'use strict';

    angular
        .module('fc')
        .controller('mainTeamCtrl', mainTeamCtrl);

    mainTeamCtrl.$inject = ['$scope', 'configSrv'];

    function mainTeamCtrl($scope, configSrv) {

        $scope.teamId = configSrv.getMainTeamId();
        $scope.tourneysIds = configSrv.mainTeamTourneyIds;
        $scope.publicationsCount = configSrv.teamPublicationsCount;
        $scope.teamTitle = 'Главаня команда';

        // TODO: Change config to promise
        //scope.mainTeamId = 
        //configSrv.mainTeamTourneyIds
    }
})();
