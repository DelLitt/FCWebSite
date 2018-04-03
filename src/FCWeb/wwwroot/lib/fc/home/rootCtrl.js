(function () {
    'use strict';

    angular
        .module('fc')
        .controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['$scope', '$translate', 'configSrv'];

    function rootCtrl($scope, $translate, configSrv) {
        //alert("Screen W: " + window.screen.availWidth + ", H: " + window.screen.availHeight);

        $scope.sqreenW = window.screen.availWidth;
        $scope.sqreenH = window.screen.availHeight;

        $scope.changeLanguage = function (langKey) {
            $translate.use(langKey);
        };
    }
})();
