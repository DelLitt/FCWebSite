namespace FCBLL.Ranking.Standings.Decorators
{
    using System.Collections.Generic;
    using System.Linq;
    using Builders;
    using FCCore.Common;
    using FCCore.Diagnostic.Transformation.Extensions;
    using FCCore.Model;

    public abstract class TableDecorator : TableBase
    {
        protected const short ResolvedValue = -9999;
        public TableBase Table { get; set; }

        public TableDecorator(TableBase table) : base(table.Records)
        {
            Table = table;
        }

        public override void Sort()
        {
            Table.Sort();

            log.Trace("Sorting table '{0}'.", Name);

            IEnumerable<IGrouping<short, TableRecord>> groupedRecords = GetGroupsByPointsVirtual(Table.Records);

            log.Trace("Groups count: {0}", groupedRecords.Count());
            log.Trace("Groups: [{0}]", string.Join(", ", groupedRecords.Select(g => g.Key)));

            if (groupedRecords.Count() == Records.Count()) { return; }

            foreach (IGrouping<short, TableRecord> recordsGroup in groupedRecords)
            {
                if (recordsGroup.Key == ResolvedValue) { continue; }

                IEnumerable<TableRecord> virtualRecords = GetNewRecordsWithPriorities(recordsGroup);
                FillZeroVirtualPoints(virtualRecords);

                log.Trace("Virtual point applied to group with Key {0}:", recordsGroup.Key);
                log.Trace(virtualRecords.ToTextTable());

                IEnumerable<IGrouping<short, TableRecord>> groupedRecordsInnner = GetGroupsByPointsVirtual(virtualRecords);
                log.Trace("Groups after virtual point applied: [{0}]", string.Join(", ", groupedRecordsInnner.Select(g => g.Key)));

                // No changes were applied, go to next group
                if (groupedRecordsInnner.Count() == 1)
                {
                    log.Trace("Group with Key {0} cannot be resolved by '{1}'.", recordsGroup.Key, Name);
                    continue;
                }

                // max position must be from real group. we don't know what happens with virtual records outside
                short maxPosition = recordsGroup.Max(g => g.Position);
                log.Trace("Max position: {0}", maxPosition);

                // If changes where applied, sort postions in the real table
                SetPositionsByFolowingPriorities(maxPosition, virtualRecords);
                // If changes where applied, set virtual points of single rows to -1
                SetSingleGroupsAsResolved(groupedRecordsInnner);

                log.Trace("Table after group with key {0} changes applied:", recordsGroup.Key);
                log.Trace(Records.ToTextTable());
            }

            SaveLog();
        }        

        protected IEnumerable<TableRecord> GetNewRecordsWithPriorities(IEnumerable<TableRecord> records)
        {
            IEnumerable<TableRecord> virtualRecords = TempClone(records);

            CalculatePriorities(virtualRecords);

            return virtualRecords;
        }

        protected abstract void CalculatePriorities(IEnumerable<TableRecord> records);

        protected void SetPositionsByFolowingPriorities(short maxPosition, IEnumerable<TableRecord> records)
        {
            if (Guard.IsEmptyIEnumerable(records)) { return; }

            IEnumerable<TableRecord> orderedRecords = records.OrderBy(r => r.PointsVirtual);

            foreach (TableRecord tr in orderedRecords)
            {
                tr.Position = maxPosition--;
            }

            SortRecordsAsFollowings(orderedRecords);
        }

        protected void SortRecordsAsFollowings(IEnumerable<TableRecord> records)
        {
            foreach (TableRecord tr in records)
            {
                TableRecord currentTr = Records.FirstOrDefault(r => r.teamId == tr.teamId);
                currentTr.Position = tr.Position;
            }
        }

        protected IEnumerable<IGrouping<short, TableRecord>> GetGroupsByPointsVirtual(IEnumerable<TableRecord> records)
        {
            return records.GroupBy(r => r.PointsVirtual);
        }

        private void FillZeroVirtualPoints(IEnumerable<TableRecord> records)
        {
            if (Guard.IsEmptyIEnumerable(records)) { return; }

            foreach (TableRecord record in Records)
            {
                if (record.PointsVirtual == TableRecordBuilder.NotCalculatedValue)
                {
                    TableRecord virtualRecord = records.FirstOrDefault(r => r.teamId == record.teamId);
                    if(virtualRecord != null)
                    {
                        record.PointsVirtual = virtualRecord.PointsVirtual;
                    }
                }
            }
        }

        private void SetSingleGroupsAsResolved(IEnumerable<IGrouping<short, TableRecord>> groupedRecords)
        {
            foreach (IGrouping<short, TableRecord> groupedRecord in groupedRecords)
            {
                if (groupedRecord.Count() == 1)
                {
                    TableRecord currentTr = Records.FirstOrDefault(r => r.teamId == groupedRecord.First().teamId);
                    currentTr.PointsVirtual = ResolvedValue;
                }
            }
        }        

        public static IEnumerable<TableRecord> SortTableByRule1(TableBase table)
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
