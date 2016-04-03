namespace FCWeb.ViewModels
{
    using FCCore.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PersonViewModel
    {
        public int id { get; set; } = -1;
        public bool active { get; set; }
        public DateTime? birthDate { get; set; }
        public short? cityId { get; set; }
        public byte? height { get; set; }
        public string image { get; set; }
        public string info { get; set; }
        public string nameFirst { get; set; }
        public string nameLast { get; set; }
        public string nameMiddle { get; set; }
        public string nameNick { get; set; }
        public byte? number { get; set; }
        public short? personStatusId { get; set; }
        public short? roleId { get; set; }
        public int? teamId { get; set; }
        public byte? weight { get; set; }

        //public virtual ICollection<PersonCareer> PersonCareer { get; set; }
        //public virtual ICollection<PersonStatistics> PersonStatistics { get; set; }
        //public virtual Person PersonNavigation { get; set; }
        //public virtual Person InversePersonNavigation { get; set; }
        //public virtual PersonStatus personStatus { get; set; }
        //public virtual PersonRole role { get; set; }
        //public virtual Team team { get; set; }
    }
}
