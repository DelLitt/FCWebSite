namespace FCWeb.ViewModels.Schedule
{
    using System;
    using System.Collections.Generic;

    public class ScheduleItemViewModel
    {
        public DateTime date { get; set; }
        public EntityLinkViewModel round { get; set; }
        public EntityLinkViewModel tourney { get; set; }
        public IEnumerable<DayGamesViewModel> daysGames { get; set; }
    }

    public class DayGamesViewModel
    {
        public string day { get; set; }
        public IEnumerable<ScheduleGameViewModel> games { get; set; }
    }
}
