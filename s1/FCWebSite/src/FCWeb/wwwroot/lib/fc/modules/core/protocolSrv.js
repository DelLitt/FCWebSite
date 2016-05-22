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

        this.saveProtocol = function (id, protocol, success, failure) {
            if (angular.isDefined(id) && parseInt(id) > 0) {
                apiSrv.put('/api/protocols/', id, protocol,
                                success,
                                function (response) {
                                    if (angular.isFunction(failure)) {
                                        failure(response);
                                    }

                                    protocolSaveFailed(response);
                                });
            } else {
                apiSrv.post('/api/protocols/', protocol,
                                success,
                                function (response) {
                                    if (angular.isFunction(failure)) {
                                        failure(response);
                                    }

                                    protocolSaveFailed(response);
                                });

            }
        }

        function protocolSaveFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();