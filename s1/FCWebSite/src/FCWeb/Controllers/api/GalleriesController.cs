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

        //// GET: api/values/latest
        //[HttpGet("search")]
        //public IEnumerable<VideoViewModel> Get([FromQuery] string txt)
        //{
        //    var videoModels = new List<VideoViewModel>();

        //    IEnumerable<VideoViewModel> searchedData = imageGalleryBll.SearchByTitle(txt).ToViewModel();

        //    videoModels.AddRange(searchedData);

        //    return videoModels;
        //}

        // GET: api/values/latest
        [HttpGet("latest/{count:range(0,20)}/{offset:int?}")]
        public IEnumerable<ImageGalleryShortViewModel> Get(int count, int offset)
        {
            return imageGalleryBll.GetMainImageGalleries(count, offset).ToShortViewModel();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ImageGalleryViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                DateTime utcNow = DateTime.UtcNow;

                var imageGallery = new ImageGallery()
                {                    
                    DateDisplayed = utcNow,
                    DateChanged = utcNow,
                    DateCreated = utcNow,
                    Author = MainCfg.DefaultAuthor,
                    Enable = true,
                    Visibility = MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News
                };

                return imageGallery.ToViewModel();
            }

            return imageGalleryBll.GetImageGallery(id).ToViewModel();
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
