namespace FCCore.Abstractions.Bll
{
    using Model;
    using System;
    using System.Collections.Generic;

    public interface IGameBll : IFCBll
    {
        bool FillTeams { get; set; }
        bool FillRounds { get; set; }
        bool FillTourneys { get; set; }
        bool FillStadiums { get; set; }

        Game GetGame(int id);
        IEnumerable<Game> GetRoundGames(int roundId);
        IEnumerable<Game> GetGamesByRounds(IEnumerable<int> roundIds);
        IEnumerable<Game> GetGamesByTourney(int tourneyId);
        IEnumerable<Game> GetGamesByTourneys(IEnumerable<int> tourneyIds);
        IEnumerable<Game> GetGamesByTourneyBetweenTeams(int tourneyId, IEnumerable<int> teamIds);
        IEnumerable<Game> GetTeamGames(int teamId, IEnumerable<int> tourneyIds, DateTime date, int daysShift);
        IEnumerable<Game> GetTeamActualRoundGamesOfTourneys(int teamId, IEnumerable<int> tourneyIds, DateTime date);
        IEnumerable<Game> GetTeamActualRoundGames(int teamId, IEnumerable<int> roundIds, DateTime date);
        IEnumerable<Game> GetTeamPrevNextGames(int teamId, IEnumerable<int> tourneyIds, DateTime date, int daysShift);
        IEnumerable<Game> GetTeamPrevNextGamesForce(int teamId, IEnumerable<int> tourneyIds, DateTime date, int daysShift);
        Game SaveGame(Game entity);
        int RemoveGame(int gameId, bool removeProtocol = true);
        bool SaveGameNote(int gameId, string note);
    }
}
