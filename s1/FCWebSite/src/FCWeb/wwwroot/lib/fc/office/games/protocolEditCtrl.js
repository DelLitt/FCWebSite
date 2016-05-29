(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('protocolEditCtrl', protocolEditCtrl);

    protocolEditCtrl.$inject = ['$scope', '$routeParams', '$location', 'protocolSrv', 'notificationManager'];

    function protocolEditCtrl($scope, $routeParams, $location, protocolSrv, notificationManager) {

        if (!angular.isDefined($scope.forms)) {
            $scope.forms = {};
        }

        $scope.gameId = $routeParams.id;
        $scope.protocol = {};
        $scope.saveEdit = saveEdit;

        function saveEdit(form) {
            $scope.submitted = true;

            if (!form.$valid || !validate($scope.protocol)) {
                return;
            }

            protocolSrv.saveProtocol($scope.gameId, $scope.protocol, protocolSaved);
        }

        function validate(protocol) {
            // TODO: Add validation
            return true;
        }

        function protocolSaved(response) {
            notificationManager.displayInfo("Protocol saved: " + response);
            $location.path('/office')
        }
    }
})();
