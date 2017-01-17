namespace FCWeb.Controllers.Api
{
    using Core;
    using Core.Extensions;
    using FCCore.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

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
            appConfig.images.teams = MainCfg.Images.Teams;
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
            appConfig.images.teams = MainCfg.Images.Teams;
            appConfig.images.store = MainCfg.Images.Store;
            appConfig.settingsVisibility = MainCfg.SettingsVisibility.ToViewModel();
            appConfig.eventGroupFriendlyNames = EventHelper.FriendlyNames;

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
