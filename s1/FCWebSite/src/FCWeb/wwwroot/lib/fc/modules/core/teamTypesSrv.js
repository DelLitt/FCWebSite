(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('teamTypesSrv', teamTypesSrv);

    teamTypesSrv.$inject = ['apiSrv', 'notificationManager'];

    function teamTypesSrv(apiSrv, notificationManager) {

        this.loadTeamType = function (id, success, failure) {
            apiSrv.get('/api/teamtypes/' + id, 
                null, 
                success, 
                function (response) {
                    if(angular.isFunction(failure)) {
                        failure(response);
                    }

                    teamTypesLoadFail(response);
                });
        }

        this.loadAllTeamTypes = function (success, failure) {
            apiSrv.get('/api/teamtypes',
                null,
                success,
                function (response) {
                    if (angular.isFunction(failure)) {
                        failure(response);
                    }

                    teamTypesLoadFail(response);
                });
        }

        function teamTypesLoadFail(response) {
            notificationManager.displayError(response.data);
        }
    }
})();