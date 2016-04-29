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
                urlinit: '@',
                key: '@',
                val: '@',
                min: '@',
                inputdata: '=',
                id: '=',
                text: '=',
                onselect: '&',
                removeselected: '@'
            },

            link: function link(scope, element, attrs) {
                var min = angular.isString(scope.min) ? parseInt(scope.min) : 1;

                if (min <= 0) {
                    min = 1;
                }

                scope.current = 0;
                scope.selected = false;

                scope.removeselected = angular.isString(scope.removeselected)
                    ? scope.removeselected.toLowerCase() === "true"
                    : false;

                if (!angular.isArray(scope.inputdata)) {
                    scope.inputdata = [];
                }

                //scope.inputdata = [{ "theKey": "The key 1", "theVal": "The val 1" }, { "theKey": "The key 2", "theVal": "The val 2" }]

                scope.search = function (event) {

                    if (event.keyCode == 27) {
                        scope.suggestedData = [];

                        if (!angular.isObject(scope.selItem)) {
                            scope.text = '';
                            scope.aclass = 'form-group';
                        }

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
                                    alert('error');
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

                scope.select = function (item) {
                    scope.id = item[scope.key];
                    scope.text = item[scope.val];
                    scope.current = null;
                    scope.selected = true;
                    scope.selItem = item;

                    if (scope.removeselected) {
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

            template: '<div ng-class="aclass"> <input type="text" ng-model="text" ng-keyup="search($event);" ng-click="click();"' +
                        'ng-keydown="selected=false;" style="width:100%;" class="form-control">' +
                      '</input>' +
                      '<span class="glyphicon glyphicon-ok form-control-feedback"></span>' +
                        '<div class="list-group table-condensed overlap" ng-hide="!suggestedData.length" style="width:100%">' +
                            '<a href="javascript:void();" class="list-group-item noTopBottomPad" ng-repeat="item in suggestedData|filter:model track by $index" ' +
                               'ng-click="select(item)" style="cursor:pointer" ' +
                               'ng-class="{active:isCurrent(item)}" ' +
                               'ng-mouseenter="setCurrent(item)">' +
                                 ' {{item[val]}}<br />' +
                                 '<i>{{item[key]}} </i>' +
                            '</a> ' +
                        '</div> </div>'
        };
    }

})();