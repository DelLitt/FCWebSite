namespace FCCore.Media.Image.Storage
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Logging;
    using Model.Configuration;
    using Sizing;

    public class LocalImageStorage : IImageStorage
    {
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        private string path;
        private IFileProvider fileProvider;
        private IImageSizeInfo imageSizeInfo;
        private IImageResizer imageResizer = MainCfg.ServiceProvider.GetService<IImageResizer>();
        private ILogger logger = MainCfg.ServiceProvider.GetService<ILogger<LocalImageStorage>>();

        public LocalImageStorage(string path, IFileProvider fileProvider)
        {
            this.path = path;
            this.fileProvider = fileProvider;
            this.imageSizeInfo = new LocalImageSizeInfo(this.path);
        }

        public LocalImageStorage(string path, ImageSizeSetting imageSizeSetting, IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;

            var fi = new FileInfo(path);
            string fullNameWithoutExt = path.Replace(fi.Extension, string.Empty);
            this.path = string.Format(
                CultureInfo.InvariantCulture, 
                "{0}{1}{2}", 
                fullNameWithoutExt, 
                WebHelper.GetUrlImageFullSizeKey(imageSizeSetting.Key),
                fi.Extension);

            this.imageSizeInfo = new LocalImageSizeInfo(this.path);
        }

        public bool IsImageSupported
        {
            get
            {
                return imageSizeInfo.IsPathValid;
            }          
        }

        public string ImagePath { get { return path; } }

        public ImageSizeSetting SizeSetting
        {
            get
            {
                return imageSizeInfo.ImageSizeSetting;
            }
        }

        public IFileInfo GetImageOriginal()
        {
            return fileProvider.GetFileInfo(imageSizeInfo.OriginalPath);
        }

        public IFileInfo GetImageDirectly()
        {
            return GetImageDirectly(path);
        }

        public IFileInfo GetImageDirectly(string directPath)
        {
            return fileProvider.GetFileInfo(directPath);
        }

        public async Task<IFileInfo> GetImageVariantAsync()
        {
            IFileInfo originalFileInfo = GetImageOriginal();

            if (!originalFileInfo.Exists)
            {
                logger.LogError(
                    MainCfg.LogEventId, 
                    "Couldn't get the variant '{0}' of the file '{1}'. Original file doesn't exist!",
                    imageSizeInfo.ImageSizeSetting.Key,
                    imageSizeInfo.OriginalPath);

                return originalFileInfo;
            }

            string variantImagePath = GetImageVariantPath(originalFileInfo.PhysicalPath);

            string virtualVariantImagePath = WebHelper.ToVirtualPath(variantImagePath);

            IFileInfo variantFileInfo = fileProvider.GetFileInfo(virtualVariantImagePath);

            if (variantFileInfo.Exists)
            {
                return variantFileInfo;
            }

            await semaphoreSlim.WaitAsync();

            try
            {
                await AddResizedImageAsync(originalFileInfo, variantFileInfo);

                logger.LogInformation(MainCfg.LogEventId, "The resized file '{0}' was successfully saved!", variantImagePath);
            }
            finally
            {
                semaphoreSlim.Release();
            }

            return fileProvider.GetFileInfo(virtualVariantImagePath);
        }

        private async Task AddResizedImageAsync(IFileInfo originalFileInfo, IFileInfo variantFileInfo)
        {
            await Task.Run(() => { AddResizedImage(originalFileInfo, variantFileInfo); });
        }

        private void AddResizedImage(IFileInfo originalFileInfo, IFileInfo variantFileInfo)
        {
            DirectoryInfo variantDirectory = new FileInfo(variantFileInfo.PhysicalPath).Directory;

            if (!variantDirectory.Exists)
            {
                variantDirectory.Create();

                logger.LogInformation(
                    MainCfg.LogEventId,
                    "The variant directory '{0}' was successfully created!",
                    variantDirectory.FullName);
            }

            using (var outputImageStream = new FileStream(variantFileInfo.PhysicalPath, FileMode.Create))
            {
                using (Stream inputImageStream = originalFileInfo.CreateReadStream())
                {
                    imageResizer.Resize(inputImageStream, imageSizeInfo, outputImageStream);
                }
            }
        }

        private string GetImageVariantPath(string originalFilePysicalPath)
        {
            // string physicalPath = WebHelper.ToPhysicalPath(originalPath);

            var fileInfoClassic = new FileInfo(originalFilePysicalPath);

            string originalDirectoryPath = fileInfoClassic.Directory.FullName;
            string variantDirectoryName = imageSizeInfo.UrlFullKey;
            string variantDirectoryPath = Path.Combine(originalDirectoryPath, variantDirectoryName, fileInfoClassic.Name);

            return variantDirectoryPath;
        }
    }
}
