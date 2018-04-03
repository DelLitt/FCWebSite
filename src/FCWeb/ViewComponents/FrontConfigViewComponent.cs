namespace FCWeb.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using ViewModels.Configuration;

    [ViewComponent(Name = "FrontConfig")]
    public class FrontConfigViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return Content(JsonConvert.SerializeObject(new FrontConfiguration()));
            //return Json(new FrontConfiguration());
        }

        //public IViewComponentResult InvokeAsync()
        //{
        //    return Content(JsonConvert.SerializeObject(new FrontConfiguration()));
        //    //return Json(new FrontConfiguration());
        //}
    }
}
