using System;
using System.Collections.Generic;
using System.Linq;

namespace FCWeb.ViewModels.Manage
{
    public class TourneyRoundViewModel
    {
        public int roundId { get; set; }
        public bool current { get; set; }
        public RoundViewModel round { get; set; }
    }

    public class RoundViewModel
    {
        public int roundId { get; set; }
        public string tourney { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
        public Dictionary<DateInfoFormat1, IEnumerable<GameShortViewModel>> dateGames { get; set; }
    }

    public class DateInfoFormat1
    {
        public DateTime date { get; set; }
        public string dateString { get; set; }
        public string dayString { get; set; }
    }

    public class GameShortViewModel
    {
        public DateTime start { get; set; }
        public string home { get; set; }
        public string away { get; set; }
        public int homeScore { get; set; }
        public int awayScore { get; set; }
        public string extra { get; set; }
    }
}
