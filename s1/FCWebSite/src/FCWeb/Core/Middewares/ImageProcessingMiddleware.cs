namespace FCWeb.Core.Middewares
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Common;
    using Microsoft.AspNet.FileProviders;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class ImageProcessingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ImageProcessingMiddleware> logger;
        private readonly Dictionary<string, DateTime> imageChache = new Dictionary<string, DateTime>();

        public ImageProcessingMiddleware(RequestDelegate next, ILogger<ImageProcessingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            logger.LogInformation("Processing image image");

            string imagePath = context.Request.Path;

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                string imageContentType = GetContentType(imagePath);

                logger.LogInformation("Image path: {0}", imagePath);

                if (!string.IsNullOrWhiteSpace(imageContentType))
                {
                    logger.LogInformation("Image content type: {0}", imageContentType);

                    string physicalPath = WebHelper.ToPhysicalPath(imagePath);

                    logger.LogInformation("Physical path: {0}", physicalPath);

                    if (!imageChache.ContainsKey(imagePath))
                    {
                        // resize
                    }                    

                    if (File.Exists(physicalPath))
                    {
                        //context.Response.Clear();
                        //context.Response.ContentType = imageContentType;

                        await context.Response.SendFileAsync(physicalPath);

                        return;
                    }
                }
            }            

            await this.next.Invoke(context).ConfigureAwait(false);
        }

        private static string GetContentType(string path)
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
