namespace FCWeb.ViewModels
{
    public class TeamViewModel
    {
        public int id { get; set; }
        public bool active { get; set; }
        public string address { get; set; }
        public short? cityId { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string image { get; set; }
        public short? mainTourneyId { get; set; }
        public string name { get; set; }
        public string nameFull { get; set; }
        public string namePre { get; set; }
        public short? stadiumId { get; set; }
        public byte teamTypeId { get; set; }
        public string webSite { get; set; }
    }
}
