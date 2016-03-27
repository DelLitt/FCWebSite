using System;

namespace FCWeb.ViewModels
{
    public class GameShortViewModel
    {
        public DateTime start { get; set; }
        public string time { get; set; }
        public string home { get; set; }
        public string away { get; set; }
        public byte? homeScore { get; set; }
        public byte? awayScore { get; set; }
        public string extra { get; set; }
    }
}
