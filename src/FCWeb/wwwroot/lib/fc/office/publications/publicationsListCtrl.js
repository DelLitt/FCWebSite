(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('publicationsListCtrl', publicationsListCtrl);

    publicationsListCtrl.$inject = ['$scope', 'publicationsSrv', 'helper'];

    function publicationsListCtrl($scope, publicationsSrv, helper) {

        $scope.loading = true;
        $scope.loadingImage = helper.getLoadingImg();
        $scope.publications = [];
        $scope.text = '';
        $scope.search = search;

        init();

        function init() {
            publicationsSrv.loadNotFilteredPublications(50, 0, publicationsLoaded);
        }        

        function publicationsLoaded(response) {
            var publications = response.data;

            $scope.publications = publications;
            $scope.loading = false;
        }

        function search(event) {

            if (event.keyCode == 27) {
                $scope.text = '';
                init();
                return;
            }

            if ($scope.text.length && $scope.text.length >= 3) {
                publicationsSrv.search($scope.text, publicationsLoaded);
                return;
            }
            else if ($scope.text.length && $scope.text.length < 3) {
                init();
            }            
        }
    }
})();
