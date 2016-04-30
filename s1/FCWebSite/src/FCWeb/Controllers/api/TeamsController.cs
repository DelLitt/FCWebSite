// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using FCCore.Abstractions.Bll;
    using Core.Extensions;
    using ViewModels;

    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        //[FromServices]
        private ITeamBll teamBll { get; set; }

        public TeamsController(ITeamBll teamBll)
        {
            this.teamBll = teamBll;
        }

        // GET: api/values/latest
        [HttpGet("search")]
        public IEnumerable<TeamViewModel> Get([FromQuery] string txt)
        {
            return teamBll.SearchByName(txt).ToViewModel();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TeamViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                return new TeamViewModel()
                {
                    teamTypeId = 1
                };
            }

            return teamBll.GetTeam(id).ToViewModel();
        }
    }
}
