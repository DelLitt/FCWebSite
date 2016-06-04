(function () {
    'use strict';

    angular
        .module('fc')
        .controller('personCtrl', personCtrl);

    personCtrl.$inject = ['$scope', '$routeParams', '$sce', 'personsSrv', 'helper', 'filterFilter', 'tourneysSrv'];

    function personCtrl($scope, $routeParams, $sce, personsSrv, helper, filterFilter, tourneysSrv) {


        $scope.loading = true; 
        $scope.persons = {};
        $scope.flagSrc = "";
        $scope.tab = 0;
        $scope.labels = {
            position: ""
        };
        $scope.setTab = function (val) {
            $scope.tab = val;
        }

        loadData($routeParams.id);

        function loadData(personId) {
            if (personId < 0) {
                return;
            }

            personsSrv.loadPerson(personId, personLoaded);
        }

        function personLoaded(response) {
            var person = response.data;

            $scope.person = person;

            $scope.showBirthDate = angular.isDate($scope.person.birthDate);
            $scope.showBirthPlace = angular.isObject($scope.person.city);
            $scope.showTeam = angular.isObject($scope.person.team);
            $scope.showRole = angular.isObject($scope.person.role);
            $scope.showNumber = angular.isNumber($scope.person.number);
            $scope.showHeight = angular.isNumber($scope.person.height);
            $scope.showWeight = angular.isNumber($scope.person.weight);
            $scope.showInfo = angular.isObject($scope.person.info) && angular.isString($scope.person.info.description);
            $scope.showCareer = angular.isObject($scope.person.info) && angular.isArray($scope.person.info.career);
            $scope.showAchievements = angular.isObject($scope.person.info) && angular.isArray($scope.person.info.achievements);

            $scope.info = $scope.showInfo ? $sce.trustAsHtml($scope.person.info.description) : "";
            $scope.career = $scope.showCareer ? $scope.person.info.career : [];
            $scope.achievements = $scope.showAchievements ? $scope.person.info.achievements : [];

            var imageUploadData = personsSrv.getImageUploadData($scope.person);            

            $scope.image = angular.isString($scope.person.image) && $scope.person.image.length > 0
                ? imageUploadData.path + '/' + $scope.person.image
                : helper.getPersonEmptyImage();

            $scope.flagSrc = helper.getFlagSrc($scope.person.city.countryId);

            $scope.labels.position = helper.locLabels.position($scope.person.role.personRoleGroupId);

            $scope.loading = false;
        }
    }
})();
