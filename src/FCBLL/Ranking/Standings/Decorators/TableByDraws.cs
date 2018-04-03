namespace FCBLL.Ranking.Standings.Decorators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Model;

    public class TableByDraws : TableDecorator
    {
        public TableByDraws(TableBase table) : base(table)
        {
        }

        protected override void CalculatePriorities(IEnumerable<TableRecord> records)
        {
            foreach(TableRecord record in records)
            {
                record.PointsVirtual = record.Draws;
            }
        }
    }
}
