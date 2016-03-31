(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', 'publicationsSrv', 'helper'];

    function indexCtrl($scope, publicationsSrv, helper) {

        $scope.publications = {
            loading : true
        };

        loadData();

        function loadData() {
            //publicationsSrv.testAuth();
            publicationsSrv.loadLatestPublications(helper.getRandom(0, 20), latestPublicationsLoaded);
        }

        function latestPublicationsLoaded(response) {
            var publications = response.data;

            $scope.publications.main = publications.length > 0 ? publications[0] : null;
            $scope.publications.rows = publications.length > 1 ? helper.formRows(publications, 3, 1) : [];
        }
    }
})();
