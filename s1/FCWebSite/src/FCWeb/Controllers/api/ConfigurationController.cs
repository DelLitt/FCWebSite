using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using FCWeb.ViewModels;
using FCCore.Configuration;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
{
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("office/{id:int?}")]
        public AppConfigurationOfficeViewModel Get(int id)
        {
            var appConfig = new AppConfigurationOfficeViewModel();

            appConfig.images = new ImagesConfigurationViewModel();
            appConfig.images.persons = MainCfg.Images.Persons;

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
