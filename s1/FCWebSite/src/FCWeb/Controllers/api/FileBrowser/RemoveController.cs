// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api.FileBrowser
{
    using System.Linq;
    using Microsoft.AspNet.Mvc;
    using ViewModels;
    using Microsoft.AspNet.Authorization;
    using Core;
    using FCCore.Configuration;
    using Microsoft.AspNet.Http;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;

    [Route("api/filebrowser/[controller]")]
    [Authorize]
    public class RemoveController : Controller
    {
        // POST api/values
        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public IActionResult Post([FromBody]IEnumerable<FileViewModel> data)
        {
            if (Request.Form.ContainsKey("data"))
            {
                var filesData = JsonConvert.DeserializeObject<IEnumerable<FileViewModel>>(Request.Form["data"]);
                if (filesData == null)
                {
                    throw new ArgumentNullException(nameof(filesData), nameof(filesData) + " is not found in request body!");
                }

                var result = StorageHelper.RemoveFiles(filesData);

                return Ok(result);
            }

            return new HttpStatusCodeResult(Convert.ToInt32(HttpStatusCode.BadRequest));
        }
    }   
}
