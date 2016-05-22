namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using System;

    public class PublicationDal : DalBase, IPublicationDal
    {
        public IEnumerable<Publication> GetLatestPublications(int count, int offset, short visibility)
        {
            if(count <= 0)
            {
                return new Publication[0];
            }

            return Context.Publication
                .OrderByDescending(p => p.DateDisplayed)
                .Skip(offset)
                .Take(count)
                .Where(p => p.Visibility == visibility);
        }

        public Publication GetPublication(int id)
        {
            return Context.Publication.FirstOrDefault(p => p.Id == id);
        }

        public int SavPublication(Publication entity)
        {
            if (entity.Id > 0)
            {
                Context.Publication.Update(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }
            else
            {
                Context.Publication.Add(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity.Id;
        }
    }
}
