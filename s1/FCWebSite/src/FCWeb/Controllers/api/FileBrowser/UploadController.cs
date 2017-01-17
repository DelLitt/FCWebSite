// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api.FileBrowser
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Core;
    using FCCore.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using FCCore.Model.Storage;
    using Microsoft.AspNetCore.Http;

    [Route("api/filebrowser/[controller]")]
    [Authorize]
    public class UploadController : Controller
    {
        // POST api/values
        [HttpPost]
        [Authorize(Roles = "admin,press")]
        public IActionResult Post()
        {
            if (Request.Form.ContainsKey("data"))
            {
                var uploadData = JsonConvert.DeserializeObject<UploadDataModelView>(Request.Form["data"]);

                if(uploadData == null)
                {
                    throw new ArgumentNullException(nameof(uploadData.path), "Data is not found in upload request body!");
                }

                if (string.IsNullOrWhiteSpace(uploadData.path))
                {
                    throw new ArgumentNullException(nameof(uploadData.path), 
                        string.Format("Parameter '{0}' is not found in upload request body!", nameof(uploadData.path)));
                }

                var savedFiles = new List<StorageFile>();

                //get posted file from web form
                foreach (IFormFile file in Request.Form.Files)
                {
                    var formUpload = new FormUpload(uploadData.path, MainCfg.AllowedImageExtensions);

                    StorageFile savedFile = formUpload.SaveFile(file);
                    savedFiles.Add(savedFile);
                }

                if(savedFiles.Any())
                {
                    return Ok(savedFiles);
                }
            }

            return new BadRequestResult();
            //return new StatusCodeResult(Convert.ToInt32(HttpStatusCode.BadRequest));
        }
    }   
}
