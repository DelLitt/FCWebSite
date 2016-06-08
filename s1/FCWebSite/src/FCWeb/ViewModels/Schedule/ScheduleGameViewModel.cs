namespace FCWeb.ViewModels.Schedule
{
    using System;

    public class ScheduleGameViewModel
    {
        public int id { get; set; }
        public short roundId { get; set; }
        public DateTime date { get; set; }
        public EntityLinkViewModel home { get; set; }
        public EntityLinkViewModel away { get; set; }
        public byte? awayAddScore { get; set; }
        public byte? awayPenalties { get; set; }
        public byte? awayScore { get; set; }
        public byte? homeAddScore { get; set; }
        public byte? homePenalties { get; set; }
        public byte? homeScore { get; set; }
        public bool showTime { get; set; }
    }
}
