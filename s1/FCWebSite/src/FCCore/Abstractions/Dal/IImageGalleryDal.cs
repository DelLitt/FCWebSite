namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IImageGalleryDal : IDalBase
    {
        ImageGallery GetImageGallery(int id);
        IEnumerable<ImageGallery> GetLatestImageGalleries(int count, int offset, short visibility);
        IEnumerable<ImageGallery> SearchByTitle(string text);
        int SaveImageGallery(ImageGallery entity);
    }
}
