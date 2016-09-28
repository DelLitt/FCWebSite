namespace FCWeb.ViewModels.Game
{
    using System.Collections.Generic;

    public class GameQuickInfoGroupViewModel
    {
        public int teamId { get; set; }
        public string actionTitle { get; set; }
        public IEnumerable<GameQuickInfoViewModel> games { get; set; }
    }
}
