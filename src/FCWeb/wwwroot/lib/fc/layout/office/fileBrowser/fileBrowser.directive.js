(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('fileBrowser', fileBrowser);

    fileBrowser.$inject = ['$routeParams', 'apiSrv', 'notificationManager', '$httpParamSerializer'];

    function fileBrowser($routeParams, apiSrv, notificationManager, $httpParamSerializer) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                model: '='
            },

            link: function link(scope, element, attrs) {

                if (!angular.isDefined(scope.model)) {
                    notificationManager.displayError('File browser model is not defined! ("fileBrowser")');
                }

                scope.model.allowRemove = angular.isDefined(scope.model.allowRemove)
                    ? scope.model.allowRemove
                    : false

                if (scope.model.allowRemove) {
                    scope.canSelect = true;
                    scope.canRemove = true;
                }

                scope.directoryView = {};

                scope.contentUpload = {
                    multiple: scope.model.multiple,
                    onUploadSuccess: function (uploadData) {
                        loadData(scope.directoryView.path, scope.model.root);
                    }
                };
                
                scope.$watch(function (scope) {
                    return scope.model.path;
                },
                function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        console.log("Path was changed to: " + newValue);
                        scope.openFolder(newValue);
                    }
                });

                scope.selectedFile = scope.initialItem;

                scope.selectFile = function selectFile(file) {
                    scope.selectedFile = file;
                }

                scope.openFolder = function openFolder(path) {
                    loadData(path, scope.model.root, scope.model.options);
                }

                // upload later on form submit or something similar
                scope.submit = function () {
                    if (scope.form.file.$valid && scope.file) {
                        scope.upload(scope.file);
                    }
                };

                scope.ok = function () {
                    if (angular.isFunction(scope.model.onOk)) {
                        scope.model.onOk(scope.selectedFile);
                    }
                };

                scope.cancel = function () {
                    if (angular.isFunction(scope.model.onCancel)) {
                        scope.model.onCancel();
                    }
                };

                scope.remove = function () {
                    if (!scope.model.allowRemove) {
                        return;
                    }

                    var remFiles = [];

                    for (var i = 0; i < scope.directoryView.files.length; i++) {
                        if (scope.directoryView.files[i].isChecked) {
                            remFiles.push(scope.directoryView.files[i]);
                            if (scope.selectedFile == scope.directoryView.files[i]) {
                                scope.selectedFile = null;
                            }
                        }
                    }

                    if (remFiles.length == 0) {
                        return;
                    }

                    var postForms = $httpParamSerializer({ data: angular.toJson(remFiles) });
                    var config = { headers: { "Content-Type": "application/x-www-form-urlencoded" } };

                    console.log("Removing is started...");

                    apiSrv.post('/api/filebrowser/remove', postForms, config, removeSuccess, failure);
                }

                function loadData(path, root, options) {
                    if (!(angular.isString(path) && angular.isString(root) && path.length > 0 && root.length > 0)) {
                        return;
                    }

                    console.log("Loading data for: " + path);

                    var createNewParam = angular.isObject(options) && options.createNew === true
                        ? '&createNew=true'
                        : '';

                    apiSrv.get('/api/filebrowser?path=' + path + '&root=' + root + createNewParam, null, loadSuccess, failure);
                }

                function loadSuccess(response) {
                    console.log("Data was loaded succesfully for: " + response.data.path);

                    scope.directoryView = response.data;
                    scope.contentUpload.path = response.data.path;                    
                }

                function removeSuccess(response) {
                    console.log("Files was removed successfully...");

                    loadData(scope.model.path, scope.model.root, scope.model.options);
                }

                function failure(response) {
                    notificationManager.displayError(response.data);
                }

                loadData(scope.model.path, scope.model.root, scope.model.options);

            },
            templateUrl: '/lib/fc/layout/office/fileBrowser/fileBrowser.html'
        }
    }
})();