using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class TableRecord
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public short Draws { get; set; }
        public short Games { get; set; }
        public short GoalsAgainst { get; set; }
        public short GoalsFor { get; set; }
        public short Loses { get; set; }
        public short Points { get; set; }
        public short PointsVirtual { get; set; }
        public short Position { get; set; }
        public int teamId { get; set; }
        public short tourneyId { get; set; }
        public short Wins { get; set; }

        public virtual Team Team { get; set; }
        public virtual Tourney tourney { get; set; }
    }
}
