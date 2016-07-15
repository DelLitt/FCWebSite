(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('videosListCtrl', videosListCtrl);

    videosListCtrl.$inject = ['$scope', 'videosSrv', 'helper'];

    function videosListCtrl($scope, videosSrv, helper) {

        $scope.loading = true;
        $scope.loadingImage = helper.getLoadingImg();
        $scope.videos = [];
        $scope.text = '';
        $scope.search = search;

        init();

        function init() {
            videosSrv.loadNotFilteredVideos(50, 0, videosLoaded);
        }

        function videosLoaded(response) {
            var videos = response.data;

            $scope.videos = videos;
            $scope.loading = false;
        }

        function search(event) {

            if (event.keyCode == 27) {
                $scope.text = '';
                init();
                return;
            }

            if ($scope.text.length && $scope.text.length >= 3) {
                videosSrv.search($scope.text, videosLoaded);
                return;
            }
            else if ($scope.text.length && $scope.text.length < 3) {
                init();
            }
        }
    }
})();
