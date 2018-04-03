(function () {
    'use strict';

    angular
        .module('fc')
        .controller('youthTeamsCtrl', youthTeamsCtrl);

    youthTeamsCtrl.$inject = ['$scope', 'teamsSrv', 'configSrv', 'helper'];

    function youthTeamsCtrl($scope, teamsSrv, configSrv, helper) {

        $scope.loadingImage = helper.getLoadingImg();
        $scope.loadingYT = true;
        $scope.teamRows = [];
        $scope.publicationsFilter = ['youth'];
        
        loadData();

        function loadData(urlKey) {
            teamsSrv.loadYothTeams(configSrv.Current.MainTeamId, teamsLoaded);
        }

        function teamsLoaded(response) {
            $scope.teamRows = helper.formRows(response.data, 2, 0);
            $scope.loadingYT = false;
        }
    }
})();
