namespace FCCore.Abstractions.Bll
{
    using Model;
    using System.Collections.Generic;

    public interface IImageGalleryBll
    {
        ImageGallery GetImageGallery(int id);
        IEnumerable<ImageGallery> GetMainImageGalleries(int count, int offset);
        IEnumerable<ImageGallery> SearchByTitle(string text);
        int SaveImageGallery(ImageGallery entity);
    }
}
