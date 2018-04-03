(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('mainTable', mainTable);

    mainTable.$inject = ['helper'];

    function mainTable(helper) {
        return {
            restrict: 'E',
            replace: true,
            scope: { model: '=' },
            link: function link(scope, element, attrs) {
                scope.getTeamViewLink = helper.getTeamViewLink;
            },
            templateUrl: '/lib/fc/layout/mainTable.html'
        }
    }
})();