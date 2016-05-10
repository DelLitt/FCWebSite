namespace FCWeb.Core.Extensions
{
    using System;
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

            Guid? tempGuid = team.Id == 0 ? Guid.NewGuid() : (Guid?)null;

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
                webSite = team.WebSite,
                tempGuid = tempGuid
            };
        }

        public static IEnumerable<TeamViewModel> ToViewModel(this IEnumerable<Team> teams)
        {
            if (Guard.IsEmptyIEnumerable(teams)) { return new TeamViewModel[0]; }

            return teams.Select(v => v.ToViewModel()).ToList();
        }

        public static Team ToBaseModel(this TeamViewModel teamView)
        {
            if (teamView == null) { return null; }

            return new Team()
            {
                Id = teamView.id,
                Active = teamView.active,
                Address = teamView.address,
                cityId = teamView.cityId,
                Description = teamView.description,
                Email = teamView.email,
                Image = teamView.image,
                Name = teamView.name,
                NameFull = teamView.nameFull,
                NamePre = teamView.namePre,
                mainTourneyId = teamView.mainTourneyId,
                stadiumId = teamView.stadiumId,
                teamTypeId = teamView.teamTypeId,
                WebSite = teamView.webSite
            };
        }
    }
}
