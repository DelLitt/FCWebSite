namespace FCCore.Abstractions.Dal
{
    using Model;
    using System.Collections.Generic;

    public interface ITourneyDal : IDalBase
    {
        bool FillRounds { get; set; }
        bool FillGames { get; set; }
        bool FillProtocols { get; set; }

        Tourney GetTourney(int tourneyId);
        Tourney GetTourneyByRoundId(int roundId);
        IEnumerable<Tourney> GetTourneys(IEnumerable<int> ids);
        IEnumerable<Tourney> GetAll();        
        IEnumerable<Tourney> SearchByNameFull(string text);
        Tourney SaveTourney(Tourney entity);
    }
}
