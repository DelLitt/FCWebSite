(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('unionSrv', unionSrv);

    unionSrv.$inject = ['$rootScope', 'apiSrv', 'helper', 'configSrv', 'notificationManager'];

    function unionSrv($rootScope, apiSrv, helper, configSrv, notificationManager) {

        this.loadMainPagePublications = function (success, failure) {
            apiSrv.get('/api/union/mainpage/base',
                       null,
                       success,
                       function (response) {
                            if (failure != null) {
                                failure(response);
                            }

                mainPagePublicationsLoadFailed(response);
            });
        }

        function mainPagePublicationsLoadFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();