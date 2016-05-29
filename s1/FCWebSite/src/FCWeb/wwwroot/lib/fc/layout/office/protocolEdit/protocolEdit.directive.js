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
                gameId: '=',
                protocol: '='
            },

            link: function link(scope, element, attrs) {

                loadData(scope.gameId);

                function loadData(gameId) {
                    if (gameId <= 0) {
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
                            "gameId": scope.gameId,
                            "minute": null,
                            "personId": null,
                            "teamId": 0,
                            "extraTime": false,
                            "mainPerson": null,
                            "extraPerson": null
                        });
                    }
                }

                scope.subs = {
                    removeItem: function (index, side) {
                        scope.protocol[side].subs.splice(index, 1);
                    },
                    addItem: function (side) {
                        scope.protocol[side].subs.push({
                            "id": 0,
                            "customIntValue": null,
                            "eventId": 32,
                            "gameId": scope.gameId,
                            "minute": 1,
                            "personId": null,
                            "teamId": 0,
                            "extraTime": false,
                            "mainPerson": null,
                            "extraPerson": null
                        });
                    }
                }

                scope.cards = {
                    removeItem: function (index, side) {
                        scope.protocol[side].cards.splice(index, 1);
                    },
                    addItem: function (side) {
                        scope.protocol[side].cards.push({
                            "id": 0,
                            "customIntValue": null,
                            "eventId": 0,
                            "gameId": scope.gameId,
                            "minute": 1,
                            "personId": null,
                            "teamId": 0,
                            "extraTime": false,
                            "eventModel": null,
                            "mainPerson": null,
                            "extraPerson": null
                        });
                    }
                }

                scope.others = {
                    removeItem: function (index, side) {
                        scope.protocol[side].others.splice(index, 1);
                    },
                    addItem: function (side) {
                        scope.protocol[side].others.push({
                            "id": 0,
                            "customIntValue": null,
                            "eventId": 0,
                            "gameId": scope.gameId,
                            "minute": 1,
                            "personId": null,
                            "teamId": 0,
                            "extraTime": false,
                            "eventModel": null,
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