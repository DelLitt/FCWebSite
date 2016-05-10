namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using System.Net;
    using Core;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Configuration;
    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class StadiumsController : Controller
    {
        //[FromServices]
        private IStadiumBll stadiumBll { get; set; }

        public StadiumsController(IStadiumBll stadiumBll)
        {
            this.stadiumBll = stadiumBll;
        }

        // GET: api/values/latest
        [HttpGet("search")]
        public IEnumerable<StadiumViewModel> Get([FromQuery] string txt)
        {
            stadiumBll.FillCities = true;
            return stadiumBll.SearchByNameFull(txt).ToViewModel();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<StadiumViewModel> Get()
        {
            stadiumBll.FillCities = true;
            return stadiumBll.GetAll().ToViewModel();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public StadiumViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                return new StadiumViewModel();
            }

            stadiumBll.FillCities = true;
            return stadiumBll.GetStadium(id).ToViewModel();
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public void Post([FromBody]StadiumViewModel stadiumView)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            int teamId = stadiumBll.SaveStadium(stadiumView.ToBaseModel());

            // move images from temp folder
            if (teamId > 0
                && !string.IsNullOrWhiteSpace(stadiumView.image)
                && stadiumView.tempGuid.HasValue)
            {
                string tempGuid = stadiumView.tempGuid.ToString();
                string storagePath = MainCfg.Images.Teams.Replace("{id}", teamId.ToString());
                string tempPath = MainCfg.Images.Teams.Replace("{id}", tempGuid);

                StorageHelper.MoveFromTempToStorage(storagePath, tempPath, tempGuid);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,press")]
        public void Put(int id, [FromBody]StadiumViewModel stadiumView)
        {
            if (id != stadiumView.id)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            stadiumBll.SaveStadium(stadiumView.ToBaseModel());
        }
    }
}
