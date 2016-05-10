namespace FCWeb.ViewModels
{
    using System;

    public class StadiumViewModel
    {
        public int id { get; set; }
        public string address { get; set; }
        public int? capacity { get; set; }
        public int? cityId { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public string nameFull { get; set; }
        public string nameAndCity { get; set; }
        public Guid? tempGuid { get; set; }
    }
}
