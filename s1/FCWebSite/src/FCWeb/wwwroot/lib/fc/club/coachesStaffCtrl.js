(function () {
    'use strict';

    angular
        .module('fc')
        .controller('coachesStaffCtrl', coachesStaffCtrl);

    coachesStaffCtrl.$inject = ['$scope', 'configSrv', 'personsSrv'];

    function coachesStaffCtrl($scope, configSrv, personsSrv) {        
        $scope.mainCoaches = [];
        $scope.youthCoaches = [];

        personsSrv.loadCoachesStaff(configSrv.Current.MainTeamId, staffLoaded);

        function staffLoaded(response) {
            var persons = response.data;

            if(!angular.isArray(persons) && persons.length > 0) {
                return;
            }

            var main = [],
                youth = [];

            for (var i = 0; i < persons.length; i++) {
                if (persons[i].roleId == configSrv.Current.PersonRoleIds.CoachYouth) {
                    youth.push(persons[i]);
                } else {
                    main.push(persons[i]);
                }
            }

            if (main.length > 0) {
                $scope.mainCoaches = main;
            }

            if (youth.length > 0) {
                $scope.youthCoaches = youth;
            }
        }
    }
})();
