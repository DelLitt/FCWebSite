(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('autocompleteCustom', autocompleteCustom);

    autocompleteCustom.$inject = ['apiSrv'];
    
    function autocompleteCustom(apiSrv) {
        return {
            restrict: 'E',
            scope: {
                url: '@',
                minsymbols: '@',
                title: '@',
                retkey: '@',
                displaykey: '@',
                modeldisplay: '=',
                subtitle: '@',
                modelret: '=',
                changed: '='
            },

            link: function (scope, elem, attrs) {
                scope.current = 0;
                scope.selected = false;

                // watch initialId
                scope.$watch(function (scope) {
                    return scope.modelret;
                },
                function (newValue, oldValue) {
                    if (oldValue == 0 && newValue != oldValue) {
                        init(newValue);
                    }
                });

                scope.search = function (txt) {

                    if (angular.isString(txt)
                        && angular.isDefined(scope.minsymbols)
                        && txt.length + 1 >= parseInt(scope.minsymbols)) {

                        scope.ajaxClass = 'loadImage';

                        var getUrl = scope.url.endsWith('/') ? scope.url : scope.url + '/';

                        apiSrv.get(getUrl + 'search/' + txt,
                            null,
                            function (response) {
                                scope.TypeAheadData = response.data;
                                scope.ajaxClass = '';
                            },
                            function (response) {
                                alert('error');
                            });
                    }
                }

                function init(id) {

                    if (angular.isNumber(id)) {

                        scope.ajaxClass = 'loadImage';

                        var getUrl = scope.url.endsWith('\\') ? scope.url : scope.url + '\\';

                        apiSrv.get(getUrl + id,
                            null,
                            function (response) {
                                scope.ajaxClass = '';
                                scope.handleSelection(response.data[scope.retkey], response.data[scope.displaykey]);
                            },
                            function (response) {
                                alert('error');
                            });
                    }
                }

                scope.handleSelection = function (key, val) {
                    scope.modelret = key;
                    scope.modeldisplay = val;
                    scope.current = 0;
                    scope.selected = true;

                    selectionChanged(key, val);
                }

                scope.isCurrent = function (index) {
                    return scope.current == index;
                }

                scope.setCurrent = function (index) {
                    scope.current = index;
                }

                scope.showActions = function () {
                    scope.TypeAheadData = [
                        { id: -1, title: "Не установлено" },
                        { id: 0, title: "Новое видео" }
                    ];

                    scope.selected = false;
                    scope.ajaxClass = '';
                }

                function selectionChanged(key, val) {
                    if (angular.isFunction(scope.changed)) {
                        scope.changed(key, val);
                    }
                }
            },
            template: '<input type="text" ng-model="modeldisplay" ng-KeyPress="search(modeldisplay);" ng-click="showActions();"' +
                        'ng-keydown="selected=false;" style="width:100%;" ng-class="ajaxClass">' +
                        '</input>' + 
                        '<div class="list-group table-condensed overlap" ng-hide="!modeldisplay.length || selected" style="width:100%">' +
                            '<a href="javascript:void();" class="list-group-item noTopBottomPad" ng-repeat="item in TypeAheadData|filter:model  track by $index" ' +
                               'ng-click="handleSelection(item[retkey],item[displaykey])" style="cursor:pointer" ' +
                               'ng-class="{active:isCurrent($index)}" ' +
                               'ng-mouseenter="setCurrent($index)">' +
                                 ' {{item[title]}}<br />' +
                                 '<i>{{item[subtitle]}} </i>' +
                            '</a> ' +
                        '</div>'
        }
    }
})();