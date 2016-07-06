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

        function galleriesLoadFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();