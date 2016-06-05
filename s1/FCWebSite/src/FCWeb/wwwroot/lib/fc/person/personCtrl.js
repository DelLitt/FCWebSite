(function () {
    'use strict';

    angular
        .module('fc')
        .controller('personCtrl', personCtrl);

    personCtrl.$inject = ['$scope', '$routeParams'];

    function personCtrl($scope, $routeParams) {
        $scope.personId = $routeParams.id;
    }
})();
