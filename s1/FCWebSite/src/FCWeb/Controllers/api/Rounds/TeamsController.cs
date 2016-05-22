namespace FCWeb.Controllers.Api.Rounds
{
    using System.Collections.Generic;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNet.Mvc;
    using ViewModels;

    [Route("api/round/{roundId}/[controller]")]
    public class TeamsController : Controller
    {
        //[FromServices]
        private ITeamBll teamBll { get; set; }

        public TeamsController(ITeamBll teamBll)
        {
            this.teamBll = teamBll;
        }

        /// <summary>
        /// Search available teams of the round
        /// </summary>
        /// <param name="roundId">Round id</param>
        /// <param name="txt">Text for search</param>
        /// <returns></returns>
        [HttpGet("search")]
        public IEnumerable<TeamViewModel> Get(int roundId, [FromQuery] string txt)
        {
            teamBll.FillCities = true;

            return teamBll.SearchByDefault(txt, roundId).ToViewModel();
        }

        /// <summary>
        /// Gets all available teams of the round
        /// </summary>
        /// <param name="roundId">Round id</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TeamViewModel> Get(int roundId)
        {
            teamBll.FillCities = true;

            return teamBll.GetTeamsByRound(roundId).ToViewModel();
        }
    }
}
