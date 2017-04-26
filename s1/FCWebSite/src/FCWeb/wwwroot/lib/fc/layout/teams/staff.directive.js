(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('staff', staff);

    staff.$inject = ['personsSrv', 'configSrv', 'helper', 'filterFilter'];

    function staff(personsSrv, configSrv, helper, filterFilter) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                teamId: '=',
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
                    if (angular.isArray(newValue) && newValue.length > 0) {
                        staffLoaded(newValue);
                    }
                });

                function staffLoaded(persons) {
                    angular.forEach(persons, function (item) {
                        item.flagSrc = helper.getFlagSrc(item.city.countryId);

                        var imageUploadData = personsSrv.getImageUploadData(item);
                        item.src = helper.getPersonImage(item.image, imageUploadData);

                        item.showRole = angular.isObject(item.role);
                    });

                    scope.rows = persons.length > 0 ? helper.formRows(persons, 4, 0) : [];
                    scope.loadingStaff = false;
                }
            },
            templateUrl: '/lib/fc/layout/teams/staff.html'
        }
    }
})();