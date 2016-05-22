(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('protocolEditCtrl', protocolEditCtrl);

    protocolEditCtrl.$inject = ['$scope', '$routeParams', '$location', 'notificationManager'];

    function protocolEditCtrl($scope, $routeParams, $location, notificationManager) {

        if (!angular.isDefined($scope.forms)) {
            $scope.forms = {};
        }

        $scope.gameId = $routeParams.id;
    }
})();
