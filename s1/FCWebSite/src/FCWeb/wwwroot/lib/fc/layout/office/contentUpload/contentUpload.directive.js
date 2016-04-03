(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('contentUpload', contentUpload);

    function contentUpload() {
        return {
            restrict: 'E',
            require: '?ngModel',
            replace: true,
            controller: "contentUploadCtrl",
            templateUrl: '/lib/fc/layout/office/contentUpload/contentUpload.html'
        }
    }
})();