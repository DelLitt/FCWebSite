namespace FCCore.Abstractions.Dal
{
    using Model;
    using System;
    using System.Collections.Generic;

    public interface IGameDal : IDalBase
    {
        bool FillTeams { get; set; }
        bool FillRounds { get; set; }

        Game GetGame(int id);
        IEnumerable<Game> GetRoundGames(int roundId);
        IEnumerable<Game> GetGamesByRounds(IEnumerable<int> roundIds);
        IEnumerable<Game> GetGamesByTourneys(IEnumerable<int> tourneyIds);
        Game GetTeamNearestGame(int teamId, IEnumerable<int> roundIds, DateTime date);                        
        int SaveGame(Game entity);
    }
}
