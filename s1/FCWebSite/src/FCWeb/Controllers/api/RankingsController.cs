namespace FCWeb.Controllers.Api
{
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Model;
    using Microsoft.AspNet.Mvc;
    using ViewModels;

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
    }
}
