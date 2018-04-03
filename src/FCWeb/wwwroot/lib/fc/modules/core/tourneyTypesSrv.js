(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('tourneyTypesSrv', tourneyTypesSrv);

    tourneyTypesSrv.$inject = ['apiSrv', 'notificationManager'];

    function tourneyTypesSrv(apiSrv, notificationManager) {

        this.loadTourneyType = function (id, success, failure) {
            apiSrv.get('/api/tourneys/' + id,
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    tourneyTypesLoadFailed(response);
                });
        }

        this.loadAllTourneys = function (success, failure) {
            apiSrv.get("/api/tourneytypes/", 
                        null, 
                        success,
                        function (response) {
                            if (failure != null) {
                                failure(response);
                            }

                            tourneyTypesLoadFailed(response);
                        });
        }

        this.search = function (text, success, failure) {
            var url = 'api/tourneytypes/search?txt=' + encodeURIComponent(text)

            apiSrv.get(url,
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    tourneyTypesLoadFailed(response);
                });
        }

        function tourneyTypesLoadFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();