namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels.Team;

    public static class TeamTypeExtensions
    {
        public static TeamTypeViewModel ToViewModel(this TeamType teamType)
        {
            if (teamType == null) { return null; }

            return new TeamTypeViewModel()
            {
                id = teamType.Id,
                name = teamType.Name,
                nameFull = teamType.NameFull
            };
        }

        public static IEnumerable<TeamTypeViewModel> ToViewModel(this IEnumerable<TeamType> teamTypes)
        {
            if (Guard.IsEmptyIEnumerable(teamTypes)) { return new TeamTypeViewModel[0]; }

            return teamTypes.Select(v => v.ToViewModel()).ToList();
        }
    }
}
