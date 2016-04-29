// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using FCCore.Abstractions.Bll;
    using FCWeb.Core.Extensions;
    using FCWeb.ViewModels;
    using Microsoft.AspNet.Authorization;
    using System.Net;
    using FCCore.Model;
    using Core;
    using FCCore.Configuration;
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        //[FromServices]
        private IPersonBll personBll { get; set; }

        public PersonsController(IPersonBll personBll)
        {
            this.personBll = personBll;
        }

        //// GET: api/values/latest
        //[HttpGet]
        //public IEnumerable<PublicationViewModel> Get()
        //{
        //    return personBll.GetTeamPersons(3).ToViewModel();
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public PersonViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"))
                && id == 0)
            {
                return new Person().ToViewModel();
            }

            return personBll.GetPerson(id).ToViewModel();
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public void Post([FromBody]PersonViewModel personView)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            int personId = personBll.SavePerson(personView.ToBaseModel());

            // move images from temp folder
            if (!string.IsNullOrWhiteSpace(personView.image)
                && personView.tempGuid.HasValue)
            {
                string tempGuid = personView.tempGuid.ToString();
                string storagePath = MainCfg.Images.Persons.Replace("{id}", personId.ToString());
                string tempPath = MainCfg.Images.Persons.Replace("{id}", tempGuid);

                StorageHelper.MoveFromTempToStorage(storagePath, tempPath, tempGuid);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,press")]
        public void Put(int id, [FromBody]PersonViewModel personView)
        {
            if (id != personView.id)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            personBll.SavePerson(personView.ToBaseModel());
        }
    }
}
