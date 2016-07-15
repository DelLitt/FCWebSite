namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IImageGalleryDal : IDalBase
    {
        ImageGallery GetImageGallery(int id);
        ImageGallery GetImageGallery(string urlKey);
        IEnumerable<ImageGallery> GetLatestImageGalleries(int count, int offset);
        IEnumerable<ImageGallery> GetLatestImageGalleries(int count, int offset, short visibility);
        IEnumerable<ImageGallery> SearchByDefault(string text);
        int SaveImageGallery(ImageGallery entity);
    }
}
