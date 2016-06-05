(function () {
    'use strict';

    angular
        .module('fc')
        .controller('specialistsStaffCtrl', specialistsStaffCtrl);

    specialistsStaffCtrl.$inject = ['$scope', 'configSrv', 'personsSrv'];

    function specialistsStaffCtrl($scope, configSrv, personsSrv) {

        $scope.teamId = configSrv.getMainTeamId();
        $scope.publicationsCount = configSrv.teamPublicationsCount;
        $scope.title = 'SPECIALISTS_STAFF';
        $scope.persons = [];

        personsSrv.loadSpecialistsStaff($scope.teamId, staffLoaded);

        function staffLoaded(response) {
            $scope.persons = response.data;
        }
    }
})();
