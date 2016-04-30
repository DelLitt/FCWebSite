namespace FCCore.Model
{
    using System.Collections.Generic;

    public partial class Country
    {
        public Country()
        {
            City = new HashSet<City>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }

        public virtual ICollection<City> City { get; set; }
    }
}
