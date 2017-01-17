namespace FCWeb.Controllers.Api
{
    using System.Collections.Generic;
    using System.Net;
    using Core.Extensions;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [Route("api/[controller]")]
    public class PersonCareersController : Controller
    {
        //[FromServices]
        private IPersonCareerBll personCareerBll { get; set; }

        public PersonCareersController(IPersonCareerBll personCareerBll)
        {
            this.personCareerBll = personCareerBll;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IEnumerable<PersonCareerViewModel> Get(int id)
        {
            return personCareerBll.GetPersonCareer(id).ToViewModel();
        }

        //// POST api/values
        //[HttpPost]
        //[Authorize(Roles = "admin,press")]
        //public void Post([FromBody]PersonViewModel personView)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        return;
        //    }

        //    int personId = personBll.SavePerson(personView.ToBaseModel());

        //    // move images from temp folder
        //    if (!string.IsNullOrWhiteSpace(personView.image)
        //        && personView.tempGuid.HasValue)
        //    {
        //        string tempGuid = personView.tempGuid.ToString();
        //        string storagePath = MainCfg.Images.Persons.Replace("{id}", personId.ToString());
        //        string tempPath = MainCfg.Images.Persons.Replace("{id}", tempGuid);

        //        StorageHelper.MoveFromTempToStorage(storagePath, tempPath, tempGuid);
        //    }
        //}
    }
}
