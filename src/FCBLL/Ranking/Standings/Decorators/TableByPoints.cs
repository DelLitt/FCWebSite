namespace FCBLL.Ranking.Standings.Decorators
{
    using System.Collections.Generic;
    using FCCore.Model;

    public class TableByPoints : TableDecorator
    {
        public TableByPoints(TableBase table) : base(table)
        {
        }

        protected override void CalculatePriorities(IEnumerable<TableRecord> records)
        {
            foreach (TableRecord record in records)
            {
                record.PointsVirtual = record.Points;
            }
        }
    }
}
