﻿String.prototype.translit = (function () {
    var L = {
        'А': 'A', 'а': 'a', 'Б': 'B', 'б': 'b', 'В': 'V', 'в': 'v', 'Г': 'G', 'г': 'g',
        'Д': 'D', 'д': 'd', 'Е': 'E', 'е': 'e', 'Ё': 'Yo', 'ё': 'yo', 'Ж': 'Zh', 'ж': 'zh',
        'З': 'Z', 'з': 'z', 'И': 'I', 'и': 'i', 'Й': 'Y', 'й': 'y', 'К': 'K', 'к': 'k',
        'Л': 'L', 'л': 'l', 'М': 'M', 'м': 'm', 'Н': 'N', 'н': 'n', 'О': 'O', 'о': 'o',
        'П': 'P', 'п': 'p', 'Р': 'R', 'р': 'r', 'С': 'S', 'с': 's', 'Т': 'T', 'т': 't',
        'У': 'U', 'у': 'u', 'Ф': 'F', 'ф': 'f', 'Х': 'Kh', 'х': 'kh', 'Ц': 'Ts', 'ц': 'ts',
        'Ч': 'Ch', 'ч': 'ch', 'Ш': 'Sh', 'ш': 'sh', 'Щ': 'Sch', 'щ': 'sch', 'Ъ': '"', 'ъ': '"',
        'Ы': 'Y', 'ы': 'y', 'Ь': "'", 'ь': "'", 'Э': 'E', 'э': 'e', 'Ю': 'Yu', 'ю': 'yu',
        'Я': 'Ya', 'я': 'ya'
    },
        r = '',
        k;
    for (k in L) r += k;
    r = new RegExp('[' + r + ']', 'g');
    k = function (a) {
        return a in L ? L[a] : '';
    };
    return function () {
        return this.replace(r, k);
    };
})();

String.prototype.endsWith = function (suffix) {
    return this.indexOf(suffix, this.length - suffix.length) !== -1;
};

(function (module) {
    'use strict';

    module.factory('helper', ['configSrv', function (configSrv) {
        var thisIsPrivate = "Private";

        return {

            getRandom: function(min, max) {
                return Math.floor(Math.random() * (max - min) + min);
            },

            formRows: function(array, elementsPerRow, skip) {

                if(!angular.isArray(array) || array.length == 0) {
                    return [];
                }

                var firstIndex = 0,
                    lastIndex = 0,
                    row = [],
                    rows = [],
                    rowsCount = Math.ceil((array.length) / elementsPerRow),
                    offset = angular.isNumber(skip) ? skip : 0;

                for (var i = 0; i < rowsCount; i++) {

                    firstIndex = i * elementsPerRow + offset;
                    lastIndex = Math.min(firstIndex + elementsPerRow, array.length);
                    row = array.slice(firstIndex, lastIndex);

                    rows.push(row);
                }

            return rows;
            },

            createUrlKey: function (value) {

                if (!angular.isString(value)) {
                    return "";
                }

                var d = new Date();
                var curr_date = ("0" + d.getDate()).slice(-2);
                var curr_month = ("0" + (d.getMonth() + 1)).slice(-2);
                var curr_year = d.getFullYear();

                var strDate = curr_year + "-" + curr_month + "-" + curr_date;


                var result = value.translit() + "-" + strDate;

                return result.replace(/\s/g, "-");
            },

            locLabels : {
                position: function (roleId) {
                    return configSrv.PersonGroups.Player.indexOf(roleId) >= 0 ? "POSITION" : "POST";
                }
            },

            getPersonImage: function (image, imageUploadData) {
                return angular.isString(image) && image.length > 0
                    ? imageUploadData.path + '/' + image
                    : this.getPersonEmptyImage();
            },

            getFlagSrc: function(countryId) {
                return 'images/skin/flags/' + countryId + '.png';
            },

            getPersonEmptyImage: function() {
                return 'images/skin/empty/EmptyPersonImage.png';
            },

            getLoadingImg: function () {
                return 'images/skin/loading.gif';
            },

            remTZOffset: function (date) {
                return new Date(date.getTime() + (new Date()).getTimezoneOffset() * 60000)
            },

            addTZOffset: function (date) {
                return new Date(date.getTime() - (new Date()).getTimezoneOffset() * 60000)
            },

            getPrivate: thisIsPrivate,

            // TEAM HELPER START
            // TODO: Encapsulate to separate class helper

            getTeamImage: function (team) {
                if (angular.isString(team.image) && team.image.length > 0) {
                    return this.getTeamImageFolder(team.id) + "/" + team.image;
                } else {
                    return this.getTeamEmptyImage();
                }
            },

            getTeamFakeInfoImage: function (team) {
                if (hasTeamExtendedInfo(team)
                        && angular.isString(team.descriptionData.fakeInfo.image)
                        && team.descriptionData.fakeInfo.image.length > 0) {
                    return this.getTeamImageFolder(team.id) + "/" + team.descriptionData.fakeInfo.image;
                } else {
                    return this.getTeamFakeEmptyImage();
                }
            },

            getTeamImageFolder: function (id) {
                return configSrv.Current.Images.Teams.replace("{id}", id);
            },

            getTeamViewLink: function (team) {
                return '/team/' + team.id;
            },

            getTeamDescription: function(team) {
                return angular.isObject(team.descriptionData) ? team.descriptionData.description : "";
            },

            hasTeamExtendedInfo: function (team) {
                return angular.isObject(team.descriptionData) && angular.isObject(team.descriptionData.fakeInfo);
            },

            getTeamEmptyImage: function () {
                return 'images/skin/empty/EmptyLogoImage.png';
            },

            getTeamFakeEmptyImage: function () {
                return 'images/skin/empty/EmptyPreview.png';
            },

            // TEAM HELPER END
        };
    }]);

})(angular.module('fc.core'));