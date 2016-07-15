(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('imageGallerySrv', imageGallerySrv);

    imageGallerySrv.$inject = ['helper', 'apiSrv', 'notificationManager', 'configSrv'];

    function imageGallerySrv(helper, apiSrv, notificationManager, configSrv) {

        this.loadMainGalleries = function (count, success, failure) {
            this.loadGalleriesPack(count, 0, ['main'], success, failure);
        }

        this.loadNotFilteredGalleries = function (count, skip, success, failure) {
            this.loadGalleriesPack(count, skip, ['main', 'news', 'reserve', 'youth', 'authorized'], success, failure);
        }

        this.loadGalleriesPack = function (count, offset, groups, success, failure) {
            var visibilityParams = '';

            if(angular.isArray(groups) && groups.length > 0) {
                var delim = '';

                for(var i = 0; i < groups.length; i++) {
                    delim = i == 0 ? '?' : '&';
                    visibilityParams += delim + 'groups=' + groups[i];
                }
            }

            apiSrv.get('/api/galleries/' + count + "/" + offset + visibilityParams, null, success, function (response) {
                if (failure != null) {
                    failure(response);
                }

                galleriesLoadFailed(response);
            });
        }

        this.loadGallery = function (id, success, failure) {
            apiSrv.get('/api/publications/' + id,
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    galleriesLoadFailed(response);
                });
        }

        this.loadGalleryByUrlKey = function (urlKey, success, failure) {
            apiSrv.get('/api/publications/' + urlKey,
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    galleriesLoadFailed(response);
                });
        }

        this.search = function (text, success, failure) {
            var url = 'api/galleries/search/default?txt=' + encodeURIComponent(text)

            apiSrv.get(url,
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    galleriesLoadFailed(response);
                });
        }

        this.saveGallery = function (id, gallery, success, failure) {
            if (angular.isDefined(id) && parseInt(id) > 0) {
                apiSrv.put('/api/galleries/', id, gallery,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    gallerySaveFailed(response);
                                });
            } else {
                apiSrv.post('/api/galleries/', gallery, null,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    gallerySaveFailed(response);
                                });

            }
        }

        function galleriesLoadFailed(response) {
            notificationManager.displayError(response.data);
        }

        function gallerySaveFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();