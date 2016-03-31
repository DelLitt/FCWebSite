namespace FCWeb.Controllers
{
    using Core;
    using FCCore.Configuration;
    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Net.Http.Headers;
    using System.IO;
    using System.Linq;

    [Authorize]
    public class OfficeController : Controller
    {
        private IHostingEnvironment hostingEnv;

        public OfficeController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult UploadFilesAjax()
        {
            long size = 0;
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                filename = hostingEnv.WebRootPath + $@"\{filename}";
                size += file.Length;

                var contentType = file.ContentType;

                using (var readStream = file.OpenReadStream())
                {
                    using (var fileStream = new FileStream(filename, FileMode.CreateNew))
                    {
                        readStream.Seek(0, SeekOrigin.Begin);
                        readStream.CopyTo(fileStream);
                    }
                }
            }
            //string message = @"{files.Count} file(s) /       { size}
            //bytes uploaded successfully!";
            return Ok("dsdsd");
        }
    }
}
