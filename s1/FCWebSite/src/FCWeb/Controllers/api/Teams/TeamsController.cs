namespace FCWeb.Controllers.Api.Teams
{
    using System.Collections.Generic;
    using System.Net;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Common;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.Mvc;
    using ViewModels.Team;

    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        //[FromServices]
        private ITeamBll teamBll { get; set; }

        public TeamsController(ITeamBll teamBll)
        {
            this.teamBll = teamBll;
        }

        [Authorize(Roles = "admin,press")]
        public IEnumerable<TeamViewModel> Get()
        {
            return teamBll.GetTeams().ToViewModel();
        }

        // GET: api/values/latest
        [HttpGet("search")]
        public IEnumerable<TeamViewModel> Get([FromQuery] string txt)
        {
            teamBll.FillCities = true;

            return teamBll.SearchByDefault(txt).ToViewModel();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TeamViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                return new Team()
                {
                    teamTypeId = 1
                }
                .ToViewModel();
            }

            teamBll.FillCities = true;

            return teamBll.GetTeam(id).ToViewModel();
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public void Post([FromBody]TeamViewModel teamView)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            int teamId = teamBll.SaveTeam(teamView.ToBaseModel());

            // move images from temp folder
            if (teamId > 0
                && !string.IsNullOrWhiteSpace(teamView.image)
                && teamView.tempGuid.HasValue)
            {
                string tempGuid = teamView.tempGuid.ToString();
                string storagePath = MainCfg.Images.Teams.Replace("{id}", teamId.ToString());
                string tempPath = MainCfg.Images.Teams.Replace("{id}", tempGuid);

                LocalStorageHelper.MoveFromTempToStorage(storagePath, tempPath, tempGuid);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,press")]
        public void Put(int id, [FromBody]TeamViewModel teamView)
        {
            if (id != teamView.id)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            teamBll.SaveTeam(teamView.ToBaseModel());
        }
    }
}
