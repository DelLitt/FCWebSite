namespace FCBLL.Implementations
{
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using System.Collections.Generic;
    using FCCore.Model;
    using FCCore.Configuration;

    public class ImageGalleryBll : IImageGalleryBll
    {
        private IImageGalleryDal dalImageGallery;
        private IImageGalleryDal DalImageGallery
        {
            get
            {
                if (dalImageGallery == null)
                {
                    dalImageGallery = DALFactory.Create<IImageGalleryDal>();
                }

                return dalImageGallery;
            }
        }

        public IEnumerable<ImageGallery> GetMainImageGalleries(int count, int offset)
        {
            short visibility = (short)(MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News);
            return DalImageGallery.GetLatestImageGalleries(count, offset, visibility);
        }

        public ImageGallery GetImageGallery(int id)
        {
            return DalImageGallery.GetImageGallery(id);
        }

        public int SaveImageGallery(ImageGallery entity)
        {
            return DalImageGallery.SaveImageGallery(entity);
        }

        public IEnumerable<ImageGallery> SearchByTitle(string text)
        {
            if(string.IsNullOrWhiteSpace(text)) { return new ImageGallery[0]; }

            return DalImageGallery.SearchByTitle(text);
        }
    }
}
