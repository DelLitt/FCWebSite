(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('tourneysSrv', tourneysSrv);

    tourneysSrv.$inject = ['apiSrv', 'notificationManager'];

    function tourneysSrv(apiSrv, notificationManager) {

        this.loadTourney = function (id, success, failure) {
            apiSrv.get('/api/tourneys/' + id + '/content',
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    tourneysLoadFailed(response);
                });
        }

        this.loadTourneys = function (tourneyIds, success, failure) {
            var url = "/api/tourneys/list?";

            tourneyIds.forEach(function (element, index, array) {
                url = url + "tourneyIds=" + element + (index < array.length - 1 ? "&" : "")
            });

            apiSrv.get(url, null, success, 
                        function (response) {
                            if (failure != null) {
                                failure(response);
                            }

                            notificationManager.displayError(response.data);
                        });
        }

        this.loadAllTourneys = function (success, failure) {
            apiSrv.get("/api/tourneys/", 
                        null, 
                        success,
                        function (response) {
                            if (failure != null) {
                                failure(response);
                            }

                            notificationManager.displayError(response.data);
                        });
        }

        this.search = function (text, success, failure) {
            var url = 'api/tourneys/search?txt=' + encodeURIComponent(text)

            apiSrv.get(url,
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    tourneysLoadFailed(response);
                });
        }

        function tourneysLoadFailed(response) {
            notificationManager.displayError(response.data);
        }

        this.saveTourney = function (id, tourney, success, failure) {
            if (angular.isDefined(id) && parseInt(id) > 0) {
                apiSrv.put('/api/tourneys/',
                                id,
                                tourney,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    tourneySaveFailed(response);
                                });
            } else {
                apiSrv.post('/api/tourneys/',
                                tourney,
                                null,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    tourneySaveFailed(response);
                                });

            }
        }

        function tourneySaveFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();