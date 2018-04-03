(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('quickTourneyEditCtrl', quickTourneyEditCtrl);

    quickTourneyEditCtrl.$inject = ['$scope', '$uibModalInstance', 'tourneysSrv', 'configSrv', 'notificationManager', 'tourney'];

    function quickTourneyEditCtrl($scope, $uibModalInstance, tourneysSrv, configSrv, notificationManager, tourney) {

        $scope.dateOptions = {
            showWeeks: false
        };

        if (angular.isObject(tourney)) {
            $scope.tourney = angular.copy(tourney);
            $scope.tourney.dateStart = angular.isDate(tourney.dateStart)
                ? tourney.dateStart
                : new Date(tourney.dateStart);
            $scope.tourney.dateEnd = angular.isDate(tourney.dateEnd)
                ? tourney.dateEnd
                : new Date(tourney.dateEnd);
        } else {
            var now = new Date();
            $scope.tourney = angular.copy(configSrv.Current.EmptyTourney);
            $scope.tourney.tourneyTypeId = 1;
            $scope.tourney.dateStart = now;
            $scope.tourney.dateEnd = now;
        }

        $scope.cityInitUrl = angular.isNumber($scope.tourney.cityId)
            ? "/api/cities/" + $scope.tourney.cityId
            : null;

        $scope.ok = function () {
            tourneysSrv.saveTourney($scope.tourney.id, $scope.tourney, tourneySaved);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        function tourneySaved(response) {
            var savedTourney = response.data;
            notificationManager.displayInfo('Tournament (ID: ' + savedTourney.id + ') was saved successfully!');

            $uibModalInstance.close(savedTourney);
        }
    }
})();
