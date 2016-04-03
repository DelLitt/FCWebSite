(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('personsSrv', personsSrv);

    personsSrv.$inject = ['$rootScope', 'helper', 'apiSrv', 'notificationManager'];

    function personsSrv($rootScope, helper, apiSrv, notificationManager) {

        this.loadPerson = function (id, success, failure) {
            apiSrv.get('/api/persons/' + id, null, success, personsLoadFail);
        }

        function personsLoadFail(response, customLoadFail) {
            if (angular.isFunction(customLoadFail)) {
                customLoadFail(response);
            }

            notificationManager.displayError(response.data);
        }

        this.getImageUploadPath = function (id) {
            if (angular.isDefined($rootScope.appConfig)) {
                if (angular.isObject($rootScope.appConfig.images)) {
                    if (angular.isString($rootScope.appConfig.images.persons)) {
                        return $rootScope.appConfig.images.persons.replace('{id}', id)
                    }
                    else {
                        notificationManager.displayError('Wrong configuraion appConfig.images.persons!');
                    }
                } else {
                    notificationManager.displayError('Wrong configuraion appConfig.images!');
                }
            } else {
                notificationManager.displayError('Object appConfig is not defined!');
            }
        }
    }
})();