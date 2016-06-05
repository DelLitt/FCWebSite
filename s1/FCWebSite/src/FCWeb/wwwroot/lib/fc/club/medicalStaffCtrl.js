(function () {
    'use strict';

    angular
        .module('fc')
        .controller('medicalStaffCtrl', medicalStaffCtrl);

    medicalStaffCtrl.$inject = ['$scope', 'configSrv', 'personsSrv'];

    function medicalStaffCtrl($scope, configSrv, personsSrv) {

        $scope.teamId = configSrv.getMainTeamId();
        $scope.publicationsCount = configSrv.teamPublicationsCount;
        $scope.title = 'MEDICAL_STAFF';
        $scope.persons = [];

        personsSrv.loadMedicalStaff($scope.teamId, staffLoaded);

        function staffLoaded(response) {
            $scope.persons = response.data;
        }
    }
})();
