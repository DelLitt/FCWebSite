namespace FCCore.Abstractions.Dal
{
    using Model;
    using System;
    using System.Collections.Generic;

    public interface IGameDal : IDalBase
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
        IEnumerable<Game> GetTeamGames(int teamId, IEnumerable<int> tourneyIds, DateTime dateStart, DateTime dateEnd);
        Game GetTeamNearestGame(int teamId, IEnumerable<int> roundIds, DateTime date);
        Game SaveGame(Game entity);
        int RemoveGame(int gameId, bool removeProtocol = true);
    }
}
