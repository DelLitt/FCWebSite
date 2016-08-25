(function () {
    'use strict';

    angular
        .module('fc')
        .controller('youthTeamsCtrl', youthTeamsCtrl);

    youthTeamsCtrl.$inject = ['$scope', 'teamsSrv', 'configSrv', 'helper'];

    function youthTeamsCtrl($scope, teamsSrv, configSrv, helper) {

        $scope.loadingImage = helper.getLoadingImg();
        $scope.loading = true;
        $scope.teams = [];
        
        loadData();

        function loadData(urlKey) {
            teamsSrv.loadYothTeams(configSrv.Current.MainTeamId, teamsLoaded);
        }

        function teamsLoaded(response) {
            $scope.teams = response.data;           
        }
    }
})();
