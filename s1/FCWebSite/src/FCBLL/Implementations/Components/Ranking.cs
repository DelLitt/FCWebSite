namespace FCBLL.Implementations.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCBLL.Ranking.Standings;
    using FCBLL.Ranking.Standings.Decorators;
    using FCCore.Abstractions.Bll.Components;
    using FCCore.Common;
    using FCCore.Model;

    public class Ranking : IRanking
    {
        public IEnumerable<TableRecord> CalculateTable(int tourneyId)
        {
            var tourneyBll = new TourneyBll();
            Tourney tourney = tourneyBll.GetTourney(tourneyId);

            if(tourney == null) { return new TableRecord[] { }; }

            return CalculateTable(tourney);
        }

        public IEnumerable<TableRecord> CalculateTable(Tourney tourney)
        {
            if (tourney == null) { return new TableRecord[] { }; }

            var gameBll = new GameBll();

            IEnumerable<Game> games = gameBll.GetGamesByTourney(tourney.Id);

            short rule = tourney?.tourneyTypeId ?? 1;

            return CalculateTable(rule, games, tourney.Id);
        }

        public IEnumerable<TableRecord> CalculateTable(int rule, IEnumerable<Game> games, short tourneyId = 0)
        {
            TableBase table = CalculateTableByRule(rule, games, tourneyId);
            return SortTablePositionsByRule(rule, table);
        }

        private TableBase CalculateTableByRule(int rule, IEnumerable<Game> games, short tourneyId = 0)
        {
            TableBase table;

            switch(rule)
            {
                case 1:
                    table = new ClassicTable();
                    break;
                default:
                    table = new ClassicTable();
                    break;
            }

            table.BuildFromGames(tourneyId, games);

            return table;
        }

        private IEnumerable<TableRecord> SortTablePositionsByRule(int rule, TableBase table)
        {
            IEnumerable<TableRecord> tableRecords;

            switch (rule)
            {
                case 1:
                    tableRecords = SortPositionsByRule1(table);
                    break;
                default:
                    tableRecords = SortPositionsByRule1(table);
                    break;
            }

            return tableRecords;
        }

        private IEnumerable<TableRecord> SortPositionsByRule1(TableBase table)
        {
            Guard.CheckNull(table, nameof(table));

            var tableByPoints = new TableByPoints(table);
            var tableByWins = new TableByWins(tableByPoints);
            var tableByPrivateMatches = new TableByPrivateMatches(tableByWins);
            var tableByGoalsDifference = new TableByGoalsDifference(tableByPrivateMatches);
            var tableByGoalsScored = new TableByGoalsFor(tableByGoalsDifference);
            var tableByGoalsAgainst = new TableByGoalsAgainst(tableByGoalsScored);
            var tableByDraws = new TableByDraws(tableByGoalsAgainst);
            var tableByAlphabet = new TableByAlphabet(tableByDraws);

            tableByAlphabet.Sort();

            return tableByAlphabet.Records;
        }
    }
}
