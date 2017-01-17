namespace FCWeb.Controllers.Api.Tourneys
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Core.Extensions;
    using Core.ViewModelHepers;
    using FCCore.Abstractions.Bll;
    using FCCore.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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
            return tourneyBll.GetAll().OrderByDescending(t => t.Id).ToViewModel();
        }

        // GET: api/values/latest
        [HttpGet("search")]
        public IEnumerable<TourneyViewModel> Get([FromQuery] string txt)
        {
            return tourneyBll.SearchByNameFull(txt).ToViewModel();
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public TourneyViewModel Post([FromBody]TourneyViewModel tourneyView)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            return SaveTourney(tourneyView);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,press")]
        public TourneyViewModel Put(int id, [FromBody]TourneyViewModel tourneyView)
        {
            if (id != tourneyView.id)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            return SaveTourney(tourneyView);
        }

        private TourneyViewModel SaveTourney(TourneyViewModel tourneyView)
        {
            Tourney tourney = tourneyView.ToBaseModel();
            Tourney savedTourney = tourneyBll.SaveTourney(tourney);

            return savedTourney.ToViewModel();
        }
    }
}
