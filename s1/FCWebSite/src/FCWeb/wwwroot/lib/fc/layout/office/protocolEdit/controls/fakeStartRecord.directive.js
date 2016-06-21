(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('fakeStartRecord', fakeStartRecord);

    fakeStartRecord.$inject = [];
    
    function fakeStartRecord() {

        return {
            restrict: 'E',
            scope: {
                record: '='
            },

            link: function link(scope, element, attrs) {
          
            },

            templateUrl: '/lib/fc/layout/office/protocolEdit/controls/fakeStartRecord.html'

        };
    }

})();