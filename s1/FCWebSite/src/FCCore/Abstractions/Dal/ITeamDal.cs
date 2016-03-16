using FCCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Abstractions.DAL
{
    public interface ITeamDal : IDalBase
    {
        IEnumerable<Team> GetTeams(IEnumerable<int> ids);
    }
}
