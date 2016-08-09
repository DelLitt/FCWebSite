namespace FCWeb.Core.ViewModelHepers
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Common;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.Extensions.DependencyInjection;
    using ViewModels;

    public class GameVMHelper
    {
        private List<Team> teams { get; set; }
        private List<Stadium> stadiums { get; set; }

        public GameVMHelper(List<Team> teams = null, List<Stadium> stadiums = null)
        {
            this.teams = teams != null
                ? teams
                : new List<Team>();

            this.stadiums = stadiums != null
                ? stadiums
                : new List<Stadium>();
        }

        public void FillTeamsEntityLinks(IEnumerable<GameViewModel> games)
        {
            Guard.CheckNull(games, nameof(games));

            if (!games.Any()) { return; }

            var teamIdsPackAll = new List<int>();

            teamIdsPackAll.AddRange(games.Select(g => g.homeId));
            teamIdsPackAll.AddRange(games.Select(g => g.awayId));
            IEnumerable<int> teamIdsPack = teamIdsPackAll.Distinct();

            IEnumerable<int> newTeamIds = teamIdsPack.Except(teams.Select(t => t.Id));

            if(newTeamIds.Any())
            {
                var teamBll = MainCfg.ServiceProvider.GetService<ITeamBll>();
                teams.AddRange(teamBll.GetTeams(newTeamIds));
            }

            if(!teams.Any())
            {
                throw new ViewModelMappingException(nameof(teams), typeof(IEnumerable<GameViewModel>));
            }

            var stadiumIdsPack = new List<int>();

            stadiumIdsPack.AddRange(games.Where(g => g.stadiumId.HasValue).Select(g => g.stadiumId.Value).Distinct());

            IEnumerable<int> newStadiumIds = stadiumIdsPack.Except(stadiums.Select(s => s.Id));

            if (newStadiumIds.Any())
            {
                var stadiumBll = MainCfg.ServiceProvider.GetService<IStadiumBll>();
                stadiums.AddRange(stadiumBll.GetStadiums(newStadiumIds));
            }

            if (!stadiums.Any())
            {
                throw new ViewModelMappingException(nameof(stadiums), typeof(IEnumerable<GameViewModel>));
            }

            foreach (GameViewModel game in games)
            {
                game.dataEL = new GameEntityLinkData()
                {
                    home = GetTeamEntityLinkData(game.homeId),
                    away = GetTeamEntityLinkData(game.awayId),
                    stadium = GetStadiumEntityLinkData(game.stadiumId)
                };
            }
        }

        private EntityLinkViewModel GetTeamEntityLinkData(int teamId)
        {
            Team team = teams.FirstOrDefault(t => t.Id == teamId);
            if (team == null)
            {
                throw new ViewModelMappingException(nameof(team) + " with Id: " + teamId.ToString(), typeof(GameViewModel));
            }

            return new EntityLinkViewModel()
            {
                id = team.Id.ToString(),
                text = team.Name,
                title = team.NameExtended()
            };
        }

        private EntityLinkViewModel GetStadiumEntityLinkData(int? stadiumId)
        {
            if(!stadiumId.HasValue)
            {
                return new EntityLinkViewModel()
                {
                    id = "0",
                    text = "-",
                    title = "-"
                };
            }

            Stadium stadium = stadiums.FirstOrDefault(s => s.Id == stadiumId.Value);
            if (stadium == null)
            {
                throw new ViewModelMappingException(nameof(stadium) + " with Id: " + stadiumId.Value.ToString(), typeof(GameViewModel));
            }

            return new EntityLinkViewModel()
            {
                id = stadium.Id.ToString(),
                text = stadium.Name,
                title = stadium.NameExtended()
            };
        }
    }
}
