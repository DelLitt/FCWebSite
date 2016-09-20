namespace FCBLL.Ranking.Standings
{
    using System;
    using FCCore.Model;

    public class ClassicTable : TableBase
    {   
        public ClassicTable()
        { }

        public ClassicTable(TableRecord[] tableRecords) : base(tableRecords)
        {
        }

        protected override short PointsForWin { get { return 3; } }
        protected override short PointsForDraw { get { return 1; } }

        public override int Rule
        {
            get
            {
                return 1;
            }
        }

        public override void Sort()
        {
            SaveLog();
        }
    }
}
