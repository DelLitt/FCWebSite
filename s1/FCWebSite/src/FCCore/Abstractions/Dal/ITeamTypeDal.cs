namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface ITeamTypeDal : IDalBase
    {
        TeamType GetTeamType(int id);
        IEnumerable<TeamType> GetTeamTypes();
    }
}
