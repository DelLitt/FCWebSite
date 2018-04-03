(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('protocolFakeEdit', protocolFakeEdit);

    protocolFakeEdit.$inject = ['apiSrv', 'protocolSrv'];
    
    function protocolFakeEdit(apiSrv, protocolSrv) {

        return {
            restrict: 'E',
            scope: {
                fake: '='
            },

            link: function link(scope, element, attrs) {

                scope.main = {
                    removeItem: function (index, side) {
                        scope.fake[side].main.splice(index, 1);
                    },
                    addItem: function (side) {
                        scope.fake[side].main.push({
                            "name": "",
                            "minute": 0,
                            "extraTime": false,
                            "info": "",
                            "data": ""
                        });
                    }
                }

                scope.reserve = {
                    removeItem: function (index, side) {
                        scope.fake[side].reserve.splice(index, 1);
                    },
                    addItem: function (side) {
                        scope.fake[side].reserve.push({name: ""});
                    }
                }

                scope.event = {
                    removeItem: function (index, side, event) {
                        scope.fake[side][event].splice(index, 1);
                    },
                    addItem: function (side, event) {
                        scope.fake[side][event].push({
                            "name": "",
                            "minute": 0,
                            "extraTime": false,
                            "info": "",
                            "data": ""
                        });
                    }
                }
                
            },

            templateUrl: '/lib/fc/layout/office/protocolEdit/protocolFakeEdit.html'
        };
    }

})();