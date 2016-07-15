// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Claims;
    using Core;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class GalleriesController : Controller
    {
        //[FromServices]
        private IImageGalleryBll imageGalleryBll { get; set; }

        public GalleriesController(IImageGalleryBll imageGalleryBll)
        {
            this.imageGalleryBll = imageGalleryBll;
        }

        [HttpGet("{id:int}")]
        public ImageGalleryViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                DateTime utcNow = DateTime.UtcNow;

                return new ImageGalleryViewModel()
                {
                    dateDisplayed = utcNow,
                    dateChanged = utcNow,
                    dateCreated = utcNow,
                    author = MainCfg.DefaultAuthor,
                    enable = true,
                    visibility = MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News
                };
            }

            return imageGalleryBll.GetImageGallery(id).ToViewModel();
        }

        [HttpGet("{urlKey}")]
        public ImageGalleryViewModel Get(string urlKey)
        {
            return imageGalleryBll.GetImageGallery(urlKey).ToViewModel();
        }

        [HttpGet("{count:range(0,50)}/{offset:int}")]
        public IEnumerable<ImageGalleryShortViewModel> Get(int count, int offset, [FromQuery] string[] groups)
        {
            return imageGalleryBll.GetLatestImageGalleries(count, offset, groups).ToShortViewModel();
        }

        [HttpGet("search/{method}")]
        public IEnumerable<ImageGalleryShortViewModel> Get(string method, [FromQuery] string txt)
        {
            if (method.Equals("default", StringComparison.OrdinalIgnoreCase))
            {
                return imageGalleryBll.SearchByDefault(txt).ToShortViewModel();
            }

            return imageGalleryBll.SearchByDefault(txt).ToShortViewModel();
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public void Post([FromBody]ImageGalleryViewModel imageGalleryView)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            ImageGallery imageGallery = imageGalleryView.ToBaseModel();

            // TODO: Convert user Id from Guid to String in DB
            Guid userCreated = new Guid(User.GetUserId());
            imageGallery.userCreated = userCreated;
            imageGallery.userChanged = userCreated;

            DateTime utcNow = DateTime.UtcNow;
            imageGallery.DateCreated = utcNow;
            imageGallery.DateChanged = utcNow;

            int imageGalleryId = imageGalleryBll.SaveImageGallery(imageGallery);

            // move images from temp folder
            if (imageGalleryId > 0 && imageGalleryView.createNew)
            {
                string tempGuid = imageGalleryView.tempGuid.ToString();
                string tempPath = imageGalleryView.path;
                string storagePath = imageGallery.GetGalleryUniquePath();

                StorageHelper.MoveFromTempToStorage(storagePath, tempPath, tempGuid);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,press")]
        public void Put(int id, [FromBody]ImageGalleryViewModel imageGalleryView)
        {
            if (id != imageGalleryView.id)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            ImageGallery imageGallery = imageGalleryView.ToBaseModel();

            // TODO: Convert user Id from Guid to String in DB
            imageGallery.userChanged = new Guid(User.GetUserId());
            imageGallery.DateChanged = DateTime.UtcNow;

            imageGalleryBll.SaveImageGallery(imageGallery);
        }
    }
}
