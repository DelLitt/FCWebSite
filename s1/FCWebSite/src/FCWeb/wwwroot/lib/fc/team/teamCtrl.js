(function () {
    'use strict';

    angular
        .module('fc')
        .controller('teamCtrl', teamCtrl);

    teamCtrl.$inject = ['$scope', '$routeParams', '$sce', 'teamsSrv', 'configSrv', 'helper'];

    function teamCtrl($scope, $routeParams, $sce, teamsSrv, configSrv, helper) {

        $scope.teamId = $routeParams.id;
        $scope.tourneyIds = [];
        $scope.loadingTeam = true;
        $scope.loadingImage = helper.getLoadingImg();
        $scope.showExtended = false;
        $scope.hasLatestResults = true;
        $scope.hasRankingTable = true;
        $scope.hasTourneysData = true;

        $scope.$watch(function ($scope) {
            return $scope.hasLatestResults;
        },
        function (newValue, oldValue) {
            setTourneyDataVisibility();
        });

        $scope.$watch(function ($scope) {
            return $scope.hasRankingTable;
        },
        function (newValue, oldValue) {
            setTourneyDataVisibility();
        });

        function setTourneyDataVisibility() {
            $scope.hasTourneysData = $scope.hasRankingTable && $scope.hasLatestResults;
        }

        loadData();

        function loadData() {
            teamsSrv.loadTeam($scope.teamId, teamLoaded);
        };

        function teamLoaded(response) {
            $scope.team = response.data;
            $scope.loadingTeam = false;

            $scope.imageLogo = helper.getTeamImage($scope.team);
            $scope.city = angular.isObject($scope.team.city) ? $scope.team.city.name : "-";
            $scope.mainTourney = angular.isObject($scope.team.mainTourney) ? $scope.team.mainTourney.nameFull : "-";
            $scope.stadium = angular.isObject($scope.team.stadium) ? $scope.team.stadium.name : "-";
            $scope.teamType = angular.isObject($scope.team.teamType) ? $scope.team.teamType.name : "-";

            $scope.showWebsite = angular.isString($scope.team.webSite) && $scope.team.webSite.length > 0;
            $scope.showTeamType = angular.isObject($scope.team.teamType);
            $scope.showMainTourney = angular.isObject($scope.team.mainTourney);

            var descriptionHtml = helper.getTeamDescription($scope.team);
            $scope.description = $sce.trustAsHtml(descriptionHtml);
            $scope.showDescription = angular.isString(descriptionHtml) && descriptionHtml.length > 0;

            $scope.showExtended = helper.hasTeamExtendedInfo($scope.team);
            if ($scope.showExtended) {
                $scope.imageTeam = helper.getTeamFakeInfoImage($scope.team);
                $scope.fakePalyesText = helper.getFakePlayersText($scope.team);
                $scope.showFakePalyes = $scope.fakePalyesText.length > 0;
                $scope.customHeadCoach = helper.getCustomHeadCoach($scope.team);
                $scope.showCoaches = angular.isObject($scope.customHeadCoach);
                $scope.coachViewLink = helper.getPersonViewLink($scope.customHeadCoach);
            }

            $scope.mainTourneyId = angular.isNumber($scope.team.mainTourneyId) ? $scope.team.mainTourneyId : configSrv.Current.MainTableTourneyId;
            $scope.tourneyIds = [$scope.mainTourneyId];
        }
    }
})();
