// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api.FileBrowser
{
    using Microsoft.AspNetCore.Mvc;
    using FCCore.Model.Storage;
    using FCCore.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;
    using System;
    using FCCore.Diagnostic.Logging;

    [Route("api/[controller]")]
    [Authorize]
    public class FileBrowserController : Controller
    {
        private ILogger<FileBrowserController> logger { get; set; }

        public FileBrowserController(ILogger<FileBrowserController> logger)
        {
            this.logger = logger;
        }

        // GET: api/values
        [HttpGet]
        [Authorize(Roles = "admin,press")]
        public StorageFolder Get([FromQuery] string path, [FromQuery] string root, [FromQuery] bool createNew)
        {
            bool allowCreate = createNew
                && User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"));

            StorageFolder folderView = null;

            logger.LogInformationHook($"Get files for file browser. Path: {path}. Root: {root}. Create new: {createNew}.");

            try
            {
                folderView = LocalStorageHelper.GetFolderView(path, root, allowCreate);
            }
            catch(Exception ex)
            {
                logger.LogErrorHook(ex.ToString());
            }

            return folderView;
        }
    }   
}
