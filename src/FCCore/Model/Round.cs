using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class Round
    {
        public Round()
        {
            Game = new HashSet<Game>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }
        public short? roundFormatId { get; set; }
        public string TeamList { get; set; }
        public short tourneyId { get; set; }

        public virtual ICollection<Game> Game { get; set; }
        public virtual RoundFormat roundFormat { get; set; }
        public virtual Tourney tourney { get; set; }
    }
}
