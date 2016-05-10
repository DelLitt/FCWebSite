namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface ITeamDal : IDalBase
    {
        Team GetTeam(int id);
        IEnumerable<Team> GetTeams(IEnumerable<int> ids);
        IEnumerable<Team> SearchByName(string text);
        int SaveTeam(Team entity);
    }
}
