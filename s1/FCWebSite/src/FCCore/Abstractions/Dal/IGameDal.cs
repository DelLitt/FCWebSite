namespace FCCore.Abstractions.Dal
{
    using Model;
    using System;
    using System.Collections.Generic;

    public interface IGameDal : IDalBase
    {
        bool FillTeams { get; set; }
        bool FillRounds { get; set; }
        
        Game GetTeamNearestGame(int teamId, IEnumerable<int> roundIds, DateTime date);
        IEnumerable<Game> GetRoundsGames(IEnumerable<int> roundIds);
        IEnumerable<Game> GetRoundGames(int roundId);
        Game GetGame(int id);
        int SaveGame(Game entity);
    }
}
