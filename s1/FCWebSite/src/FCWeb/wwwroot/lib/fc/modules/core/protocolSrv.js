(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('protocolSrv', protocolSrv);

    protocolSrv.$inject = ['helper', 'apiSrv', 'notificationManager'];

    function protocolSrv(helper, apiSrv, notificationManager) {

        this.loadProtocol = function (id, success, failure) {
            apiSrv.get('/api/game/' + id + '/protocol', null,
                success,
                function (response) {
                    if (angular.isFunction(failure)) {
                        failure(response);
                    }

                    protocolsLoadFail(response);
                });
        }

        function protocolsLoadFail(response) {
            notificationManager.displayError(response.data);
        }

        this.saveProtocol = function (gameId, protocol, success, failure) {
            apiSrv.post('/api/game/' + gameId + '/protocol/', protocol,
                            success,
                            function (response) {
                                if (angular.isFunction(failure)) {
                                    failure(response);
                                }

                                protocolSaveFailed(response);
                            });

        }

        function protocolSaveFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();