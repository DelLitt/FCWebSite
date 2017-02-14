namespace FCWeb.Controllers.Api.Teams
{
    using System.Collections.Generic;
    using System.Net;
    using Core.Extensions;
    using Core.Extensions.ViewModel;
    using FCCore.Abstractions.Bll;
    using FCCore.Common;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Team;

    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private ITeamBll teamBll { get; set; }

        public TeamsController(ITeamBll teamBll)
        {
            this.teamBll = teamBll;
        }

        [HttpGet("create/{mode}")]
        [Authorize(Roles = "admin,press")]
        public TeamViewModel Get(bool mode)
        {
            if(!mode)
            {
                Response.StatusCode =  (int)HttpStatusCode.MethodNotAllowed;
            }

            //if (User.Identity.IsAuthenticated
            //    && (User.IsInRole("admin") || User.IsInRole("press")))
            //{
            //}

            return new Team() { teamTypeId = 1 }.ToViewModel();
        }

        [Authorize(Roles = "admin,press")]
        public IEnumerable<TeamViewModel> Get()
        {
            teamBll.FillTeamType = true;
            teamBll.FillCities = true;

            return teamBll.GetTeams().ToViewModel();
        }

        [HttpGet("{id}")]
        public TeamViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press")))
            {
                return GetForce(id);
            }

            string cacheKey = teamBll.ObjectKeyGenerator.GetStringKey(this.GetType().FullName + nameof(Get), id);
            TeamViewModel result = teamBll.Cache.GetOrCreate(cacheKey, () => { return GetForce(id); });

            return result;
        }

        private TeamViewModel GetForce(int id)
        {
            teamBll.FillCities = true;
            teamBll.FillMainTourney = true;
            teamBll.FillStadium = true;
            teamBll.FillTeamType = true;

            return teamBll.GetTeam(id)
                          .ToViewModel()
                          .FillCustomCoaches();
        }

        [HttpGet("search")]
        public IEnumerable<TeamViewModel> Get([FromQuery] string txt)
        {
            teamBll.FillTeamType = true;
            teamBll.FillCities = true;

            return teamBll.SearchByDefault(txt).ToViewModel();
        }

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
