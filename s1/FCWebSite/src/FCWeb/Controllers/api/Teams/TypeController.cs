namespace FCWeb.Controllers.Api.Teams
{
    using System.Collections.Generic;
    using Core;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNet.Mvc;
    using ViewModels.Team;

    [Route("api/teams/[controller]")]
    public class TypeController : Controller
    {
        //[FromServices]
        private ITeamBll teamBll { get; set; }

        public TypeController(ITeamBll teamBll)
        {
            this.teamBll = teamBll;
        }

        [HttpGet("{typeName}")]
        public IEnumerable<TeamViewModel> Get(string typeName)
        {
            int teamTypeId = TeamTypeHelper.GetIdByFiendlyName(typeName);

            teamBll.Active = true;
            return teamBll.GetTeamsByType(teamTypeId).ToViewModel();
        }

        [HttpGet("{typeName}/parent/{parentId}/")]
        public IEnumerable<TeamViewModel> Get(string typeName, int parentId)
        {
            int teamTypeId = TeamTypeHelper.GetIdByFiendlyName(typeName);

            teamBll.Active = true;
            return teamBll.GetTeamsByTypeAndParent(teamTypeId, parentId).ToViewModel();
        }
    }
}
