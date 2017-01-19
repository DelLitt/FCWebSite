namespace FCCore.Media.Image.Sizing
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Common;
    using Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Model.Configuration;

    public class LocalImageSizeInfo : IImageSizeInfo
    {
        private ILogger logger = MainCfg.ServiceProvider.GetService<ILogger<LocalImageSizeInfo>>();

        private string path;

        private bool isImageSizeKeyInitialized = false;
        private string imageSizeKey = string.Empty;
        private string ImageSizeKey
        {
            get
            {
                if(!isImageSizeKeyInitialized)
                {
                    isImageSizeKeyInitialized = true;

                    string variantKey = WebHelper.UrlImageKeySizePrefix;

                    int indexOfLastLetterVariant = path.IndexOf(variantKey) + variantKey.Length;

                    if (indexOfLastLetterVariant < variantKey.Length || indexOfLastLetterVariant == path.Length + 1)
                    {
                        imageSizeKey = string.Empty;
                        return imageSizeKey;
                    }

                    int indexOfLastDot = path.LastIndexOf('.');

                    if (indexOfLastLetterVariant >= indexOfLastDot)
                    {
                        imageSizeKey = string.Empty;
                        return imageSizeKey;
                    }

                    int keyLength = indexOfLastDot - indexOfLastLetterVariant;

                    imageSizeKey = path.Substring(indexOfLastLetterVariant, keyLength);
                }

                return imageSizeKey;
            }
        }

        private bool isPathValidInitialized = false;
        private bool isPathValid = false;
        public bool IsPathValid
        {
            get
            {
                if (!isPathValidInitialized)
                {
                    isPathValidInitialized = true;

                    if(!WebHelper.CheckIfPathIsAllowed(path))
                    {
                        isPathValid = false;
                        return isPathValid;
                    }

                    if (string.IsNullOrWhiteSpace(ImageSizeKey))
                    {
                        isPathValid = false;
                        return isPathValid;
                    }

                    int lastDotPos = path.LastIndexOf('.');

                    if (lastDotPos == path.Length - 1)
                    {
                        isPathValid = false;

                        logger.LogInformation(
                            MainCfg.LogEventId,
                            "The path '{0}' contains the key '{1}' of image size has wrong extension!",
                            path,
                            ImageSizeKey);

                        return isPathValid;
                    }

                    string imageExtension = path.Substring(lastDotPos);

                    isPathValid = MainCfg.AllowedImageExtensions.Contains(imageExtension);

                    logger.LogInformation(
                        MainCfg.LogEventId,
                        "The path '{0}' contains the key '{1}' of image size has not allowed extension '{2}!",
                        path,
                        ImageSizeKey,
                        imageExtension);
                }

                return isPathValid;
            }
        }

        public string UrlFullKey
        {
            get
            {
                if (!IsPathValid)
                {
                    return string.Empty;
                }

                return WebHelper.GetUrlImageFullSizeKey(ImageSizeKey);
            }
        }

        public string OriginalPath
        {
            get
            {
                if (!IsPathValid)
                {
                    return path;
                }

                return path.Replace(UrlFullKey, string.Empty);
            }
        }

        private bool isImageSizeSettingInitialized;
        private ImageSizeSetting imageSizeSetting;
        public ImageSizeSetting ImageSizeSetting
        {
            get
            {
                if(!isImageSizeSettingInitialized)
                {
                    isImageSizeSettingInitialized = true;

                    if (imageSizeSetting == null && IsPathValid)
                    {
                        try
                        {
                            imageSizeSetting = MainCfg.ImageSizesAvailble.SingleOrDefault(s => s.Key.Equals(ImageSizeKey, StringComparison.OrdinalIgnoreCase));
                        }
                        catch(InvalidOperationException)
                        {
                            imageSizeSetting = MainCfg.ImageSizesAvailble.First(s => s.Key.Equals(ImageSizeKey, StringComparison.OrdinalIgnoreCase));

                            logger.LogError(
                                MainCfg.LogEventId, 
                                "The file 'appsettings.json' contains more than one element of image size with the same key '{0}' of the setting 'ImageSizesAvailble'!", 
                                ImageSizeKey);
                        }
                    }
                }

                return imageSizeSetting;
            }
        }

        public LocalImageSizeInfo(string path)
        {
            this.path = path.ToLower();
        }
    }
}
