namespace FCWeb.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using Newtonsoft.Json;
    using ViewModels.Team;

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
                tempGuid = tempGuid,                
                searchDefault = team.NameExtended(),
                descriptionData = SerializationHelper.FromJsonSafely<TeamDescriptionViewModel>(team.Description),
                title = team.NameExtended(),
                city = team.city.ToViewModel(),
                mainTourney = team.mainTourney.ToViewModel(),
                stadium = team.stadium.ToViewModel(),
                teamType = team.teamType.ToViewModel()
            };
        }

        public static TeamShortViewModel ToShortViewModel(this Team team)
        {
            if (team == null) { return null; }

            Guid? tempGuid = team.Id == 0 ? Guid.NewGuid() : (Guid?)null;

            return new TeamShortViewModel()
            {
                id = team.Id,
                active = team.Active,
                image = team.Image,
                name = team.Name,
                namePre = team.NamePre,
                teamTypeId = team.teamTypeId,
                searchDefault = team.NameExtended(),
                title = team.NameExtended(),
                city = team?.city?.Name ?? string.Empty,
                stadium = team?.stadium?.NameExtended() ?? string.Empty                
            };
        }

        public static IEnumerable<TeamViewModel> ToViewModel(this IEnumerable<Team> teams)
        {
            if (Guard.IsEmptyIEnumerable(teams)) { return new TeamViewModel[0]; }

            return teams.Select(v => v.ToViewModel()).ToList();
        }

        public static IEnumerable<TeamShortViewModel> ToShortViewModel(this IEnumerable<Team> teams)
        {
            if (Guard.IsEmptyIEnumerable(teams)) { return new TeamShortViewModel[0]; }

            return teams.Select(v => v.ToShortViewModel()).ToList();
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
                Description = teamView.descriptionData != null
                    ? JsonConvert.SerializeObject(teamView.descriptionData)
                    : teamView.description,
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

        public static string NameExtended(this Team team)
        {
            return team.city != null
                    ? string.Format(CultureInfo.CurrentCulture, "{0} ({1})", team.Name, team.city.NameFull)
                    : team.NameFull;
        }
    }
}
