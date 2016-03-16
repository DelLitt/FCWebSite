using FCCore.Common;
using FCCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Abstractions.DAL
{
    public interface IPublicationDal : IDalBase
    {
        IEnumerable<Publication> GetLatestPublications(int count, int offset, short visibility);
    }
}
