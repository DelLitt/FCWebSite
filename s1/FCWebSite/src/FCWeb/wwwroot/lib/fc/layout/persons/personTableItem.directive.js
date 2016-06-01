(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('personTableItem', personTableItem);

    personTableItem.$inject = ['$filter', 'personsSrv', 'statsSrv', 'configSrv'];

    function personTableItem($filter, personsSrv, statsSrv, configSrv) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                persons: '=',
                loaded: '=',
                tourneys: '=',
                teamid: '='
            },
            link: function link(scope, element, attrs) {
                var orderBy = $filter('orderBy');

                scope.$watch(function (scope) {
                    return scope.loaded;
                },
                function (newValue, oldValue) {
                    if (angular.isDefined(newValue) && newValue === true) {
                        initPersons();
                    }
                });

                scope.tourneyId = 8;
                scope.selectedTourney = {};
                scope.tourneyStats = [];

                scope.const = {
                    assists: 'assists',
                    games: 'games',
                    goals: 'goals',
                    substitutes: 'substitutes',
                    yellows: 'yellows',
                    reds: 'reds'
                }

                scope.$watch(function (scope) {
                    return scope.selectedTourney;
                },
                function (newValue, oldValue) {
                    if (angular.isObject(scope.selectedTourney)
                        && angular.isDefined(scope.selectedTourney.id)) {
                        scope.tourneyId = newValue.id;
                        loadPersonStats(scope.tourneyId);
                    }                    
                });

                scope.order = function (predicate) {
                    scope.predicate = predicate;
                    scope.reverse = (scope.predicate === predicate) ? !scope.reverse : false;
                    scope.persons = orderBy(scope.persons, predicate, scope.reverse);
                };

                function initPersons() {
                    scope.selectedTourney = angular.isArray(scope.tourneys) ? scope.tourneys[scope.tourneys.length - 1] : {};

                    angular.forEach(scope.persons, function (item) {
                        var imageUploadData = this.getImageUploadData(item);
                        item.src = imageUploadData.path + '/' + item.image;
                    }, personsSrv);

                    scope.order('number', true);
                }

                function loadPersonStats(tourneyId) {
                    if (scope.tourneyStats.indexOf(tourneyId) > -1) {
                        return;
                    }

                    statsSrv.loadTeamTourneyStats(scope.teamid, tourneyId, personStatsLoaded);
                }

                function personStatsLoaded(response) {
                    var stats = response.data;

                    if (!angular.isArray(scope.persons)) {
                        return;
                    }

                    angular.forEach(scope.persons, function (item) {
                        var hasStats = false;

                        for (var i = 0; i < this.length; i++) {
                            if (item.id == this[i].personId) {
                                setStats(item, scope.tourneyId, this[i]);
                                hasStats = true;
                            }
                        }

                        if (!hasStats) {
                            setStats(item, scope.tourneyId, null);
                        }                        

                        scope.tourneyStats.push(scope.selectedTourney.id);

                    }, stats);

                    function setStats(person, tourneyId, stats) {
                        var lStats = stats;

                        if (!angular.isObject(lStats)) {
                            lStats = {
                                    assists: 0,
                                    games: 0,
                                    goals: 0,
                                    substitutes: 0,
                                    yellows: 0,
                                    reds: 0
                                }
                        }

                        person[getFieldName(scope.const.assists)] = lStats.assists;
                        person[getFieldName(scope.const.games)] = lStats.games;
                        person[getFieldName(scope.const.goals)] = lStats.goals;
                        person[getFieldName(scope.const.substitutes)] = lStats.substitutes;
                        person[getFieldName(scope.const.yellows)] = lStats.yellows;
                        person[getFieldName(scope.const.reds)] = lStats.reds;
                    }

                    scope.getField = getFieldName;

                    function getFieldName(name) {
                        return name + scope.selectedTourney.id.toString()
                    }
                }
            },
            templateUrl: '/lib/fc/layout/persons/personTableItem.html'
        }
    }
})();