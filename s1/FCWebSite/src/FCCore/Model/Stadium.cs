namespace FCCore.Model
{
    public partial class Stadium
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int? Capacity { get; set; }
        public int? cityId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }

        public virtual City city { get; set; }
    }
}
