(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('personCardItem', personCardItem);

    personCardItem.$inject = ['personsSrv'];

    function personCardItem(personsSrv) {
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
            templateUrl: '/lib/fc/layout/persons/personCardItem.html'
        }
    }
})();