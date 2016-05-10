﻿namespace FCCore.Abstractions.Dal
{
    using Model;
    using System.Collections.Generic;

    public interface ITourneyDal : IDalBase
    {
        Tourney GetTourney(int tourneyId);
        Tourney GetTourneyByRoundId(int roundId);
        IEnumerable<Tourney> GetAll();
        IEnumerable<Tourney> GetTourneys(IEnumerable<int> ids);
        IEnumerable<Tourney> SearchByNameFull(string text);
    }
}
