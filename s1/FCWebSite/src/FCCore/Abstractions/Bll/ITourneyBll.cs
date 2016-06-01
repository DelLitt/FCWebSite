namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface ITourneyBll
    {
        Tourney GetTourney(int tourneyId);
        Tourney GetTourneyByRoundId(int roundId);
        IEnumerable<Tourney> GetTourneys(IEnumerable<int> ids);
        IEnumerable<Tourney> GetAll();
        IEnumerable<Tourney> SearchByNameFull(string text);
    }
}
