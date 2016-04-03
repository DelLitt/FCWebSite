using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCDAL.Model;
using FCCore.Model;
using FCCore.Abstractions.Dal;

namespace FCDAL.Implemetations
{
    public class TeamDal : DalBase, ITeamDal
    {
        public IEnumerable<Team> GetTeams(IEnumerable<int> ids)
        {
            if(ids == null) { return new Team[0]; }

            return Context.Team.Where(t => ids.Contains(t.Id));
        }
    }
}
