(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('hotPublications', hotPublications);

    hotPublications.$inject = ['helper', '$interval'];

    function hotPublications(helper, $interval) {
        return {
            restrict: 'E',
            replace: true,
            scope:
                {
                    model: '='
                },
            link: function link(scope, element, attrs) {                
                scope.loading = true;
                scope.loadingImage = helper.getLoadingImg();

                // watch initialId
                scope.$watch("model",
                    function (newValue) {
                        if (angular.isArray(newValue)) {
                            initJQ(newValue);
                        }
                });

                function initJQ(newValue) {
                    scope.loading = false;

                    var sliderArea = element.find("#slider");

                    sliderArea.append("<br/><p>hoook!!!</p>")

                    for (var i = 0; i < newValue.length; i++) {
                        var item = newValue[i];
                        sliderArea.append("<img src='" + item.img + "' alt='" + item.title + "' data-caption='#caption-" + i + "'>");
                        element.append("<div id='caption-" + i + "' style='display:none;'>" +
                                        "<a href='publication/" + item.urlKey + "'>" + item.title + "<small><br />" +
                                         item.header + "</small></a>" +
                                       "</div>");
                    }

                    var slider = new IdealImageSlider.Slider('#slider');
                    slider.addCaptions();
                    slider.start();
                    console.log('inited');
                }
            },
            templateUrl: '/lib/fc/layout/publications/hotPublications.html'
        }
    }
})();