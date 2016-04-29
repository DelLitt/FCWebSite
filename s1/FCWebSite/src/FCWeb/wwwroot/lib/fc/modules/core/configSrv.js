﻿(function () {
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

        this.getPersonImageUploadPath = function (person) {
            return getImagesValue('persons');
        }

        this.getImageStorePath = function () {
            return getImagesValue('store');
        }


        function getImagesValue(key) {
            if (!angular.isDefined($rootScope.appConfig)) {
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
            if (!angular.isDefined($rootScope.appConfig)) {
                notificationManager.displayError('Object appConfig is not defined!');
                return;
            }

            if (!angular.isObject($rootScope.appConfig.settingsVisibility)) {
                notificationManager.displayError('Wrong configuraion appConfig.settingsVisibility!');
                return;
            }

            return appConfig.settingsVisibility;
        }

        this.urlKeyRegexPattern = '^[a-zA-Z0-9_-]+$';
    }
})();