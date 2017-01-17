namespace FCWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using FCCore.Abstractions.Bll;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;

    [Authorize(Roles = "admin,press")]
    public class HomeController : Controller
    {
        private IPublicationBll publicationBLL;

        public HomeController(IPublicationBll publicationBLL)
        {
            this.publicationBLL = publicationBLL;
        }

        public IActionResult Index()
        {
            ViewBag.Mix = publicationBLL.GetMainPublications(5, 0);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
