using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCDAL.Model;
using FCCore.Model;
using FCCore.Abstractions;
using FCCore.Abstractions.DAL;
using FCCore.Configuration;
using FCCore.Common;

namespace FCDAL.Implemetations
{
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
    }
}
