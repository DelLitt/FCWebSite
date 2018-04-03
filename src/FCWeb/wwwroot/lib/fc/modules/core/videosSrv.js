(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('videosSrv', videosSrv);

    videosSrv.$inject = ['helper', 'apiSrv', 'notificationManager', 'configSrv'];

    function videosSrv(helper, apiSrv, notificationManager, configSrv) {

        this.loadMainVideos = function (count, success, failure) {
            this.loadVideosPack(count, 0, ['main'], success, failure);
        }

        this.loadMainVideosPack = function (count, skip, success, failure) {
            this.loadVideosPack(count, skip, ['main'], success, failure);
        }

        this.loadNotFilteredVideos = function (count, skip, success, failure) {
            this.loadVideosPack(count, skip, ['main', 'news', 'reserve', 'youth', 'authorized'], success, failure);
        }

        this.loadVideosPack = function (count, offset, groups, success, failure) {
            var visibilityParams = '';

            if(angular.isArray(groups) && groups.length > 0) {
                var delim = '';

                for(var i = 0; i < groups.length; i++) {
                    delim = i == 0 ? '?' : '&';
                    visibilityParams += delim + 'groups=' + groups[i];
                }
            }

            apiSrv.get('/api/videos/' + count + "/" + offset + visibilityParams, null, success, function (response) {
                if (failure != null) {
                    failure(response);
                }

                videosLoadFailed(response);
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

                    videosLoadFailed(response);
                });
        }

        this.createVideo = function (success, failure) {
            apiSrv.get('/api/videos/create',
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    videosLoadFailed(response);
                });
        }

        this.loadVideoByUrlKey = function (urlKey, success, failure) {
            apiSrv.get('/api/videos/' + urlKey,
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    videosLoadFailed(response);
                });
        }

        this.search = function (text, success, failure) {
            var url = 'api/videos/search/default?txt=' + encodeURIComponent(text)

            apiSrv.get(url,
                null,
                success,
                function (response) {
                    if (failure != null) {
                        failure(response);
                    }

                    videosLoadFailed(response);
                });
        }

        function videosLoadFailed(response) {
            notificationManager.displayError(response.data);
        }

        this.saveVideo = function (id, video, success, failure) {
            if (angular.isDefined(id) && parseInt(id) > 0) {
                apiSrv.put('/api/videos/', id, video,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    videoSaveFailed(response);
                                });
            } else {
                apiSrv.post('/api/videos/', video, null,
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