// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.api
{
    using System.Linq;
    using Microsoft.AspNet.Mvc;
    using ViewModels;
    using Microsoft.AspNet.Authorization;
    using Core;
    using FCCore.Configuration;
    using Microsoft.AspNet.Http;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    [Authorize]
    public class FileBrowserController : Controller
    {
        // GET: api/values
        [HttpGet]
        public FolderViewModel Get([FromQuery] string path)
        {
            var folderView = StorageHelper.GetFolderView(path);

            return folderView;
        }

        // POST api/values
        [HttpPost("upload")]
        public void Post()
        {
            if (Request.Form.ContainsKey("data"))
            {
                var uploadData = JsonConvert.DeserializeObject<UploadDataModelView>(Request.Form["data"]);

                //get posted file from web form
                foreach (IFormFile file in Request.Form.Files)
                {
                    var formUpload = new FormUpload(uploadData.path, MainCfg.AllowedImageExtensions);
                    formUpload.SaveFile(file);
                }
            }
        }
    }   
}
