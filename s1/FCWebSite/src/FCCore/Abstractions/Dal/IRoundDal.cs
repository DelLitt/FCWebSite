namespace FCCore.Abstractions.Dal
{
    using Model;
    using System.Collections.Generic;

    public interface IRoundDal : IDalBase
    {
        bool FillTourneys { get; set; }
        bool FillGames { get; set; }

        Round GetRound(int id);
        IEnumerable<Round> GetRounds(IEnumerable<int> ids);
        IEnumerable<Round> GetRoundsByTourney(int tourneyId);
        IEnumerable<int> GetRoundIdsOfTourneys(IEnumerable<int> tourneyIds, int? sortByTeamId = null);
        IEnumerable<Round> GetRoundsOfTourneys(IEnumerable<int> tourneyIds);
        IEnumerable<Round> SearchByNameFull(int tourneyId, string text);
        Round SaveRound(Round entity);
    }
}
