using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class TeamType
    {
        public TeamType()
        {
            Team = new HashSet<Team>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }

        public virtual ICollection<Team> Team { get; set; }
    }
}
