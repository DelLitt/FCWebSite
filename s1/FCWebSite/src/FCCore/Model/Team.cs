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
        }

        public int Id { get; set; }
        public bool Active { get; set; }
        public string Address { get; set; }
        public short? cityId { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public short? mainTourneyId { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }
        public string NamePre { get; set; }
        public short? stadiumId { get; set; }
        public byte teamTypeId { get; set; }
        public string WebSite { get; set; }

        public virtual ICollection<Game> Game { get; set; }
        public virtual ICollection<Game> GameNavigation { get; set; }
        public virtual TableRecord TableRecord { get; set; }
    }
}
