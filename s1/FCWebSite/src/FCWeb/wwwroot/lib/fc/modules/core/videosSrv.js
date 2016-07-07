(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('videosSrv', videosSrv);

    videosSrv.$inject = ['helper', 'apiSrv', 'notificationManager', 'configSrv'];

    function videosSrv(helper, apiSrv, notificationManager, configSrv) {

        this.loadMainVideos = function (count, success, failure) {
            apiSrv.get('/api/videos/latest/' + count, null, success, function (response) {
                if (failure != null) {
                    failure(response);
                }

                videoLoadFailed(response);
            });
        }

        this.loadVideosPack = function (count, offset, success, failure) {
            apiSrv.get('/api/videos/latest/' + count + "/" + offset, null, success, function (response) {
                if (failure != null) {
                    failure(response);
                }

                videoLoadFailed(response);
            });
        }

        this.loadVideo = function (id, success, failure) {
            apiSrv.get('/api/videos/' + id,
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    videoLoadFailed(response);
            });
        }

        function videoLoadFailed(response) {
            notificationManager.displayError(response.data);
        }

        //this.getImagesPath = function () {
        //    return configSrv.getImageStorePath();
        //}

        this.saveVideo = function (id, video, success, failure) {
            if (angular.isDefined(id) && parseInt(id) > 0) {
                apiSrv.put('/api/videos/', id, video, null,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    videoSaveFailed(response);
                                });
            } else {
                apiSrv.post('/api/videos/', video,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    videoSaveFailed(response);
                                });

            }
        }

        function videoSaveFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();