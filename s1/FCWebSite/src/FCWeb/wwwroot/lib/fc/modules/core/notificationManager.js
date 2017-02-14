(function () {
    'use strict';

    angular
        .module('fc.core')
        .factory('notificationManager', notificationManager);

    notificationManager.$inject = ['$log'];
    
    function notificationManager($log) {

        //toastr.options = {
        //    "debug": false,
        //    "positionClass": "toast-top-right",
        //    "onclick": null,
        //    "fadeIn": 300,
        //    "fadeOut": 1000,
        //    "timeOut": 3000,
        //    "extendedTimeOut": 1000
        //};

        var manager = {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        };

        return manager;

        function displaySuccess(message) {
            //toastr.success(message);
            $log.info(message);
        }

        function displayError(error) {
            if (Array.isArray(error)) {
                error.forEach(function (error) {
                    //toastr.error(err);
                    $log.error(error);
                });
            } else {
                //toastr.error(error);
                $log.error(error);
            }
        }

        function displayWarning(message) {
            //toastr.warning(message);
            $log.warning(message);
        }

        function displayInfo(message) {
            //toastr.info(message);
            $log.info(message);
        }
    }
})();