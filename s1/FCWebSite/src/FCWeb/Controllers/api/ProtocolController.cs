namespace FCWeb.Controllers.Api.Games
{
    using System.Collections.Generic;
    using Core;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Bll.Protocol;
    using FCCore.Model;
    using Microsoft.AspNet.Mvc;
    using ViewModels.Protocol;

    [Route("api/game/{id}/[controller]")]
    public class ProtocolController : Controller
    {
        //[FromServices]
        private IProtocolRecordBll protocolBll { get; set; }

        //[FromServices]
        private IGameBll gameBll { get; set; }

        public ProtocolController(IProtocolRecordBll protocolBll, IGameBll gameBll)
        {
            this.protocolBll = protocolBll;
            this.gameBll = gameBll;
        }

        // GET api/protocols/5
        [HttpGet]
        public GameProtocolViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                return new GameProtocolViewModel();
            }

            gameBll.FillTeams = true;

            Game game = gameBll.GetGame(id);

            if(game == null) { return null; }

            IEnumerable<ProtocolRecord> protocolRecords = protocolBll.GetProtocolRecords(game.Id);

            IGameProtocolManager protocolManager = protocolBll.GetGameProtocolManager(game.Id);

            var modelBuilder = new ProtocolViewModelBuilder(protocolManager);

            return modelBuilder.ViewModel;
        }

        //// POST api/values
        //[HttpPost]
        //[Authorize(Roles = "admin,press")]
        //public void Post([FromBody]GameViewModel gameView)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        return;
        //    }

        //    //int gameId = protocolBll.SaveGame(gameView.ToBaseModel());
        //}

        //// PUT api/values/5
        //[HttpPut]
        //[Authorize(Roles = "admin,press")]
        //public void Put(int id, [FromBody]GameViewModel gameView)
        //{
        //    if (id != gameView.id)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        return;
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        return;
        //    }

        //    //gameBll.SaveGame(gameView.ToBaseModel());
        //}
    }
}
