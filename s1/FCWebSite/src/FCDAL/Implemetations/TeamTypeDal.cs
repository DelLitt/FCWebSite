namespace FCDAL.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using FCCore.Common;
    using Exceptions;
    using System;

    public class TeamTypeDal : DalBase, ITeamTypeDal
    {
        public TeamType GetTeamType(int id)
        {
            return Context.TeamType.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TeamType> GetTeamTypes()
        {
            return Context.TeamType.Take(LimitEntitiesSelections);
        }
    }
}
