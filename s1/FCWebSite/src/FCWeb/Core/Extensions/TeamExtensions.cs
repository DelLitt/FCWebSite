namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class TeamExtensions
    {
        public static TeamViewModel ToViewModel(this Team team)
        {
            if (team == null) { return null; }

            return new TeamViewModel()
            {
                id = team.Id,
                active = team.Active,
                address = team.Address,
                cityId = team.cityId,
                description = team.Description,
                email = team.Email,
                image = team.Image,
                mainTourneyId = team.mainTourneyId,
                name = team.Name,
                nameFull = team.NameFull,
                namePre = team.NamePre,
                stadiumId = team.stadiumId,
                teamTypeId = team.teamTypeId,
                webSite = team.WebSite
            };
        }

        public static IEnumerable<TeamViewModel> ToViewModel(this IEnumerable<Team> teams)
        {
            if (Guard.IsEmptyIEnumerable(teams)) { return new TeamViewModel[0]; }

            return teams.Select(v => v.ToViewModel()).ToList();
        }
    }
}
