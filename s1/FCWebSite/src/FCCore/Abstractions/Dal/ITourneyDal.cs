namespace FCCore.Abstractions.Dal
{
    using Model;
    using System.Collections.Generic;

    public interface ITourneyDal : IDalBase
    {
        Tourney GetTourney(int tourneyId);
        Tourney GetTourneyByRoundId(int roundId);
        IEnumerable<Tourney> GetTourneys(IEnumerable<int> ids);
        IEnumerable<Tourney> GetAll();        
        IEnumerable<Tourney> SearchByNameFull(string text);
    }
}
