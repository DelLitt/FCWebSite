String.prototype.translit = (function () {
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
        var availableGoalsInfo = ['eGoalPenalty', 'eGoalAuto'];
        var availableEventsInfo = ['eYellowRoughing', 'eYellowDangerous', 'eYellowHanding', 'eYellowUnsport', 'eRedDoubleYellow', 'eRedRoughing', 'eRedLastResort', 'eRedUnsport', 'eRedKeeperHandOfSquad'];

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
                    return configSrv.Current.PersonGroups.Player.indexOf(roleId) >= 0 ? "POSITION" : "POST";
                }
            },

            getFlagSrc: function(countryId) {
                return 'images/skin/flags/' + countryId + '.png';
            },

            remTZOffset: function (date) {
                return new Date(date.getTime() + (new Date()).getTimezoneOffset() * 60000)
            },

            addTZOffset: function (date) {
                return new Date(date.getTime() - (new Date()).getTimezoneOffset() * 60000)
            },

            getPrivate: thisIsPrivate,

            // PERSON HELPER START
            // TODO: Encapsulate into separate class helper

            getPersonViewLink: function (person) {
                if (!angular.isObject(person)) {
                    return "javascrip:void(0);"
                }

                return '/person/' + person.id;
            },

            getPersonImage: function (image, imageUploadData) {
                return angular.isString(image) && image.length > 0
                    ? imageUploadData.path + '/' + image
                    : this.getPersonEmptyImage();
            },

            getPersonEmptyImage: function () {
                return 'images/skin/empty/EmptyPersonImage.png';
            },

            getLoadingImg: function () {
                return 'images/skin/loading.gif';
            },

            isGK: function (person) {
                return person.roleId == configSrv.Current.PersonRoleIds.PlayerGoalkeeper
            },

            // PERSON HELPER END

            // TEAM HELPER START
            // TODO: Encapsulate into separate class helper

            getTeamViewLinkById: function (teamId) {
                return '/team/' + teamId;
            },

            getTeamViewLink: function (team) {
                console.log("Called!");
                return '/team/' + team.id;
            },

            getTeamImage: function (team) {
                if (angular.isString(team.image) && team.image.length > 0) {
                    return this.getTeamImageFolder(team.id) + "/" + team.image;
                } else {
                    return this.getTeamEmptyImage();
                }
            },

            getTeamFakeInfoImage: function (team) {
                if (this.hasTeamExtendedInfo(team)
                        && angular.isString(team.descriptionData.fakeInfo.image)
                        && team.descriptionData.fakeInfo.image.length > 0) {
                    return this.getTeamImageFolder(team.id) + "/" + team.descriptionData.fakeInfo.image;
                } else {
                    return this.getTeamFakeEmptyImage();
                }
            },

            getFakePlayersText: function (team) {
                if (this.hasTeamExtendedInfo(team) && angular.isArray(team.descriptionData.fakeInfo.persons)) {
                    var players = [];

                    for (var i = 0; i < team.descriptionData.fakeInfo.persons.length; i++) {

                        var plr = team.descriptionData.fakeInfo.persons[i];
                        var num = angular.isNumber(plr.number) ? plr.number + ". " : "";
                        var date = angular.isString(plr.dateOfBirth) ? new Date(plr.dateOfBirth) : null;
                        var birthDate = angular.isDate(date) ? " (" + date.getDay() + "." + date.getMonth() + 1 + "." + date.getFullYear() + ")" : "";

                        players.push(num + plr.name + birthDate)
                    }

                    var result = this.strJoin(", ", players);

                    return result;
                } else {
                    return "";
                }
            },

            getCustomHeadCoach: function (team) {
                if (this.hasTeamExtendedInfo(team)
                    && angular.isArray(team.descriptionData.fakeInfo.coaches)
                    && team.descriptionData.fakeInfo.coaches.length > 0) {
                    return team.descriptionData.fakeInfo.coaches[0];
                }

                return null;
            },

            getTeamImageFolder: function (id) {
                return configSrv.Current.Images.Teams.replace("{id}", id);
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

            // PROTOCOL HELPER START
            hasProtocolExtraLink: function (item) {
                return angular.isObject(item)
                    && angular.isObject(item.extra)
                    && angular.isObject(item.extra.main)
                    && item.extra.main.text.length > 0;
            },

            hasProtocolInfo: function (item) {
                return angular.isObject(item)
                    && angular.isString(item.info)
                    && item.info.length > 0;
            },

            hasProtocolData: function (item) {
                return angular.isObject(item)
                    && angular.isString(item.data)
                    && item.data.length > 0;
            },

            filterGoalsInfo: function (info) {
                if (angular.isString(info)) {
                    for (var i = 0; i < availableGoalsInfo.length; i++) {
                        if (info.toLowerCase() == availableGoalsInfo[i].toLowerCase()) {
                            return info;
                        }
                    }
                }

                return "";
            },

            filterEventsInfo: function (info) {
                if (angular.isString(info)) {
                    for (var i = 0; i < availableEventsInfo.length; i++) {
                        if (info.toLowerCase() == availableEventsInfo[i].toLowerCase()) {
                            return info;
                        }
                    }
                }

                return "";
            },
            // PROTOCOL HELPER END

            strJoin: function (separator, strArray) {
                var result = "";

                for (var i = 0; i < strArray.length; i++) {
                    var sep = (i < strArray.length - 1) ? separator : "";
                    result = result + strArray[i] + sep;
                }

                return result;
            }
        };
    }]);

})(angular.module('fc.core'));