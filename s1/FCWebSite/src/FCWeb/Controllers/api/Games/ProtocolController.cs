namespace FCWeb.Controllers.Api.Games
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Core;
    using Core.Extensions;
    using Core.Protocol;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Bll.Protocol;
    using FCCore.Common;
    using FCCore.Model;
    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.Mvc;
    using ViewModels.Protocol;

    [Route("api/games/{id}/[controller]")]
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

        [HttpGet("{form}")]
        public object Get(int id, string form)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                return new ProtocolGameViewModel();
            }

            object result = null;

            gameBll.FillTeams = true;

            Game game = gameBll.GetGame(id);

            if (game == null) { return null; }
            
            if(form.Equals("default", StringComparison.OrdinalIgnoreCase))
            {
                result = GetDefaultProtocol(game);
            }
            else if (form.Equals("text", StringComparison.OrdinalIgnoreCase))
            {
                result = GetTextProtocol(game);
            }

            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public void Post(int id, [FromBody]ProtocolGameViewModel protocolView)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            var records = protocolView.ToRecordsList();

            int count = protocolBll.SaveProtocol(id, records);

            Game game = gameBll.GetGame(id);

            if(game == null) { return; }

            var gameNoteBuilder = new GameNoteBuilder(game);
            gameNoteBuilder.FakeProtocol = protocolView.fake;

            gameBll.SaveGame(gameNoteBuilder.Game);
        }

        private ProtocolGameViewModel GetDefaultProtocol(Game game)
        {
            IGameProtocolManager protocolManager = ProtocolGameManagerFactory.Create(game.Id);

            var modelBuilder = new ProtocolViewModelBuilder(protocolManager);

            return modelBuilder.ViewModel;
        }

        private TextProtocolViewModel GetTextProtocol(Game game)
        {
            IGameProtocolManager protocolManager = ProtocolGameManagerFactory.Create(game.Id);

            ITextProtocolBuilder textProtocolBuilderReal = TextProtocolBuilderFactory.Create(protocolManager);
            ITextProtocolBuilder textProtocolBuilderFake = TextProtocolBuilderFactory.Create(new GameNoteBuilder(game));

            ITextProtocolBuilder textProtocolBuilderHome = textProtocolBuilderReal.IsAvailableHome
                                                            ? textProtocolBuilderReal
                                                            : textProtocolBuilderFake;

            ITextProtocolBuilder textProtocolBuilderAway = textProtocolBuilderReal.IsAvailableAway
                                                            ? textProtocolBuilderReal
                                                            : textProtocolBuilderFake;

            var textProtocol = new TextProtocolViewModel();

            textProtocol.home.main = textProtocolBuilderHome.GetMainSquad(Side.Home);
            textProtocol.home.reserve = textProtocolBuilderHome.GetReserve(Side.Home);
            textProtocol.home.goals = textProtocolBuilderHome.GetGoals(Side.Home);
            textProtocol.home.yellows = textProtocolBuilderHome.GetYellows(Side.Home);
            textProtocol.home.reds = textProtocolBuilderHome.GetReds(Side.Home);
            textProtocol.home.others = textProtocolBuilderHome.GetOthers(Side.Home);

            textProtocol.away.main = textProtocolBuilderAway.GetMainSquad(Side.Away);
            textProtocol.away.reserve = textProtocolBuilderAway.GetReserve(Side.Away);
            textProtocol.away.goals = textProtocolBuilderAway.GetGoals(Side.Away);
            textProtocol.away.yellows = textProtocolBuilderAway.GetYellows(Side.Away);
            textProtocol.away.reds = textProtocolBuilderAway.GetReds(Side.Away);
            textProtocol.away.others = textProtocolBuilderAway.GetOthers(Side.Away);

            return textProtocol;
        }
    }
}
