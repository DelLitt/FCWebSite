namespace FCCore.Model
{
    using System.Collections.Generic;

    public partial class City
    {
        public City()
        {
            Stadium = new HashSet<Stadium>();
        }

        public int Id { get; set; }
        public short countryId { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }

        public virtual ICollection<Stadium> Stadium { get; set; }
        public virtual Country country { get; set; }
    }
}
