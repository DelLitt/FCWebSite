using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using FCCore.Abstractions.Bll;
using FCWeb.ViewModels;
using FCWeb.Core.Extensions;
using FCCore.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    [Route("api/[controller]")]
    public class RankingsController : Controller
    {
        //[FromServices]
        private ITableRecordBll tableRecordBll { get; set; }
        //[FromServices]
        private ITourneyBll tourneyBll { get; set; }

        public RankingsController(ITableRecordBll tableRecordBll, ITourneyBll tourneyBll)
        {
            this.tableRecordBll = tableRecordBll;
            this.tourneyBll = tourneyBll;
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        public RankingTableViewModel Get(int id)
        {
            Tourney tourney = tourneyBll.GetTourney(id);
            string tourneyName = tourney?.Name ?? string.Empty;

            tableRecordBll.FillTeams = true;
            return tableRecordBll.GetTourneyTable(id).ToViewModel(tourneyName);
        }




        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
