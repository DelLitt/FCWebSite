(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('imageGallerySrv', imageGallerySrv);

    imageGallerySrv.$inject = ['helper', 'apiSrv', 'notificationManager', 'configSrv'];

    function imageGallerySrv(helper, apiSrv, notificationManager, configSrv) {

        this.loadMainGalleries = function (count, success, failure) {
            apiSrv.get('/api/galleries/latest/' + count, null, success, function (response) {
                if (failure != null) {
                    failure(response);
                }

                galleriesLoadFailed(response);
            });
        }

        this.loadGalleriesPack = function (count, offset, success, failure) {
            apiSrv.get('/api/galleries/latest/' + count + "/" + offset, null, success, function (response) {
                if (failure != null) {
                    failure(response);
                }

                galleriesLoadFailed(response);
            });
        }

        this.loadGallery = function (id, success, failure) {
            apiSrv.get('/api/galleries/' + id,
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