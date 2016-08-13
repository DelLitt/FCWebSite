namespace FCWeb.Controllers.Api.Games
{
    using System.Collections.Generic;
    using System.Net;
    using Core.Extensions;
    using Core.ViewModelHepers;
    using FCCore.Abstractions.Bll;
    using FCCore.Model;
    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        //[FromServices]
        private IGameBll gameBll { get; set; }

        public GamesController(IGameBll gameBll)
        {
            this.gameBll = gameBll;
        }

        // GET api/games/5
        [HttpGet("{id}")]
        public GameViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                return new GameViewModel();
            }

            gameBll.FillRounds = true;
            gameBll.FillTeams = true;

            return gameBll.GetGame(id).ToViewModel();
        }

        [HttpGet]
        public IEnumerable<GameViewModel> Get([FromQuery] int[] tourneyIds)
        {
            return gameBll.GetGamesByTourneys(tourneyIds).ToViewModel();
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
