namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface IRoundBll
    {
        bool FillTourneys { get; set; }
        IEnumerable<Round> GetRounds(IEnumerable<int> ids);
        IEnumerable<int> GetRoundIdsOfTourneys(IEnumerable<int> tourneyIds, int? sortByTeamId = null);
    }
}
