namespace FCWeb.Controllers.Api.Games
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Core.Extensions;
    using Core.ViewModelHepers;
    using FCCore.Abstractions.Bll;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Game;

    [Route("api/games/[controller]")]
    public class QuickInfoController : Controller
    {
        private IGameBll gameBll { get; set; }

        public QuickInfoController(IGameBll gameBll)
        {
            this.gameBll = gameBll;
        }

        [HttpGet("{id}")]
        [ResponseCache(VaryByQueryKeys = new string[] { "id" }, Duration = Constants.Cache_MiddleVaryByParamDurationSeconds)]
        public GameQuickInfoGroupViewModel Get(int id)
        {
            IEnumerable<int> tourneyIds;
            string actionTitle;

            gameBll.FillTeams = true;
            gameBll.FillRounds = true;
            gameBll.FillStadiums = true;
            gameBll.FillTourneys = true;

            // TODO: Try to avoid hardcoding here
            // hardcoded only main and reserve team
            if (id == MainCfg.ReserveTeamId)
            {
                tourneyIds = MainCfg.ReserveTeamTourneyIds;
                actionTitle = "SHOW_QUICK_GAMES_TEAM_0";
            }
            else
            {
                // if it is not reserve, then it can be only main
                tourneyIds = MainCfg.MainTeamTourneyIds;
                actionTitle = "SHOW_QUICK_GAMES_TEAM_1";
            }

            IEnumerable<GameQuickInfoViewModel> games =
                gameBll.GetTeamPrevNextGames(id, tourneyIds, DateTime.UtcNow, MainCfg.QuickGameInfoDaysShift)
                    .ToGameQuickInfoViewModel();

            return new GameQuickInfoGroupViewModel()
            {
                actionTitle = actionTitle,
                teamId = id,
                games = games
            };
        }

        [HttpGet]
        public IEnumerable<GameViewModel> Get([FromQuery] int[] tourneyIds)
        {
            return gameBll.GetGamesByTourneys(tourneyIds).ToViewModel();
        }

        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public GameViewModel Post([FromBody]GameViewModel gameView)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            return SaveGame(gameView);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,press")]
        public GameViewModel Put(int id, [FromBody]GameViewModel gameView)
        {
            if (id != gameView.id)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            return SaveGame(gameView);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,press")]
        public int Delete(int id)
        {
            return gameBll.RemoveGame(id);
        }

        private GameViewModel SaveGame(GameViewModel gameView)
        {
            Game game = gameBll.SaveGame(gameView.ToBaseModel());

            gameView = game.ToViewModel();

            var gameVMHelper = new GameVMHelper();
            gameVMHelper.FillTeamsEntityLinks(new GameViewModel[] { gameView });

            return gameView;
        }
    }
}
