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
        private IGamesBll gameBll { get; set; }

        //[FromServices]
        private IRoundBll roundBll { get; set; }

        public RoundController(IGamesBll gameBll, IRoundBll roundBll)
        {
            this.gameBll = gameBll;
            this.roundBll = roundBll;
        }

        // GET api/values/5
        // /api/games/round/32/mode/1
        [HttpGet("{id:int}/mode/{mode:int}")]
        public RoundViewModel Get(int id, int mode)
        {
            if (mode == 1)
            {
                gameBll.FillRounds = true;
                gameBll.FillTeams = true;

                IEnumerable<RoundViewModel> roundViews = gameBll.GetRoundGames(id).ToRoundViewModel();

                return roundViews.FirstOrDefault();
            }

            return null;
        }

        // GET api/values/5
        // /api/games/round/team/3/slider?tourneyIds=8&tourneyIds=10
        [HttpGet("team/{teamId}/slider")]
        public IEnumerable<TourneyRoundViewModel> Get(int teamId, [FromQuery] int[] tourneyIds)
        {
            var actualDate = new DateTime(2015, 9, 10);

            roundBll.FillTourneys = true;
            IEnumerable<int> roundIds = roundBll.GetRoundIdsOfTourneys(tourneyIds, teamId);

            if (Guard.IsEmptyIEnumerable(roundIds)) { return new TourneyRoundViewModel[0]; }

            gameBll.FillRounds = true;
            gameBll.FillTeams = true;
            IEnumerable<Game> roundGames = gameBll.GetTeamActualRoundGames(teamId, roundIds, actualDate);

            if (Guard.IsEmptyIEnumerable(roundGames)) { return new TourneyRoundViewModel[0]; }

            var roundsSlider = new List<TourneyRoundViewModel>();

            RoundViewModel roundView = roundGames.ToRoundViewModel().FirstOrDefault();

            foreach (int roundId in roundIds)
            {
                roundsSlider.Add(new TourneyRoundViewModel()
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
