using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class EventGroup
    {
        public EventGroup()
        {
            Event = new HashSet<Event>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
