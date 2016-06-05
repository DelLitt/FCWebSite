(function () {
    'use strict';

    angular
        .module('fc')
        .controller('seniorStaffCtrl', seniorStaffCtrl);

    seniorStaffCtrl.$inject = ['$scope', 'configSrv', 'personsSrv'];

    function seniorStaffCtrl($scope, configSrv, personsSrv) {

        $scope.teamId = configSrv.getMainTeamId();
        $scope.publicationsCount = configSrv.teamPublicationsCount;
        $scope.title = 'SENIOR_STAFF';
        $scope.persons = [];

        personsSrv.loadDirectionStaff($scope.teamId, staffLoaded);

        function staffLoaded(response) {
            $scope.persons = response.data;
        }
    }
})();
