namespace FCWeb.Controllers.Api.Games
{
    using System.Collections.Generic;
    using System.Net;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Configuration;
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

        //// GET: api/games
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

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
        public void Post([FromBody]GameViewModel gameView)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            int gameId = gameBll.SaveGame(gameView.ToBaseModel());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,press")]
        public void Put(int id, [FromBody]GameViewModel gameView)
        {
            if (id != gameView.id)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            gameBll.SaveGame(gameView.ToBaseModel());
        }
    }
}
