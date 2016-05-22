(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('protocolEdit', protocolEdit);

    protocolEdit.$inject = ['apiSrv', 'protocolSrv'];
    
    function protocolEdit(apiSrv, protocolSrv) {

        return {
            restrict: 'E',
            scope: {
                gameId: '='
            },

            link: function link(scope, element, attrs) {

                loadData(scope.gameId);

                function loadData(gameId) {
                    if (gameId < 0) {
                        return;
                    }

                    protocolSrv.loadProtocol(gameId, protocolLoaded);
                }

                function protocolLoaded(response) {
                    var protocol = response.data;

                    scope.protocol = protocol;
                }

                scope.goals = {
                    removeItem: function (index, side) {
                        scope.protocol[side].goals.splice(index, 1);
                    },
                    addItem: function (side) {
                        scope.protocol[side].goals.push({
                            "id": 0,
                            "customIntValue": null,
                            "eventId": 0,
                            "gameId": 0,
                            "minute": null,
                            "personId": null,
                            "teamId": 0,
                            "extraTime": false,
                            "mainPerson": null,
                            "extraPerson": null
                        });
                    }
                }
                
            },

            templateUrl: '/lib/fc/layout/office/protocolEdit/protocolEdit.html'
        };
    }

})();