// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using FCCore.Abstractions.Bll;
    using FCWeb.Core.Extensions;
    using FCWeb.ViewModels;
    using System;
    using FCCore.Configuration;
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
        [HttpGet("latest/{count:range(0,20)}/{offset:int?}")]
        public IEnumerable<PublicationShortViewModel> Get(int count, int offset)
        {
            return publicationBll.GetMainPublications(count, offset).ToShortViewModel();
        }

        //// GET: api/values/latest
        //[HttpGet]
        //public IEnumerable<PublicationViewModel> Get()
        //{
        //    return publicationBLL.GetMainPublications(10, 0).ToViewModel();
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public PublicationViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated 
                && User.IsInRole("admin")
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

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
