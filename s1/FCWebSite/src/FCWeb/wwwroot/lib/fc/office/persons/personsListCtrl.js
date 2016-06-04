(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('personsListCtrl', personsListCtrl);

    personsListCtrl.$inject = ['$scope', 'personsSrv', 'helper'];

    function personsListCtrl($scope, personsSrv, helper) {

        $scope.loading = true;
        $scope.loadingImage = helper.getLoadingImg();
        $scope.persons = [];

        personsSrv.loadAllPersons(personsLoaded);

        function personsLoaded(response) {
            var persons = response.data;

            $scope.persons = persons;
            $scope.loading = false;
        }
    }
})();
