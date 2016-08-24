(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('teamEditCtrl', teamEditCtrl);

    teamEditCtrl.$inject = ['$scope', '$routeParams', '$location', 'teamsSrv', 'teamTypesSrv', 'fileBrowserSrv', 'notificationManager', 'configSrv'];

    function teamEditCtrl($scope, $routeParams, $location, teamsSrv, teamTypesSrv, fileBrowserSrv, notificationManager, configSrv) {

        if (!angular.isDefined($scope.forms)) {
            $scope.forms = {};
        }

        $scope.loading = true;
        $scope.openFileBrowser = openFileBrowser;
        $scope.openFileBrowserDescr = openFileBrowserDescr;
        $scope.saveEdit = saveEdit;
        $scope.team = {};
        $scope.description = '';
        $scope.teamTypes = [];

        $scope.fake = {
            image: null,
            persons: [],
            coaches: [],
            addPerson: addFakePerson,
            removePerson: removeFakePerson,
            addCoach: addCoach,
            removeCoach: removeCoach
        };

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
                true,
                false,
                false,
                function (selectedFile) {
                    setTeamImage(selectedFile.name);
                },
                null,
                {
                    createNew: $scope.fileBrowser.createNew
                }
            );
        }

        function openFileBrowserDescr() {
            fileBrowserSrv.open(
                $scope.fileBrowser.path,
                $scope.fileBrowser.root,
                false,
                false,
                false,
                function (selectedFile) {
                    setTeamPhoto(selectedFile.name);
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

            var photo = null;
            
            if (angular.isObject($scope.team.descriptionData)) {
                $scope.description = $scope.team.descriptionData.description;

                if (angular.isObject($scope.team.descriptionData.fakeInfo)) {
                    $scope.fake.persons = $scope.team.descriptionData.fakeInfo.persons || [];

                    for (var i = 0; i < $scope.fake.persons.length; i++) {
                        $scope.fake.persons[i].dateOfBirth = new Date($scope.fake.persons[i].dateOfBirth);
                    }

                    $scope.fake.coaches = $scope.team.descriptionData.fakeInfo.coaches || [];
                    photo = $scope.team.descriptionData.fakeInfo.image;
                }
            }

            setTeamImage(team.image);
            setTeamPhoto(photo);

            teamTypesSrv.loadAllTeamTypes(teamTypesLoaded);
        }

        function teamTypesLoaded(response) {
             $scope.teamTypes = response.data;
        }

        function setTeamImage(image) {
            if (angular.isObject($scope.fileBrowser)
                && angular.isString($scope.fileBrowser.path)
                && angular.isString(image)) {

                if (angular.isObject($scope.team)
                    && $scope.team.image != image) {
                    $scope.team.image = image;
                }

                $scope.image = $scope.fileBrowser.path + '/' + image;
            }
        }

        function setTeamPhoto(image) {
            if (angular.isObject($scope.fileBrowser)
                && angular.isString($scope.fileBrowser.path)
                && angular.isString(image)) {

                if (angular.isObject($scope.team)
                    && angular.isObject($scope.team.descriptionData)
                    && angular.isObject($scope.team.descriptionData.fakeInfo)
                    && $scope.team.descriptionData.fakeInfo.image != image) {
                    $scope.team.descriptionData.fakeInfo.image = image;
                }

                $scope.fake.image = $scope.fileBrowser.path + '/' + image;
            }
        }

        function addFakePerson() {
            $scope.fake.persons.push({
                number: null,
                name: '',
                dateOfBirth: new Date()
            });
        }

        function removeFakePerson(index) {
            $scope.fake.persons.splice(index, 1);
        }

        function addCoach() {
            var emptyCoach = {};
            angular.copy(configSrv.Current.EmptyEntityLink, emptyCoach);
            $scope.fake.coaches.push(emptyCoach);
        }

        function removeCoach(index) {
            $scope.fake.coaches.splice(index, 1);
        }

        function saveEdit(form) {

            $scope.submitted = true;

            if (!form.$valid || !validateCustomForms()) {
                    return;
            }

            if (!angular.isObject($scope.team.descriptionData)) {
                $scope.team.descriptionData = {
                    description: '',
                    fakeInfo: {},
                    data: null
                };
            }

            $scope.team.descriptionData.description = $scope.description;

            if (!angular.isObject($scope.team.descriptionData.fakeInfo)) {
                $scope.team.descriptionData.fakeInfo = {};
            }

            $scope.team.descriptionData.fakeInfo.persons = $scope.fake.persons;
            $scope.team.descriptionData.fakeInfo.coaches = $scope.fake.coaches;

            teamsSrv.saveTeam($scope.team.id || 0, $scope.team, teamSaved);
        }

        function validateCustomForms() {

            if (angular.isNumber($scope.team.cityId) && $scope.team.cityId <= 0) {
                alert("Укажите город!");
                return false;
            }

            if (angular.isNumber($scope.team.stadiumId) && $scope.team.stadiumId <= 0) {
                alert("Укажите стадион!");
                return false;
            }

            if (angular.isNumber($scope.team.mainTourneyId) && $scope.team.mainTourneyId <= 0) {
                alert("Укажите основной турнир!");
                return false;
            }

            return true;
        }

        function teamSaved(response) {
            $location.path('/office/teams');
        }
    }
})();
