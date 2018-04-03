namespace FCCore.Abstractions.Bll.ImageGallery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGalleryStorage
    {
        IEnumerable<string> GetImagesList();
    }
}
