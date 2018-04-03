namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface ITeamTypeBll
    {
        TeamType GetTeamType(int id);
        IEnumerable<TeamType> GetTeamTypes();
    }
}
