using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class Game
    {
        public int Id { get; set; }
        public int? Audience { get; set; }
        public byte? AwayAddScore { get; set; }
        public int awayId { get; set; }
        public byte? AwayPenalties { get; set; }
        public byte? awayScore { get; set; }
        public DateTime GameDate { get; set; }
        public byte? HomeAddScore { get; set; }
        public int homeId { get; set; }
        public byte? HomePenalties { get; set; }
        public byte? homeScore { get; set; }
        public string Note { get; set; }
        public bool Played { get; set; }
        public string Referees { get; set; }
        public short roundId { get; set; }
        public bool ShowTime { get; set; }
        public int? stadiumId { get; set; }

        public virtual Team away { get; set; }
        public virtual Team home { get; set; }
        public virtual Round round { get; set; }
    }
}
