﻿namespace FCWeb.Controllers.Api.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Core.Extensions;
    using Core.ViewModelHepers;
    using FCCore.Abstractions.Bll;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Game;

    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private IGameBll gameBll { get; set; }

        public GamesController(IGameBll gameBll)
        {
            this.gameBll = gameBll;
        }

        [HttpGet("{id}")]
        public GameViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press")))
            {
                return GetForce(id);
            }

            string cacheKey = gameBll.ObjectKeyGenerator.GetStringKey(this.GetType().FullName + nameof(Get), id);
            GameViewModel result = gameBll.Cache.GetOrCreate(cacheKey, () => { return GetForce(id); });

            return result;
        }

        private GameViewModel GetForce(int id)
        {
            if (id == 0)
            {
                return new GameViewModel();
            }

            gameBll.FillRounds = true;
            gameBll.FillTeams = true;
            gameBll.FillStadiums = true;
            gameBll.FillTourneys = true;

            return gameBll.GetGame(id).ToViewModel();
        }

        [HttpGet]
        public IEnumerable<GameViewModel> Get([FromQuery] int[] tourneyIds)
        {
            return gameBll.GetGamesByTourneys(tourneyIds).ToViewModel();
        }

        [HttpGet("{id:int}/{mode}")]
        [ResponseCache(VaryByQueryKeys = new string[] { "id", "mode", "tourneyIds" }, Duration = Constants.Cache_MiddleVaryByParamDurationSeconds)]
        public IEnumerable<GameQuickInfoViewModel> Get(int id, string mode, [FromQuery] int[] tourneyIds)
        {
            gameBll.FillTeams = true;
            gameBll.FillTourneys = true;
            gameBll.FillRounds = true;
            gameBll.FillStadiums = true;

            int daysShift = mode.Equals("quick", StringComparison.OrdinalIgnoreCase) ? MainCfg.TeamGamesInfoDaysShift : MainCfg.MaxGamesInfoDaysShift;

            IEnumerable <GameQuickInfoViewModel> games = 
                gameBll.GetTeamGames(id, tourneyIds, DateTime.UtcNow, daysShift)
                       .OrderByDescending(g => g.GameDate)
                       .ToGameQuickInfoViewModel();

            return games;
        }

        // POST api/values
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

        // PUT api/values/5
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

        // DELETE api/values/5
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
