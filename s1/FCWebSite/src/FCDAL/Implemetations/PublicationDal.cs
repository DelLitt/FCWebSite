namespace FCDAL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class PublicationDal : DalBase, IPublicationDal
    {
        public Publication GetPublication(int id)
        {
            return Context.Publication.FirstOrDefault(p => p.Id == id);
        }

        public Publication GetPublication(string urlKey)
        {
            if (string.IsNullOrWhiteSpace(urlKey)) { return null; }

            return Context.Publication.FirstOrDefault(p => p.URLKey.Equals(urlKey, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Publication> GetLatestPublications(int count, int offset)
        {
            if (count <= 0)
            {
                return new Publication[0];
            }

            return Context.Publication
                .OrderByDescending(p => p.DateDisplayed)
                .Skip(offset)
                .Take(count);
        }

        public IEnumerable<Publication> GetLatestPublications(int count, int offset, short visibility)
        {
            if (count <= 0)
            {
                return new Publication[0];
            }

            return Context.Publication
                .Where(p => (p.Visibility & visibility) != 0)
                .OrderByDescending(p => p.DateDisplayed)
                .Skip(offset)
                .Take(count);
        }

        public IEnumerable<Publication> SearchByDefault(string text)
        {
            return Context.Publication.Where(e => e.Title.Contains(text));
        }

        public int SavPublication(Publication entity)
        {
            if (entity.Id > 0)
            {
                Context.Publication.Update(entity);
            }
            else
            {
                Context.Publication.Add(entity);
            }

            Context.SaveChanges();

            return entity.Id;
        }
    }
}
