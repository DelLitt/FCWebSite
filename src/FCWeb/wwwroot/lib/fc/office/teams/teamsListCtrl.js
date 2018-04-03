(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('teamsListCtrl', teamsListCtrl);

    teamsListCtrl.$inject = ['$scope', 'teamsSrv', 'helper'];

    function teamsListCtrl($scope, teamsSrv, helper) {

        $scope.loading = true;
        $scope.loadingImage = helper.getLoadingImg();
        $scope.teams = [];
        $scope.text = '';
        $scope.teamType = '';
        $scope.search = search;
        $scope.load = load;

        var lastLength = 0;
        var length = 0;

        //init();

        $scope.$watch(function (scope) {
            return $scope.teamType;
        },
        function (newValue, oldValue) {
            if (newValue !== oldValue) {
                load();
            }

            if ($scope.teamType.length == 0) {
                $scope.teamType = 'main';
            }
        });      

        function load() {
            length = angular.isNumber($scope.text.length) ? $scope.text.length : 0;

            if (length >= 3) {
                teamsSrv.searchByType($scope.teamType, $scope.text, teamsLoaded);
            }
            else if ($scope.text.length == 0 || lastLength > $scope.text.length) {
                init();
            }
        }

        function init() {
            teamsSrv.loadTeamsList($scope.teamType, teamsLoaded);
        }

        function teamsLoaded(response) {
            $scope.teams = response.data;
            $scope.loading = false;
        }

        function search(event) {

            if (event.keyCode == 27) {
                $scope.text = '';
                init();
                lastLength = length = 0;
                return;
            }

            $scope.load();
            lastLength = length;
        }
    }
})();
