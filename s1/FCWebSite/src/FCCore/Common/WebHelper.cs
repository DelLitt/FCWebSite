using FCCore.Configuration;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Common
{
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
