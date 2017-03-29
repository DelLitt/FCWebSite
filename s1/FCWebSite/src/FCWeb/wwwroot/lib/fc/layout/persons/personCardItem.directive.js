(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('personCardItem', personCardItem);

    personCardItem.$inject = ['personsSrv', 'helper'];

    function personCardItem(personsSrv, helper) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                person: '=',
                imgVariant: '@'
            },
            link: function link(scope, element, attrs) {
                var imageUploadData = personsSrv.getImageUploadData(scope.person);
                var image = helper.getPersonImage(scope.person.image, imageUploadData);

                scope.image = helper.addFileVariant(image, scope.imgVariant);
            },
            templateUrl: '/lib/fc/layout/persons/personCardItem.html'
        }
    }
})();