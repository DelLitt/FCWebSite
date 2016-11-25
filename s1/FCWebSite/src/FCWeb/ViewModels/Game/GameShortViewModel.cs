namespace FCWeb.ViewModels.Game
{
    using System;

    public class GameShortViewModel
    {
        public int id { get; set; }
        public DateTime start { get; set; }
        public string time { get; set; }
        public EntityLinkViewModel home { get; set; }
        public EntityLinkViewModel away { get; set; }
        public byte? homeScore { get; set; }
        public byte? awayScore { get; set; }
        public string extra { get; set; }
    }
}
