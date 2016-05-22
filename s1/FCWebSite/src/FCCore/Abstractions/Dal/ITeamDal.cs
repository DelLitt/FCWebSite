namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface ITeamDal : IDalBase
    {
        bool FillCities { get; set; }

        Team GetTeam(int id);
        IEnumerable<Team> GetTeams(IEnumerable<int> ids);
        IEnumerable<Team> GetAll();
        IEnumerable<Team> SearchByDefault(string text);
        IEnumerable<Team> SearchByDefault(string text, IEnumerable<int> teamIds);
        int SaveTeam(Team entity);
    }
}
