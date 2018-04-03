namespace FCBLL.Ranking.Standings.Decorators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Bll.Components;
    using FCCore.Common;
    using FCCore.Common.Comparers;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.Extensions.DependencyInjection;

    public class TableByPrivateMatches : TableDecorator
    {
        private IGameBll gameBll = MainCfg.ServiceProvider.GetService<IGameBll>();

        public TableByPrivateMatches(TableBase table) : base(table)
        {
        }

        protected override void CalculatePriorities(IEnumerable<TableRecord> records)
        {
            // If previous rules haven't solved any rows, the recursion must be stopped.
            if (records.Count() == Records.Count()) { return; }

            short tourneyId = records.First().tourneyId;
            IEnumerable<int> teamIds = records.Select(rg => rg.teamId);
            IEnumerable<Game> games = gameBll.GetGamesByTourneyBetweenTeams(tourneyId, teamIds);

            if (!games.Any()) { return; }

            TableBase table = new ClassicTable();
            table.BuildFromGames(tourneyId, games);

            IRanking ranking = new Implementations.Components.Ranking();
            ranking.CalculateTable(Rule, games);

            IEnumerable<TableRecord> prioritizedTable = ranking.CalculateTable(Rule, games);

            foreach (TableRecord record in records)
            {
                record.PointsVirtual = prioritizedTable.FirstOrDefault()?.PointsVirtual ?? 0;
            }
        }

        //protected override IEnumerable<TableRecord> SpecialSort(IGrouping<short, TableRecord> recordsGroup)
        //{
        //    if(Guard.IsEmptyIEnumerable(recordsGroup)) { return new TableRecord[0]; }

        //    if(recordsGroup.Count() == Records.Count()) { return recordsGroup; }

        //    short tourneyId = recordsGroup.First().tourneyId;
        //    IEnumerable<int> teamIds = recordsGroup.Select(rg => rg.teamId);
        //    IEnumerable<Game> games = gameBll.GetGamesByTourneyBetweenTeams(tourneyId, teamIds);

        //    if (!games.Any()) { return recordsGroup; }

        //    TableBase table = new ClassicTable();
        //    table.BuildFromGames(tourneyId, games);

        //    IEnumerable<TableRecord> sortedRecords = SortTableByRule1(table);

        //    foreach(TableRecord tr in sortedRecords)
        //    {
        //        if(!tableRecordsCache.Contains(tr, new TableRecordsStoreComarer()))
        //        {
        //            tableRecordsCache.Add(tr);
        //        }
        //    }

        //    return tableRecordsCache;
        //}
    }
}
