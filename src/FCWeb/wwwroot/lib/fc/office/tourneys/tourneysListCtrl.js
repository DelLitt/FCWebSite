(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('tourneysListCtrl', tourneysListCtrl);

    tourneysListCtrl.$inject = ['$scope', 'tourneysSrv', 'helper', '$uibModal', 'notificationManager'];

    function tourneysListCtrl($scope, tourneysSrv, helper, $uibModal, notificationManager) {

        $scope.loading = true;
        $scope.loadingImage = helper.getLoadingImg();
        $scope.tourneys = [];
        $scope.text = '';
        $scope.search = search;

        init();

        function init() {
            tourneysSrv.loadAllTourneys(tourneysLoaded);
        }

        function tourneysLoaded(response) {
            var tourneys = response.data;

            $scope.tourneys = tourneys;
            $scope.loading = false;
        }

        $scope.editTourney = function (tourney) {
            var lScope = $scope;

            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'lib/fc/layout/office/quickTourneyEdit.html',
                controller: 'quickTourneyEditCtrl',
                //size: size,
                resolve: {
                    tourney: tourney,
                }
            });

            modalInstance.result.then(function (savedTourney) {

                if (angular.isObject(tourney) && tourney.id > 0) {
                    for (var i = 0; i < lScope.tourneys.length; i++) {
                        if (lScope.tourneys[i].id == savedTourney.id) {
                            lScope.tourneys[i] = savedTourney;
                        }
                    }
                } else {
                    lScope.tourneys.unshift(savedTourney);
                }
            }, function () {
                notificationManager.displayInfo('Modal dismissed at: ' + new Date());
            });
        }

        var lastLength = 0;
        var length = 0;

        function search(event) {

            if (event.keyCode == 27) {
                $scope.text = '';
                init();
                lastLength = length = 0;
                return;
            }

            length = angular.isNumber($scope.text.length) ? $scope.text.length : 0;

            if (length >= 3) {
                tourneysSrv.search($scope.text, tourneysLoaded);
            }
            else if (lastLength > $scope.text.length) {
                init();
            }

            lastLength = length;
        }
    }
})();
