namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using FCCore.Abstractions.Bll;
    using Core.Extensions;
    using ViewModels;

    [Route("api/[controller]")]
    public class PersonRolesController : Controller
    {
        //[FromServices]
        private IPersonRoleBll personRoleBll { get; set; }

        public PersonRolesController(IPersonRoleBll personRoleBll)
        {
            this.personRoleBll = personRoleBll;
        }

        // GET: api/values/search
        [HttpGet("search")]
        public IEnumerable<PersonRoleViewModel> Get([FromQuery] string txt)
        {
            return personRoleBll.SearchByNameFull(txt).ToViewModel();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public PersonRoleViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                return new PersonRoleViewModel();
            }

            return personRoleBll.GetPersonRole(id).ToViewModel();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<PersonRoleViewModel> Get()
        {
            return personRoleBll.GetAll().ToViewModel();
        }
    }
}
