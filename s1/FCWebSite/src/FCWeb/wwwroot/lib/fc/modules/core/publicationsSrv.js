(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('publicationsSrv', publicationsSrv);

    publicationsSrv.$inject = ['helper', 'apiSrv', 'notificationManager'];

    function publicationsSrv(helper, apiSrv, notificationManager) {

        this.loadLatestPublications = function (count, success, failure) {
            apiSrv.get('/api/publications/latest/' + count, null, success, function (response) {
                if (failure != null) {
                    failure(response);
                }

                publicationsLoadFailed(response);
            });
        }

        function publicationsLoadFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();