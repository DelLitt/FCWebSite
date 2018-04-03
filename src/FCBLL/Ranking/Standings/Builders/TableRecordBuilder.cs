namespace FCBLL.Ranking.Standings.Builders
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;

    public class TableRecordBuilder
    {
        public const short NotCalculatedValue = -8888;

        private IEnumerable<Game> games;

        public short PointsForWin { get; set; } = 3;
        public short PointsForDraw { get; set; } = 1;

        public TableRecordBuilder(IEnumerable<Game> games)
        {
            Guard.CheckNull(games, nameof(games));

            this.games = games.Where(g => g.homeScore.HasValue && g.awayScore.HasValue);
        }

        public TableRecord GetTableRecord(int teamId, short tourneyId)
        {
            short winsCount = GetWinsCount(teamId);
            short drawsCount = GetDrawsCount(teamId);

            return new TableRecord()
            {
                teamId = teamId,
                tourneyId = tourneyId,
                Games = GetGamesCount(teamId),
                Wins = winsCount,
                Draws = drawsCount,
                Loses = GetLosesCount(teamId),
                GoalsFor = GetGoalsForCount(teamId),
                GoalsAgainst = GetGoalsAgainstCount(teamId),
                Points = (short)CalculatePoints(winsCount, drawsCount),
                Active = true,
                PointsVirtual = NotCalculatedValue,
                Position = 0
            };
        }

        public short GetGamesCount(int teamId)
        {
            return (short)games.Count(g => g.homeId == teamId || g.awayId == teamId);
        }

        public short GetWinsCount(int teamId)
        {
            return (short)games.Count(g => (g.homeId == teamId && g.homeScore > g.awayScore)
                                        || (g.awayId == teamId && g.homeScore < g.awayScore));
        }

        public short GetDrawsCount(int teamId)
        {
            return (short)games.Count(g => (g.homeId == teamId || g.awayId == teamId) && g.homeScore == g.awayScore);
        }

        public short GetLosesCount(int teamId)
        {
            return (short)games.Count(g => (g.homeId == teamId && g.homeScore < g.awayScore)
                                        || (g.awayId == teamId && g.homeScore > g.awayScore));
        }

        public short GetGoalsForCount(int teamId)
        {
            return (short)(games.Where(g => g.homeId == teamId).Sum(g => g.homeScore)
                         + games.Where(g => g.awayId == teamId).Sum(g => g.awayScore));
        }

        public short GetGoalsAgainstCount(int teamId)
        {
            return (short)(games.Where(g => g.homeId == teamId).Sum(g => g.awayScore)
                         + games.Where(g => g.awayId == teamId).Sum(g => g.homeScore));
        }

        public short GetPointsCount(int teamId)
        {
            return (short)CalculatePoints(GetWinsCount(teamId), GetDrawsCount(teamId));
        }

        protected virtual int CalculatePoints(int winsCount, int drawsCount)
        {
            return winsCount * PointsForWin + drawsCount * PointsForDraw;
        }
    }
}
