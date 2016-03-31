(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('customOnChange', customOnChange);

    function customOnChange() {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var onChangeFunc = scope.$eval(attrs.customOnChange);
                element.bind('change', onChangeFunc);
            }
        };
    }
})();