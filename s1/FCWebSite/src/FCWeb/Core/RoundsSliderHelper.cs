namespace FCWeb.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Bll;
    using FCCore.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;
    using ViewModels.Schedule;
    using FCCore.Model;
    using ViewModels;
    using FCCore.Common;
    using Extensions;

    public class RoundsSliderHelper
    {
        public static IEnumerable<RoundSliderViewModel> GetRoundsSlider(int teamId, int[] tourneyIds, DateTime date)
        {
            IRoundBll roundBll = MainCfg.ServiceProvider.GetService<IRoundBll>();

            roundBll.FillTourneys = true;
            IEnumerable<int> roundIds = roundBll.GetRoundIdsOfTourneys(tourneyIds, teamId);

            if (Guard.IsEmptyIEnumerable(roundIds)) { return new RoundSliderViewModel[0]; }

            IGameBll gameBll = MainCfg.ServiceProvider.GetService<IGameBll>();

            gameBll.FillTourneys = true;
            gameBll.FillRounds = true;
            gameBll.FillTeams = true;

            IEnumerable<Game> roundGames = gameBll.GetTeamActualRoundGames(teamId, roundIds, date);

            if (Guard.IsEmptyIEnumerable(roundGames)) { return new RoundSliderViewModel[0]; }

            var roundsSlider = new List<RoundSliderViewModel>();

            RoundInfoViewModel roundView = roundGames.ToRoundInfoViewModel().FirstOrDefault();

            foreach (int roundId in roundIds)
            {
                roundsSlider.Add(new RoundSliderViewModel()
                {
                    roundId = roundId,
                    current = roundView?.roundId == roundId,
                    roundGames = roundView?.roundId == roundId ? roundView : null
                });
            }

            return roundsSlider;
        }
    }
}
