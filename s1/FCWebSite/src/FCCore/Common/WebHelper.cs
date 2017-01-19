namespace FCCore.Common
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
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

        public static string UrlImageKeySizePrefix
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, ".{0}", MainCfg.ImageVariantsKeyword);
            }
        }

        public static string GetUrlImageFullSizeKey(string sizeKey)
        {
            return string.Format(CultureInfo.InvariantCulture, ".{0}{1}", MainCfg.ImageVariantsKeyword, sizeKey);
        }

        public static bool CheckIfPathIsAllowed(string path)
        {
            IEnumerable<string> availableRoots = MainCfg.UploadRoots;

            if (!availableRoots.Any())
            {
                throw new KeyNotFoundException("Available roots don't set!");
            }

            bool allowed = false;
            foreach (var ar in availableRoots)
            {
                if (path.StartsWith(ar) || path.StartsWith('/' + ar))
                {
                    allowed = true;
                    break;
                }
            }

            return allowed;
        }

        public static string GetImageContentType(string path)
        {
            switch (Path.GetExtension(path).ToLower())
            {
                case ".bmp": return "Image/bmp";
                case ".gif": return "Image/gif";
                case ".jpg": return "Image/jpeg";
                case ".png": return "Image/png";
                default: break;
            }

            return string.Empty;
        }
    }
}
