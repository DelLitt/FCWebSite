(function () {
    'use strict';

    angular
        .module('fc')
        .controller('mainTeamCtrl', mainTeamCtrl);

    mainTeamCtrl.$inject = ['$scope', 'personsSrv', 'configSrv', 'helper', 'filterFilter'];

    function mainTeamCtrl($scope, personsSrv, configSrv, helper, filterFilter) {

        // TODO: Change config to promise
         var mainTeamId = configSrv.getMainTeamId();

         $scope.loading = true;

        $scope.goalkeepers = {};
        $scope.defenders = {};
        $scope.midfielders = {};
        $scope.forwards = {};
        
        loadData();

        function loadData() {

            personsSrv.loadTeamMainPlayers(mainTeamId, mainTeamLoaded);
        }

        function mainTeamLoaded(response) {
            var persons = response.data;

            var goalkeepers = filterFilter(persons, { roleId: configSrv.positions.rrGoalkeeper });
            var defenders = filterFilter(persons, { roleId: configSrv.positions.rrDefender });
            var midfielders = filterFilter(persons, { roleId: configSrv.positions.rrMidfielder });
            var forwards = filterFilter(persons, { roleId: configSrv.positions.rrForward });

            $scope.goalkeepers.rows = goalkeepers.length > 0 ? helper.formRows(goalkeepers, 3, 0) : [];
            $scope.defenders.rows = defenders.length > 0 ? helper.formRows(defenders, 3, 0) : [];
            $scope.midfielders.rows = midfielders.length > 0 ? helper.formRows(midfielders, 3, 0) : [];
            $scope.forwards.rows = forwards.length > 0 ? helper.formRows(forwards, 3, 0) : [];
            //$scope.person.BirthDate = new Date(person.BirthDate);
        }
    }
})();
