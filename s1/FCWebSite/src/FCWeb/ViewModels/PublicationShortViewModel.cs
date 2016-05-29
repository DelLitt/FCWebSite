namespace FCWeb.ViewModels
{
    public class PublicationShortViewModel
    {
        public int id { get; set; }
        public string urlKey { get; set; }        
        public string title { get; set; }
        public string header { get; set; }
        public string img { get; set; }
        public bool hasVideo { get; set; }
        public bool hasPhoto { get; set; }
    }
}
