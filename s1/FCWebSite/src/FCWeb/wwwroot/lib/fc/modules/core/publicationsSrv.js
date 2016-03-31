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

        this.testAuth = function () {
            apiSrv.get('/api/publications/151', null, 
                function (response) {
                    console.log('Success');
                    console.log(response);
                },
                function (response) {
                    console.log('Failure');
                    console.log(response);
            });
        }

        function publicationsLoadFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();