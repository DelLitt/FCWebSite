(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('personsSrv', personsSrv);

    personsSrv.$inject = ['$rootScope', 'helper', 'apiSrv', 'notificationManager', 'configSrv'];

    function personsSrv($rootScope, helper, apiSrv, notificationManager, configSrv) {

        this.loadPerson = function (id, success, failure) {
            apiSrv.get('/api/persons/' + id, null, success, personsLoadFail);
        }

        this.loadTeamMainPlayers = function (id, success, failure) {
            apiSrv.get('/api/teams/' + id + '/persons/mainteam', null, success, personsLoadFail);
        }

        this.loadCoachesStaff = function (id, success, failure) {
            apiSrv.get('/api/teams/' + id + '/persons/coaches', null, success, personsLoadFail);
        }

        this.loadDirectionStaff = function (id, success, failure) {
            apiSrv.get('/api/teams/' + id + '/persons/direction', null, success, personsLoadFail);
        }

        this.loadMedicalStaff = function (id, success, failure) {
            apiSrv.get('/api/teams/' + id + '/persons/medics', null, success, personsLoadFail);
        }

        this.loadSpecialistsStaff = function (id, success, failure) {
            apiSrv.get('/api/teams/' + id + '/persons/specialists', null, success, personsLoadFail);
        }

        this.loadAllPersons = function (success, failure) {
            apiSrv.get('/api/persons', null, success, personsLoadFail);
        }

        function personsLoadFail(response, customLoadFail) {
            if (angular.isFunction(customLoadFail)) {
                customLoadFail(response);
            }

            notificationManager.displayError(response.data);
        }

        this.savePerson = function (id, person, success, failure) {
            if (angular.isDefined(id) && parseInt(id) > 0) {
                apiSrv.put('/api/persons/', id, person,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    personSaveFailed(response);
                                });
            } else {
                apiSrv.post('/api/persons/', person, null,
                                success,
                                function (response) {
                                    if (failure != null) {
                                        failure(response);
                                    }

                                    personSaveFailed(response);
                                });

            }
        }

        function personSaveFailed(response) {
            notificationManager.displayError(response.data);
        }

        this.getImageUploadData = function (person) {
            var uniqueKey = 0;
            var createNew = false;
            
            if (angular.isNumber(person.id) && person.id > 0) {
                uniqueKey = person.id;
                createNew = true;
            } else if (angular.isDefined(person.tempGuid)) {
                uniqueKey = person.tempGuid;
                createNew = true;
            }

            var personPath = configSrv.Current.Images.Persons;

            return {
                path: personPath.replace('{id}', uniqueKey),
                createNew: createNew
            };
        }
    }
})();