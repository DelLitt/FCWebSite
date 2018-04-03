(function () {
    'use strict';

    angular
        .module('fc.ui')
        .controller('ticketsCtrl', ticketsCtrl);

    ticketsCtrl.$inject = ['$scope', 'apiSrv'];

    function ticketsCtrl($scope, apiSrv) {
        $scope.data1 = "xxx";

        apiSrv.get('/api/games/schedule/test/' + 5,
            null,
            success,
            function (response) {
            });

        function success(response) {
            $scope.data1 = response.data;
        }
    }
})();
