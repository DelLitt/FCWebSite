(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('rankingTable', rankingTable);

    rankingTable.$inject = ['rankingsSrv', 'helper'];
    
    function rankingTable(rankingsSrv, helper) {

        return {
            link: link,
            restrict: 'E',
            scope: {
                tourneyId: '=',
                hasData: '='
            },
            templateUrl: '/lib/fc/layout/ranking/rankingTable.html'
        };

        function link(scope, element, attrs) {

            scope.loadingRT = true;
            scope.loadingImage = helper.getLoadingImg();

            scope.$watch(function (scope) {
                return scope.tourneyId;
            },
            function (newValue, oldValue) {
                if (angular.isNumber(newValue) || newValue > 0) {
                    loadData();
                }
            });

            function loadData() {
                rankingsSrv.loadRankingTable(scope.tourneyId, rankingLoaded);
            }

            function rankingLoaded(response) {
                scope.ranking = response.data;
                scope.loadingRT = false;
                scope.hasData = angular.isObject(scope.ranking);
            }
        }
    }

})();