namespace FCWeb.Controllers.Api.Tourneys
{
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [Route("api/tourneys/[controller]")]
    public class RoundController : Controller
    {
        //[FromServices]
        private ITourneyBll tourneyBll { get; set; }

        //[FromServices]
        private IRoundBll roundBll { get; set; }

        public RoundController(ITourneyBll tourneyBll, IRoundBll roundBll)
        {
            this.tourneyBll = tourneyBll;
            this.roundBll = roundBll;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TourneyViewModel Get(int id)
        {
            return tourneyBll.GetTourneyByRoundId(id).ToViewModel();
        }
    }
}
