namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;

    public interface IImageGalleryBll
    {
        Model.ImageGallery GetImageGallery(int id);
        Model.ImageGallery GetImageGallery(string urlKey);
        IEnumerable<Model.ImageGallery> GetMainImageGalleries(int count, int offset);
        IEnumerable<Model.ImageGallery> GetLatestImageGalleries(int count, int offset);
        IEnumerable<Model.ImageGallery> GetLatestImageGalleries(int count, int offset, IEnumerable<string> groups);
        IEnumerable<Model.ImageGallery> SearchByDefault(string text);
        int SaveImageGallery(Model.ImageGallery entity);
    }
}
