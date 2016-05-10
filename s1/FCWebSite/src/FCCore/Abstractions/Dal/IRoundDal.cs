namespace FCCore.Abstractions.Dal
{
    using Model;
    using System.Collections.Generic;

    public interface IRoundDal : IDalBase
    {
        bool FillTourneys { get; set; }
        IEnumerable<Round> GetRounds(IEnumerable<int> ids);
        IEnumerable<int> GetRoundIdsOfTourneys(IEnumerable<int> tourneyIds, int? sortByTeamId = null);
    }
}
