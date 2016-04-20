(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('videoEdit', videoEdit);

    function videoEdit() {
        return {
            restrict: 'E',
            scope: {
                videoid: '=',
                forms: '=',
                callbackdata: '='
            },
            replace: true,
            controller: "videoEditCtrl",
            templateUrl: '/lib/fc/office/videos/videoEdit.html'
        }
    }
})();