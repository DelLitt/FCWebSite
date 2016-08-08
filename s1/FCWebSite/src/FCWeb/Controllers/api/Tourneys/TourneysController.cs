namespace FCWeb.Controllers.Api.Tourneys
{
    using System.Collections.Generic;
    using Core.Extensions;
    using Core.ViewModelHepers;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNet.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class TourneysController : Controller
    {
        //[FromServices]
        private ITourneyBll tourneyBll { get; set; }

        public TourneysController(ITourneyBll tourneyBll)
        {
            this.tourneyBll = tourneyBll;
        }

        // GET api/values/5
        [HttpGet("{id}/{mode?}")]
        public TourneyViewModel Get(int id, string mode = null)
        {
            if(!string.IsNullOrWhiteSpace(mode))
            {
                if(mode.Equals("content", System.StringComparison.OrdinalIgnoreCase))
                {
                    tourneyBll.FillRounds = true;
                    tourneyBll.FillGames = true;
                }
            }

            TourneyViewModel tourneyVM = tourneyBll.GetTourney(id).ToViewModel();

            var tourneyVMHelper = new TourneyVMHelper(tourneyVM);
            tourneyVMHelper.FillRoundsAvailableTeams();

            return tourneyVM;
        }

        // GET api/values/5
        [HttpGet("list")]
        public IEnumerable<TourneyViewModel> Get([FromQuery] int[] tourneyIds)
        {
            return tourneyBll.GetTourneys(tourneyIds).ToViewModel();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<TourneyViewModel> Get()
        {
            return tourneyBll.GetAll().ToViewModel();
        }

        // GET: api/values/latest
        [HttpGet("search")]
        public IEnumerable<TourneyViewModel> Get([FromQuery] string txt)
        {
            return tourneyBll.SearchByNameFull(txt).ToViewModel();
        }
    }
}
