(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('fakeEventRecord', fakeEventRecord);

    fakeEventRecord.$inject = [];
    
    function fakeEventRecord() {

        return {
            restrict: 'E',
            scope: {
                record: '='
            },

            link: function link(scope, element, attrs) {
          
            },

            templateUrl: '/lib/fc/layout/office/protocolEdit/controls/fakeEventRecord.html'

        };
    }

})();