// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api.Publications
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Claims;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class PublicationsController : Controller
    {
        //[FromServices]
        private IPublicationBll publicationBll { get; set; }

        public PublicationsController(IPublicationBll publicationBll)
        {
            this.publicationBll = publicationBll;
        }

        // GET: api/values/latest
        [HttpGet("{count:range(0,50)}/{offset:int}")]
        public IEnumerable<PublicationShortViewModel> Get(int count, int offset, [FromQuery] string[] groups)
        {
            return publicationBll.GetLatestPublications(count, offset, groups).ToShortViewModel();
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        public PublicationViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
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

            return publicationBll.GetPublication(id).ToViewModel();
        }

        [HttpGet("{urlKey}")]
        public PublicationViewModel Get(string urlKey)
        {
            return publicationBll.GetPublication(urlKey).ToViewModel();
        }

        [HttpGet("search/{method}")]
        public IEnumerable<PublicationViewModel> Get(string method, [FromQuery] string txt)
        {
            if (method.Equals("default", StringComparison.OrdinalIgnoreCase))
            {
                return publicationBll.SearchByDefault(txt).ToViewModel();
            }

            return publicationBll.SearchByDefault(txt).ToViewModel();
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
            Guid userCreated = new Guid(User.GetUserId());
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
            publication.userChanged = new Guid(User.GetUserId());
            publication.DateChanged = DateTime.UtcNow;

            publicationBll.SavePublication(publication);
        }
    }
}
