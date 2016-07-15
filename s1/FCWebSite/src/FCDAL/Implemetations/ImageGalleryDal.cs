namespace FCDAL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class ImageGalleryDal : DalBase, IImageGalleryDal
    {
        public ImageGallery GetImageGallery(int id)
        {
            return Context.ImageGallery.FirstOrDefault(g => g.Id == id);
        }

        public ImageGallery GetImageGallery(string urlKey)
        {
            if (string.IsNullOrWhiteSpace(urlKey)) { return null; }

            return Context.ImageGallery.FirstOrDefault(g => g.URLKey.Equals(urlKey, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<ImageGallery> GetLatestImageGalleries(int count, int offset)
        {
            if (count <= 0)
            {
                return new ImageGallery[0];
            }

            return Context.ImageGallery
                .OrderByDescending(g => g.DateDisplayed)
                .Skip(offset)
                .Take(count);
        }

        public IEnumerable<ImageGallery> GetLatestImageGalleries(int count, int offset, short visibility)
        {
            if (count <= 0)
            {
                return new ImageGallery[0];
            }

            return Context.ImageGallery
                .Where(g => (g.Visibility & visibility) != 0)
                .OrderByDescending(g => g.DateDisplayed)
                .Skip(offset)
                .Take(count);
        }

        public IEnumerable<ImageGallery> SearchByDefault(string text)
        {
            return Context.ImageGallery.Where(v => v.Title.Contains(text));
        }

        public int SaveImageGallery(ImageGallery entity)
        {
            if(entity.Id > 0)
            {
                Context.ImageGallery.Update(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }
            else
            {
                Context.ImageGallery.Add(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity.Id;
        }
    }
}
