namespace FCWeb.Controllers.Api.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Common;
    using FCCore.Model;
    using Microsoft.AspNet.Mvc;
    using ViewModels;

    [Route("api/games/[controller]")]
    public class RoundController : Controller
    {
        //[FromServices]
        private IGameBll gameBll { get; set; }
        //[FromServices]
        private IRoundBll roundBll { get; set; }

        public RoundController(IGameBll gameBll, IRoundBll roundBll)
        {
            this.gameBll = gameBll;
            this.roundBll = roundBll;
        }

        // GET api/values/5
        // /api/games/round/32/mode/1
        [HttpGet("{id:int}/mode/{mode:int}")]
        public RoundInfoViewModel Get(int id, int mode)
        {
            if (mode == 1)
            {
                gameBll.FillRounds = true;
                gameBll.FillTeams = true;
                gameBll.FillTourneys = true;

                IEnumerable<RoundInfoViewModel> roundViews = gameBll.GetRoundGames(id).ToRoundInfoViewModel();

                return roundViews.FirstOrDefault();
            }

            return null;
        }

        // GET api/values/5
        // /api/games/round/team/3/slider?tourneyIds=8&tourneyIds=10
        [HttpGet("team/{teamId}/slider")]
        public IEnumerable<RoundSliderViewModel> Get(int teamId, [FromQuery] int[] tourneyIds)
        {
            var actualDate = new DateTime(2015, 9, 10);

            roundBll.FillTourneys = true;
            IEnumerable<int> roundIds = roundBll.GetRoundIdsOfTourneys(tourneyIds, teamId);

            if (Guard.IsEmptyIEnumerable(roundIds)) { return new RoundSliderViewModel[0]; }

            gameBll.FillTourneys = true;
            gameBll.FillRounds = true;
            gameBll.FillTeams = true;
            IEnumerable<Game> roundGames = gameBll.GetTeamActualRoundGames(teamId, roundIds, actualDate);

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
