namespace FCBLL.Implementations.ImageGallery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Abstractions.Bll.ImageGallery;
    using FCCore.Common;
    using FCCore.Model;

    public class GalleryStorageFactory : IGalleryStorageFactory
    {
        public IGalleryStorage Create(ImageGallery imageGallery)
        {
            Guard.CheckNull(imageGallery, nameof(imageGallery));

            // TODO: Implement logic of selection different implementation

            return new LocalGalleryStorage(imageGallery);
        }
    }
}
