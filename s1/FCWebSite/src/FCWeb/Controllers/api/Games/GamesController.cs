namespace FCWeb.Controllers.Api.Games
{
    using System.Collections.Generic;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNet.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        //[FromServices]
        private IGamesBll gameBll { get; set; }

        public GamesController(IGamesBll gameBll)
        {
            this.gameBll = gameBll;
        }

        // GET: api/games
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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

            return gameBll.GetGame(id).ToViewModel();
        }
    }
}
