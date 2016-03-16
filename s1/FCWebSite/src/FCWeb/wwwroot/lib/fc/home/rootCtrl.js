(function () {
    'use strict';

    angular
        .module('fc')
        .controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['$scope', '$translate'];

    function rootCtrl($scope, $translate) {       

        $scope.changeLanguage = function (langKey) {
            $translate.use(langKey);
        };
    }
})();
