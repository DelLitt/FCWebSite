using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class Event
    {
        public Event()
        {
            ProtocolRecord = new HashSet<ProtocolRecord>();
        }

        public short Id { get; set; }
        public short eventGroupId { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }

        public virtual ICollection<ProtocolRecord> ProtocolRecord { get; set; }
        public virtual EventGroup eventGroup { get; set; }
    }
}
