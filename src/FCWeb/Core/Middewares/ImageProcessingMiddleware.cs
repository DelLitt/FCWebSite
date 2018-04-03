namespace FCWeb.Core.Middewares
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Configuration;
    using FCCore.Media.Image.Storage;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Logging;

    public class ImageProcessingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ImageProcessingMiddleware> logger;
        private readonly IHostingEnvironment hostingEnviroment;
        private IFileProvider fileProvider;

        private readonly ConcurrentDictionary<string, KeyValuePair<string, DateTime>> imageChache = 
            new ConcurrentDictionary<string, KeyValuePair<string, DateTime>>();

        public ImageProcessingMiddleware(RequestDelegate next, ILogger<ImageProcessingMiddleware> logger, IHostingEnvironment hostingEnviroment)
        {
            this.next = next;
            this.logger = logger;
            this.hostingEnviroment = hostingEnviroment;
            //this.fileProvider = new PhysicalFileProvider(hostingEnviroment.WebRootPath);
            this.fileProvider = this.hostingEnviroment.WebRootFileProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            logger.LogTrace(MainCfg.LogEventId, "Start image middleware...");

            string imagePath = context.Request.Path;
            DateTime now = DateTime.UtcNow;
            IFileInfo fileInfo;

            if (!string.IsNullOrWhiteSpace(imagePath) && imagePath.Length > 4)
            {
                logger.LogInformation(MainCfg.LogEventId, "The request path is '{0}'", imagePath);

                IImageStorage imageStorage = new LocalImageStorage(imagePath, fileProvider);

                fileInfo = GetImageFromCache(imageStorage, now);

                if((fileInfo == null || !fileInfo.Exists) && imageStorage.IsImageSupported)
                {
                    fileInfo = await GetImageFromStorageAsync(imageStorage, now).ConfigureAwait(false);

                    if (fileInfo == null || !fileInfo.Exists)
                    {
                        logger.LogError(MainCfg.LogEventId, "Couldn't get the image '{0}' from storage!", imagePath);

                        fileInfo = await new LocalImageStorage(MainCfg.Images.EmptyPreview, imageStorage.SizeSetting, fileProvider)
                                                .GetImageVariantAsync()
                                                .ConfigureAwait(false);
                    }
                }

                if(fileInfo != null && fileInfo.Exists)
                {
                    await context.Response.SendFileAsync(fileInfo).ConfigureAwait(false);
                    return;
                }                
            }

            await this.next.Invoke(context).ConfigureAwait(false);
        }

        private IFileInfo GetImageFromCache(IImageStorage imageStorage, DateTime requestTime)
        {
            KeyValuePair<string, DateTime> cachedPath;
            IFileInfo fileInfo = null;

            if (imageChache.TryGetValue(imageStorage.ImagePath, out cachedPath))
            {
                if (requestTime.Subtract(cachedPath.Value).Seconds > MainCfg.MaxImagesMiddlewareCacheSeconds)
                {
                    if (imageChache.TryRemove(imageStorage.ImagePath, out cachedPath))
                    {
                        logger.LogTrace(
                            MainCfg.LogEventId, 
                            "Path '{0}' was removed from the image cache due to time expiration!", 
                            imageStorage.ImagePath);
                    }
                    else
                    {
                        logger.LogInformation(
                            MainCfg.LogEventId, 
                            "Couldn't remove the path '{0}' from the image cache due to time expiration!", 
                            imageStorage.ImagePath);
                    }
                }

                fileInfo = imageStorage.GetImageDirectly(imageStorage.ImagePath);
            }

            return fileInfo;
        }

        private async Task<IFileInfo> GetImageFromStorageAsync(IImageStorage imageStorage, DateTime requestTime)
        {
            IFileInfo fileInfo = null;
            KeyValuePair<string, DateTime> cacheItem;

            fileInfo = imageStorage.GetImageDirectly();

            if (fileInfo == null || !fileInfo.Exists)
            {
                fileInfo = await imageStorage.GetImageVariantAsync();
            }

            if (fileInfo!= null && fileInfo.Exists)
            {
                if(!imageChache.TryGetValue(imageStorage.ImagePath, out cacheItem))
                {
                    cacheItem = new KeyValuePair<string, DateTime>(fileInfo.PhysicalPath, requestTime);

                    if (imageChache.TryAdd(imageStorage.ImagePath, cacheItem))
                    {
                        logger.LogInformation(
                            MainCfg.LogEventId, 
                            "Path '{0}' was succesfully aedded to image cache.", 
                            imageStorage.ImagePath);

                        if(imageChache.Count > MainCfg.MaxImagesMiddlewareCachedRequests)
                        {
                            KeyValuePair<string, KeyValuePair<string, DateTime>>? lastCachedItem = imageChache.OrderBy(c => c.Value.Value).LastOrDefault();

                            if(lastCachedItem.HasValue)
                            {
                                KeyValuePair<string, DateTime> removedCachedValue;

                                if(imageChache.TryRemove(lastCachedItem.Value.Key, out removedCachedValue))
                                {
                                    logger.LogTrace(
                                        MainCfg.LogEventId,
                                        "Path '{0}' was removed from the image cache due to the maximum value of the cahce items has been exceeded!", 
                                        imageStorage.ImagePath);
                                }
                                else
                                {
                                    logger.LogInformation(
                                        MainCfg.LogEventId,
                                        "Couldn't remove the path '{0}' from the image cache due the maximum value of the cahce items has been exceeded!", 
                                        imageStorage.ImagePath);
                                }
                            }
                        }
                    }
                    else
                    {
                        logger.LogInformation(MainCfg.LogEventId, "Couldn't add the path '{0}' to image cache.", imageStorage.ImagePath);
                    }
                }
            }

            return fileInfo;
        }

            //private static string GetContentType(string path)
            //{
            //    switch (Path.GetExtension(path).ToLower())
            //    {
            //        case ".gif": return "Image/gif";
            //        case ".jpg": return "Image/jpeg";
            //        case ".png": return "Image/png";
            //        default: break;
            //    }
            //    return string.Empty;
            //}
        }
}
