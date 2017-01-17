namespace FCWeb.Controllers.Api.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Core;
    using Core.Extensions;
    using Core.Protocol;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Bll.Protocol;
    using FCCore.Common;
    using FCCore.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;
    using ViewModels.Protocol;

    [Route("api/games/{id}/[controller]")]
    public class ProtocolController : Controller
    {
        //[FromServices]
        private IProtocolRecordBll protocolBll { get; set; }
        //[FromServices]
        private IGameBll gameBll { get; set; }
        //[FromServices]
        private IPersonStatisticsBll personStatisticsBll { get; set; }
        //[FromServices]
        private IPersonBll personBll { get; set; }

        public ProtocolController(IProtocolRecordBll protocolBll, IGameBll gameBll, IPersonStatisticsBll personStatisticsBll, IPersonBll personBll)
        {
            this.protocolBll = protocolBll;
            this.gameBll = gameBll;
            this.personStatisticsBll = personStatisticsBll;
            this.personBll = personBll;
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

        [HttpGet("temp/xxx")]
        public IEnumerable<PersonStatistics> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            gameBll.FillRounds = true;
            Game game = gameBll.GetGame(id);           

            if (game == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            ProtocolGameViewModel oldProtocolView = GetDefaultProtocol(game);

            IEnumerable<int> personIds = GetAllPersonIdsFromBothProtocols(oldProtocolView, new ProtocolGameViewModel());

            personBll.FillPersonCareer = true;
            IEnumerable<Person> persons = personBll.GetPersons(personIds);

            IEnumerable<PersonStatistics> personStatistics = personStatisticsBll.CalculateTourneyStatistics(game.round.tourneyId, persons);
            personStatisticsBll.SavePersonStatistics(game.round.tourneyId, personStatistics);

            return personStatistics;
        }

        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public IEnumerable<PersonStatistics> Post(int id, [FromBody]ProtocolGameViewModel protocolView)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            gameBll.FillRounds = true;
            Game game = gameBll.GetGame(id);

            if (game == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            ProtocolGameViewModel oldProtocolView = GetDefaultProtocol(game);

            var records = protocolView.ToRecordsList();

            int count = protocolBll.SaveProtocol(id, records);

            var gameNoteBuilder = new GameNoteBuilder(game);
            gameNoteBuilder.FakeProtocol = protocolView.fake;

            gameBll.SaveGame(gameNoteBuilder.Game);

            IEnumerable<int> personIds = GetAllPersonIdsFromBothProtocols(oldProtocolView, new ProtocolGameViewModel());

            personBll.FillPersonCareer = true;
            IEnumerable<Person> persons = personBll.GetPersons(personIds);

            IEnumerable<PersonStatistics> personStatistics = personStatisticsBll.CalculateTourneyStatistics(game.round.tourneyId, persons);
            personStatisticsBll.SavePersonStatistics(game.round.tourneyId, personStatistics);

            return personStatistics;
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

        private IEnumerable<int> GetAllPersonIdsFromBothProtocols(ProtocolGameViewModel oldProtocol, ProtocolGameViewModel newProtocol)
        {
            List<int> allPersonIds = 
                oldProtocol.home.playersAll != null 
                ? oldProtocol.home.playersAll.Select(p => p.id).ToList()
                : new List<int>();

            IEnumerable<PersonViewModel> oldAwayPersonModels = oldProtocol.away.playersAll ?? new List<PersonViewModel>();
            allPersonIds.AddRange(oldAwayPersonModels.Where(m => !allPersonIds.Contains(m.id)).Select(p => p.id));

            IEnumerable<PersonViewModel> newHomePersonModels = newProtocol.home.playersAll ?? new List<PersonViewModel>();
            allPersonIds.AddRange(newHomePersonModels.Where(m => !allPersonIds.Contains(m.id)).Select(p => p.id));

            IEnumerable<PersonViewModel> newAwayPersonModels = newProtocol.away.playersAll ?? new List<PersonViewModel>();
            allPersonIds.AddRange(newAwayPersonModels.Where(m => !allPersonIds.Contains(m.id)).Select(p => p.id));

            return allPersonIds;
        }
    }
}
