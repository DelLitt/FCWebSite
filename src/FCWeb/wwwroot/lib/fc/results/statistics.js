(function () {
    'use strict';

    angular
        .module('fc')
        .controller('statisticsCtrl', statisticsCtrl);

    statisticsCtrl.$inject = ['$scope', '$translate', 'helper', 'personsSrv', 'tourneysSrv'];

    function statisticsCtrl($scope, $translate, helper, personsSrv, tourneysSrv) {
        var teamId, 
            persons = [], 
            tourneys = [], 
            tourneysIds = [];

        $scope.selectedData = {};
        $scope.loadingImage = helper.getLoadingImg();
        $scope.loadingTourneys = true;

        $scope.teamOptions = [
            { index: 0, team: "MAIN_TEAM", teamId: 3, tourneysIds: [13, 14, 129] },
            { index: 1, team: "RESERVE_TEAM", teamId: 2072, tourneysIds: [118] }
        ];

        $scope.$watch(function (scope) {
            return $scope.teamOpt;
        },
        function (newValue, oldValue) {
            if (angular.isObject(newValue)) {
                teamId = newValue.teamId;
                tourneysIds = newValue.tourneysIds;

                loadData();
            }
        });

        function loadData() {
            $scope.loadingTourneys = true;
            personsSrv.loadTeamMainPlayers(teamId, mainTeamLoaded);
        }

        function mainTeamLoaded(response) {
            persons = response.data;

            // tournaments needed to display names in the dropdown
            tourneysSrv.loadTourneys(tourneysIds, tourneysLoaded);
        }

        function tourneysLoaded(response) {
            tourneys = response.data;

            $scope.selectedData = {
                teamId: teamId,
                persons: persons,
                tourneys: tourneys
            }

            $scope.loadingTourneys = false;
        }
    }
})();
