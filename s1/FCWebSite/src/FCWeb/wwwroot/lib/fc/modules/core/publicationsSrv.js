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

        this.loadPublicationByUrlKey = function (urlKey, success, failure) {
            apiSrv.get('/api/publications/' + urlKey,
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

        this.savePublication = function (id, publication, success, failure) {
            if (angular.isDefined(id) && parseInt(id) > 0) {
                apiSrv.put('/api/publications/', id, publication,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    videoSaveFailed(response);
                                });
            } else {
                apiSrv.post('/api/publications/', publication,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    publicationSaveFailed(response);
                                });

            }
        }

        function publicationSaveFailed(response) {
            notificationManager.displayError(response.data);
        }

        this.getImagesPath = function () {
            return configSrv.getImageStorePath();
        }
    }
})();