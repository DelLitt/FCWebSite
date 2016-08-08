namespace FCWeb.ViewModels.Schedule
{
    using System.Collections.Generic;

    public class DayGamesViewModel
    {
        public string day { get; set; }
        public IEnumerable<ScheduleGameViewModel> games { get; set; }
    }
}
