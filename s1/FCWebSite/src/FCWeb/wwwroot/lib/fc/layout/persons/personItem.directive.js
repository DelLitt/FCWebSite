(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('personItem', personItem);

    personItem.$inject = ['personsSrv'];

    function personItem(personsSrv) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                person: '='
            },
            link: function link(scope, element, attrs) {
                var imageUploadData = personsSrv.getImageUploadData(scope.person);
                scope.image = imageUploadData.path + '/' + scope.person.image;
            },
            templateUrl: '/lib/fc/layout/persons/personItem.html'
        }
    }
})();