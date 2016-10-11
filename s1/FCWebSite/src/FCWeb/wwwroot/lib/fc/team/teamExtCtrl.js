(function () {
    'use strict';

    angular
        .module('fc')
        .controller('teamExtCtrl', teamExtCtrl);

    teamExtCtrl.$inject = ['$scope', '$routeParams', 'teamsSrv', 'helper'];

    function teamExtCtrl($scope, $routeParams, teamsSrv, helper) {

        var teamId = $routeParams.id;

        $scope.loadingTeam = true;
        $scope.loadingImage = helper.getLoadingImg();

        loadData();

        function loadData() {
            teamsSrv.loadTeam(teamId, teamLoaded);
        };

        function teamLoaded(response) {
            $scope.team = response.data;

            $scope.loadingTeam = false;
        }
    }
})();
