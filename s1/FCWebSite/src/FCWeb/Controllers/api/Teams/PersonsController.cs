namespace FCWeb.Controllers.Api.Teams
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Common;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [Route("api/teams/{teamId}/[controller]")]
    public class PersonsController : Controller
    {
        //[FromServices]
        private IPersonBll personBll { get; set; }

        public PersonsController(IPersonBll personBll)
        {
            this.personBll = personBll;
        }

        [HttpGet("{group}")]
        public IEnumerable<PersonViewModel> Get(int teamId, string group)
        {
            PersonGroup personGroup = PersonGroupHelper.FromString(group);

            personBll.FillTeams = true;
            personBll.FillCities = true;
            personBll.FillPersonRoles = true;

            IEnumerable<PersonViewModel> personsView = personBll.GetTeamPersons(teamId, personGroup).ToViewModel();

            return personsView.OrderBy(p => p.number);
        }
    }
}
