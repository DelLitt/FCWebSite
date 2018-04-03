(function () {
    'use strict';

    angular
        .module('fc')
        .controller('coachesStaffCtrl', coachesStaffCtrl);

    coachesStaffCtrl.$inject = ['$scope', 'configSrv', 'personsSrv'];

    function coachesStaffCtrl($scope, configSrv, personsSrv) {        
        $scope.mainCoaches = [];

        personsSrv.loadCoachesStaff(configSrv.Current.MainTeamId, staffLoaded);

        function staffLoaded(response) {
            var persons = response.data;

            if(!angular.isArray(persons) && persons.length > 0) {
                return;
            }

            var main = [];
            for (var i = 0; i < persons.length; i++) {
                if (persons[i].roleId != configSrv.Current.PersonRoleIds.CoachYouth) {
                    main.push(persons[i]);
                }
            }

            if (main.length > 0) {
                $scope.mainCoaches = main;
            }
        }
    }
})();
