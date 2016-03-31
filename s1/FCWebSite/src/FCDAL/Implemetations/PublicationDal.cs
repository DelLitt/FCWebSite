namespace FCDAL.Implemetations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.DAL;

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
    }
}
