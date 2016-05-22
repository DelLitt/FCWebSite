namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface IRoundBll
    {
        bool FillTourneys { get; set; }

        Round GetRound(int id);
        IEnumerable<Round> GetRounds(IEnumerable<int> ids);
        IEnumerable<Round> GetRoundsByTourney(int tourneyId);
        IEnumerable<int> GetRoundIdsOfTourneys(IEnumerable<int> tourneyIds, int? sortByTeamId = null);
        IEnumerable<Round> SearchByNameFull(int tourneyId, string text);
    }
}
