namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface ITeamDal : IDalBase
    {
        bool? Active { get; set; }
        bool FillCities { get; set; }

        Team GetTeam(int id);
        IEnumerable<Team> GetTeamsByType(int teamTypeId);
        IEnumerable<Team> GetTeamsByTypeAndParent(int teamTypeId, int parentTeamId);
        IEnumerable<Team> GetTeams(IEnumerable<int> ids);
        IEnumerable<Team> GetTeams();
        IEnumerable<Team> SearchByDefault(string text);
        IEnumerable<Team> SearchByDefault(string text, IEnumerable<int> teamIds);
        int SaveTeam(Team entity);
    }
}
