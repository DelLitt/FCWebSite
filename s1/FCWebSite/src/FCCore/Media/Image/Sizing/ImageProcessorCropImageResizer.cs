namespace FCCore.Media.Image.Sizing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Configuration;
    using ImageProcessorCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class ImageProcessorCropImageResizer : IImageResizer
    {
        private ILogger logger = MainCfg.ServiceProvider.GetService<ILogger<ImageProcessorCropImageResizer>>();

        public void Resize(Stream inputStream, IImageSizeInfo imageSizeInfo, Stream outputStream)
        {
            var resizeOptions = new ResizeOptions()
            {
                Size = new Size(imageSizeInfo.ImageSizeSetting.Width, imageSizeInfo.ImageSizeSetting.Height),
                Mode = ResizeMode.Crop
            };

            var image = new Image(inputStream);

            image.Resize(resizeOptions)
                 .Save(outputStream);

            logger.LogInformation(
                MainCfg.LogEventId,
                "Image was successfully resized. Original size: {0}x{1}px. New size: {2}x{3}px.",
                image.Width,
                image.Height,
                imageSizeInfo.ImageSizeSetting.Width,
                imageSizeInfo.ImageSizeSetting.Height);
        }
    }
}
