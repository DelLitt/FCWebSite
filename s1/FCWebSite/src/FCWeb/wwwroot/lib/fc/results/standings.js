(function () {
    'use strict';

    angular
        .module('fc')
        .controller('standingsCtrl', standingsCtrl);

    standingsCtrl.$inject = ['$scope', '$translate', 'configSrv'];

    function standingsCtrl($scope, $translate, configSrv) {

        $translate('MAIN_TEAM_RESULTS').then(function (translation) {
            $scope.title = translation;
        });

        $scope.seasonOptions = [
            {
                index: 0,
                season: "2017",
                teamOptions:
                  [
                      { index: 0, team: "MAIN_TEAM", tourneyId: 14 },
                      { index: 1, team: "RESERVE_TEAM", tourneyId: 118 }
                  ]
            },
            {
                index: 1,
                season: "2016",
                teamOptions:
                  [
                      { index: 0, team: "MAIN_TEAM", tourneyId: 12 }
                  ]
            },
            {
                index: 1,
                season: "2015",
                teamOptions:
                  [
                      { index: 0, team: "MAIN_TEAM", tourneyId: 10 }
                  ]
            }
,
            {
                index: 1,
                season: "2014",
                teamOptions:
                  [
                      { index: 0, team: "MAIN_TEAM", tourneyId: 8 }
                  ]
            }
        ];

        $scope.$watch(function (scope) {
            return $scope.seasonOpt;
        },
        function (newValue, oldValue) {
            if (angular.isObject(newValue)) {
                $scope.teamOptions = newValue.teamOptions;
                $scope.teamOpt = $scope.teamOptions[0];
            }
        });

        $scope.teamId = configSrv.Current.MainTeamId;
        $scope.hasData = false;


        // TODO: Change config to promise
        //scope.mainTeamId = 
        //configSrv.mainTeamTourneyIds
    }
})();
