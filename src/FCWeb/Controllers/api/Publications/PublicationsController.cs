﻿namespace FCWeb.Controllers.Api.Publications
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
    public class PublicationsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private IPublicationBll publicationBll { get; set; }

        public PublicationsController(UserManager<ApplicationUser> userManager, IPublicationBll publicationBll)
        {
            this.userManager = userManager;
            this.publicationBll = publicationBll;
        }

        [HttpGet("create")]
        [Authorize(Roles = "admin,press")]
        public PublicationViewModel Get()
        {
            //if (User.Identity.IsAuthenticated
            //    && (User.IsInRole("admin") || User.IsInRole("press")))
            //{
            //}

            DateTime utcNow = DateTime.UtcNow;

            return new PublicationViewModel()
            {
                dateDisplayed = utcNow,
                dateChanged = utcNow,
                dateCreated = utcNow,
                author = MainCfg.DefaultAuthor,
                enable = true,
                visibility = MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News
            };
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "admin,press")]
        public PublicationViewModel Get(int id)
        {
            return publicationBll.GetPublication(id).ToViewModel();
        }

        [HttpGet("{urlKey}")]
        [ResponseCache(VaryByQueryKeys = new string[] { "urlKey" }, Duration = Constants.Cache_ShortVaryByParamDurationSeconds)]
        public PublicationViewModel Get(string urlKey)
        {
            return publicationBll.GetPublication(urlKey).ToViewModel();
        }

        [HttpGet("{count:range(0,50)}/{offset:int}")]
        [ResponseCache(VaryByQueryKeys = new string[] { "count", "offset", "groups" }, Duration = Constants.Cache_MiddleVaryByParamDurationSeconds)]
        public IEnumerable<PublicationShortViewModel> Get(int count, int offset, [FromQuery] string[] groups)
        {
            return publicationBll.GetLatestPublications(count, offset, groups).ToShortViewModel();
        }

        [HttpGet("search/{method}")]
        public IEnumerable<PublicationShortViewModel> Get(string method, [FromQuery] string txt)
        {
            if (method.Equals("default", StringComparison.OrdinalIgnoreCase))
            {
                return publicationBll.SearchByDefault(txt).ToShortViewModel();
            }

            return publicationBll.SearchByDefault(txt).ToShortViewModel();
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public void Post([FromBody]PublicationViewModel publicationView)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            Publication publication = publicationView.ToBaseModel();

            // TODO: Convert user Id from Guid to String in DB
            Guid userCreated = new Guid(userManager.GetUserId(User));
            publication.userCreated = userCreated;
            publication.userChanged = userCreated;

            DateTime utcNow = DateTime.UtcNow;
            publication.DateCreated = utcNow;
            publication.DateChanged = utcNow;

            publicationBll.SavePublication(publication);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,press")]
        public void Put(int id, [FromBody]PublicationViewModel publicationView)
        {
            if (id != publicationView.id)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            Publication publication = publicationView.ToBaseModel();

            // TODO: Convert user Id from Guid to String in DB
            publication.userChanged = new Guid(userManager.GetUserId(User));
            publication.DateChanged = DateTime.UtcNow;

            publicationBll.SavePublication(publication);
        }
    }
}
