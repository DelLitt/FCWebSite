namespace FCWeb.Controllers
{
    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.Mvc;

    [Authorize]
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
