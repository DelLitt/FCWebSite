(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('personEditCtrl', personEditCtrl);

    personEditCtrl.$inject = ['$scope', '$routeParams', '$uibModal', 'personsSrv', 'notificationManager'];

    function personEditCtrl($scope, $routeParams, $uibModal, personsSrv, notificationManager) {

        $scope.dateOptions = {
            showWeeks: false
        };

        $scope.person = {
            loading: true,
        };

        $scope.contentUpload = {
            onUploadSuccess: function (uploadData) {
                if (angular.isArray(uploadData)) {
                    setPersonImage(uploadData[0].name);
                }
            }
        };

        $scope.openFileBrowser = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'lib/fc/office/utils/filebrowser.html',
                controller: 'fileBrowserCtrl'
            });

            modalInstance.result.then(function (selectedImage) {
                $scope.person.image = selectedImage;
            }, function () {
                // $log.info('Modal dismissed at: ' + new Date());
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

            $scope.contentUpload.path = personsSrv.getImageUploadPath(person.id);
            setPersonImage(person.image);
        }

        function setPersonImage(image) {
            if (angular.isDefined($scope.contentUpload)
                && angular.isString($scope.contentUpload.path)) {

                if (angular.isObject($scope.person) 
                    && $scope.person.image != image) {
                    $scope.person.image = image;
                }

                $scope.image = $scope.contentUpload.path + '/' + image;
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
