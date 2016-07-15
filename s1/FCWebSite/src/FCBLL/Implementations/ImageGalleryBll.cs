namespace FCBLL.Implementations
{    
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Configuration;
    using FCCore.Helpers;
    using FCCore.Model;

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

        public ImageGallery GetImageGallery(int id)
        {
            return DalImageGallery.GetImageGallery(id);
        }

        public ImageGallery GetImageGallery(string urlKey)
        {
            return DalImageGallery.GetImageGallery(urlKey);
        }

        public IEnumerable<ImageGallery> GetLatestImageGalleries(int count, int offset)
        {
            return DalImageGallery.GetLatestImageGalleries(count, offset);
        }

        public IEnumerable<ImageGallery> GetLatestImageGalleries(int count, int offset, IEnumerable<string> groups)
        {
            var visibility = (short)VisibilityHelper.VisibilityFromStrings(groups);

            return DalImageGallery.GetLatestImageGalleries(count, offset, visibility);
        }

        public IEnumerable<ImageGallery> GetMainImageGalleries(int count, int offset)
        {
            short visibility = (short)(MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News);

            return DalImageGallery.GetLatestImageGalleries(count, offset, visibility);
        }

        public int SaveImageGallery(ImageGallery entity)
        {
            return DalImageGallery.SaveImageGallery(entity);
        }

        public IEnumerable<ImageGallery> SearchByDefault(string text)
        {
            if(string.IsNullOrWhiteSpace(text)) { return new ImageGallery[0]; }

            return DalImageGallery.SearchByDefault(text);
        }
    }
}
