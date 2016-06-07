(function () {
    'use strict';

    angular
        .module('fc')
        .controller('coachesStaffCtrl', coachesStaffCtrl);

    coachesStaffCtrl.$inject = ['$scope', 'configSrv', 'personsSrv'];

    function coachesStaffCtrl($scope, configSrv, personsSrv) {

        $scope.teamId = configSrv.getMainTeamId();
        $scope.publicationsCount = configSrv.teamPublicationsCount;
        $scope.title = 'COACHES_STAFF';
        $scope.persons = [];

        personsSrv.loadCoachesStaff($scope.teamId, staffLoaded);

        function staffLoaded(response) {
            $scope.persons = response.data;
        }
    }
})();
