namespace FCCore.Common
{
    using Configuration;
    using Microsoft.AspNet.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public static class WebHelper
    {
        public static string ToVirtualPath(string physicalPath)
        {
            if(string.IsNullOrWhiteSpace(physicalPath)) { return string.Empty; }

            var hostingEnv = MainCfg.ServiceProvider.GetService<IHostingEnvironment>();

            return physicalPath.Replace(hostingEnv.WebRootPath, string.Empty)
                .Replace(@"\", "/")
                .TrimStart('/');
        }

        public static string ToPhysicalPath(string virtualPath)
        {
            if (string.IsNullOrWhiteSpace(virtualPath)) { return string.Empty; }

            var hostingEnv = MainCfg.ServiceProvider.GetService<IHostingEnvironment>();

            return hostingEnv.MapPath(virtualPath);
        }
    }
}
