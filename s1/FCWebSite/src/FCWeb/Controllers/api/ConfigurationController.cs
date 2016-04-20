using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using FCWeb.ViewModels;
using FCCore.Configuration;
using FCWeb.Core.Extensions;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
        // GET: api/values
        [HttpGet]
        public AppConfigurationViewModel Get()
        {
            var appConfig = new AppConfigurationViewModel();

            appConfig.images = new ImagesConfigurationViewModel();
            appConfig.images.persons = MainCfg.Images.Persons;
            appConfig.settingsVisibility = MainCfg.SettingsVisibility.ToViewModel();

            return appConfig;
        }

        // GET api/values/5
        [HttpGet("office/{id:int?}")]
        public AppConfigurationOfficeViewModel Get(int id)
        {
            var appConfig = new AppConfigurationOfficeViewModel();

            appConfig.images = new ImagesConfigurationOfficeViewModel();
            appConfig.images.persons = MainCfg.Images.Persons;
            appConfig.images.store = MainCfg.Images.Store;
            appConfig.settingsVisibility = MainCfg.SettingsVisibility.ToViewModel();

            return appConfig;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
