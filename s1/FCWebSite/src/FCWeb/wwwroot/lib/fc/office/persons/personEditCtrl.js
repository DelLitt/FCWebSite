(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('personEditCtrl', personEditCtrl);

    personEditCtrl.$inject = ['$scope', '$routeParams', 'personsSrv', 'fileBrowserSrv', 'notificationManager'];

    function personEditCtrl($scope, $routeParams, personsSrv, fileBrowserSrv, notificationManager) {

        if (!angular.isDefined($scope.forms)) {
            $scope.forms = {};
        }

        $scope.loading = true;
        $scope.openFileBrowser = openFileBrowser;
        $scope.saveEdit = saveEdit;
        $scope.person = {
            info: {
                description: '',
                career: [],
                achievements: []
            }
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

        $scope.info = {
            career: {
                removeItem: function (index) {
                    $scope.person.info.career.splice(index, 1);
                },
                addItem: function () {
                    $scope.person.info.career.push({
                        yearStart: currentYear,
                        yearEnd: currentYear,
                        team: ''
                    });
                }
            },
            achievements: {
                removeItem: function (index) {
                    $scope.person.info.achievements.splice(index, 1);
                },
                addItem: function () {
                    $scope.person.info.achievements.push({
                        season: '',
                        team: '',
                        achievement: ''
                    });
                }
            }
        };

        $scope.carItem = {
                dateStart: curDate,
                dateFinish: curDate,
                personId: angular.isNumber($scope.person.id) && $scope.person.id > 0 ? $scope.person.id : $routeParams.id,
                // TODO: Hardcoded teams slutsk
                urlinit: "/api/teams/3"
        }

        $scope.careerTools = {
            removeItem: function (index) {
                $scope.person.career.splice(index, 1);
            },
            addItem: function (item) {
                $scope.person.career.push(angular.copy(item));
            },
            teamInitUrl: function (id) {
                return angular.isNumber(id) ? "/api/teams/" + id : null;
            }
        }

        function openFileBrowser() {
            fileBrowserSrv.open(
                $scope.fileBrowser.path,
                $scope.fileBrowser.root,
                function (selectedFile) {
                    setPersonImage(selectedFile.name);
                },
                null,
                {
                    createNew: $scope.fileBrowser.createNew
                }
            );
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
            $scope.person.birthDate = new Date(person.birthDate);

            angular.forEach($scope.person.career, function (value) {
                if(angular.isString(value.dateStart)) {
                    value.dateStart = new Date(value.dateStart);
                }
                
                if (angular.isString(value.dateFinish)) {
                    value.dateFinish = new Date(value.dateFinish);
                }

                value.urlinit = angular.isNumber(value.teamId) ? "/api/teams/" + value.teamId : null;
            });

            $scope.cityInitUrl = angular.isNumber($scope.person.cityId)
                ? "/api/cities/" + $scope.person.cityId
                : null;
            $scope.teamInitUrl = angular.isNumber($scope.person.teamId)
                ? "/api/teams/" + $scope.person.teamId
                : null;
            $scope.personRoleInitUrl = angular.isNumber($scope.person.roleId)
                ? "/api/personroles/" + $scope.person.roleId
                : null;
            $scope.personStatusInitUrl = angular.isNumber($scope.person.personStatusId)
                ? "/api/personstatuses/" + $scope.person.personStatusId
                : null;
            $scope.customIntValueInitUrl = angular.isNumber($scope.person.customIntValue)
                ? "/api/teams/" + $scope.person.customIntValue
                : null;            

            var imageUploadData = personsSrv.getImageUploadData(person);
            $scope.fileBrowser.path = imageUploadData.path;
            $scope.fileBrowser.createNew = imageUploadData.createNew;
            $scope.fileBrowser.root = imageUploadData.path;

            setPersonImage(person.image);
        }

        $scope.hadledCareerItems = [];

        function handleCareerTourneys() {
            if (!angular.isArray($scope.person.career)) { return; }

            angular.forEach($scope.person.career, function (value) {
                var index = this.hadledCareerItems.indexOf(value);
                if (index == -1) {
                    hadledCareerItems.push(value);
                }

                angular.forEach(hadledCareerItems, function(val) {
                    var i = this.person.career.indexOf(val);
                    if (i == -1) {
                        var j = this.hadledCareerItems.indexOf(val);
                        this.hadledCareerItems.splice(j, 1);
                    }
                }, this)

            }, $scope);
        }

        function setPersonImage(image) {
            if (angular.isObject($scope.fileBrowser)
                && angular.isString($scope.fileBrowser.path)
                && angular.isString(image)) {

                if (angular.isObject($scope.person)
                    && $scope.person.image != image) {
                    $scope.person.image = image;
                }

                $scope.image = $scope.fileBrowser.path + '/' + image;
            }
        }

        function saveEdit(form) {

            $scope.submitted = true;

            if (!form.$valid) {
                    return;
            }

            personsSrv.savePerson($scope.person.id || 0, $scope.person, personSaved);
        }

        function personSaved(response) {
            alert(response.data);
        }
    }
})();
