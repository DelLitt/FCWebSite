(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['$scope', '$rootScope', 'apiSrv', 'notificationManager'];

    function rootCtrl($scope, $rootScope, apiSrv, notificationManager) {

        apiSrv.get('/api/configuration/office', null, loadConfigurationSuccess, loadConfigurationFail);

        function loadConfigurationSuccess(response) {
            $rootScope.appConfig = response.data;
        }

        function loadConfigurationFail(response) {
            notificationManager.displayError(response);
        }
    }
})();
