namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface ITeamBll
    {
        bool FillCities { get; set; }

        Team GetTeam(int id);
        IEnumerable<Team> GetTeamsByRound(int roundId);
        IEnumerable<Team> SearchByDefault(string text);
        IEnumerable<Team> SearchByDefault(string text, int roundId);
        int SaveTeam(Team entity);
    }
}
