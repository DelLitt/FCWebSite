using FCCore.Common;
using FCCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Abstractions.DAL
{
    public interface ITourneyDal : IDalBase
    {
        Tourney GetTourney(int tourneyId);
        IEnumerable<Tourney> GetTourneys(IEnumerable<int> ids);
    }
}
