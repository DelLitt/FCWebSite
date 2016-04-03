// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNet.Mvc;
    using FCCore.Abstractions.Bll;
    using FCCore.Model;
    using FCCore.Common;
    using ViewModels;
    using Core.Extensions;

    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        //[FromServices]
        private IGamesBll gameBll { get; set; }

        //[FromServices]
        private IRoundBll roundBll { get; set; }

        public GamesController(IGamesBll gameBll, IRoundBll roundBll)
        {
            this.gameBll = gameBll;
            this.roundBll = roundBll;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("round/short/{id:int}")]
        public RoundViewModel Get(int id)
        {
            gameBll.FillRounds = true;
            gameBll.FillTeams = true;

            IEnumerable<RoundViewModel> roundViews = gameBll.GetRoundGames(id).ToRoundViewModel();

            return roundViews.FirstOrDefault();
        }

        // GET api/values/5
        // /api/games/3/slider?tourneyIds=8&tourneyIds=10
        [HttpGet("{teamId}/slider")]
        public IEnumerable<TourneyRoundViewModel> Get(int teamId, [FromQuery] int[] tourneyIds)
        {
            var actualDate = new DateTime(2015, 9, 10);

            roundBll.FillTourneys = true;
            IEnumerable<int> roundIds = roundBll.GetRoundIdsOfTourneys(tourneyIds, teamId);

            if (Guard.IsEmptyIEnumerable(roundIds)) { return new TourneyRoundViewModel[0]; }

            gameBll.FillRounds = true;
            gameBll.FillTeams = true;
            IEnumerable<Game> roundGames = gameBll.GetTeamActualRoundGames(teamId, roundIds, actualDate);

            if(Guard.IsEmptyIEnumerable(roundGames)) { return new TourneyRoundViewModel[0]; }

            var roundsSlider = new List<TourneyRoundViewModel>();

            RoundViewModel roundView = roundGames.ToRoundViewModel().FirstOrDefault();
            
            foreach(int roundId in roundIds)
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

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
