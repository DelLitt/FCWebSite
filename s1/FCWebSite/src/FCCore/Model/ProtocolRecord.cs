using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class ProtocolRecord
    {
        public int Id { get; set; }
        public int? CustomIntValue { get; set; }
        public short eventId { get; set; }
        public int gameId { get; set; }
        public byte? Minute { get; set; }
        public int? personId { get; set; }
        public int teamId { get; set; }

        public virtual Event _event { get; set; }
        public virtual Game game { get; set; }
        public virtual Person person { get; set; }
        public virtual Team team { get; set; }
    }
}
