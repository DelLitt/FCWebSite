// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using FCCore.Abstractions.Bll;
    using Core.Extensions;
    using ViewModels;

    [Route("api/[controller]")]
    public class PersonStatusesController : Controller
    {
        //[FromServices]
        private IPersonStatusBll personStatusBll { get; set; }

        public PersonStatusesController(IPersonStatusBll personStatusBll)
        {
            this.personStatusBll = personStatusBll;
        }

        // GET: api/values/search
        [HttpGet("search")]
        public IEnumerable<PersonStatusViewModel> Get([FromQuery] string txt)
        {
            return personStatusBll.SearchByNameFull(txt).ToViewModel();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public PersonStatusViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                return new PersonStatusViewModel();
            }

            return personStatusBll.GetPersonStatus(id).ToViewModel();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<PersonStatusViewModel> Get()
        {
            return personStatusBll.GetAll().ToViewModel();
        }
    }
}
