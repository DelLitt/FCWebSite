(function() {
    'use strict';

    angular
        .module('fc.ui')
        .directive('personTableItem', personTableItem);

    personTableItem.$inject = ['$filter', 'personsSrv', 'statsSrv', 'configSrv', 'helper'];

    function personTableItem($filter, personsSrv, statsSrv, configSrv, helper) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                model: '='
            },
            link: function link(scope, element, attrs) {
                var orderBy = $filter('orderBy');

                scope.tourneyId = 100500;
                scope.selectedTourney = {};
                scope.tourneyStats = [];
                scope.loadingImage = helper.getLoadingImg();
                scope.loadingStats = true;
                scope.isGK = helper.isGK;

                scope.const = {
                    assists: 'assists',
                    games: 'games',
                    goals: 'goals',
                    substitutes: 'substitutes',
                    yellows: 'yellows',
                    reds: 'reds',
                    customIntValue: 'customIntValue'
                }

                scope.$watch(function (scope) {
                    return scope.model;
                },
                function (newValue, oldValue) {
                    if (angular.isObject(newValue)) {
                        initTourneyStats();
                    }
                });

                function initTourneyStats() {
                    scope.selectedTourney = angular.isArray(scope.model.tourneys) ? scope.model.tourneys[scope.model.tourneys.length - 1] : {};
                    scope.order('number', false);
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

                scope.order = function (predicate, reverse) {
                    scope.predicate = predicate;
                    scope.reverse = angular.isDefined(reverse) 
                        ? reverse
                        : ((scope.predicate === predicate) ? !scope.reverse : false);
                    scope.model.persons = orderBy(scope.model.persons, predicate, scope.reverse);
                };

                function loadPersonStats(tourneyId) {
                    //if (scope.tourneyStats.indexOf(tourneyId) > -1) {
                    //    return;
                    //}

                    scope.loadingStats = true;
                    statsSrv.loadTeamTourneyStats(scope.model.teamId, tourneyId, teamTourneyLoaded);
                }

                function teamTourneyLoaded(response) {
                    var stats = response.data;

                    if (!angular.isArray(scope.model.persons)) {
                        return;
                    }

                    angular.forEach(scope.model.persons, function (item) {
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

                        //scope.tourneyStats.push(scope.selectedTourney.id);

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
                                    reds: 0,
                                    customIntValue: 0
                                }
                        }

                        person[getFieldName(scope.const.assists)] = lStats.assists;
                        person[getFieldName(scope.const.games)] = lStats.games;
                        person[getFieldName(scope.const.goals)] = lStats.goals;
                        person[getFieldName(scope.const.substitutes)] = lStats.substitutes;
                        person[getFieldName(scope.const.yellows)] = lStats.yellows;
                        person[getFieldName(scope.const.reds)] = lStats.reds;
                        person[getFieldName(scope.const.customIntValue)] = lStats.customIntValue;
                    }

                    scope.getField = getFieldName;

                    function getFieldName(name) {
                        if (!angular.isObject(scope.selectedTourney)) {
                            return "";
                        }

                        return name + scope.selectedTourney.id.toString()
                    }

                    scope.loadingStats = false;
                }
            },
            templateUrl: '/lib/fc/layout/persons/personTableItem.html'
        }
    }
})();