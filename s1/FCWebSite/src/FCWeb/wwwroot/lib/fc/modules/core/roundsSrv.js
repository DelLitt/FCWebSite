(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('roundsSrv', roundsSrv);

    roundsSrv.$inject = ['apiSrv', 'notificationManager'];

    function roundsSrv(apiSrv, notificationManager) {

        this.loadRound = function (id, success, failure) {
            apiSrv.get('/api/rounds/' + id,
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    roundsLoadFailed(response);
                });
        }

        function roundsLoadFailed(response) {
            notificationManager.displayError(response.data);
        }

        this.saveRound = function (id, round, success, failure) {
            if (angular.isDefined(id) && parseInt(id) > 0) {
                apiSrv.put('/api/rounds/',
                                id,
                                round,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    roundSaveFailed(response);
                                });
            } else {
                apiSrv.post('/api/rounds/',
                                round,
                                null,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    roundSaveFailed(response);
                                });

            }
        }

        function roundSaveFailed(response) {
            notificationManager.displayError(response.data);
        }

        this.removeRound = function (id, success, failure) {
            apiSrv.delete('/api/rounds/' + id,
                            null,
                            success,
                            function (response) {
                                if (angular.isFunction(failure)) {
                                    failure(response);
                                }

                                roundRemoveFailed(response);
                            });

        }

        function roundRemoveFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();