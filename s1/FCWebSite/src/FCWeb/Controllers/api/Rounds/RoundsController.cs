namespace FCWeb.Controllers.Api.Rounds
{
    using System.Net;
    using Core.Extensions;
    using Core.ViewModelHepers;
    using FCCore.Abstractions.Bll;
    using FCCore.Model;
    using Microsoft.AspNet.Authorization;
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

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public RoundViewModel Post([FromBody]RoundViewModel roundView)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            return SaveRound(roundView);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,press")]
        public RoundViewModel Put(int id, [FromBody]RoundViewModel roundView)
        {
            if (id != roundView.id)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            return SaveRound(roundView);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,press")]
        public int Delete(int id)
        {
            return roundBll.RemoveRound(id);
        }

        private RoundViewModel SaveRound(RoundViewModel roundView)
        {
            Round round = roundView.ToBaseModel();

            roundBll.SaveRound(round);

            roundBll.FillGames = true;
            round = roundBll.GetRound(round.Id);

            roundView = round.ToViewModel();

            var roundVMHelper = new RoundVMHelper(new RoundViewModel[] { roundView });
            roundVMHelper.FillAvailableTeams();

            return roundView;
        }
    }
}
