(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('configSrv', configSrv);

    configSrv.$inject = ['$rootScope', 'helper', 'apiSrv', 'notificationManager'];

    function configSrv($rootScope, helper, apiSrv, notificationManager) {

        this.loadConfig = function (loaded) {
            apiSrv.get(
                '/api/configuration',
                null,
                function (response) {
                    loadConfigurationSuccess(response);

                    if (angular.isFunction(loaded)) {
                        loaded();
                    }
                },
                loadConfigurationFail);
        }

        this.loadConfigOffice = function (loaded) {
            apiSrv.get(
                '/api/configuration/office',
                null,
                function (response) {
                    loadConfigurationSuccess(response);

                    if(angular.isFunction(loaded)) {
                        loaded();
                    }
                },
                loadConfigurationFail);
        }

        this.settingsVisibility = {};
        var that = this;

        function loadConfigurationSuccess(response) {
            $rootScope.appConfig = response.data;
            that.settingsVisibility = $rootScope.appConfig.settingsVisibility;
        }

        function loadConfigurationFail(response) {
            notificationManager.displayError(response);
        }

        this.getPersonImageUploadPath = function () {
            return getImagesValue('persons');
        }

        this.getTeamImageUploadPath = function () {
            return getImagesValue('teams');
        }

        this.getImageStorePath = function () {
            return getImagesValue('store');
        }


        function getImagesValue(key) {
            if (!angular.isObject($rootScope.appConfig)) {
                notificationManager.displayError('Object appConfig is not defined!');
                return;
            }

            if (!angular.isObject($rootScope.appConfig.images)) {
                notificationManager.displayError('Wrong configuraion appConfig.images!');
                return;
            }

            if (!angular.isString($rootScope.appConfig.images[key])) {
                notificationManager.displayError('Wrong configuraion appConfig.images.' + key + '!');
            }

            return $rootScope.appConfig.images[key];
        }

        this.getSettingsVisibility = function () {
            if (!angular.isObject($rootScope.appConfig)) {
                notificationManager.displayError('Object appConfig is not defined!');
                return;
            }

            if (!angular.isObject($rootScope.appConfig.settingsVisibility)) {
                notificationManager.displayError('Wrong configuraion appConfig.settingsVisibility!');
                return;
            }

            return $rootScope.appConfig.settingsVisibility;
        }

        this.getEventGroupFriendlyNames = function () {
            if (!angular.isObject($rootScope.appConfig)) {
                notificationManager.displayError('Object appConfig is not defined!');
                return;
            }

            if (!angular.isObject($rootScope.appConfig.eventGroupFriendlyNames)) {
                notificationManager.displayError('Wrong configuraion appConfig.eventGroupFriendlyNames!');
                return;
            }

            return $rootScope.appConfig.eventGroupFriendlyNames;
        }

        this.getMainTeamId = function () {
            if (angular.isObject($rootScope.appConfig) && angular.isNumber($rootScope.appConfig.MainTeamId)) {
                return $rootScope.appConfig.MainTeamId;
            }

            // TODO: Clear hardcode
            return 3;
        }

        this.positions = {
            rrGoalkeeper : 2,
            rrDefender : 3,
            rrMidfielder : 4,
            rrForward : 5,
            rrPositionUnknown : 6
        }

        this.urlKeyRegexPattern = '^[a-zA-Z0-9_-]+$';
    }
})();