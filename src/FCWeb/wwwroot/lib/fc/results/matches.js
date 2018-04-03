(function () {
    'use strict';

    angular
        .module('fc')
        .controller('matchesCtrl', matchesCtrl);

    matchesCtrl.$inject = ['$scope', '$translate', 'configSrv'];

    function matchesCtrl($scope, $translate, configSrv) {

        $scope.seasonOptions = [
            {
                index: 0,
                season: "2017",
                dateStart: new Date(2017, 1, 1),
                dateEnd: new Date(2017, 11, 30),
                teamOptions: [
                    { index: 0, team: "MAIN_TEAM", tourneysIds: [4, 13, 14, 129] },
                    { index: 1, team: "RESERVE_TEAM", tourneysIds: [118] }
                ]
            },
            {
                index: 1,
                season: "2016",
                dateStart: new Date(2016, 1, 1),
                dateEnd: new Date(2016, 11, 30),
                teamOptions: [
                    { index: 0, team: "MAIN_TEAM", tourneysIds: [4, 11, 12, 13] }
                ]
            },
            {
                index: 1,
                season: "2015",
                dateStart: new Date(2015, 1, 1),
                dateEnd: new Date(2015, 11, 30),
                teamOptions: [
                    { index: 0, team: "MAIN_TEAM", tourneysIds: [4, 9, 10, 11] }
                ]
            },
            {
                index: 1,
                season: "2014",
                dateStart: new Date(2014, 1, 1),
                dateEnd: new Date(2014, 11, 30),
                teamOptions: [
                    { index: 0, team: "MAIN_TEAM", tourneysIds: [4, 7, 8, 9] }
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
    }
})();
