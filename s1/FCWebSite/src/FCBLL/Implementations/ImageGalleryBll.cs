namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Configuration;
    using FCCore.Helpers;

    public class ImageGalleryBll : FCBllBase, IImageGalleryBll
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

        public FCCore.Model.ImageGallery GetImageGallery(int id)
        {
            return DalImageGallery.GetImageGallery(id);
        }

        public FCCore.Model.ImageGallery GetImageGallery(string urlKey)
        {
            string cacheKey = GetStringKey(nameof(GetImageGallery), urlKey);

            FCCore.Model.ImageGallery result = Cache.GetOrCreate(cacheKey, () => { return DalImageGallery.GetImageGallery(urlKey); });

            return result;
        }

        public IEnumerable<FCCore.Model.ImageGallery> GetLatestImageGalleries(int count, int offset)
        {
            return DalImageGallery.GetLatestImageGalleries(count, offset);
        }

        public IEnumerable<FCCore.Model.ImageGallery> GetLatestImageGalleries(int count, int offset, IEnumerable<string> groups)
        {
            string cacheKey = GetStringKey(nameof(GetLatestImageGalleries), count, offset, groups);

            IEnumerable<FCCore.Model.ImageGallery> result = Cache.GetOrCreate(cacheKey, () => { return GetLatestImageGalleriesForce(count, offset, groups); });

            return result;
        }

        public IEnumerable<FCCore.Model.ImageGallery> GetLatestImageGalleriesForce(int count, int offset, IEnumerable<string> groups)
        {
            var visibility = (short)VisibilityHelper.VisibilityFromStrings(groups);
            return DalImageGallery.GetLatestImageGalleries(count, offset, visibility);
        }

        public IEnumerable<FCCore.Model.ImageGallery> GetMainImageGalleries(int count, int offset)
        {
            string cacheKey = GetStringMethodKey(nameof(GetMainImageGalleries), count, offset);            

            IEnumerable<FCCore.Model.ImageGallery> result = 
                Cache.GetOrCreate(cacheKey, () => { return GetMainImageGalleriesForce(count, offset); });

            return result;
        }

        public IEnumerable<FCCore.Model.ImageGallery> GetMainImageGalleriesForce(int count, int offset)
        {
            short visibility = (short)(MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News);
            return DalImageGallery.GetLatestImageGalleries(count, offset, visibility);
        }

        public int SaveImageGallery(FCCore.Model.ImageGallery entity)
        {
            return DalImageGallery.SaveImageGallery(entity);
        }

        public IEnumerable<FCCore.Model.ImageGallery> SearchByDefault(string text)
        {
            if(string.IsNullOrWhiteSpace(text)) { return new FCCore.Model.ImageGallery[0]; }

            return DalImageGallery.SearchByDefault(text);
        }
    }
}
