// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api
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

    [Route("api/[controller]")]
    [Authorize]
    public class FileBrowserController : Controller
    {
        // GET: api/values
        [HttpGet]
        public FolderViewModel Get([FromQuery] string path, [FromQuery] string root)
        {
            var folderView = StorageHelper.GetFolderView(path, root);

            return folderView;
        }

        // POST api/values
        [HttpPost("upload")]
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

                var savedFiles = new List<FileViewModel>();

                //get posted file from web form
                foreach (IFormFile file in Request.Form.Files)
                {
                    var formUpload = new FormUpload(uploadData.path, MainCfg.AllowedImageExtensions);

                    FileViewModel savedFile = formUpload.SaveFile(file);
                    savedFiles.Add(savedFile);
                }

                if(savedFiles.Any())
                {
                    return Ok(savedFiles);
                }
            }

            return new HttpStatusCodeResult(Convert.ToInt32(HttpStatusCode.BadRequest));
        }
    }   
}
