namespace FCWeb.Controllers.Api.Teams
{
    using System.Collections.Generic;
    using System.Net;
    using Core;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Team;

    [Route("api/teams/[controller]/{typeName}")]
    public class TypeController : Controller
    {
        //[FromServices]
        private ITeamBll teamBll { get; set; }

        public TypeController(ITeamBll teamBll)
        {
            this.teamBll = teamBll;
        }

        [HttpGet("{mode}")]
        public object Get(string typeName, string mode)
        {
            switch(mode.ToLowerInvariant())
            {
                case "list":
                    int teamTypeId = TeamTypeHelper.GetIdByFiendlyName(typeName);

                    teamBll.Active = true;

                    return teamBll.GetTeamsByType(teamTypeId).ToShortViewModel();
            }

            Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;

            return null;
        }

        [HttpGet]
        public IEnumerable<TeamViewModel> Get(string typeName)
        {
            int teamTypeId = TeamTypeHelper.GetIdByFiendlyName(typeName);

            teamBll.FillTeamType = true;
            teamBll.Active = true;

            return teamBll.GetTeamsByType(teamTypeId).ToViewModel();
        }

        [HttpGet("parent/{parentId}/")]
        public IEnumerable<TeamViewModel> Get(string typeName, int parentId)
        {
            int teamTypeId = TeamTypeHelper.GetIdByFiendlyName(typeName);

            teamBll.FillTeamType = true;
            teamBll.Active = true;

            return teamBll.GetTeamsByTypeAndParent(teamTypeId, parentId).ToViewModel();
        }
    }
}
