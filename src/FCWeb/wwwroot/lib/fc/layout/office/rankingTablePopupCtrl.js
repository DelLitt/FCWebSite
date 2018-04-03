(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('rankingTablePopupCtrl', rankingTablePopupCtrl);

    rankingTablePopupCtrl.$inject = ['$scope', '$uibModalInstance', 'notificationManager', 'rankingsSrv', 'rankingModel'];

    function rankingTablePopupCtrl($scope, $uibModalInstance, notificationManager, rankingsSrv, rankingModel) {

        if (!angular.isObject(rankingModel)) {
            notificationManager.displayError("Unable to load ranking table!");
            $uibModalInstance.dismiss('cancel');
        }

        if (rankingModel.changed) {
            rankingsSrv.updateRankingTable(rankingModel.tourneyId, rankingLoaded);
        } else {
            rankingsSrv.loadRankingTable(rankingModel.tourneyId, rankingLoaded);
        }        

        function rankingLoaded(response) {
            $scope.ranking = response.data;
        }

        $scope.ok = function () {
            $uibModalInstance.close('ok');
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
})();
