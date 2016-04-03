using FCCore.Common;
using FCCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Abstractions.Dal
{
    public interface IPublicationDal : IDalBase
    {
        Publication GetPublication(int id);
        IEnumerable<Publication> GetLatestPublications(int count, int offset, short visibility);        
    }
}
