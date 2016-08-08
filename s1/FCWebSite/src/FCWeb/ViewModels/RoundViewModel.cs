namespace FCWeb.ViewModels
{
    using System.Collections.Generic;

    public class RoundViewModel
    {
        public short id { get; set; }
        public string name { get; set; }
        public string nameFull { get; set; }
        public short? roundFormatId { get; set; }
        public string teamList { get; set; }
        public short tourneyId { get; set; }
        public bool editMode { get; set; }

        public IEnumerable<GameViewModel> games { get; set; }
        public IEnumerable<EntityLinkViewModel> teams { get; set; }
    }
}
