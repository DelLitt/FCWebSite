namespace FCCore.Common
{
    using System.IO;
    using Configuration;
    using Microsoft.AspNetCore.Hosting;
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

            if(virtualPath.StartsWith("/"))
            {
                virtualPath = virtualPath.Remove(0, 1);
            }

            virtualPath = virtualPath.Replace("/", @"\").Replace("~", string.Empty);       

            var hostingEnv = MainCfg.ServiceProvider.GetService<IHostingEnvironment>();

            string physicalPath = Path.Combine(hostingEnv.WebRootPath, virtualPath);

            return physicalPath;

            //var pathToFile = hostingEnv.ContentRootPath
            //                + Path.DirectorySeparatorChar.ToString()
            //   + "yourfolder"
            //   + Path.DirectorySeparatorChar.ToString()
            //   + "yourfilename.txt";

            //return hostingEnv.MapPath(virtualPath);
        }
    }
}
