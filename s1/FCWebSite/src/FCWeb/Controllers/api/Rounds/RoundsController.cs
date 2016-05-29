namespace FCWeb.Controllers.Api.Rounds
{
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNet.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class RoundsController : Controller
    {
        //[FromServices]
        private IRoundBll roundBll { get; set; }

        public RoundsController(IRoundBll roundBll)
        {
            this.roundBll = roundBll;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public RoundViewModel Get(int id)
        {
            return roundBll.GetRound(id).ToViewModel();
        }
    }
}
