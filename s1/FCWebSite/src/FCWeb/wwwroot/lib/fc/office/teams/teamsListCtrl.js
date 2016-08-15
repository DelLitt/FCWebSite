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
        $scope.search = search;

        init();

        function init() {
            teamsSrv.loadAllTeams(teamsLoaded);
        }

        function teamsLoaded(response) {
            $scope.teams = response.data;
            $scope.loading = false;
        }

        var lastLength = 0;
        var length = 0;

        function search(event) {

            if (event.keyCode == 27) {
                $scope.text = '';
                init();
                lastLength = length = 0;
                return;
            }

            length = angular.isNumber($scope.text.length) ? $scope.text.length : 0;

            if (length >= 3) {
                teamsSrv.search($scope.text, teamsLoaded);
            }
            else if (lastLength > $scope.text.length) {
                init();
            }

            lastLength = length;
        }
    }
})();
