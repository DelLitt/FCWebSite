﻿namespace FCCore.Abstractions.Bll
{
    using System.Collections.Generic;
    using Model;

    public interface ITourneyBll
    {
        bool FillRounds { get; set; }
        bool FillGames { get; set; }

        Tourney GetTourney(int tourneyId);
        Tourney GetTourneyByRoundId(int roundId);
        IEnumerable<Tourney> GetTourneys(IEnumerable<int> ids);
        IEnumerable<Tourney> GetAll();
        IEnumerable<Tourney> SearchByNameFull(string text);
    }
}
