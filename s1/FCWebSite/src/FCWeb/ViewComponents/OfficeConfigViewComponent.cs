namespace FCWeb.ViewComponents
{
    using Microsoft.AspNet.Mvc;
    using ViewModels.Configuration;

    [ViewComponent(Name = "OfficeConfig")]
    public class OfficeConfigViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return Json(new OfficeConfiguration());
        }
    }
}
