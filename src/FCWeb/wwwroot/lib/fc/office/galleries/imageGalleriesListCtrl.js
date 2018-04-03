(function () {
    'use strict';

    angular
        .module('fc.admin')
        .controller('imageGalleriesListCtrl', imageGalleriesListCtrl);

    imageGalleriesListCtrl.$inject = ['$scope', 'imageGallerySrv', 'helper'];

    function imageGalleriesListCtrl($scope, imageGallerySrv, helper) {

        $scope.loading = true;
        $scope.loadingImage = helper.getLoadingImg();
        $scope.galleries = [];
        $scope.text = '';
        $scope.search = search;

        init();

        function init() {
            imageGallerySrv.loadNotFilteredGalleries(50, 0, galleriesLoaded);
        }        

        function galleriesLoaded(response) {
            var galleries = response.data;

            $scope.galleries = galleries;
            $scope.loading = false;
        }

        function search(event) {

            if (event.keyCode == 27) {
                $scope.text = '';
                init();
                return;
            }

            if ($scope.text.length && $scope.text.length >= 3) {
                imageGallerySrv.search($scope.text, galleriesLoaded);
                return;
            }
            else if ($scope.text.length && $scope.text.length < 3) {
                init();
            }            
        }
    }
})();
