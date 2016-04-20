(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('personEditCtrl', personEditCtrl);

    personEditCtrl.$inject = ['$scope', '$routeParams', 'personsSrv', 'notificationManager'];

    function personEditCtrl($scope, $routeParams, personsSrv, notificationManager) {

        $scope.dateOptions = {
            showWeeks: false
        };

        $scope.person = {
            loading: true,
        };

        $scope.fileBrowser = {
            path: '',
            root: ''
        }

        $scope.openFileBrowser = function () {
            fileBrowserSrv.open(
                $scope.fileBrowser.path,
                $scope.fileBrowser.root,
                function (selectedFile) {
                    setPersonImage(selectedFile.name);
            });
        }

        loadData();

        function loadData() {
            personsSrv.loadPerson($routeParams.id, personLoaded);
        }

        function personLoaded(response) {
            var person = response.data;

            $scope.person = person;
            $scope.person.birthDate = new Date(person.birthDate);

            $scope.fileBrowser.path = personsSrv.getImageUploadPath(person);
            $scope.fileBrowser.root = personsSrv.getImageUploadPath(person);

            setPersonImage(person.image);
        }

        function setPersonImage(image) {
            if (angular.isDefined($scope.fileBrowser)
                && angular.isString($scope.fileBrowser.path)) {

                if (angular.isObject($scope.person) 
                    && $scope.person.image != image) {
                    $scope.person.image = image;
                }

                $scope.image = $scope.fileBrowser.path + '/' + image;
            }
        }

        //function loadImage(path) {
        //    apiSrv.get('/api/filebrowser?path=' + path, null, success, failure);
        //}

        //function success(response) {
        //    $scope.directoryView = response.data;
        //}

        //function failure(response) {
        //    notificationManager.displayError(response.data);
        //}
    }
})();
