using FCCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Abstractions.Bll
{
    public interface IGamesBll
    {
        bool FillTeams { get; set; }
        bool FillRounds { get; set; }        
        IEnumerable<Game> GetTeamActualRoundGamesOfTourneys(int teamId, IEnumerable<int> tourneyIds, DateTime date);
        IEnumerable<Game> GetTeamActualRoundGames(int teamId, IEnumerable<int> roundIds, DateTime date);
        IEnumerable<Game> GetRoundGames(int roundId);
    }
}
