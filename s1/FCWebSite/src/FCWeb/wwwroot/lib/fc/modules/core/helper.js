(function (module) {
    'use strict';

    module.factory('helper', function () {
        var thisIsPrivate = "Private";

        return {

            getRandom: function(min, max) {
                return Math.floor(Math.random() * (max - min) + min);
            },

            formRows: function(array, elementsPerRow, skip) {

                if(!angular.isArray(array) || array.length == 0) {
                    return [];
                }

                var rows = [],
                    rowsCount = Math.ceil((array.length - 1) / elementsPerRow),
                    offset = angular.isNumber(skip) ? skip : 0;

                for (var i = 0; i < rowsCount; i++) {

                    var firstIndex = i * elementsPerRow + offset,
                        lastIndex = Math.min(firstIndex + elementsPerRow, array.length),
                        row = {
                            items: array.slice(firstIndex, lastIndex)
                        };

                    rows.push(row);
                }

            return rows;
        },

            getPrivate: thisIsPrivate
        };
    });

})(angular.module('fc.core'));