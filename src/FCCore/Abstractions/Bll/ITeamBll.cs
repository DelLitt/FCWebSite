namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface ITeamBll : IFCBll
    {
        bool? Active { get; set; }
        bool FillCities { get; set; }
        bool FillMainTourney { get; set; }
        bool FillStadium { get; set; }
        bool FillTeamType { get; set; }

        Team GetTeam(int id);
        IEnumerable<Team> GetTeamsByRound(int roundId);
        IEnumerable<Team> GetTeamsByType(int teamTypeId);
        IEnumerable<Team> GetTeamsByTypeAndParent(int teamTypeId, int parentTeamId);
        IEnumerable<Team> GetTeams(IEnumerable<int> ids);
        IEnumerable<Team> GetTeams();
        IEnumerable<Team> SearchByDefault(string text);
        IEnumerable<Team> SearchByTypeDefault(string text, int typeId);
        IEnumerable<Team> SearchByDefault(string text, int roundId);
        int SaveTeam(Team entity);
    }
}
