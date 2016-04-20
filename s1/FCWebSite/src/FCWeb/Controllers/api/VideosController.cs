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
        [HttpGet("search/{text}")]
        public IEnumerable<VideoViewModel> Get(string text)
        {
            var videoModels = new List<VideoViewModel>();

            videoModels.Add(new VideoViewModel()
            {
                id = -1,
                title = "Не установлено"
            });

            videoModels.Add(new VideoViewModel()
            {
                id = 0,
                title = "Новое видео"
            });

            if (text.Length < 3) { return videoModels; }

            IEnumerable<VideoViewModel> searchedData = videoBll.SearchVideosByTitle(text).ToViewModel();

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
                && User.IsInRole("admin")
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
        public void Post([FromBody]VideoViewModel video)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                // return null;
            }

            videoBll.SaveVideo(video.ToViewModel());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]VideoViewModel video)
        {
            if (id != video.id)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                // return null;
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                // return null;
            }

            videoBll.SaveVideo(video.ToViewModel());
        }

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
