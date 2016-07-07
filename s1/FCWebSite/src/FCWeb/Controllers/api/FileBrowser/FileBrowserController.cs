// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FCWeb.Controllers.Api.FileBrowser
{
    using Microsoft.AspNet.Mvc;
    using ViewModels;
    using Microsoft.AspNet.Authorization;
    using Core;

    [Route("api/[controller]")]
    [Authorize]
    public class FileBrowserController : Controller
    {
        // GET: api/values
        [HttpGet]
        [Authorize(Roles = "admin,press")]
        public FolderViewModel Get([FromQuery] string path, [FromQuery] string root, [FromQuery] bool createNew)
        {
            bool allowCreate = createNew
                && User.Identity.IsAuthenticated
                && (User.IsInRole("admin") || User.IsInRole("press"));

            var folderView = StorageHelper.GetFolderView(path, root, allowCreate);

            return folderView;
        }
    }   
}
