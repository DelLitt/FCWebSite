(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('publicationsSrv', publicationsSrv);

    publicationsSrv.$inject = ['helper', 'apiSrv', 'notificationManager', 'configSrv'];

    function publicationsSrv(helper, apiSrv, notificationManager, configSrv) {

        this.loadMainPublications = function (count, success, failure) {
            this.loadPublicationsPack(count, 0, ['main'], success, failure);
        }

        this.loadMainPublicationsPack = function (count, skip, success, failure) {
            this.loadPublicationsPack(count, skip, ['main'], success, failure);
        }

        this.loadYouthPublications = function (count, skip, success, failure) {
            this.loadPublicationsPack(count, skip, ['youth'], success, failure);
        }

        this.loadNotFilteredPublications = function (count, skip, success, failure) {
            this.loadPublicationsPack(count, skip, ['main', 'news', 'reserve', 'youth'], success, failure);
        }

        this.loadPublicationsPack = function (count, offset, groups, success, failure) {
            var visibilityParams = '';

            if(angular.isArray(groups) && groups.length > 0) {
                var delim = '';

                for(var i = 0; i < groups.length; i++) {
                    delim = i == 0 ? '?' : '&';
                    visibilityParams += delim + 'groups=' + groups[i];
                }
            }

            apiSrv.get('/api/publications/' + count + "/" + offset + visibilityParams, null, success, function (response) {
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

        this.createPublication = function (success, failure) {
            apiSrv.get('/api/publications/create',
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

        this.search = function (text, success, failure) {
            var url = 'api/publications/search/default?txt=' + encodeURIComponent(text)

            apiSrv.get(url,
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

                                    publicationSaveFailed(response);
                                });
            } else {
                apiSrv.post('/api/publications/',
                            publication,
                            null,
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
            return configSrv.Current.Images.Store;
        }
    }
})();