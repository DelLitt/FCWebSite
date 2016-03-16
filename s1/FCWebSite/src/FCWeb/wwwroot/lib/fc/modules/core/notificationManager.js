(function () {
    'use strict';

    angular
        .module('fc.core')
        .factory('notificationManager', notificationManager);
    
    function notificationManager() {

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
            alert(message);
        }

        function displayError(error) {
            if (Array.isArray(error)) {
                error.forEach(function (err) {
                    //toastr.error(err);
                    alert(message);
                });
            } else {
                //toastr.error(error);
                alert(message);
            }
        }

        function displayWarning(message) {
            //toastr.warning(message);
            alert(message);
        }

        function displayInfo(message) {
            //toastr.info(message);
            alert(message);
        }
    }
})();