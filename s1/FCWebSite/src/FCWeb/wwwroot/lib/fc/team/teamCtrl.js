(function () {
    'use strict';

    angular
        .module('fc')
        .controller('teamCtrl', teamCtrl);

    teamCtrl.$inject = ['$scope', '$routeParams', '$sce', 'teamsSrv', 'tourneysSrv', 'configSrv', 'helper'];

    function teamCtrl($scope, $routeParams, $sce, teamsSrv, tourneysSrv, configSrv, helper) {

        $scope.teamId = $routeParams.id;
        $scope.tourneyIds = [];
        $scope.teamIds = [];
        $scope.loadingTeam = true;
        $scope.loadingImage = helper.getLoadingImg();
        $scope.showExtended = false;
        $scope.hasGameSchedule = true;
        $scope.hasRankingTable = true;
        $scope.hasTourneysData = true;        
        $scope.imgVariantLogo = "400x400";
        $scope.imgVariantTeam = "1280x720";
        $scope.tourney = null;

        $scope.$watch(function ($scope) {
            return $scope.hasGameSchedule;
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
            $scope.hasTourneysData = $scope.hasRankingTable && $scope.hasGameSchedule;
        }

        loadData();

        function loadData() {
            teamsSrv.loadTeam($scope.teamId, teamLoaded);
        };

        function teamLoaded(response) {
            $scope.team = response.data;
            $scope.loadingTeam = false;

            var imageLogo = helper.getTeamImage($scope.team);
            $scope.imageLogo = helper.addFileVariant(imageLogo, $scope.imgVariantLogo);
            $scope.city = angular.isObject($scope.team.city) ? $scope.team.city.name : "-";
            $scope.mainTourney = angular.isObject($scope.team.mainTourney) ? $scope.team.mainTourney.nameFull : "-";
            $scope.stadium = angular.isObject($scope.team.stadium) ? $scope.team.stadium.name : "-";
            $scope.teamType = angular.isObject($scope.team.teamType) ? $scope.team.teamType.name : "-";

            $scope.showWebsite = !(parseInt($scope.team.webSite) > 0) && angular.isString($scope.team.webSite) && $scope.team.webSite.length > 0;
            $scope.showTeamType = angular.isObject($scope.team.teamType);
            $scope.showMainTourney = angular.isObject($scope.team.mainTourney);

            var descriptionHtml = helper.getTeamDescription($scope.team);
            $scope.description = $sce.trustAsHtml(descriptionHtml);
            $scope.showDescription = angular.isString(descriptionHtml) && descriptionHtml.length > 0;

            $scope.showExtended = helper.hasTeamExtendedInfo($scope.team);
            if ($scope.showExtended) {
                var imageTeam = helper.getTeamFakeInfoImage($scope.team);
                $scope.imageTeam = helper.addFileVariant(imageTeam, $scope.imgVariantTeam);
                $scope.fakePalyesText = helper.getFakePlayersText($scope.team);
                $scope.showFakePalyes = $scope.fakePalyesText.length > 0;
                $scope.customHeadCoach = helper.getCustomHeadCoach($scope.team);
                $scope.showCoaches = angular.isObject($scope.customHeadCoach);
                $scope.coachViewLink = helper.getPersonViewLink($scope.customHeadCoach);
            }

            $scope.mainTourneyId = angular.isNumber($scope.team.mainTourneyId) ? $scope.team.mainTourneyId : 0;
            $scope.tourneyId = $scope.mainTourneyId;
            $scope.teamIds = [$scope.teamId];

            tourneysSrv.loadTourneys([$scope.tourneyId], tourneysLoaded);
        }

        function tourneysLoaded(response) {
            $scope.tourney = angular.isArray(response.data) ? response.data[0] : null;
        }
    }
})();
