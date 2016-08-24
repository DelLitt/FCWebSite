namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNet.Mvc;
    using ViewModels.Team;

    [Route("api/[controller]")]
    public class TeamTypesController : Controller
    {
        //[FromServices]
        private ITeamTypeBll teamTypesBll { get; set; }

        public TeamTypesController(ITeamTypeBll teamTypesBll)
        {
            this.teamTypesBll = teamTypesBll;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TeamTypeViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                return new TeamTypeViewModel();
            }

            return teamTypesBll.GetTeamType(id).ToViewModel();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<TeamTypeViewModel> Get()
        {
            return teamTypesBll.GetTeamTypes().ToViewModel();
        }
    }
}
