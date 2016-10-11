(function () {
    'use strict';

    angular
        .module('fc')
        .controller('teamCtrl', teamCtrl);

    teamCtrl.$inject = ['$scope', '$routeParams', '$sce', 'teamsSrv', 'helper'];

    function teamCtrl($scope, $routeParams, $sce, teamsSrv, helper) {

        var teamId = $routeParams.id;

        $scope.loadingTeam = true;
        $scope.loadingImage = helper.getLoadingImg();
        $scope.showExtended = false;

        loadData();

        function loadData() {
            teamsSrv.loadTeam(teamId, teamLoaded);
        };

        function teamLoaded(response) {
            $scope.team = response.data;
            $scope.loadingTeam = false;

            $scope.showExtended = helper.hasTeamExtendedInfo($scope.team);
            $scope.imageLogo = helper.getTeamImage($scope.team);
            $scope.description = $sce.trustAsHtml(helper.getTeamDescription($scope.team));
        }
    }
})();
