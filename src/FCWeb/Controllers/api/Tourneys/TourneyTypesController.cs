namespace FCWeb.Controllers.Api.Tourneys
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class TourneyTypesController : Controller
    {
        //[FromServices]
        private ITourneyTypeBll tourneyTypeBll { get; set; }

        public TourneyTypesController(ITourneyTypeBll tourneyTypeBll)
        {
            this.tourneyTypeBll = tourneyTypeBll;
        }

        [HttpGet("{id}")]
        public TourneyTypeViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                return new TourneyTypeViewModel();
            }

            return tourneyTypeBll.GetTourneyType(id).ToViewModel();
        }

        [HttpGet]
        public IEnumerable<TourneyTypeViewModel> Get()
        {
            return tourneyTypeBll.GetAll().OrderByDescending(t => t.Id).ToViewModel();
        }

        [HttpGet("search")]
        public IEnumerable<TourneyTypeViewModel> Get([FromQuery] string txt)
        {
            return tourneyTypeBll.SearchByDefault(txt).ToViewModel();
        }
    }
}
