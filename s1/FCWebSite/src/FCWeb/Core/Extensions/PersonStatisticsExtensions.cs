namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class  PersonStatisticsExtensions
    {
        public static PersonStatisticsViewModel ToViewModel(this PersonStatistics personStatistics)
        {
            if (personStatistics == null) { return null; }

            return new PersonStatisticsViewModel()
            {
                 id = personStatistics.Id,
                 assists = personStatistics.Assists,
                 customIntValue = personStatistics.CustomIntValue,
                 games = personStatistics.Games,
                 goals = personStatistics.Goals,
                 personId = personStatistics.personId,
                 reds = personStatistics.Reds,
                 substitutes = personStatistics.Substitutes,
                 teamId = personStatistics.teamId,
                 tourneyId = personStatistics.tourneyId,
                 yellows = personStatistics.Yellows,
                 team = personStatistics.team.ToViewModel(),
                 tourney = personStatistics.tourney.ToViewModel()
            };
        }

        public static IEnumerable<PersonStatisticsViewModel> ToViewModel(this IEnumerable<PersonStatistics> personsStatistics)
        {
            if (Guard.IsEmptyIEnumerable(personsStatistics)) { return new PersonStatisticsViewModel[0]; }

            return personsStatistics.Select(v => v.ToViewModel()).ToList();
        }
    }
}
