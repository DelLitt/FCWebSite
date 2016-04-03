// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using FCCore.Abstractions.Bll;
    using FCWeb.Core.Extensions;
    using FCWeb.ViewModels;

    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        //[FromServices]
        private IPersonBll personBll { get; set; }

        public PersonsController(IPersonBll personBll)
        {
            this.personBll = personBll;
        }

        //// GET: api/values/latest
        //[HttpGet]
        //public IEnumerable<PublicationViewModel> Get()
        //{
        //    return personBll.GetTeamPersons(3).ToViewModel();
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public PersonViewModel Get(int id)
        {
            return personBll.GetPerson(id).ToViewModel();
        }

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
    }
}
