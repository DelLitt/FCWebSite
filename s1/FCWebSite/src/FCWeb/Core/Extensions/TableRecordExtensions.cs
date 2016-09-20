namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;
    using ViewModels;

    public static class TableRecordExtensions
    {
        public static RankingTableViewModel ToViewModel(this IEnumerable<TableRecord> tableRecords, string tourneyName)
        {
            IEnumerable<TableRecordViewModel> rows = tableRecords == null
                ? new TableRecordViewModel[0]
                : tableRecords.Select(p => new TableRecordViewModel()
                {
                    draws = p.Draws,
                    games = p.Games,
                    goalsAgainst = p.GoalsAgainst,
                    goalsFor = p.GoalsFor,
                    loses = p.Loses,
                    points = p.Points,
                    position = p.Position,
                    team = p.Team?.Name ?? string.Empty,
                    wins = p.Wins
                });


            return new RankingTableViewModel()
            {
                name = tourneyName,
                rows = rows
            };
        }
    }
}
