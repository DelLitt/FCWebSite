// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api.FileBrowser
{
    using System;
    using System.Collections.Generic;
    using FCCore.Common;
    using FCCore.Model.Storage;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Route("api/filebrowser/[controller]")]
    [Authorize]
    public class RemoveController : Controller
    {
        // POST api/values
        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public IActionResult Post([FromBody]IEnumerable<StorageFile> data)
        {
            if (Request.Form.ContainsKey("data"))
            {
                var filesData = JsonConvert.DeserializeObject<IEnumerable<StorageFile>>(Request.Form["data"]);
                if (filesData == null)
                {
                    throw new ArgumentNullException(nameof(filesData), nameof(filesData) + " is not found in request body!");
                }

                var result = LocalStorageHelper.RemoveFiles(filesData);

                return Ok(result);
            }

            return new BadRequestResult();
            //return new StatusCodeResult(Convert.ToInt32(HttpStatusCode.BadRequest));
        }
    }   
}
