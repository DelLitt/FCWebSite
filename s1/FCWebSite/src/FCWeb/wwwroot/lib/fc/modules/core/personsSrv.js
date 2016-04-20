(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('personsSrv', personsSrv);

    personsSrv.$inject = ['$rootScope', 'helper', 'apiSrv', 'notificationManager', 'configSrv'];

    function personsSrv($rootScope, helper, apiSrv, notificationManager, configSrv) {

        this.loadPerson = function (id, success, failure) {
            apiSrv.get('/api/persons/' + id, null, success, personsLoadFail);
        }

        function personsLoadFail(response, customLoadFail) {
            if (angular.isFunction(customLoadFail)) {
                customLoadFail(response);
            }

            notificationManager.displayError(response.data);
        }

        this.getImageUploadPath = function (person) {
            return configSrv.getImageStorePath();
        }
    }
})();