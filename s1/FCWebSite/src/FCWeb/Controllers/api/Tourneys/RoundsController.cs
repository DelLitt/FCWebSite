namespace FCWeb.Controllers.Api.Tourneys
{
    using System.Collections.Generic;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [Route("api/tourneys/{tourneyId}/[controller]")]
    public class RoundsController : Controller
    {
        //[FromServices]
        private ITourneyBll tourneyBll { get; set; }
        //[FromServices]
        private IRoundBll roundBll { get; set; }

        public RoundsController(ITourneyBll tourneyBll, IRoundBll roundBll)
        {
            this.tourneyBll = tourneyBll;
            this.roundBll = roundBll;
        }

        /// <summary>
        /// Gets all rounds of tourney
        /// </summary>
        /// <param name="tourneyId">Tourney Id</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<RoundViewModel> Get(int tourneyId)
        {
            return roundBll.GetRoundsByTourney(tourneyId).ToViewModel();
        }

        /// <summary>
        /// Gets rounds with search key
        /// </summary>
        /// <param name="tourneyId">Tourney Id<</param>
        /// <param name="txt">Search key</param>
        /// <returns></returns>
        [HttpGet("search")]
        public IEnumerable<RoundViewModel> Get(int tourneyId, [FromQuery] string txt)
        {
            return roundBll.SearchByNameFull(tourneyId, txt).ToViewModel();
        }
    }
}
