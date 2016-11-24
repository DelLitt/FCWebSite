(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('person', person);

    person.$inject = ['personsSrv', '$sce', 'helper', 'filterFilter', 'statsSrv'];

    function person(personsSrv, $sce, helper, filterFilter, statsSrv) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                id: '='
            },
            link: function link(scope, element, attrs) {
                scope.loadingPerson = true;
                scope.loadingStats = true;
                scope.loadingImage = helper.getLoadingImg();
                scope.getTeamViewLink = helper.getTeamViewLinkById;
                scope.persons = {};
                scope.flagSrc = "";
                scope.tab = 0;
                scope.labels = {
                    position: ""
                };
                scope.setTab = function (val) {
                    scope.tab = val;
                }

                scope.isValidYear = function (year) {
                    return angular.isNumber(year);
                }

                scope.hasPositiveCustomIntValue = function (value) {
                    return angular.isNumber(value) && value > 0;
                }

                scope.isGK = helper.isGK;

                loadData(scope.id);

                function loadData(personId) {
                    if (personId < 0) {
                        return;
                    }

                    personsSrv.loadPerson(personId, personLoaded);                    
                }

                function personLoaded(response) {
                    var person = response.data;

                    scope.person = person;

                    scope.showBirthDate = angular.isString(scope.person.birthDate) && scope.person.birthDate.length > 0;
                    scope.showBirthPlace = angular.isObject(scope.person.city);
                    scope.showTeam = angular.isObject(scope.person.team);
                    scope.showRole = angular.isObject(scope.person.role);
                    scope.showNumber = angular.isNumber(scope.person.number);
                    scope.showHeight = angular.isNumber(scope.person.height);
                    scope.showWeight = angular.isNumber(scope.person.weight);
                    scope.showInfo = angular.isObject(scope.person.info) && angular.isString(scope.person.info.description) && scope.person.info.description.length > 0;
                    scope.showCareer = angular.isObject(scope.person.info) && angular.isArray(scope.person.info.career) && scope.person.info.career.length > 0;
                    scope.showAchievements = angular.isObject(scope.person.info) && angular.isArray(scope.person.info.achievements) && scope.person.info.achievements.length > 0;

                    scope.info = scope.showInfo ? $sce.trustAsHtml(scope.person.info.description) : "";
                    scope.career = scope.showCareer ? scope.person.info.career : [];
                    scope.achievements = scope.showAchievements ? scope.person.info.achievements : [];

                    var imageUploadData = personsSrv.getImageUploadData(scope.person);
                    scope.image = helper.getPersonImage(scope.person.image, imageUploadData);

                    scope.flagSrc = helper.getFlagSrc(scope.person.city.countryId);

                    scope.labels.position = helper.locLabels.position(scope.person.role.personRoleGroupId);

                    scope.loadingPerson = false;

                    statsSrv.loadPersonStats(person.id, statsLoaded)
                }                

                function statsLoaded(response) {
                    var stats = response.data;

                    scope.stats = stats || [];

                    scope.hideStats = scope.stats.length == 0;
                    scope.loadingStats = false;
                }
            },
            templateUrl: '/lib/fc/layout/persons/person.html'
        }
    }
})();