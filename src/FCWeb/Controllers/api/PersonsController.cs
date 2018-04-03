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
        private IPersonBll personBll { get; set; }
        private IPersonCareerBll personCareerBll { get; set; }

        public PersonsController(IPersonBll personBll, IPersonCareerBll personCareerBll)
        {
            this.personBll = personBll;
            this.personCareerBll = personCareerBll;
        }

        [HttpGet("create/{mode}")]
        [Authorize(Roles = "admin,press")]
        public PersonViewModel Get(bool mode)
        {
            if (!mode)
            {
                Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            }

            //if (User.Identity.IsAuthenticated
            //    && (User.IsInRole("admin") || User.IsInRole("press")))
            //{
            //}

            return new Person().ToViewModel();
        }

        [Authorize(Roles = "admin,press")]
        public IEnumerable<PersonViewModel> Get()
        {
            return personBll.GetPersons().ToViewModel();
        }

        [HttpGet("{id}")]
        public PersonViewModel Get(int id)
        {
            if (User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press")))
            {
                return GetForce(id);
            }

            string cacheKey = personBll.ObjectKeyGenerator.GetStringKey(this.GetType().FullName + nameof(Get), id);
            PersonViewModel result = personBll.Cache.GetOrCreate(cacheKey, () => { return GetForce(id); });

            return result;
        }

        private PersonViewModel GetForce(int id)
        {
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
