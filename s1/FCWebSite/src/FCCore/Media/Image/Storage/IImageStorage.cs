namespace FCCore.Media.Image.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.FileProviders;
    using Model.Configuration;
    using Sizing;

    public interface IImageStorage
    {
        string ImagePath { get; }
        bool IsImageSupported { get; }
        ImageSizeSetting SizeSetting { get; }
        IFileInfo GetImageOriginal();
        IFileInfo GetImageDirectly();
        IFileInfo GetImageDirectly(string path);
        Task<IFileInfo> GetImageVariantAsync();
        // void SaveImage(string path);
    }
}
