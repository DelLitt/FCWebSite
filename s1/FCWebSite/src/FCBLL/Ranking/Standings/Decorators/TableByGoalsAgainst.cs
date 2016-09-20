namespace FCBLL.Ranking.Standings.Decorators
{
    using System.Collections.Generic;
    using FCCore.Model;

    public class TableByGoalsAgainst : TableDecorator
    {
        public TableByGoalsAgainst(TableBase table) : base(table)
        {
        }

        protected override void CalculatePriorities(IEnumerable<TableRecord> records)
        {
            foreach (TableRecord record in records)
            {
                record.PointsVirtual = (short)-record.GoalsAgainst;
            }
        }
    }
}
