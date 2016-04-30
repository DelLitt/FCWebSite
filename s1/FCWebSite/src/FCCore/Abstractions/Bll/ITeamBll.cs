namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface ITeamBll
    {
        Team GetTeam(int id);
        IEnumerable<Team> SearchByName(string text);
    }
}
