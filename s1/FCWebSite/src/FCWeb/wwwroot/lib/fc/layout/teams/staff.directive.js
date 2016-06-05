(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('staff', staff);

    staff.$inject = ['personsSrv', 'configSrv', 'helper', 'filterFilter', 'publicationsSrv'];

    function staff(personsSrv, configSrv, helper, filterFilter, publicationsSrv) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                teamId: '=',
                publicationsCount: '=',
                title: '=',
                persons: '='
            },
            link: function link(scope, element, attrs) {

                scope.loadingStaff = true;
                scope.loadingNews = true;
                scope.loadingImage = helper.getLoadingImg();
                scope.personsLoaded = false;

                scope.rows = {};

                scope.$watch(function (scope) {
                    return scope.persons;
                },
                function (newValue, oldValue) {
                    if (angular.isObject(scope.persons)) {
                        staffLoaded();
                    }
                });

                loadData();

                function loadData() {                    
                    publicationsSrv.loadLatestPublications(scope.publicationsCount, publicationsLoaded);
                }

                function staffLoaded() {
                    angular.forEach(scope.persons, function (item) {
                        item.flagSrc = helper.getFlagSrc(item.city.countryId);

                        var imageUploadData = personsSrv.getImageUploadData(item);
                        item.src = helper.getPersonImage(item.image, imageUploadData);

                        item.showRole = angular.isObject(item.role);
                    });

                    scope.rows = scope.persons.length > 0 ? helper.formRows(scope.persons, 4, 0) : [];
                    scope.loadingStaff = false;
                }

                function publicationsLoaded(response) {
                    var publications = response.data;
                    scope.publications = publications;
                    scope.loadingNews = false;
                }
            },
            templateUrl: '/lib/fc/layout/teams/staff.html'
        }
    }
})();