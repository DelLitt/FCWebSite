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
                title: '='
            },
            link: function link(scope, element, attrs) {

                scope.loadingStaff = true;
                scope.loadingNews = true;
                scope.loadingImage = helper.getLoadingImg();
                scope.personsLoaded = false;

                scope.persons = {};
                scope.rows = {};

                loadData();

                function loadData() {                    
                    personsSrv.loadCoachesStaff(scope.teamId, staffLoaded);
                    publicationsSrv.loadLatestPublications(scope.publicationsCount, publicationsLoaded);
                }

                function staffLoaded(response) {
                    var persons = response.data;

                    scope.persons = persons;

                    angular.forEach(scope.persons, function (item) {
                        item.flagSrc = helper.getFlagSrc(item.city.countryId);

                        var imageUploadData = personsSrv.getImageUploadData(item);
                        item.src = helper.getPersonImage(item.image, imageUploadData);

                        item.showRole = angular.isObject(item.role);
                    });

                    scope.rows = persons.length > 0 ? helper.formRows(persons, 4, 0) : [];
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