(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['$scope', 'configSrv'];

    function rootCtrl($scope, configSrv) {
        var clickListeners = [];

        $scope.click = function () {
            angular.forEach(clickListeners, function (value, key) {
                value();
            }, null);
        }

        $scope.addRootClickListener = function(callback) {
            if (angular.isFunction(callback)) {
                clickListeners.push(callback);
            }
        }

        $scope.removeRootClickListener = function(callback) {
            if (angular.isFunction(callback)) {
                var index = scope.inputdata.indexOf(callback);
                if (index >= 0) {
                    clickListeners.splice(index, 1);
                }
            }
        }
    }
})();
