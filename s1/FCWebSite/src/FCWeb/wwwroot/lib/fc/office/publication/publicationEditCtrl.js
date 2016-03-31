(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('publicationEditCtrl', publicationEditCtrl);

    publicationEditCtrl.$inject = ['$scope', '$routeParams', '$uibModal', '$log', 'publicationsSrv', 'helper'];

    function publicationEditCtrl($scope, $routeParams, $uibModal, $log, publicationsSrv, helper) {

        $scope.dateOptions = {
            showWeeks: false
        };

        $scope.publication = {
            loading : true
        };

        $scope.openFileBrowser = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'lib/fc/office/utils/filebrowser.html',
                controller: 'fileBrowserCtrl'
            });

            modalInstance.result.then(function (selectedImage) {
                $scope.publication.image = selectedImage;
            }, function () {
                // $log.info('Modal dismissed at: ' + new Date());
            });
        }

        loadData();

        function loadData() {
            //publicationsSrv.testAuth();
            publicationsSrv.loadPublication($routeParams.id, publicationLoaded);
        }

        function publicationLoaded(response) {
            var publication = response.data;

            $scope.publication = publication;
            $scope.publication.dateDisplayed = new Date(publication.dateDisplayed);
            $scope.publication.dateChanged = new Date(publication.dateChanged);
            $scope.publication.dateCreated = new Date(publication.dateCreated);

        }
    }
})();
