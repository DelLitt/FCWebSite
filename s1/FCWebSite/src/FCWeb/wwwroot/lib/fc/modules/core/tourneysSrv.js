(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('tourneysSrv', tourneysSrv);

    tourneysSrv.$inject = ['apiSrv', 'notificationManager'];

    function tourneysSrv(apiSrv, notificationManager) {

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
    }
})();