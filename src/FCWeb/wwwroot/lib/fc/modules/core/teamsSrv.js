﻿(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('teamsSrv', teamsSrv);

    teamsSrv.$inject = ['$rootScope', 'helper', 'apiSrv', 'notificationManager', 'configSrv'];

    function teamsSrv($rootScope, helper, apiSrv, notificationManager, configSrv) {

        this.loadTeam = function (id, success, failure) {
            apiSrv.get('/api/teams/' + id,
                null,
                success,
                function (response) {
                    if (angular.isFunction(failure)) {
                        failure(response);
                    }

                    teamsLoadFail(response);
                });
        }

        this.createTeam = function (success, failure) {
            apiSrv.get('/api/teams/create/true',
                null,
                success,
                function (response) {
                    if (angular.isFunction(failure)) {
                        failure(response);
                    }

                    teamsLoadFail(response);
                });
        }

        this.loadAllTeams = function (success, failure) {
            apiSrv.get('/api/teams',
                null,
                success,
                function (response) {
                    if (angular.isFunction(failure)) {
                        failure(response);
                    }

                    teamsLoadFail(response);
                });
        }

        this.loadTeamsList = function (type, success, failure) {
            apiSrv.get('/api/teams/type/' + type + '/list',
                null,
                success,
                function (response) {
                    if (angular.isFunction(failure)) {
                        failure(response);
                    }

                    teamsLoadFail(response);
                });
        }

        this.loadYothTeams = function (parentTeamId, success, failure) {
            apiSrv.get('/api/teams/type/youth/parent/' + parentTeamId,
                null,
                success,
                function (response) {
                    if (angular.isFunction(failure)) {
                        failure(response);
                    }

                    teamsLoadFail(response);
                });
        }

        this.search = function (text, success, failure) {
            var url = 'api/teams/search?txt=' + encodeURIComponent(text)
            sendGet(url, success, failure);
        }

        this.searchByType = function (type, text, success, failure) {
            var url = 'api/teams/search/' + type + '?txt=' + encodeURIComponent(text)
            sendGet(url, success, failure);
        }

        function sendGet(url, success, failure) {
            apiSrv.get(url,
                       null,
                       success,
                       function (response) {
                           if (failure != null) {
                               failure(response);
                           }

                           teamsLoadFail(response);
                       });
        }

        function teamsLoadFail(response) {
            notificationManager.displayError(response.data);
        }

        this.saveTeam = function (id, team, success, failure) {
            if (angular.isDefined(id) && parseInt(id) > 0) {
                apiSrv.put('/api/teams/', id, team,
                                success,
                                function (response) {
                                    if (angular.isFunction(failure)) {
                                        failure(response);
                                    }

                                    teamSaveFailed(response);
                                });
            } else {
                apiSrv.post('/api/teams/', team, null,
                                success,
                                function (response) {
                                    if (angular.isFunction(failure)) {
                                        failure(response);
                                    }

                                    teamSaveFailed(response);
                                });

            }
        }

        function teamSaveFailed(response) {
            notificationManager.displayError(response.data);
        }

        this.getImageUploadData = function (team) {
            var uniqueKey = 0;
            var createNew = false;
            
            if (angular.isNumber(team.id) && team.id > 0) {
                uniqueKey = team.id;
                createNew = true;
            } else if (angular.isDefined(team.tempGuid)) {
                uniqueKey = team.tempGuid;
                createNew = true;
            }

            var teamPath = configSrv.Current.Images.Teams;

            return {
                path: teamPath.replace('{id}', uniqueKey),
                createNew: createNew
            };
        }
    }
})();