namespace FCCore.Media.Image.Sizing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Model.Configuration;

    public interface IImageSizeInfo
    {
        bool IsPathValid { get; }
        string UrlFullKey { get; }
        string OriginalPath { get; }
        ImageSizeSetting ImageSizeSetting { get; }
    }
}
