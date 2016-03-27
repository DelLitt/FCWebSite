(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('rankingsSrv', rankingsSrv);

    rankingsSrv.$inject = ['helper', 'apiSrv', 'notificationManager'];

    function rankingsSrv(helper, apiSrv, notificationManager) {

        this.loadRankingTable = function (tourneyId, success, failure) {
            apiSrv.get('/api/rankings/' + tourneyId, null, success, function (response) {
                if (failure != null) {
                    failure(response);
                }

                rankingTableLoadFailed(response);
            });
        }

        function rankingTableLoadFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();