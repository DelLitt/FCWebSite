// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Configuration;
    using FCCore.Model;
    using FCDAL.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class VideosController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        //[FromServices]
        private IVideoBll videoBll { get; set; }

        public VideosController(UserManager<ApplicationUser> userManager, IVideoBll videoBll)
        {
            this.userManager = userManager;
            this.videoBll = videoBll;
        }

        [HttpGet("{id:int}")]
        public VideoViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                DateTime utcNow = DateTime.UtcNow;

                return new VideoViewModel()
                {
                    dateDisplayed = utcNow,
                    dateChanged = utcNow,
                    dateCreated = utcNow,
                    author = MainCfg.DefaultAuthor,
                    enable = true,
                    visibility = MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News
                };
            }

            return videoBll.GetVideo(id).ToViewModel();
        }

        [HttpGet("{urlKey}")]
        public VideoViewModel Get(string urlKey)
        {
            return videoBll.GetVideo(urlKey).ToViewModel();
        }

        [HttpGet("{count:range(0,50)}/{offset:int}")]
        public IEnumerable<VideoShortViewModel> Get(int count, int offset, [FromQuery] string[] groups)
        {
            return videoBll.GetLatestVideos(count, offset, groups).ToShortViewModel();
        }

        [HttpGet("search/{method}")]
        public IEnumerable<VideoShortViewModel> Get(string method, [FromQuery] string txt)
        {
            if (method.Equals("default", StringComparison.OrdinalIgnoreCase))
            {
                return videoBll.SearchByDefault(txt).ToShortViewModel();
            }

            return videoBll.SearchByDefault(txt).ToShortViewModel();
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public void Post([FromBody]VideoViewModel videoVideo)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            Video video = videoVideo.ToBaseModel();

            // TODO: Convert user Id from Guid to String in DB
            Guid userCreated = new Guid(userManager.GetUserId(User));            
            video.userCreated = userCreated;
            video.userChanged = userCreated;

            DateTime utcNow = DateTime.UtcNow;
            video.DateCreated = utcNow;
            video.DateChanged = utcNow;

            videoBll.SaveVideo(video);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,press")]
        public void Put(int id, [FromBody]VideoViewModel videoView)
        {
            if (id != videoView.id)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            Video video = videoView.ToBaseModel();

            // TODO: Convert user Id from Guid to String in DB
            video.userChanged = new Guid(userManager.GetUserId(User));
            video.DateChanged = DateTime.UtcNow;

            videoBll.SaveVideo(video);
        }
    }
}
