(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('fcAutocomplete', fcAutocomplete);

    fcAutocomplete.$inject = ['apiSrv'];
    
    function fcAutocomplete(apiSrv) {

        return {
            restrict: 'E',
            scope: {
                urlsearch: '@',                
                key: '@',
                val: '@',
                min: '@',
                removeselected: '@',
                urlshowall: '@',
                urlinit: '=',
                inputdata: '=',
                id: '=',
                text: '=',
                addglobalescapeevent: '='
                //onselect: '&'
            },

            link: function link(scope, element, attrs) {
                var min = angular.isString(scope.min) ? parseInt(scope.min) : 1;

                if (min <= 0) {
                    min = 1;
                }

                scope.current = 0;
                scope.selected = false;

                // watch initialId
                scope.$watch(function (scope) {
                    return scope.urlinit;
                },
                function (newValue, oldValue) {
                    if (angular.isString(newValue) && newValue.length > 0) {
                        init(newValue);
                    }
                });

                scope.onRootClick = function () {
                    cancel();
                }

                if (angular.isFunction(scope.addglobalescapeevent)) {
                    scope.addglobalescapeevent(scope.onRootClick);
                }

                var isRemoveSelected = angular.isString(scope.removeselected)
                    ? scope.removeselected.toLowerCase() === "true"
                    : false;

                scope.isuseshowall = angular.isString(scope.urlshowall) && scope.urlshowall.length > 0; 

                if (!angular.isArray(scope.isuseshowall)) {
                    scope.inputdata = [];
                }

                scope.showAll = function () {

                    if (!scope.isuseshowall) {
                        return;
                    }

                    apiSrv.get(scope.urlshowall,
                        null,
                        function (response) {
                            scope.suggestedData = response.data;
                            scope.aclass = 'form-group';
                        },
                        function (response) {
                            console.log('Error autocomplete show all!');
                        });
                }

                scope.search = function (event) {

                    if (event.keyCode == 27) {
                        cancel();
                        return;
                    }

                    if (!(angular.isString(scope.text) && scope.text.length >= min)) {
                        scope.suggestedData = [];
                        scope.selItem = null;
                        scope.aclass = 'form-group';
                        return;
                    }

                    scope.suggestedData = [];

                    if (angular.isString(scope.urlsearch)) {
                        //var url = scope.urlsearch.endsWith('/') ? scope.urlsearch : scope.urlsearch + '/';
                        var url = scope.urlsearch + '?txt=' + encodeURIComponent(scope.text)

                        apiSrv.get(url,
                            null,
                            function (response) {
                                scope.suggestedData = response.data;
                                scope.selItem = null;
                                scope.aclass = 'form-group';
                            },
                            function (response) {
                                console.log('Error autocomplete search!');
                            });
                    } else {
                        if (angular.isArray(scope.inputdata)) {
                            angular.forEach(scope.inputdata, function (value, key) {
                                if (value[this.val].indexOf(this.text) !== -1) {
                                    this.suggestedData.push(value);
                                }
                            }, scope);
                        }
                    }
                }

                var init = function (urlinit) {
                    if (angular.isString(urlinit)) {
                        apiSrv.get(urlinit,
                            null,
                            function (response) {
                                scope.select(response.data);
                            },
                            function (response) {
                                console.log('Error init autocomlete!');
                            });
                    }
                }

                var cancel = function () {
                    scope.suggestedData = [];

                    if (!angular.isObject(scope.selItem)) {
                        scope.text = '';
                        scope.aclass = 'form-group';
                    }
                }

                scope.select = function (item) {
                    scope.id = item[scope.key];
                    scope.text = item[scope.val];
                    scope.current = null;
                    scope.selected = true;
                    scope.selItem = item;

                    if (isRemoveSelected) {
                        var index = scope.inputdata.indexOf(item);
                        if (index >= 0) {
                            scope.inputdata.splice(index, 1);
                        }
                    }

                    if (angular.isFunction(scope.onselect)) {
                        scope.onselect(scope.id, scope.text);
                    }

                    scope.aclass = 'form-group has-success has-feedback';
                    scope.suggestedData = [];
                }

                scope.isCurrent = function (item) {
                    return angular.isObject(scope.current)
                        ? scope.current[scope.key] == item[scope.key]
                        : false;
                }

                scope.setCurrent = function (item) {
                    scope.current = item;
                }
            },

            template: '<div ng-class="aclass">' +
                        '<div class="input-group" style="width:100%;">' +
                            '<span class="input-group-addon" ng-show="isuseshowall">' +
                                '<span style="cursor: pointer;" class="glyphicon glyphicon-menu-hamburger" title="Показать все" ng-click="showAll();"></span>' +
                             '</span>' +
                            '<input type="text" ng-model="text" ng-keyup="search($event);" ng-click="click();"' +
                                'ng-keydown="selected=false;" style="width:100%;" class="form-control">' +
                            '</input>' +
                            '<span ng-show="selItem" class="glyphicon glyphicon-ok form-control-feedback"></span>' +
                        '</div>' +
                        '<div class="list-group table-condensed overlap" ng-hide="!suggestedData.length" style="width:100%">' +
                            '<a href="javascript:void();" class="list-group-item noTopBottomPad" ng-repeat="item in suggestedData|filter:model track by $index" ' +
                               'ng-click="select(item)" style="cursor:pointer" ' +
                               'ng-class="{active:isCurrent(item)}" ' +
                               'ng-mouseenter="setCurrent(item)">' +
                                 ' {{item[val]}}<br />' +
                                 '<i>{{item[key]}} </i>' +
                            '</a> ' +
                        '</div>' +
                      '</div>'
        };
    }

})();