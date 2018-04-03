(function () {
    'use strict';

    angular
        .module('fc')
        .controller('seniorStaffCtrl', seniorStaffCtrl);

    seniorStaffCtrl.$inject = ['$scope', 'configSrv', 'personsSrv'];

    function seniorStaffCtrl($scope, configSrv, personsSrv) {

        $scope.teamId = configSrv.Current.MainTeamId;
        $scope.title = 'SENIOR_STAFF';
        $scope.persons = [];

        personsSrv.loadDirectionStaff($scope.teamId, staffLoaded);

        function staffLoaded(response) {
            $scope.persons = response.data;
        }
    }
})();
