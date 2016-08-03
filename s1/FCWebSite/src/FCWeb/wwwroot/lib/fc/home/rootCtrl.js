(function () {
    'use strict';

    angular
        .module('fc')
        .controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['$scope', '$translate', 'configSrv'];

    function rootCtrl($scope, $translate, configSrv) {
        $scope.changeLanguage = function (langKey) {
            $translate.use(langKey);
        };
    }
})();
