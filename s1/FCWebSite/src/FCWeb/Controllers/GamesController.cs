using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using FCCore.Abstractions.Bll;
using FCCore.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        //[FromServices]
        private IGamesBll gameBll { get; set; }

        //[FromServices]
        private IRoundBlll roundBll { get; set; }

        public GamesController(IGamesBll gameBll, IRoundBlll roundBll)
        {
            this.gameBll = gameBll;
            this.roundBll = roundBll;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/values/5
        [HttpGet("{teamId}/slider")]
        public IEnumerable<int> Get(int teamId, [FromQuery] int[] roundId)
        {
            IEnumerable<int> roundIds = roundBll.GetRoundIdsOfTourneys(roundId, teamId);

            return roundIds;

            //var actualDate = new DateTime(2015, 11, 8);

            //IEnumerable<Game> roundGames = gameBll.GetTeamActualRoundGames(teamId, roundIds, actualDate);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
