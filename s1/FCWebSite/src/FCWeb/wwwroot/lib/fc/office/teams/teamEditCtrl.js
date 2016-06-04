(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('teamEditCtrl', teamEditCtrl);

    teamEditCtrl.$inject = ['$scope', '$routeParams', '$location', 'teamsSrv', 'fileBrowserSrv', 'notificationManager'];

    function teamEditCtrl($scope, $routeParams, $location, teamsSrv, fileBrowserSrv, notificationManager) {

        if (!angular.isDefined($scope.forms)) {
            $scope.forms = {};
        }

        $scope.loading = true;
        $scope.openFileBrowser = openFileBrowser;
        $scope.saveEdit = saveEdit;
        $scope.team = {};
        $scope.dateOptions = {
            showWeeks: false
        };

        $scope.fileBrowser = {
            path: '',
            root: ''
        };

        var curDate = new Date();
        var currentYear = curDate.getFullYear();

        function openFileBrowser() {
            fileBrowserSrv.open(
                $scope.fileBrowser.path,
                $scope.fileBrowser.root,
                function (selectedFile) {
                    setTeamImage(selectedFile.name);
                },
                null,
                {
                    createNew: $scope.fileBrowser.createNew
                }
            );
        }

        loadData($routeParams.id);

        function loadData(teamId) {
            if (teamId < 0) {
                return;
            }

            teamsSrv.loadTeam(teamId, teamLoaded);
        }

        function teamLoaded(response) {
            var team = response.data;

            $scope.team = team;

            $scope.cityInitUrl = angular.isNumber($scope.team.cityId)
                ? "/api/cities/" + $scope.team.cityId
                : null;
            $scope.stadiumInitUrl = angular.isNumber($scope.team.stadiumId)
                ? "/api/stadiums/" + $scope.team.stadiumId
                : null;
            $scope.teamTypeInitUrl = angular.isNumber($scope.team.teamTypeId)
                ? "/api/teamTypes/" + $scope.team.teamTypeId
                : null;
            $scope.mainTourneyInitUrl = angular.isNumber($scope.team.mainTourneyId)
                ? "/api/tourneys/" + $scope.team.mainTourneyId
                : null;

            var imageUploadData = teamsSrv.getImageUploadData(team);
            $scope.fileBrowser.path = imageUploadData.path;
            $scope.fileBrowser.createNew = imageUploadData.createNew;
            $scope.fileBrowser.root = imageUploadData.path;

            setTeamImage(team.image);
        }

        function setTeamImage(image) {
            if (angular.isObject($scope.fileBrowser)
                && angular.isString($scope.fileBrowser.path)
                && angular.isString(image)) {

                if (angular.isObject($scope.person)
                    && $scope.team.image != image) {
                    $scope.team.image = image;
                }

                $scope.image = $scope.fileBrowser.path + '/' + image;
            }
        }

        function saveEdit(form) {

            $scope.submitted = true;

            if (!form.$valid) {
                    return;
            }

            teamsSrv.saveTeam($scope.team.id || 0, $scope.team, teamSaved);
        }

        function teamSaved(response) {
            $location.path('/office/teams');
        }
    }
})();
