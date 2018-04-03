using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class Team
    {
        public Team()
        {
            Game = new HashSet<Game>();
            GameNavigation = new HashSet<Game>();
            Person = new HashSet<Person>();
            PersonCareer = new HashSet<PersonCareer>();
            PersonStatistics = new HashSet<PersonStatistics>();
            ProtocolRecord = new HashSet<ProtocolRecord>();
        }

        public int Id { get; set; }
        public bool Active { get; set; }
        public string Address { get; set; }
        public int? cityId { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public short? mainTourneyId { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }
        public string NamePre { get; set; }
        public int? stadiumId { get; set; }
        public short teamTypeId { get; set; }
        public string WebSite { get; set; }

        public virtual ICollection<Game> Game { get; set; }
        public virtual ICollection<Game> GameNavigation { get; set; }
        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<PersonCareer> PersonCareer { get; set; }
        public virtual ICollection<PersonStatistics> PersonStatistics { get; set; }
        public virtual ICollection<ProtocolRecord> ProtocolRecord { get; set; }
        public virtual ICollection<TableRecord> TableRecord { get; set; }
        public virtual City city { get; set; }
        public virtual Tourney mainTourney { get; set; }
        public virtual Stadium stadium { get; set; }
        public virtual TeamType teamType { get; set; }
    }
}
