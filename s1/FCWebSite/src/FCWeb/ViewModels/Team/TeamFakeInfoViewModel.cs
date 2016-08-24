namespace FCWeb.ViewModels.Team
{
    using System.Collections.Generic;

    public class TeamFakeInfoViewModel
    {
        public string image { get; set; }
        public IEnumerable<TeamFakePersonViewModel> persons { get; set; }
        public IEnumerable<EntityLinkViewModel> coaches { get; set; }
    }
}
