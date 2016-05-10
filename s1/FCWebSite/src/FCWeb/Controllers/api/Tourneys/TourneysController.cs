namespace FCWeb.Controllers.Api.Tourneys
{
    using System.Collections.Generic;
    using Core.Extensions;
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
        [HttpGet("{id}")]
        public TourneyViewModel Get(int id)
        {
            return tourneyBll.GetTourney(id).ToViewModel();
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
