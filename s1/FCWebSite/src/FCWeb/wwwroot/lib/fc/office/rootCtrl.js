(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['configSrv'];

    function rootCtrl(configSrv) {
        configSrv.loadConfigOffice();
    }
})();
