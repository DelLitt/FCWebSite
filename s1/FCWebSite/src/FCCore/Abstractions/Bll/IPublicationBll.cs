using FCCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Abstractions.Bll
{
    public interface IPublicationBll
    {
        IEnumerable<Publication> GetMainPublications(int count, int offset);
    }
}
