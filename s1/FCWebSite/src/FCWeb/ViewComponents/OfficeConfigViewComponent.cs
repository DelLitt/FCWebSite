namespace FCWeb.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using ViewModels.Configuration;

    [ViewComponent(Name = "OfficeConfig")]
    public class OfficeConfigViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            
            return Content(JsonConvert.SerializeObject(new OfficeConfiguration()));
            //return Json(new OfficeConfiguration());
        }
    }
}
