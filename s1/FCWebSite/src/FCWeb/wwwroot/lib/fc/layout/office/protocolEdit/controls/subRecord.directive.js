(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('subRecord', subRecord);

    subRecord.$inject = [];
    
    function subRecord() {

        return {
            restrict: 'E',
            scope: {
                record: '=',
                players: '='
            },

            link: function link(scope, element, attrs) {
          
            },

            templateUrl: '/lib/fc/layout/office/protocolEdit/controls/subRecord.html'

        };
    }

})();