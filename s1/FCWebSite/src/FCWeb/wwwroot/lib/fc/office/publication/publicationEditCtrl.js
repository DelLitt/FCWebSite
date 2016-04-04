(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('publicationEditCtrl', publicationEditCtrl);

    publicationEditCtrl.$inject = ['$scope', '$routeParams', 'publicationsSrv', 'fileBrowserSrv'];

    function publicationEditCtrl($scope, $routeParams, publicationsSrv, fileBrowserSrv) {

        $scope.dateOptions = {
            showWeeks: false
        };

        $scope.publication = {
            loading : true
        };

        $scope.openFileBrowser = function () {
            fileBrowserSrv.open('images/store', function (selectedFile) {
                $scope.publication.image = selectedFile.path;
            });
        }

        loadData();

        function loadData() {
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
