(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('imageGallery', imageGallery);

    imageGallery.$inject = ['imageGallerySrv', 'helper', '$sce'];

    function imageGallery(imageGallerySrv, helper, $sce) {
        return {
            restrict: 'E',
            scope: {
                galleryId: '=',
                imgVariant: '@',
                thmbVariant: '@'
            },
            link: function link(scope, element, attrs) {

                //if(!angular.isNumber(scope.galleryId)) {
                //    scope.galleryId = 0;
                //}

                // watch initialId
                scope.$watch(function (scope) {
                    return scope.galleryId;
                },
                function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        init();
                    }
                });

                function init() {
                    if (scope.galleryId > 0) {
                        imageGallerySrv.loadGallery(scope.galleryId, galleryLoaded);
                    }
                }

                function galleryLoaded(response) {
                    var gallery = response.data;

                    scope.gallery = gallery;
                    scope.gallery.dateDisplayed = new Date(gallery.dateDisplayed);
                    scope.gallery.dateChanged = new Date(gallery.dateChanged);
                    scope.gallery.dateCreated = new Date(gallery.dateCreated);
                    scope.gallery.description = $sce.trustAsHtml(gallery.description);

                    var images = [];

                    for (var i = 0; i < scope.gallery.images.length; i++) {
                        images.push({
                            url: helper.addFileVariant(scope.gallery.images[i].url, scope.imgVariant),
                            thumbUrl: helper.addFileVariant(scope.gallery.images[i].url, scope.thmbVariant)
                        });
                    }

                    scope.images = images;
                }

                //// gallery methods
                //scope.methods = {};

                //// so you will bind openGallery method to a button on page
                //// to open this gallery like ng-click="openGallery();"
                //scope.openGallery = function () {
                //    $scope.methods.open();

                //    // You can also open gallery model with visible image index
                //    // Image at that index will be shown when gallery modal opens
                //    //scope.methods.open(index); 
                //};

                //// Similar to above function
                //scope.closeGallery = function () {
                //    $scope.methods.close();
                //};

                //scope.nextImg = function () {
                //    $scope.methods.next();
                //};

                //scope.prevImg = function () {
                //    $scope.methods.prev();
                //};
            },
            template: '<ng-image-gallery images="images" methods="methods" thumbnails="true" inline="false" on-open="opened();" on-close="closed();"></ng-image-gallery>'
        }
    }
})();