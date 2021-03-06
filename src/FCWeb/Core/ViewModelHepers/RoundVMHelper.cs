﻿namespace FCWeb.Core.ViewModelHepers
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Bll;
    using FCCore.Common;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using ViewModels;
    using ViewModels.Game;

    public class RoundVMHelper
    {
        private IEnumerable<RoundViewModel> rounds { get; set; }

        public RoundVMHelper(IEnumerable<RoundViewModel> rounds)
        {
            this.rounds = rounds != null
                ? rounds
                : new RoundViewModel[0];
        }

        public void FillAvailableTeams(bool fillGamesEntityLinks = true)
        {
            var teamIdsPack = new List<int>();

            foreach(RoundViewModel round in rounds)
            {
                var ids = !string.IsNullOrWhiteSpace(round.teamList)
                    ? JsonConvert.DeserializeObject<IEnumerable<int>>(round.teamList)
                    : new int[0];

                if(!ids.Any())
                {
                    ids = round.games.Select(r => r.homeId).Union(round.games.Select(r => r.awayId));
                }

                teamIdsPack.AddRange(ids);
            }

            IEnumerable<int> teamIds = teamIdsPack.Distinct();

            var teamBll = MainCfg.ServiceProvider.GetService<ITeamBll>();

            IEnumerable<Team> teams = teamBll.GetTeams(teamIds);

            if(Guard.IsEmptyIEnumerable(teams)) { return; }

            var gameVMHelper = new GameVMHelper(teams.ToList());

            foreach(RoundViewModel round in rounds)
            {
                var ids = !string.IsNullOrWhiteSpace(round.teamList)
                    ? JsonConvert.DeserializeObject<IEnumerable<int>>(round.teamList)
                    : new int[0];

                IEnumerable<Team> roundTeams = teams.Where(t => ids.Contains(t.Id));

                round.teams = !Guard.IsEmptyIEnumerable(roundTeams)
                    ? roundTeams.Select(t => new EntityLinkViewModel()
                                                {
                                                    id = t.Id.ToString(),
                                                    text = t.Name,
                                                    title = t.Name
                                                })
                    : new EntityLinkViewModel[0];

                round.editMode = true;

                gameVMHelper.FillTeamsEntityLinks(round.games ?? new GameViewModel[0]);
            }
        }
    }
}
