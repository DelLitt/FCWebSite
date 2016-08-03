(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('configSrv', configSrv);

    configSrv.$inject = ['$rootScope', '$document', 'notificationManager'];

    function configSrv($rootScope, $document, notificationManager) {

        var cfgElement = $document[0].getElementById('config');        
        var config = angular.fromJson(cfgElement.innerText);
        angular.element(cfgElement).remove();

        if (!angular.isObject(config)) {
            notificationManager.displayError("Unable to initialize application configuration!");
            return;
        }

        this.Current = config;
    }
})();