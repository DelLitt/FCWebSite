(function () {
    'use strict';

    angular
        .module('fc')
        .controller('coachesStaffCtrl', coachesStaffCtrl);

    coachesStaffCtrl.$inject = ['$scope', 'configSrv'];

    function coachesStaffCtrl($scope, configSrv) {

        $scope.teamId = configSrv.getMainTeamId();
        $scope.publicationsCount = configSrv.teamPublicationsCount;
        $scope.title = 'COACHES_STAFF';

        // TODO: Change config to promise
        //scope.mainTeamId = 
        //configSrv.mainTeamTourneyIds
    }
})();
