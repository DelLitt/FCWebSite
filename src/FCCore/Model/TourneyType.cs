using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class TourneyType
    {
        public TourneyType()
        {
            Tourney = new HashSet<Tourney>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }

        public virtual ICollection<Tourney> Tourney { get; set; }
    }
}
