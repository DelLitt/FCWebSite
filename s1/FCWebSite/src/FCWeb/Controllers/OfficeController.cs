namespace FCWeb.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "admin,press")]
    public class OfficeController : Controller
    {
        public OfficeController()
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
