using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class Tourney
    {
        public Tourney()
        {
            PersonCareerTourney = new HashSet<PersonCareerTourney>();
            PersonStatistics = new HashSet<PersonStatistics>();
            Round = new HashSet<Round>();
            TableRecord = new HashSet<TableRecord>();
        }

        public short Id { get; set; }
        public short? cityId { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DateStart { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }
        public short? tourneyTypeId { get; set; }

        public virtual ICollection<PersonCareerTourney> PersonCareerTourney { get; set; }
        public virtual ICollection<PersonStatistics> PersonStatistics { get; set; }
        public virtual ICollection<Round> Round { get; set; }
        public virtual ICollection<TableRecord> TableRecord { get; set; }
    }
}
