using FCCore.Common;
using FCCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Abstractions.DAL
{
    public interface IGameDal : IDalBase
    {
        bool FillTeams { get; set; }
        bool FillRounds { get; set; }
        Game GetTeamNearestGame(int teamId, IEnumerable<int> roundIds, DateTime date);
        IEnumerable<Game> GetRoundsGames(IEnumerable<int> roundIds);
        IEnumerable<Game> GetRoundGames(int roundId);
    }
}
