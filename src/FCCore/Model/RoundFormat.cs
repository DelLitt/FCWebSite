using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class RoundFormat
    {
        public RoundFormat()
        {
            Round = new HashSet<Round>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }

        public virtual ICollection<Round> Round { get; set; }
    }
}
