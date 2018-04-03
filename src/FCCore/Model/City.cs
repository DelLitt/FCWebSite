using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class City
    {
        public City()
        {
            Person = new HashSet<Person>();
            Stadium = new HashSet<Stadium>();
            Team = new HashSet<Team>();
            Tourney = new HashSet<Tourney>();
        }

        public int Id { get; set; }
        public short countryId { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }

        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<Stadium> Stadium { get; set; }
        public virtual ICollection<Team> Team { get; set; }
        public virtual ICollection<Tourney> Tourney { get; set; }
        public virtual Country country { get; set; }
    }
}
