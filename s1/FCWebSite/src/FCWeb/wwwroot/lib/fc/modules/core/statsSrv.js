(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('statsSrv', statsSrv);

    statsSrv.$inject = ['apiSrv', 'notificationManager', 'configSrv'];

    function statsSrv(apiSrv, notificationManager, configSrv) {

        this.loadPersonStats = function (personId, success, failure) {
            apiSrv.get('/api/personsstats/' + personId,
                       null,
                       success,
                       function (response) {
                           if (failure != null) {
                               failure(response);
                           }

                           teamStatsFailed(response);
                       });
        }

        this.loadTeamTourneyStats = function (teamId, tourneyId, success, failure) {
            apiSrv.get('/api/personsstats/team/' + teamId + '/tourney/' + tourneyId,
                       null,
                       success,
                       function (response) {
                           if (failure != null) {
                               failure(response);
                           }

                           teamStatsFailed(response);
                       });
        }

        function teamStatsFailed(response) {
            notificationManager.displayError(response.data);
        }
    }
})();