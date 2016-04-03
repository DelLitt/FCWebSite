using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class PersonStatistics
    {
        public int Id { get; set; }
        public short Assists { get; set; }
        public short? CustomIntValue { get; set; }
        public short Games { get; set; }
        public short Goals { get; set; }
        public int personId { get; set; }
        public short Reds { get; set; }
        public short Substitutes { get; set; }
        public int teamId { get; set; }
        public short tourneyId { get; set; }
        public short Yellows { get; set; }

        public virtual PersonStatistics PersonStatisticsNavigation { get; set; }
        public virtual PersonStatistics InversePersonStatisticsNavigation { get; set; }
        public virtual Person person { get; set; }
        public virtual Team team { get; set; }
        public virtual Tourney tourney { get; set; }
    }
}
