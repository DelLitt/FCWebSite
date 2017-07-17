(function () {
    'use strict';

    angular
        .module('fc.core')
        .service('apiSrv', apiSrv);

    apiSrv.$inject = ['$http', '$location', 'notificationManager', '$rootScope'];

    function apiSrv($http, $location, notificationManager, $rootScope) {

        this.get = function(url, config, success, failure) {
            return $http.get(url, config)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status == '401') {
                            notificationManager.displayError('Authentication required.');
                            $rootScope.previousState = $location.path();
                            $location.path('/login');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }
 
        this.post = function (url, data, config, success, failure) {
            return $http.post(url, data, config)
                    .then(function (result) {
                        if (angular.isFunction(success)) {
                            success(result);
                        }
                    }, function (error) {
                        if (error.status == '401') {
                            notificationManager.displayError('Authentication required.');
                            $rootScope.previousState = $location.path();
                            $location.path('/login');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }

        this.put = function (url, id, data, success, failure) {
            return $http.put(url + id, data)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status == '401') {
                            notificationManager.displayError('Authentication required.');
                            $rootScope.previousState = $location.path();
                            $location.path('/login');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }

        this.delete = function (url, config, success, failure) {
            return $http.delete(url, config)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status == '401') {
                            notificationManager.displayError('Authentication required.');
                            $rootScope.previousState = $location.path();
                            $location.path('/login');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }
    }
})();