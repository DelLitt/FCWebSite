namespace FCCore.Abstractions.Bll
{
    using Model;
    using System.Collections.Generic;

    public interface IImageGalleryBll
    {
        ImageGallery GetImageGallery(int id);
        ImageGallery GetImageGallery(string urlKey);
        IEnumerable<ImageGallery> GetMainImageGalleries(int count, int offset);
        IEnumerable<ImageGallery> GetLatestImageGalleries(int count, int offset);
        IEnumerable<ImageGallery> GetLatestImageGalleries(int count, int offset, IEnumerable<string> groups);
        IEnumerable<ImageGallery> SearchByDefault(string text);
        int SaveImageGallery(ImageGallery entity);
    }
}
