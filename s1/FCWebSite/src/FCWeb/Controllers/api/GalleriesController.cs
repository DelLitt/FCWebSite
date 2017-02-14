namespace FCWeb.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Common;
    using FCCore.Configuration;
    using FCCore.Extensions;
    using FCCore.Model;
    using FCDAL.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class GalleriesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private IImageGalleryBll imageGalleryBll { get; set; }

        public GalleriesController(UserManager<ApplicationUser> userManager, IImageGalleryBll imageGalleryBll)
        {
            this.userManager = userManager;
            this.imageGalleryBll = imageGalleryBll;
        }

        [HttpGet("create")]
        [Authorize(Roles = "admin,press")]
        public ImageGalleryViewModel Get()
        {
            //if (User.Identity.IsAuthenticated
            //    && (User.IsInRole("admin") || User.IsInRole("press")))
            //{
            //}

            DateTime utcNow = DateTime.UtcNow;

            return new ImageGallery()
            {
                DateDisplayed = utcNow,
                DateChanged = utcNow,
                DateCreated = utcNow,
                Author = MainCfg.DefaultAuthor,
                Enable = true,
                Visibility = MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News
            }
            .ToViewModel();
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "admin,press")]
        public ImageGalleryViewModel Get(int id)
        {
            return imageGalleryBll.GetImageGallery(id).ToViewModel();
        }

        [HttpGet("{urlKey}")]
        [ResponseCache(VaryByQueryKeys = new string[] { "urlKey" }, Duration = 300)]
        public ImageGalleryViewModel Get(string urlKey)
        {
            return imageGalleryBll.GetImageGallery(urlKey).ToViewModel();
        }

        [HttpGet("{count:range(0,50)}/{offset:int}")]
        [ResponseCache(VaryByQueryKeys = new string[] { "count", "offset", "groups" }, Duration = 180)]
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
            Guid userCreated = new Guid(userManager.GetUserId(User));
            imageGallery.userCreated = userCreated;
            imageGallery.userChanged = userCreated;

            DateTime utcNow = DateTime.UtcNow;
            imageGallery.DateCreated = utcNow;
            imageGallery.DateChanged = utcNow;

            int imageGalleryId = imageGalleryBll.SaveImageGallery(imageGallery);

            // move images from temp folder
            if (imageGalleryId > 0 && imageGalleryView.createNew)
            {
                string tempFolderName = imageGallery.GetGalleryUniqueDir(imageGalleryView.tempGuid, true);
                string tempPath = imageGalleryView.path;
                string storagePath = imageGallery.GetGalleryUniquePath();

                LocalStorageHelper.MoveFromTempToStorage(storagePath, tempPath, tempFolderName);
            }
        }

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
            imageGallery.userChanged = new Guid(userManager.GetUserId(User));
            imageGallery.DateChanged = DateTime.UtcNow;

            imageGalleryBll.SaveImageGallery(imageGallery);
        }
    }
}
