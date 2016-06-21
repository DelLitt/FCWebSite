namespace FCCore.Abstractions.Bll
{
    using Model;
    using System;
    using System.Collections.Generic;

    public interface IGameBll
    {
        bool FillTeams { get; set; }
        bool FillRounds { get; set; }

        Game GetGame(int id);
        IEnumerable<Game> GetRoundGames(int roundId);
        IEnumerable<Game> GetGamesByRounds(IEnumerable<int> roundIds);
        IEnumerable<Game> GetGamesByTourneys(IEnumerable<int> tourneyIds);
        IEnumerable<Game> GetTeamActualRoundGamesOfTourneys(int teamId, IEnumerable<int> tourneyIds, DateTime date);
        IEnumerable<Game> GetTeamActualRoundGames(int teamId, IEnumerable<int> roundIds, DateTime date);    
        int SaveGame(Game entity);
        bool SaveGameNote(int gameId, string note);
    }
}
