(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('publictionsTextList', publictionsTextList);

    publictionsTextList.$inject = ['publicationsSrv', 'configSrv', 'helper'];

    function publictionsTextList(publicationsSrv, configSrv, helper) {
        return {
            restrict: 'E',
            replace: true,
            scope:
                {
                    typeFilter: "="
                },

            link: function link(scope, element, attrs) {

                scope.loadingImage = helper.getLoadingImg();
                scope.loadingTL = true;

                scope.publications = {
                    items: [],
                    loading: true,
                    count: 0,
                    more: function () {
                        publicationsSrv.loadPublicationsPack(configSrv.Current.TextPublicationsDefaultMoreCount, this.items.length, scope.typeFilter, publicationsLoaded);
                    }
                };

                loadData();

                function loadData(urlKey) {
                    publicationsSrv.loadPublicationsPack(configSrv.Current.TextPublicationsDefaultCount, 0, scope.typeFilter, publicationsLoaded);
                }

                function publicationsLoaded(response) {
                    var publications = response.data;

                    if (!angular.isArray(publications) && publications.length == 0) {
                        return;
                    }

                    for (var i = 0; i < publications.length; i++) {
                        scope.publications.items.push(publications[i]);
                    }

                    scope.loadingTL = false;
                }
            },
            templateUrl: '/lib/fc/layout/publications/publictionsTextList.html'
        }
    }
})();