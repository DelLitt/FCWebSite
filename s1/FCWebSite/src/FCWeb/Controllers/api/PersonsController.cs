// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using System.Net;
    using Core;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Common;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        //[FromServices]
        private IPersonBll personBll { get; set; }
        private IPersonCareerBll personCareerBll { get; set; }

        public PersonsController(IPersonBll personBll, IPersonCareerBll personCareerBll)
        {
            this.personBll = personBll;
            this.personCareerBll = personCareerBll;
        }

        //[HttpGet("all")]
        [Authorize(Roles = "admin,press")]
        public IEnumerable<PersonViewModel> Get()
        {
            return personBll.GetPersons().ToViewModel();
        }

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

            personBll.FillTeams = true;
            personBll.FillCities = true;
            personBll.FillPersonRoles = true;

            Person person = personBll.GetPerson(id);
            IEnumerable<PersonCareer> personCareers = null;

            if (person != null)
            {
                personCareerBll.FillTeams = true;
                personCareers = personCareerBll.GetPersonCareer(id);
            }

            return person.ToViewModel(personCareers.ToViewModel());
        }

        [HttpGet("search")]
        public IEnumerable<PersonViewModel> Get([FromQuery] string txt)
        {
            return personBll.SearchByDefault(txt).ToViewModel();
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
            if (personId > 0
                && !string.IsNullOrWhiteSpace(personView.image)
                && personView.tempGuid.HasValue)
            {
                string tempGuid = personView.tempGuid.ToString();
                string storagePath = MainCfg.Images.Persons.Replace("{id}", personId.ToString());
                string tempPath = MainCfg.Images.Persons.Replace("{id}", tempGuid);

                LocalStorageHelper.MoveFromTempToStorage(storagePath, tempPath, tempGuid);
            }

            SavePersonCareer(personView);
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

            SavePersonCareer(personView);
        }

        private IEnumerable<int> SavePersonCareer(PersonViewModel personView)
        {
            if (Guard.IsEmptyIEnumerable(personView.career)) { return new int[0]; }

            return personCareerBll.SavePersonCareer(personView.career.ToBaseModel());
        }
    }
}
