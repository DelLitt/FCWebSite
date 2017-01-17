[assembly: System.Runtime.Versioning.TargetFramework(".NETFramework,Version=v4.6")]

namespace FCWeb.Core
{
    using System.Globalization;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public class LanguageActionFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public LanguageActionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("LanguageActionFilter");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string culture = context.RouteData.Values["culture"].ToString();
            _logger.LogInformation($"Setting the culture from the URL: {culture}");

#if DNX451
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
#elif DNXCORE50
            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);

            // https://damienbod.com/2015/10/21/asp-net-5-mvc-6-localization/
#endif
            base.OnActionExecuting(context);
        }
    }
}
