namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class ImageGalleryDal : DalBase, IImageGalleryDal
    {
        public IEnumerable<ImageGallery> GetLatestImageGalleries(int count, int offset, short visibility)
        {
            if (count <= 0)
            {
                return new ImageGallery[0];
            }

            return Context.ImageGallery
                .Where(p => p.Visibility == visibility)
                .OrderByDescending(p => p.DateDisplayed)
                .Skip(offset)
                .Take(count);
        }

        public ImageGallery GetImageGallery(int id)
        {
            return Context.ImageGallery.FirstOrDefault(p => p.Id == id);
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

        public IEnumerable<ImageGallery> SearchByTitle(string text)
        {
            return Context.ImageGallery.Where(v => v.Title.Contains(text));
        }
    }
}
