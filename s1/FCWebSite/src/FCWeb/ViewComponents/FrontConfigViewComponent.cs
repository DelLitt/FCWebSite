namespace FCWeb.ViewComponents
{
    using Microsoft.AspNet.Mvc;
    using ViewModels.Configuration;

    [ViewComponent(Name = "FrontConfig")]
    public class FrontConfigViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return Json(new FrontConfiguration());
        }
    }
}
