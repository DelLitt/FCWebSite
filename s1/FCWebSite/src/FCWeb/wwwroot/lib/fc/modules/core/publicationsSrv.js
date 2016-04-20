(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('publicationsSrv', publicationsSrv);

    publicationsSrv.$inject = ['helper', 'apiSrv', 'notificationManager', 'configSrv'];

    function publicationsSrv(helper, apiSrv, notificationManager, configSrv) {

        this.loadLatestPublications = function (count, success, failure) {
            apiSrv.get('/api/publications/latest/' + count, null, success, function (response) {
                if (failure != null) {
                    failure(response);
                }

                publicationsLoadFailed(response);
            });
        }

        this.loadPublication = function (id, success, failure) {
            apiSrv.get('/api/publications/' + id,
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    publicationsLoadFailed(response);
            });
        }

        function publicationsLoadFailed(response) {
            notificationManager.displayError(response.data);
        }

        this.getImagesPath = function () {
            return configSrv.getImageStorePath();
        }
    }
})();