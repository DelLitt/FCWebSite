using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCBLL.Ranking.Standings;
using FCBLL.Ranking.Standings.Decorators;
using FCCore.Abstractions.Bll;
using FCCore.Model;
using FCWeb.Core.Extensions;
using FCWeb.ViewModels;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ITableRecordBll tableRecordBll { get; set; }
        private IGameBll gameBll { get; set; }

        public ValuesController(ITableRecordBll tableRecordBll, IGameBll gameBll)
        {
            this.tableRecordBll = tableRecordBll;
            this.gameBll = gameBll;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return null;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public RankingTableViewModel Get(int id)
        {
            // IEnumerable<TableRecord> tableRecords = tableRecordBll.GetTourneyTable(id);
            gameBll.FillTeams = true;
            IEnumerable<Game> games = gameBll.GetGamesByTourney(id);

            TableBase table = new ClassicTable();
            table.BuildFromGames((short)id, games);

            return TableDecorator.SortTableByRule1(table).OrderBy(r => r.Points).ToViewModel("test");
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
