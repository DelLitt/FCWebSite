namespace FCCore.Abstractions.Bll
{
    using Model;
    using System;
    using System.Collections.Generic;

    public interface IGameBll
    {
        bool FillTeams { get; set; }
        bool FillRounds { get; set; }
         
        IEnumerable<Game> GetTeamActualRoundGamesOfTourneys(int teamId, IEnumerable<int> tourneyIds, DateTime date);
        IEnumerable<Game> GetTeamActualRoundGames(int teamId, IEnumerable<int> roundIds, DateTime date);
        IEnumerable<Game> GetRoundGames(int roundId);
        Game GetGame(int id);
        int SaveGame(Game entity);
    }
}
