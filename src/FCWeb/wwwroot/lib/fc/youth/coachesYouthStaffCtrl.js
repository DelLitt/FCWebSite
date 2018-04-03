(function () {
    'use strict';

    angular
        .module('fc')
        .controller('coachesYouthStaffCtrl', coachesYouthStaffCtrl);

    coachesYouthStaffCtrl.$inject = ['$scope', 'configSrv', 'personsSrv'];

    function coachesYouthStaffCtrl($scope, configSrv, personsSrv) {
        $scope.youthCoaches = [];

        personsSrv.loadCoachesStaff(configSrv.Current.MainTeamId, staffLoaded);

        function staffLoaded(response) {
            var persons = response.data;

            if(!angular.isArray(persons) && persons.length > 0) {
                return;
            }

            var youth = [];
            for (var i = 0; i < persons.length; i++) {
                if (persons[i].roleId == configSrv.Current.PersonRoleIds.CoachYouth) {
                    youth.push(persons[i]);
                }
            }

            if (youth.length > 0) {
                $scope.youthCoaches = youth;
            }
        }
    }
})();
