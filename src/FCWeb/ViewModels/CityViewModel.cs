using FCCore.Model;

namespace FCWeb.ViewModels
{
    public class CityViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nameFull { get; set; }
        public short countryId { get; set; }

        public CountryViewModel country { get; set; }
    }
}
