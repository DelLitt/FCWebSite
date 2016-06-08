using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class Totalizator
    {
        public int Id { get; set; }
        public int gameId { get; set; }
        public short voteType { get; set; }
        public string UserIP { get; set; }

        public virtual Game game { get; set; }
    }
}
