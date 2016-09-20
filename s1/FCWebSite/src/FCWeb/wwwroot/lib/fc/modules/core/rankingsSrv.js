(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('rankingsSrv', rankingsSrv);

    rankingsSrv.$inject = ['helper', 'apiSrv', 'notificationManager'];

    function rankingsSrv(helper, apiSrv, notificationManager) {

        this.loadRankingTable = function (tourneyId, success, failure) {
            apiSrv.get('/api/rankings/' + tourneyId,
                        null,
                        success,
                        function (response) {
                            if (failure != null) {
                                failure(response);
                            }

                            rankingTableLoadFailed(response);
                        });
        }

        this.updateRankingTable = function (id, success, failure) {
            apiSrv.put('/api/rankings/', id,
                            null,
                            success,
                            function (response) {
                                if (failure != null) {
                                    failure(response);
                                }

                                rankingTableSaveFailed(response);
                            });
        }

        function rankingTableLoadFailed(response) {
            notificationManager.displayError(response.data);
        }

        function rankingTableSaveFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();