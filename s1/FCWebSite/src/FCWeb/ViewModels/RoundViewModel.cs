using System;
using System.Collections.Generic;
using System.Linq;

namespace FCWeb.ViewModels
{
    public class TourneyRoundViewModel
    {
        public int roundId { get; set; }
        public bool current { get; set; }
        public RoundViewModel roundGames { get; set; }
    }

    public class RoundViewModel
    {
        public int roundId { get; set; }
        public string tourney { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
        public IList<DayGamesShortViewModel> dateGames { get; set; }
    }

    public class DayGamesShortViewModel
    {
        public DateInfoFormat1 date { get; set; }
        public IEnumerable<GameShortViewModel> games { get; set; }

}

    public class DateInfoFormat1
    {
        public DateTime date { get; set; }
        public string dateString { get; set; }
        public string dayString { get; set; }
    }
}
