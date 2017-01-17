namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        //[FromServices]
        private ICityBll cityBll { get; set; }

        public CitiesController(ICityBll cityBll)
        {
            this.cityBll = cityBll;
        }

        // GET: api/values/search
        [HttpGet("search")]
        public IEnumerable<CityViewModel> Get([FromQuery] string txt)
        {
            return cityBll.SearchByNameFull(txt).ToViewModel();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public CityViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                return new CityViewModel();
            }

            return cityBll.GetCity(id).ToViewModel();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<CityViewModel> Get()
        {
            return cityBll.GetAll().ToViewModel();
        }
    }
}
