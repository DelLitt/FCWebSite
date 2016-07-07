(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('protocolSrv', protocolSrv);

    protocolSrv.$inject = ['helper', 'apiSrv', 'notificationManager'];

    function protocolSrv(helper, apiSrv, notificationManager) {

        this.loadProtocol = function (id, success, failure) {
            apiSrv.get('/api/games/' + id + '/protocol', null,
                success,
                function (response) {
                    if (angular.isFunction(failure)) {
                        failure(response);
                    }

                    protocolsLoadFail(response);
                });
        }

        this.loadProtocolText = function (id, success, failure) {
            apiSrv.get('/api/games/' + id + '/protocol/text', null,
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
            apiSrv.post('/api/games/' + gameId + '/protocol/', protocol, null,
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