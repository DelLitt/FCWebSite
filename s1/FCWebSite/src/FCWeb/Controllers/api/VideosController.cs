// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using FCCore.Abstractions.Bll;
    using FCWeb.Core.Extensions;
    using FCWeb.ViewModels;
    using System.Net;
    using System;
    using FCCore.Configuration;
    using Microsoft.AspNet.Authorization;
    using FCCore.Model;
    using System.Security.Claims;
    using FCCore.Common;

    [Route("api/[controller]")]
    public class VideosController : Controller
    {
        //[FromServices]
        private IVideoBll videoBll { get; set; }

        public VideosController(IVideoBll videoBll)
        {
            this.videoBll = videoBll;
        }

        // GET: api/values/latest
        [HttpGet("search")]
        public IEnumerable<VideoViewModel> Get([FromQuery] string txt)
        {
            var videoModels = new List<VideoViewModel>();

            //videoModels.Add(new VideoViewModel()
            //{
            //    id = -1,
            //    title = "Не установлено"
            //});

            //videoModels.Add(new VideoViewModel()
            //{
            //    id = 0,
            //    title = "Новое видео"
            //});

            //if (txt.Length < 3) { return videoModels; }

            IEnumerable<VideoViewModel> searchedData = videoBll.SearchVideosByTitle(txt).ToViewModel();

            videoModels.AddRange(searchedData);

            return videoModels;
        }

        //// GET: api/values/latest
        //[HttpGet("latest/{count:range(0,20)}/{offset:int?}")]
        //public IEnumerable<PublicationShortViewModel> Get(int count, int offset)
        //{
        //    return videoBll.GetMainPublications(count, offset).ToShortViewModel();
        //}

        //// GET: api/values/latest
        //[HttpGet]
        //public IEnumerable<PublicationViewModel> Get()
        //{
        //    return publicationBLL.GetMainPublications(10, 0).ToViewModel();
        //}

        // GET api/values/5
        [HttpGet("{id}")]
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
            Guid userCreated = new Guid(User.GetUserId());            
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
            video.userChanged = new Guid(User.GetUserId());
            video.DateChanged = DateTime.UtcNow;

            videoBll.SaveVideo(video);
        }

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
