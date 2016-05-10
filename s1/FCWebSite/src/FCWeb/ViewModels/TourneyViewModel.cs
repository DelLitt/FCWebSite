namespace FCWeb.ViewModels
{
    using System;

    public class TourneyViewModel
    {
        public short id { get; set; }
        public int? cityId { get; set; }
        public DateTime? dateEnd { get; set; }
        public DateTime? dateStart { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string nameFull { get; set; }
        public short? tourneyTypeId { get; set; }
    }
}
