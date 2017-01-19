namespace FCCore.Media.Image.Sizing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IImageResizer
    {
        void Resize(Stream inputStream, IImageSizeInfo imageSizeInfo, Stream outputStream);
    }
}
